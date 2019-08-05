using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_vive_camera : MonoBehaviour
{
    public GameObject karate;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.XR.InputTracking.disablePositionalTracking = true;
    }

    // Update is called once per frame
    void Update()
    {
        // The parentTransform is actually a null point for the camera,
        // so we need to set its parent position to 0, 0, 0
        //   var VRParent = hmd.transform.parent;
        //   VRParent.localPosition = Vector3.zero;
        float change = transform.position.y / 1.6f;
        transform.localScale = new Vector3(0, change, 0);
          
    }
}
