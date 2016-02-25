using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour
{
    public bool hasParent = false;

    public float speed = 50f;

    public float fireRate;
    public float damage;
    public float timeToFire;
    
    
    
   // public LayerMask notToHit;
    Transform firePoint;

    void Awake()
    {
        //notToHit = 1 << LayerMask.NameToLayer("Player");
        fireRate = 0;
        damage = 10;
        firePoint = transform.FindChild("FirePoint");
        
        if (firePoint == null)
        {
            Debug.LogError("No firepoint object found");
        }
    }

    void Update()
    {
        
        if (fireRate == 0)
        {
            if (Input.GetKeyDown(KeyCode.Z) && hasParent)
            {
                //Shoot();
                
            }
        }
       
        if (transform.parent != null)
        {
            hasParent = true;
        }
        else
        {
            hasParent = false;
        }
    }
    void Shoot()
    {
        GameObject obj = ObjectPoolerScript.current.GetPooledObject();
        if (gameObject.transform.parent.localScale.x < 0)
        {
            obj.transform.position = firePoint.position;
            obj.SetActive(true);
            obj.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
            
        }
        else
        {
            obj.transform.position = firePoint.position;
            obj.SetActive(true);
            obj.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        }        
    }
}
