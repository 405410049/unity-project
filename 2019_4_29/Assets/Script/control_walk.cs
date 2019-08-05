using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control_walk : MonoBehaviour
{
    private Animator _animator;
    private float strength;
    void Start()
    {
        _animator = this.GetComponent<Animator>();
        strength = 0.0F;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _animator.SetFloat("Strength", 0.0F);
            _animator.SetBool("isAttack", true);
            strength = strength + 0.2F;
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            _animator.SetFloat("Strength", strength);
            strength = 0.0F;
        }


        if (Input.GetKeyDown(KeyCode.W))
        {
            _animator.SetBool("isMove", true);
            _animator.SetFloat("Speed", 0);
        }
        if (Input.GetKeyUp(KeyCode.W))
            _animator.SetBool("isMove", false);

        if (Input.GetKeyDown(KeyCode.S))
        {
            _animator.SetBool("isMove", true);
            _animator.SetFloat("Speed", 1);
        }
        if (Input.GetKeyUp(KeyCode.S))
            _animator.SetBool("isMove", false);

        /* hook right */
        if(Input.GetKeyDown(KeyCode.Keypad0))
            _animator.SetBool("isRightHook", true);
        if (Input.GetKeyUp(KeyCode.Keypad0))
            _animator.SetBool("isRightHook", false);

        /* hook left */
        if (Input.GetKeyDown(KeyCode.Keypad1))
            _animator.SetBool("isLeftHook", true);
        if (Input.GetKeyUp(KeyCode.Keypad1))
            _animator.SetBool("isLeftHook", false);

        /* straight right */
        if (Input.GetKeyDown(KeyCode.Keypad2))
            _animator.SetBool("isRightSt", true);
        if (Input.GetKeyUp(KeyCode.Keypad2))
            _animator.SetBool("isRightSt", false);


        /* straight left */
        if (Input.GetKeyDown(KeyCode.Keypad3))
            _animator.SetBool("isLeftSt", true);
        if (Input.GetKeyUp(KeyCode.Keypad3))
            _animator.SetBool("isLeftSt", false);

        /* upper right */
        if (Input.GetKeyDown(KeyCode.Keypad4))
            _animator.SetBool("isRightUp", true);
        if (Input.GetKeyUp(KeyCode.Keypad4))
            _animator.SetBool("isRightUp", false);

        /* upper left */
        if (Input.GetKeyDown(KeyCode.Keypad5))
            _animator.SetBool("isLeftUp", true);
        if (Input.GetKeyUp(KeyCode.Keypad5))
            _animator.SetBool("isLeftUp", false);

        /* high kick right */
        if (Input.GetKeyDown(KeyCode.Keypad6))
            _animator.SetBool("isRightHK", true);
        if (Input.GetKeyUp(KeyCode.Keypad6))
            _animator.SetBool("isRightHK", false);

        /* high kick left */
        if (Input.GetKeyDown(KeyCode.Keypad7))
            _animator.SetBool("isLeftHK", true);
        if (Input.GetKeyUp(KeyCode.Keypad7))
            _animator.SetBool("isLeftHK", false);

        /* low kick right */
        if (Input.GetKeyDown(KeyCode.Keypad8))
            _animator.SetBool("isRightBK", true);
        if (Input.GetKeyUp(KeyCode.Keypad8))
            _animator.SetBool("isRightBK", false);

        /* low key left */
        if (Input.GetKeyDown(KeyCode.Keypad9))
            _animator.SetBool("isLeftBK", true);
        if (Input.GetKeyUp(KeyCode.Keypad9))
            _animator.SetBool("isLeftBK", false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            _animator.SetBool("isAttack", false);
 //           _animator.SetFloat("Strengh", 0.0F);
        }
    }
}
