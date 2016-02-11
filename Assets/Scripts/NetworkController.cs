using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkController : MonoBehaviour
{
    private bool serverIsUp = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !serverIsUp)
        {
            serverIsUp = true;
            StartServer();
        }
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
        NetworkManager.singleton.StartHost();
        Debug.Log("asd");
    }
}
