using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control_sound : MonoBehaviour
{
    public AudioClip Clip_1;
    public AudioClip Clip_2;
    public AudioClip Clip_3;
    public AudioSource Source_1;
    public AudioSource Source_2;
    public AudioSource Source_3;
    // Start is called before the first frame update
    void Start()
    {
        Source_1.clip = Clip_1;
        Source_2.clip = Clip_2;
        Source_3.clip = Clip_3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
            Source_1.Play();

        if (Input.GetKeyDown(KeyCode.Keypad1))
            Source_2.Play();

        if (Input.GetKeyDown(KeyCode.Keypad2))
            Source_3.Play();

        if (Input.GetKeyDown(KeyCode.Keypad3))
            Source_1.Play();

        if (Input.GetKeyDown(KeyCode.Keypad4))
            Source_2.Play();

        if (Input.GetKeyDown(KeyCode.Keypad5))
            Source_3.Play();
    }
}
