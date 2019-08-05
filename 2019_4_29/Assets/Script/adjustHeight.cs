using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//for camera object

public class adjustHeight : NetworkBehaviour
{
    private float fixedHeight = 1.75f;
    private float adjHeight = 0;
    private float primitiveHeight;
    private float adjust_x = 0;
    private float adjust_z = 0;
    private bool flag = false;
    public GameObject body;
    public GameObject camera;
    public GameObject small;
    public GameObject cameraControl;
    // Start is called before the first frame update
    void Start()
    {
        if(isLocalPlayer)
            primitiveHeight = cameraControl.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.B) && !flag)
            {
                adjHeight = camera.transform.position.y - fixedHeight;
                /*allObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
                //allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
                foreach (GameObject go in allObjects)
                    if (go.activeInHierarchy)
                    {
                        go.transform.position = new Vector3(go.transform.position.x, (go.transform.position.y - lastHeight) + adjHeight, go.transform.position.z);
                        print(go + " is an active object");
                    }*/
                small.transform.localPosition = new Vector3(small.transform.localPosition.x - camera.transform.localPosition.x, primitiveHeight - adjHeight, small.transform.localPosition.z - camera.transform.localPosition.z);
                //transform.localPosition = new Vector3(transform.localPosition.x - CameraRig.transform.localPosition.x - camera.transform.localPosition.x, primitiveHeight - adjHeight, transform.localPosition.z - CameraRig.transform.localPosition.z - camera.transform.localPosition.z);
                //transform.localPosition = new Vector3(transform.localPosition.x , primitiveHeight - adjHeight, transform.localPosition.z);
                flag = true;
            }
            if (flag)
            {
                body.transform.position = new Vector3(camera.transform.position.x, body.transform.position.y, camera.transform.position.z);
                body.transform.eulerAngles = new Vector3(body.transform.eulerAngles.x, camera.transform.eulerAngles.y - 5.0f, body.transform.eulerAngles.z);
            }
        }
    }
}
