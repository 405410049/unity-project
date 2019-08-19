using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//放在effect的prefab上
public class destroy_effect : MonoBehaviour
{
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
