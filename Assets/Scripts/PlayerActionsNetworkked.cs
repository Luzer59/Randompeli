using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerActionsNetworkked : NetworkBehaviour
{
    public string poolName;
    public NetworkPoolManager bulletPool;
    public GameObject shootPivot;
    public GameObject shootPoint;

    public int health;

    void Start()
    {
        bulletPool = NetworkPoolManager.GetPoolByName(poolName);
    }

    void OnStartLocalPlayer()
    {
        //tag = "Team" + Network.connections.Length + 2;
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        tag = "Team" + (Network.connections.Length + 1).ToString("0");

        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPoint.z = 0f;
        Vector3 direction = (mouseWorldPoint - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        shootPivot.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Input.GetMouseButtonDown(0))
        {
            CmdFire();
        }
    }
    
    [Command]
    void CmdFire()
    {
        var bullet = bulletPool.ServerCreateFromPool(shootPoint.transform.position, shootPivot.transform.rotation);
        if (bullet == null)
            return;
        bullet.tag = tag;
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * 10f;

        // spawn the bullet on the clients
        NetworkServer.Spawn(bullet);
        bullet.tag = gameObject.tag;
    }

    [Command]
    public void CmdTakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            NetworkServer.Destroy(gameObject);
        }
    }
}
