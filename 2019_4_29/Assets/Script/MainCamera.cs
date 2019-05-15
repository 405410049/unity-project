using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MainCamera : NetworkBehaviour
{
    // Start is called before the first frame update
    public GameObject mainCamera;
    void Start()
    {
        if(isServer)
            mainCamera.SetActive(true);
        else
            mainCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
