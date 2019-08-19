using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UnityEngine.Networking
{
    [AddComponentMenu("Network/NetworkManagerHUD")]
    [RequireComponent(typeof(NetworkManager))]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public class customHUD : MonoBehaviour
    {
        public NetworkManager manager;
        public InputField address;
        void Awake()
        {
            manager = GetComponent<NetworkManager>();
        }
        public void StartServer()
        {
            manager.StartServer();
        }
        public void StartHost()
        {
            manager.StartHost();
        }
        public void StartClient()
        {
            manager.networkAddress = address.text;
            manager.StartClient();           
        }

        void OnGUI()
        {
            //StopHost後還是沒辦法重連
        //    if (NetworkServer.active || NetworkClient.active)
           //     if (GUI.Button(new Rect(200, 130, 170, 20), "Stop"))
             //       manager.StopHost();
            if (NetworkServer.active)
                GUI.Label(new Rect(200, 100, 300, 20), "Server: port=" + manager.networkPort);
            if (NetworkClient.active)
                GUI.Label(new Rect(100, 100, 300, 20), "Client: address=" + manager.networkAddress + " port=" + manager.networkPort);
        }
    }
}
