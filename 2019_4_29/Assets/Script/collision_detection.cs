using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision_detection : MonoBehaviour
{
    public GameObject hitReactObj;
    private Animator hitAnimator;
    // Start is called before the first frame update
    void Start()
    {
        hitAnimator = hitReactObj.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Vector3 rayPostion = new Vector3(transform.position.x + 0.2f, transform.position.y + 1.4f, transform.position.z); //punch
            //Vector3 rayPostion = new Vector3 (transform.position.x+0.1f,transform.position.y+1.6f,transform.position.z); //head
            RaycastHit hit;
            Ray myRay = new Ray(rayPostion, Vector3.forward); //Vector3.forward=(0,0,1)
            if (Physics.Raycast(myRay, out hit, 0.8f))
            {
                print(hit.collider.name);
                if (hit.collider.name=="CC_Base_Body")
                {
                    hitReactObj = hit.collider.gameObject;
                    hitAnimator = hitReactObj.GetComponent<Animator>();
                    hitAnimator.SetTrigger("bodyHit");
                }
                    //hitAnimator.SetTrigger("bodyHit");
       //         if (hit.collider.name == "CC_Base_Eye")
       //             hitAnimator.SetTrigger("headHit");
            }

        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Vector3 rayPostion = new Vector3(transform.position.x + 0.2f, transform.position.y + 0.5f, transform.position.z); //kick
            RaycastHit hit;
            Ray myRay = new Ray(rayPostion, Vector3.forward); //Vector3.forward=(0,0,1)
            if (Physics.Raycast(myRay, out hit, 0.8f))
            {
                print(hit.collider.name);
                if (hit.collider.name == "pants")
                {
                    hitReactObj = hit.collider.gameObject;
                    hitAnimator = hitReactObj.GetComponent<Animator>();
                    hitAnimator.SetTrigger("footHit");
                }
                    //hitAnimator.SetTrigger("footHit");
            }
        }
           
          /*      if (hit.collider.name == "karate")
                {
                    hitAnimator = hitReactObj.GetComponent<Animator>();
                    hitAnimator.SetTrigger("bodyHit");
                }*/
    }
  /*  void OnTriggerEnter(Collider obj)
    {
        print("123");
    }*/
}
