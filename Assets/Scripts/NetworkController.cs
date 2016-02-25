using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class NetworkController : NetworkBehaviour
{
    private bool serverIsUp = false;
    public List<GameObject> connectedPlayers = new List<GameObject>();

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space) && !serverIsUp)
        {
            serverIsUp = true;
            StartServer();
        }*/
    }

    void StartServer()
    {
        /*if (NetworkManager.singleton.StartServer())
        {
            Debug.Log("Server started");
        }
        else
        {
            Debug.Log("Server failed to start");
        }*/
        //NetworkManager.singleton.conne //StartHost();

        //Debug.Log("asd");
    }

}
