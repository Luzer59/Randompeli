using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerActionsNetworkked : NetworkBehaviour
{
    public string poolName;
    public NetworkPoolManager bulletPool;
    public GameObject shootPivot;
    public GameObject shootPoint;

    public float bulletSpeed;
    public int health;

    void Start()
    {
        bulletPool = NetworkPoolManager.GetPoolByName(poolName);
    }

    public override void OnStartLocalPlayer()
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
            CmdFire(shootPivot.transform.position, shootPivot.transform.rotation);
        }
    }
    
    [Command]
    void CmdFire(Vector3 position, Quaternion rotation)
    {
        var bullet = bulletPool.ServerCreateFromPool(position, rotation);
        if (bullet == null)
            return;
        bullet.tag = tag;
        //bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * 10f;

        // spawn the bullet on the clients
        Debug.Log(bullet.transform.right * bulletSpeed);
        bullet.GetComponent<BulletController>().direction = bullet.transform.right * bulletSpeed;
        NetworkServer.Spawn(bullet);
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
