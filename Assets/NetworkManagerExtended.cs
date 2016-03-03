using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class NetworkManagerExtended : NetworkManager
{
    private List<NetworkConnection> connections = new List<NetworkConnection>();

    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("Connected");
        connections.Add(conn);
    }
}
