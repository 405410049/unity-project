using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class disableOtherCamera : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
        {
            GameObject cam=GameObject.Find("Camera");
            cam.gameObject.SetActive(false);
        }
    }

}
