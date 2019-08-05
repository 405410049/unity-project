using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class pc_role_effect : NetworkBehaviour
{
    private float damage = 25;

    public GameObject pc_effect1;
    public GameObject pc_effect2;
    private Animator auto_animator;
    private Transform pos;
    private Animator animator;
    private Animator karate_animator;
    void Start()
    {
        animator = this.GetComponent<Animator>();
        karate_animator= GameObject.FindWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider obj)
    {
        //產生特效
           
        print(obj);
        if (obj.name == "CC_Base_Body"&&animator.GetBool("punch"))
        {
            string uIdentity = obj.transform.parent.name;
          //  print("id :" + uIdentity);
           // CmdTellServerWhoWasHit(uIdentity, 0, 0);
          //  print("hit " + obj);
            GameObject left_hand = GameObject.Find("mixamorig:LeftHandIndex1");
            Instantiate(pc_effect1, left_hand.transform);
            karate_animator.SetTrigger("bodyHit");
        }
        if (obj.name == "CC_Base_Body"&&animator.GetBool("punch_R"))
        {
           string uIdentity = obj.transform.parent.name;
     //       print("id :" + uIdentity);
      //      CmdTellServerWhoWasHit(uIdentity, 0, 0);
      //      print("hit " + obj);
            GameObject right_hand = GameObject.Find("mixamorig:RightHandIndex1");
            Instantiate(pc_effect1, right_hand.transform);
            karate_animator.SetTrigger("bodyHit");
        }
        if (obj.name == "pants"&& animator.GetBool("kick"))
        {
            string uIdentity = obj.transform.parent.name;
        //    CmdTellServerWhoWasHit(uIdentity, 0, 2);
         //   print("hit " + obj);
            GameObject right_foot = GameObject.Find("mixamorig:RightUpLeg");
            Instantiate(pc_effect2, right_foot.transform);
            karate_animator.SetTrigger("footHit");
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
