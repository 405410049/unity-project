using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class raycast : NetworkBehaviour
{
    private float damage = 25;
    //private float range = 200;
    //[SerializeField] private Transform camTransform;
    private RaycastHit hit;
    public GameObject effect1;
    public GameObject effect2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChechIfShooting();
    }

    void ChechIfShooting()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            //Shoot();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
          //  Kick();
        }
    }
    private void OnTriggerEnter(Collider obj)
    {
        //print(obj.name);
        if (obj.name == "CC_Base_Body" && Input.GetKey(KeyCode.P))
        {
            string uIdentity = obj.transform.parent.name;
            print("id :" + uIdentity);
            CmdTellServerWhoWasHit(uIdentity, 0, 0);
            print("hit " + obj);
            GameObject left_hand = GameObject.Find("mixamorig:LeftHandIndex1");
            Vector3 effect_pos = new Vector3(left_hand.transform.position.x, left_hand.transform.position.y, left_hand.transform.position.z);
            Instantiate(effect1, effect_pos, transform.rotation);
        }
        if (obj.name == "pants" && Input.GetKey(KeyCode.K))
        {
            string uIdentity = obj.transform.parent.name;
            CmdTellServerWhoWasHit(uIdentity, 0, 2);
            print("hit " + obj);
            GameObject right_foot = GameObject.Find("mixamorig:RightUpLeg");
            Vector3 effect_pos = new Vector3(right_foot.transform.position.x, right_foot.transform.position.y, right_foot.transform.position.z);
            Instantiate(effect2, effect_pos, transform.rotation);
        }
    }

    void Shoot()
    {
        Vector3 rayPostion = new Vector3(transform.position.x + 0.2f, transform.position.y + 1.4f, transform.position.z); //punch
        Ray myRay = new Ray(rayPostion, Vector3.forward); //Vector3.forward=(0,0,1)
        //Debug.DrawRay(camTransform.position, camTransform.TransformDirection(Vector3.forward) * 10, Color.red);
        if (Physics.Raycast(myRay, out hit, 0.8f))
        {
            string uIdentity = hit.transform.name;
            //Debug.Log(hit.collider.name + ":" + uIdentity);
            Vector3 endPos = rayPostion + new Vector3(0, 0, 0.8f);
            Debug.DrawLine(rayPostion,endPos, Color.red);
            if (hit.collider.name == "CC_Base_Body")
            {
                CmdTellServerWhoWasHit(uIdentity, damage, 0);
            }
            else if(hit.collider.name == "CC_Base_Eye")
            {
                CmdTellServerWhoWasHit(uIdentity, damage, 1);
            }
        }
        /*Sucessful code
         * if (Physics.Raycast(camTranform.TransformPoint(0, 0, 0.5f), camTranform.forward, out hit, range))
        {
            Debug.Log(hit.transform.tag);
            if(hit.transform.tag == "Player")
            {
                string uIdentity = hit.transform.name;
                CmdTellServerWhoWasHit(uIdentity, damage);
            }
        }*/
    }

    void Kick()
    {
        Vector3 rayPostion = new Vector3(transform.position.x + 0.2f, transform.position.y + 0.5f, transform.position.z); //kick
        Ray myRay = new Ray(rayPostion, Vector3.forward); //Vector3.forward=(0,0,1)
        if (Physics.Raycast(myRay, out hit, 0.8f))
        {
            Debug.Log(hit.collider.name);
            string uIdentity = hit.transform.name;
            if (hit.collider.name == "pants")
            {
                CmdTellServerWhoWasHit(uIdentity, damage, 2);
            }
        }
    }

    [Command]
    void CmdTellServerWhoWasHit(string uniqueID, float dmg, int part)
    {
        RpcWhoWasHit(uniqueID, dmg, part);
        GameObject go = GameObject.Find(uniqueID);
        Debug.Log("Find : " + go);
        switch (part)
        {
            case 0:
        //        go.GetComponent<NetworkAnimator>().SetTrigger("bodyHit");
                go.GetComponent<Animator>().SetTrigger("bodyHit");
                break;
            case 1:
         //       go.GetComponent<NetworkAnimator>().SetTrigger("headHit");
                go.GetComponent<Animator>().SetTrigger("headHit");
                break;
            case 2:
          //      go.GetComponent<NetworkAnimator>().SetTrigger("footHit");
                go.GetComponent<Animator>().SetTrigger("footHit");
                break;
            default:
                break;
        }
        //go.GetComponent<NetworkAnimator>().SetTrigger("bodyHit");
    }


    [ClientRpc]
    void RpcWhoWasHit(string uniqueID, float dmg, int part)
    {
        GameObject go = GameObject.Find(uniqueID);
        Debug.Log("client Find : " + go);
        switch (part)
        {
            case 0:
                go.GetComponent<NetworkAnimator>().SetTrigger("bodyHit");
                //go.GetComponent<Animator>().SetTrigger("bodyHit");
                break;
            case 1:
                go.GetComponent<NetworkAnimator>().SetTrigger("headHit");
                //go.GetComponent<Animator>().SetTrigger("headHit");
                break;
            case 2:
                go.GetComponent<NetworkAnimator>().SetTrigger("footHit");
                //go.GetComponent<Animator>().SetTrigger("footHit");
                break;
            default:
                break;
        }
    }
}
