using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ItemPickUp : NetworkBehaviour
{
    
    public float pickUpDistance = 1f;
    public int pickupLayer;
    public Transform carriedObject = null;
   
    
    
    

    void Awake()
    {
        pickupLayer = 1 << LayerMask.NameToLayer("Item");
        
        
    }
    
    private void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.Z) && carriedObject == null)
        {
            PickUp();  
        }
        if (Input.GetKeyDown(KeyCode.X) && carriedObject != null)
        {

            Drop();
        }
    }
   public void Drop()
    {
        carriedObject.parent = null;
        carriedObject.gameObject.AddComponent(typeof(Rigidbody2D));
        carriedObject = null;    
    }
    private void PickUp()
    {
        Collider2D[] pickups = Physics2D.OverlapCircleAll(transform.position, pickUpDistance, pickupLayer);
        
    
        float dist = Mathf.Infinity;

        for(int i = 0; i < pickups.Length; i++)
        {
            float newDist = (transform.position - pickups[i].transform.position).sqrMagnitude;

            

            if (newDist < dist)
            {
                carriedObject = pickups[i].transform;
                dist = newDist;
            }
        }

        if (carriedObject != null)
        {
            Destroy(carriedObject.GetComponent<Rigidbody2D>());
            
            /*if (transform.localScale.x != carriedObject.localScale.x)
            {
                carriedObject.localScale = transform.localScale;
            }*/

            carriedObject.parent = GetComponent<PlayerActionsNetworkked>().shootPoint.transform;
            carriedObject.localPosition = new Vector3(0f, 0f, 0f);
            
         

        }
        
    }
}
