using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class NetworkController : NetworkBehaviour
{
    private bool serverIsUp = false;
    public List<NetworkPlayer> connectedPlayers = new List<NetworkPlayer>();
    public int playerCount = 0;

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space) && !serverIsUp)
        {
            serverIsUp = true;
            StartServer();
        }*/
    }

    public void OnServerConnect(NetworkConnection conn)
    {
        Debug.Log("Connected");
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        Debug.Log("Player " + playerCount++ + " connected from " + player.ipAddress + ":" + player.port);
    }

    void OnPlayerDisconnected(NetworkPlayer player)
    {
        Debug.Log("Player " + " disconnected from " + player.ipAddress + ":" + player.port);
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
