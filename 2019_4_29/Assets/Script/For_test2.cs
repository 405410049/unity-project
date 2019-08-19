using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class For_test2 : NetworkBehaviour
{
    public GameObject playerCamera;
    public float speed = 3;
    private Animator _animator;
    private Vector3 prePos;
    private GameObject bodyControl;
    private bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            playerCamera.SetActive(true);
            _animator = this.GetComponent<Animator>();
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
        }
        else
        {
            playerCamera.SetActive(false);
            return;
        }

        //測試角色跟著camera移動後播出動畫
   /*     bodyControl = this.transform.GetChild(1).gameObject;
        prePos = bodyControl.transform.position;
        InvokeRepeating("recordPos", 0f, 0.2f);
        InvokeRepeating("judgeMove", 0.2f, 0.2f);*/
    }

    private void judgeMove()
    {
        Vector3 nowPos=bodyControl.transform.position;
        float changeX = Mathf.Abs(nowPos.x - prePos.x);
        float changeZ = Mathf.Abs(nowPos.z - prePos.z);
        //     print(changeX+" "+changeZ); 

        if (changeX >= 0.025 || changeZ >= 0.025)
        {
            _animator.speed = 0.5f;
            _animator.SetBool("walkForward", true);
            _animator.speed = 1;
        }
        else
            _animator.SetBool("walkForward", false);
        
    }
    private void recordPos()
    {
        prePos = bodyControl.transform.position;
    }
    void Update()
    {

        // PUNCH();
        // KICK();
        // 控制前後左右
        if (isLocalPlayer)
        {
            var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
            var z = Input.GetAxis("Vertical") * Time.deltaTime * 1.3f;

            /*transform.Rotate(0, x, 0);
            transform.Translate(0, 0, z);*/

            if (Input.GetKeyDown("escape"))
            {
                //Cursor.lockState = CursorLockMode.None;
                //Cursor.visible = true;
            }
            if (Input.GetKeyDown(KeyCode.F4))
            {
                if (flag)
                    flag = false;
                else
                    flag = true;
            }
            if (flag == true)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    transform.position = transform.position + new Vector3(playerCamera.transform.forward.x * Time.deltaTime * 1.3f, 0, playerCamera.transform.forward.z * Time.deltaTime * 1.5f);
                    _animator.SetBool("walkForward", true);
                }
                else
                    _animator.SetBool("walkForward", false);
                if (Input.GetKey(KeyCode.S))
                {
                    transform.position = transform.position + new Vector3(playerCamera.transform.forward.x * Time.deltaTime * (-1.3f), 0, playerCamera.transform.forward.z * Time.deltaTime * (-1.5f));
                    _animator.SetBool("walkBackward", true);
                }
                else
                    _animator.SetBool("walkBackward", false);
                if (Input.GetKey(KeyCode.A))
                    _animator.SetBool("walkLeft", true);
                else
                    _animator.SetBool("walkLeft", false);
                if (Input.GetKey(KeyCode.D))
                    _animator.SetBool("walkRight", true);
                else
                    _animator.SetBool("walkRight", false);

                if (Input.GetKey(KeyCode.Y))
                {
                    _animator.SetFloat("speed", 0f);
                }
                if (Input.GetKey(KeyCode.U))
                {
                    _animator.SetFloat("speed", 0.5f);
                }
                if (Input.GetKey(KeyCode.I))
                {
                    _animator.SetFloat("speed", 1f);
                }
                if (Input.GetKey(KeyCode.O))
                {
                    _animator.SetBool("hook_R", true);
                }
                else
                    _animator.SetBool("hook_R", false);

                if (Input.GetKey(KeyCode.N))
                {
                    _animator.SetFloat("hitDirection", 0f);
                }
                if (Input.GetKey(KeyCode.M))
                {
                    _animator.SetFloat("hitDirection", 1.5f);
                }

                if (Input.GetKey(KeyCode.P))
                {
                    _animator.SetBool("punch", true);
                }
                else
                    _animator.SetBool("punch", false);
                if (Input.GetKey(KeyCode.L))
                {
                    _animator.SetBool("punch_R", true);
                }
                else
                    _animator.SetBool("punch_R", false);
                if (Input.GetKey(KeyCode.K))
                    _animator.SetBool("kick", true);
                else
                    _animator.SetBool("kick", false);
            }
        }
    }
}
