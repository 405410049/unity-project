using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class changeBeltColor : NetworkBehaviour
{
    private bool isChange = false;
    [SyncVar] private Color objColor;
    [SyncVar] private Color objColor_2;
    [SyncVar] private GameObject objId;
    public Material body;
    public Material finger;
    public Material toe;
    public Material invisible;

    GameObject tmp;
    private NetworkIdentity objNetId;
    private void Start()
    {
        if (isLocalPlayer)
        {
            ChangePainting();
            HideHead();
        }

    }

    private void Update()
    {
        if(isLocalPlayer && isChange)
        {
            CmdPaint(transform.gameObject, objColor);//, objColor_2);
        }
    }

    void HideHead()
    {
        GameObject head = transform.Find("bodyControl").transform.Find("CC_Base_Body").gameObject; 
        Material[] mats = head.GetComponent<SkinnedMeshRenderer>().materials;
        mats[1] = invisible;
        mats[2] = invisible;
        head.GetComponent<SkinnedMeshRenderer>().materials = mats;
        transform.Find("bodyControl").transform.Find("CC_Base_Eye").GetComponent<SkinnedMeshRenderer>().enabled = false ;
        transform.Find("bodyControl").transform.Find("CC_Base_Teeth").GetComponent<SkinnedMeshRenderer>().enabled = false;
        transform.Find("bodyControl").transform.Find("CC_Base_Tongue").GetComponent<SkinnedMeshRenderer>().enabled = false;
    }

    void ChangePainting()
    {
        objColor = new Color(Random.value, Random.value, Random.value, Random.value);   //確定顏色
        //objColor_2 = new Color(Random.value, Random.value, Random.value, Random.value);
        /*
         * 因為我們視角看不到自己的顏色，所以不確定自己看自己會不會變顏色
         * 如果顏色沒變，要變自己的顏色的話
         * 下面這行可以試試看
         * GameObject tmp = transform.Find("pasted__belt").gameObject;
         * tmp.GetComponent<SkinnedMeshRenderer>().materials[0].color = objColor;
         */
        /*if (Input.GetKeyDown(KeyCode.T))          //success Code
        {
            tmp = transform.Find("pasted__belt").gameObject;
            tmp.GetComponent<SkinnedMeshRenderer>().materials[0].color = objColor;
        }*/
        CmdPaint(transform.gameObject, objColor);//, objColor_2);   // 讓server改顏色，傳自己的gamobject根要改的顏色進去
        isChange = true;
        //CmdPaint(transform.name, objColor);
    }

    /*[Command]
    void CmdPaint(string uname, Color col)
    {
        RpcPaint(uname, col);
        GameObject tmp = GameObject.Find(uname);
        GameObject tmp2 = tmp.transform.Find("pasted__belt").gameObject;
        tmp2.GetComponent<SkinnedMeshRenderer>().materials[0].color = col;
    }

    [ClientRpc]
    void RpcPaint(string uname, Color col)
    {
        GameObject tmp = GameObject.Find(uname);
        GameObject tmp2 = tmp.transform.Find("pasted__belt").gameObject;
        tmp2.GetComponent<SkinnedMeshRenderer>().materials[0].color = col;
    }*/

    [Command]
    void CmdPaint(GameObject obj, Color col)//, Color col_2)
    {
        GameObject tmp = obj.transform.Find("bodyControl").transform.Find("pants").gameObject;     //找到要改顏色的object
        tmp.GetComponent<SkinnedMeshRenderer>().materials[0].color = col;   //把他的MeshRenderer的material改顏色，不一定是這個component，看你們的component是哪種
        tmp = obj.transform.Find("bodyControl").transform.Find("cloth").gameObject;
        tmp.GetComponent<SkinnedMeshRenderer>().materials[0].color = col;
        RpcPaint(obj, col);//, col_2); //server 廣播到各個client
    }

    [ClientRpc]
    void RpcPaint(GameObject obj, Color col)//, Color col_2)
    {
        GameObject tmp = obj.transform.Find("bodyControl").transform.Find("pants").gameObject;
        tmp.GetComponent<SkinnedMeshRenderer>().materials[0].color = col;
        tmp = obj.transform.Find("bodyControl").transform.Find("cloth").gameObject;
        tmp.GetComponent<SkinnedMeshRenderer>().materials[0].color = col;
    }




}
