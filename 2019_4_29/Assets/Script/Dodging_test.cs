using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Dodging_test : NetworkBehaviour
{
    private Animator animator;
    public bool flag = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "hand_foot")
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                //animator.SetTrigger("P_dodging");
                //print(other.transform.root.gameObject);
                CmdDodge(other.transform.root.gameObject, "P_dodging");
             //   animator.SetBool("test", true);
            }
         //   else
               // animator.SetBool("test", false);
            if (Input.GetKeyDown(KeyCode.L))
                CmdDodge(other.transform.root.gameObject, "L_dodging");
            // animator.SetTrigger("L_dodging");
            //  GameObject auto_body=this.transform.Find("CC_Base_Body").gameObject;
            //    auto_body.GetComponent<BoxCollider>().enabled= false;
        }
    }
    /*private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "hand_foot")
        {
            print("test tag");
            animator.SetBool("dodging2", false);
        }
    }*/
    // Start is called before the first frame update
    void Start()
    {
            animator = this.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                CmdSetFlag(transform.gameObject);
            }
        }
    }

    [Command]
    void CmdSetFlag(GameObject obj)
    {
        if (obj.GetComponent<ForAuto>().flag == false)
        {
            obj.GetComponent<ForAuto>().flag = true;
            //obj.GetComponent<BoxCollider>().enabled = true;
            RpcSetFlag(obj);
        }
        else
        {
            obj.GetComponent<ForAuto>().flag = false;
            //obj.GetComponent<BoxCollider>().enabled = false;
            RpcSetFlag(obj);
        }
    }
    [ClientRpc]
    void RpcSetFlag(GameObject obj)
    {
        if (obj.GetComponent<ForAuto>().flag == false)
        {
            obj.GetComponent<ForAuto>().flag = true;
            //obj.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            obj.GetComponent<ForAuto>().flag = false;
            //obj.GetComponent<BoxCollider>().enabled = false;
        }
    }

    [Command]
    void CmdDodge(GameObject obj , string part)
    {
            obj.GetComponent<NetworkAnimator>().SetTrigger(part);
            print("trigger " + part);
            RpcDodge(obj, part);
    }
    [ClientRpc]
    void RpcDodge(GameObject obj, string part)
    {
            obj.GetComponent<NetworkAnimator>().SetTrigger(part);
            print("trigger " + part);
    }


}