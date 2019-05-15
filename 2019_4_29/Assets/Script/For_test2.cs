using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class For_test2 : NetworkBehaviour
{
    public GameObject playerCamera;
    public float speed = 3;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            playerCamera.SetActive(true);
            _animator = this.GetComponent<Animator>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            playerCamera.SetActive(false);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // PUNCH();
        // KICK();
        // 控制前後左右
        if (isLocalPlayer)
        {
            var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
            var z = Input.GetAxis("Vertical") * Time.deltaTime * 1.3f;

            transform.Rotate(0, x, 0);
            transform.Translate(0, 0, z);

            if (Input.GetKeyDown("escape"))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            if (Input.GetKey(KeyCode.W))
                _animator.SetBool("walkForward", true);
            else
                _animator.SetBool("walkForward", false);
            if (Input.GetKey(KeyCode.S))
                _animator.SetBool("walkBackward", true);
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


            if (Input.GetKey(KeyCode.P))
                _animator.SetBool("punch", true);
            else
                _animator.SetBool("punch", false);
            if (Input.GetKey(KeyCode.K))
                _animator.SetBool("kick", true);
            else
                _animator.SetBool("kick", false);
        }
    }
}
