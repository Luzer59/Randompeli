using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public float xSmooth = 2f;

    private Transform player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void FixedUpdate()
    {
        TrackPlayer();
    }

    void TrackPlayer()
    {
        float targetX = transform.position.x;

        targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);

        transform.position = new Vector3(targetX, 0, transform.position.z);
    }
    
}
