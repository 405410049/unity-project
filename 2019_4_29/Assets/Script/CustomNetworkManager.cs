using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CustomNetworkManager : NetworkManager
{
    public InputField address;
    public void StartHosting()
    {
        base.StartHost();
    }
    public void StartServer()
    {
        base.StartServer();
    }
    public void StartClient()
    {
        print(address.text);
        base.networkAddress = address.text;
        base.StartClient();
    }
}
