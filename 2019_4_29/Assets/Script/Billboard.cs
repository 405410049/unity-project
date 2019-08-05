using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Billboard : NetworkBehaviour
{
    private GameObject[] players;
    public Camera cam;
    void Update()
    {
        if (isLocalPlayer)
        {
            CmdLook();
        }
    }

    [Command]
    void CmdLook()
    {
        RpcLook();
    }
    [ClientRpc]
    void RpcLook()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in players)
        {
            print(player);
            player.transform.Find("HealthBar").transform.LookAt(cam.transform);
        }
    }
}
