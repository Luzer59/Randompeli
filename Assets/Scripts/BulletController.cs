using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BulletController : NetworkBehaviour
{
    public NetworkPooledObjectValues values;
    public int damage;
    public Vector2 direction;

    new private Rigidbody2D rigidbody;

    void Awake()
    {
        values = GetComponent<NetworkPooledObjectValues>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidbody.position += direction * Time.deltaTime;
    }

    [ServerCallback]
    void OnTriggerEnter2D(Collider2D other)
    {
        if (values)
        {
            if (values.pool)
            {
                if (other.tag != tag)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (other.tag == "Team" + i)
                        {
                            other.GetComponent<PlayerActionsNetworkked>().CmdTakeDamage(damage);
                            break;
                        }
                    }

                    values.pool.ServerReturnToPool(gameObject);
                }
                else
                {
                    Debug.Log("Tag not different");
                }
            }
            else
            {
                Debug.Log("NetworkPoolManager not present");
            }
        }
        else
        {
            Debug.Log("NetworkPooledObjectValues not present");
        }
    }
}
