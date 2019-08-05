using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pc_role : MonoBehaviour
{
    private Animator animator;
    private GameObject player;
    void Start()
    {
        animator = this.GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        InvokeRepeating("randomAction", 1f,1f);
        InvokeRepeating("stop", 1f, 0.45f);      
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.FindChild("bodyControl").transform);
    }
    void randomAction()
    {
        int speed_num = Random.Range(0, 2);
        switch(speed_num)
        {
            case 0:
                animator.SetFloat("speed", 0f);
                break;
            case 1:
                animator.SetFloat("speed", 0.5f);
                break;
            case 2:
                animator.SetFloat("speed", 1f);
                break;
        }
        int action_num = Random.Range(0, 3);
        switch(action_num)
        {
            case 0:
                animator.SetBool("punch", true);
                break;
            case 1:
                animator.SetBool("punch_R", true);
                break;
            case 2:
                animator.SetBool("kick", true);
                break;
            case 3:
                animator.SetBool("hook_R", true);
                break;
        }
    }
   void stop()
    {
        animator.SetBool("punch", false);
        animator.SetBool("punch_R", false);
        animator.SetBool("kick", false);
        animator.SetBool("hook_R", false);
    //    yield return new WaitForSeconds(0.2f);
    }
}
