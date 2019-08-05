using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class vive_camera : NetworkBehaviour
//public class vive_camera : MonoBehaviour
{
    public GameObject obj;
    public GameObject obj_2;
    public GameObject obj_3;
    public GameObject obj_4;
    public Animator _animator;
    private float tmp, now;
    public float dif;
    //private float height = 1.7f;
    //private float change;

    private void Start()
    {
        UnityEngine.XR.InputTracking.disablePositionalTracking = true;
        tmp = transform.eulerAngles.y;
        dif = 1.0f;
        //transform.position = new Vector3(obj_2.transform.position.x, transform.position.y, obj_2.transform.position.z);
        //target = transform.Find("playerCamera");
    }

    void Update()
    {
        //if(isLocalPlayer)
            Refresh();
         //   print(transform.eulerAngles.x);
            if (transform.eulerAngles.x > 180 && transform.eulerAngles.x < 350)
            {
                //print("look up");
                obj_3.transform.position = obj_3.transform.position + new Vector3(transform.forward.x * Time.deltaTime * 1.3f, 0, transform.forward.z * Time.deltaTime * 1.5f);
                _animator.SetBool("walkForward", true);
            }
            else if(transform.eulerAngles.x > 30 && transform.eulerAngles.x < 180){
                //print("look down");
                obj_3.transform.position = obj_3.transform.position + new Vector3(transform.forward.x * Time.deltaTime * (-1.3f), 0, transform.forward.z * Time.deltaTime * (-1.5f));
                _animator.SetBool("walkBackward", true);
            }
            else
            {
                _animator.SetBool("walkForward", false);
                _animator.SetBool("walkBackward", false);

            }
    }

    public void Refresh()
    {
        if (obj == null)
        {
            Debug.LogWarning("Missing target ref !", this);

            return;
        }

        now = transform.eulerAngles.y;

        //obj.transform.position = new Vector3(obj_2.transform.position.x , obj.transform.position.y, obj_2.transform.position.z);
        transform.position = new Vector3(obj_4.transform.position.x, obj_4.transform.position.y, obj_4.transform.position.z);
        obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, transform.eulerAngles.y - 5.0f, obj.transform.eulerAngles.z);
        if ((now - tmp) < (-dif))
            _animator.SetBool("walkLeft", true);
        else
            _animator.SetBool("walkLeft", false);

        if ((now - tmp) > dif)
            _animator.SetBool("walkRight", true);
        else
            _animator.SetBool("walkRight", false);


        tmp = now;

        //change = transform.position.y / height;
        //obj.transform.localScale = new Vector3(change, change,change);
    }
    /*[Command]
    void CmdCHANGE()
    {
        obj.transform.position = new Vector3(transform.position.x, obj.transform.position.y, transform.position.z);
        obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, transform.eulerAngles.y, obj.transform.eulerAngles.z);
    }
    [ClientRpc]
    void RpcCHANGE()
    {
        obj.transform.position = new Vector3(transform.position.x, obj.transform.position.y, transform.position.z);
        obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, transform.eulerAngles.y, obj.transform.eulerAngles.z);
    }*/

}
