using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BulletController : NetworkBehaviour
{
    public NetworkPooledObjectValues values;

    void Awake()
    {
        values = GetComponent<NetworkPooledObjectValues>();
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
