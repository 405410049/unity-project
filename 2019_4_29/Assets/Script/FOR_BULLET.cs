using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOR_BULLET : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        var health = hit.GetComponent<Health>();
        var _animator = collision.transform.root.GetComponent<Animator>();
        if (health != null)
        {
            health.TakeDamage(10);
        }
        if (_animator != null)
        {
            _animator.SetTrigger("bodyHit");
        }
        Destroy(gameObject);
    }
}
