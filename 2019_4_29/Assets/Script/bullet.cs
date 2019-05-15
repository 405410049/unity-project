using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class bullet : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    private void Start()
    {
        if (!hasAuthority)
        {
            return;
        }
    }

    private void Update()
    {
        if (hasAuthority)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                CmdFire();
            }
        }
    }

    [Command]
    void CmdFire()
    {
        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        NetworkServer.Spawn(bullet);
        //bullet.transform.position = new Vector3(transform.position.x + 0.2f, transform.position.y + 1.4f, transform.position.z); //punch
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawn.transform.forward * 5.5f;
        Destroy(bullet, 0.15f);
    }
}
