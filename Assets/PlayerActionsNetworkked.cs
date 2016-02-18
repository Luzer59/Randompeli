using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerActionsNetworkked : NetworkBehaviour
{
    public string poolName;
    public NetworkPoolManager bulletPool;

    void Start()
    {
        bulletPool = NetworkPoolManager.GetPoolByName(poolName);
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
            Debug.Log(Vector3.Normalize(transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        }
    }
    
    [Command]
    void CmdFire()
    {
        var bullet = bulletPool.ServerCreateFromPool(transform.position - transform.right , Quaternion.identity);
        if (bullet == null)
            return;

        bullet.GetComponent<Rigidbody2D>().velocity = (transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized;

        // spawn the bullet on the clients
        NetworkServer.Spawn(bullet);
        bullet.tag = gameObject.tag;
    }
 
}
