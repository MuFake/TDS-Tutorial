using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    bool isShooting = false;
    bool isReloading = false;
    public Transform shootPos;
    public GameObject projectilePrefab;
    public float bulletSpeed;
    public float bulletDamage;
    public float curAmmo;
    public float maxAmmo;
    public float reloadTime = 1.5f;
    public float shootDelay = 0.2f;
    public float destroyTimer = 10.0f;

    void Update()
    {
        if(Input.GetMouseButton(0) && !isShooting && curAmmo > 0)
        {
            isShooting = true;
            GameObject clone;
            clone = Instantiate(projectilePrefab, shootPos.position, shootPos.rotation);
            clone.GetComponent<BulletScript>().damage = bulletDamage;
            clone.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
            Destroy(clone, destroyTimer);
            Invoke("ShotReset", shootDelay);
            curAmmo--;
        }

        if(Input.GetMouseButton(0) && curAmmo == 0  && !isReloading || Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            isReloading = true;
            Invoke("Reload", reloadTime);
        }
    }

    void ShotReset()
    {
        isShooting = false;
    }

    void Reload()
    {
        isReloading = false;
        curAmmo = maxAmmo;
    }
}
