using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class raycast : NetworkBehaviour
{
    private GameObject[] players;
    private GameObject cam;
    private int damage = 25;
    private RaycastHit hit;
    public GameObject effect1;
    public GameObject effect2;
    public RectTransform healthbar;
    public GameObject spawn_obj;
    private Animator selfAnimator;
    void Start()
    {
        selfAnimator = GetComponent<Animator>();
        if (isLocalPlayer)
        {
            cam = transform.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChechIfShooting();
        if (isLocalPlayer)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                //print(player);
                if(player.name!="auto(Clone)")
                    player.transform.Find("bodyControl").transform.Find("HealthBar").transform.LookAt(cam.transform);
            }
        }
    }

    void ChechIfShooting()
    {
        if (!isLocalPlayer)
            return;
        if (Input.GetKeyDown(KeyCode.F1))
        {
            print("Spawn");
            CmdSpawn();
        }
    }
    private void OnTriggerEnter(Collider obj)
    {
        //print(obj.name);
        // if (obj.name == "CC_Base_Body" && Input.GetKey(KeyCode.P))
        if (obj.name == "CC_Base_Body" && selfAnimator.GetCurrentAnimatorStateInfo(0).IsName("L_Punch_tree") && selfAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f) //(selfAnimator.GetBool("punch") || Input.GetKey(KeyCode.P)))
        {
            string uIdentity = obj.transform.root.name;
            print("id :" + uIdentity);
            CmdTellServerWhoWasHit(uIdentity, 10, 0);
            print("hit " + obj);
            GameObject left_hand = GameObject.Find("mixamorig:LeftHandIndex1");
            Instantiate(effect1, left_hand.transform);
        }
        if (obj.name == "CC_Base_Body" && selfAnimator.GetCurrentAnimatorStateInfo(0).IsName("R_Punch") && selfAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f)
        {
            string uIdentity = obj.transform.root.name;
            print("id :" + uIdentity);
            CmdTellServerWhoWasHit(uIdentity, 10, 0);
            print("hit " + obj);
            GameObject right_hand = GameObject.Find("mixamorig:RightHandIndex1");
            Instantiate(effect1, right_hand.transform);
        }
        if (obj.name == "pants" && selfAnimator.GetCurrentAnimatorStateInfo(0).IsName("Kick") && selfAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f)
        {
            string uIdentity = obj.transform.root.name;
            CmdTellServerWhoWasHit(uIdentity, 20, 2);
            print("hit " + obj);
            GameObject right_foot = GameObject.Find("mixamorig:RightFoot");
            Instantiate(effect2, right_foot.transform);
        }
        //對auto的判斷
        if (obj.tag == "auto_part" && obj.name == "CC_Base_Body" && (selfAnimator.GetCurrentAnimatorStateInfo(0).IsName("L_Punch_tree") && selfAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f || selfAnimator.GetCurrentAnimatorStateInfo(0).IsName("R_Punch") && selfAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f))
        {
            print("hit auto");
            Animator animator = obj.transform.parent.gameObject.GetComponent<Animator>();
            animator.SetTrigger("bodyHit");
        }
        if (obj.tag == "auto_part" && obj.name == "pants" && selfAnimator.GetCurrentAnimatorStateInfo(0).IsName("Kick") && selfAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f)
        {
            print("hit auto");
            Animator animator = obj.transform.parent.gameObject.GetComponent<Animator>();
            animator.SetTrigger("footHit");
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


    [Command]
    void CmdTellServerWhoWasHit(string uniqueID, int dmg, int part)
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
        Health health =  go.GetComponent<Health>();
        if (health != null)
        {
            print("SV found health");
            //health.TakeDamage(dmg);
            //
            //RectTransform healthbar;
            int currentHealth = health.getCurrentHealth();
            currentHealth -= dmg;
            health.setCurrentHealth(currentHealth);
            print(currentHealth);
            health.setBarSize(currentHealth);
            if (currentHealth <= 0)
                go.GetComponent<Animator>().SetBool("isKnockOut", true);
            //healthbar = go.transform.Find("ForeGround").GetComponent<RectTransform>();
            //healthbar.sizeDelta = new Vector2(currentHealth * 2, healthbar.sizeDelta.y);
        }
        else
            print("SV not find health");
        //go.GetComponent<NetworkAnimator>().SetTrigger("bodyHit");
    }


    [ClientRpc]
    void RpcWhoWasHit(string uniqueID, int dmg, int part)
    {
        GameObject go = GameObject.Find(uniqueID);
        //Debug.Log("client Find : " + go);
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
        
        Health health = go.GetComponent<Health>();
        if (health != null)
        {
            print("CL found health");
            //health.TakeDamage(dmg);
            //RectTransform healthbar;
            int currentHealth = health.getCurrentHealth();
            currentHealth -= dmg;
            health.setCurrentHealth(currentHealth);
            print(currentHealth);
            health.setBarSize(currentHealth);
            if (currentHealth <= 0)
                go.GetComponent<Animator>().SetBool("isKnockOut",true);
            //healthbar = go.transform.Find("ForeGround").GetComponent<RectTransform>();
            //healthbar.sizeDelta = new Vector2(currentHealth * 2, healthbar.sizeDelta.y);
        }
        else
            print("CL not find health");
    }
    [Command]
    void CmdSpawn()
    {
        GameObject go = Instantiate(spawn_obj, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        NetworkServer.Spawn(go);
    }
 
}
