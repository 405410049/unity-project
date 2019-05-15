using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy_effect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndDestroy(0.5f));
    }

    IEnumerator WaitAndDestroy(float waitTime)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(transform.gameObject);

    }

}
