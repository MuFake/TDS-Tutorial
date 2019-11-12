# Top Down Shooting Tutorial

This tutorial will show you how to set up a shooting script for your Top Down character controller, and how to apply damage to objects.

## 1. Set up your Scene

Make sure you have the `Character Controller` and the `Main Camera` in the scene from the previous tutorial, and make sure that it works.

Create a cube objet and name it `Gun`.

Remove the `Box Collider` from `Gun`.

Parent the `Gun` object to the `Player` and then scale it and position infront of the `Character Controller` so that it looks like a gun of some sort.

Create a `Sphere` and call it `Bullet Prefab`.

Scale down the `Bullet Prefab` to the size of bullet you want to have.

Add `Rigidbody` component to the `Bullet Prefab` and make sure that `use Gravity` is ticked off. 

Drag and drop the `Bullet Prefab` into your `Prefabs` folder, and delete the `Bullet Prefab` inside of the scene.

Now we are set up and can start adding the code.

## 2. Aim
This code will allow the player to look around and aim their gun at potential enemies.

Create a new script and call it `LookScript`.

Drag and drop `LookScript` onto the `Player`.

Paste the following code into `LookScript`.

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookScript : MonoBehaviour
{
    public Camera mainCam;

    void Update()
    {
        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            transform.LookAt(hit.point);
        }

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
```

Now locate the `LookScript` in the Inspector of `Player` and then drop the `Main Camera` form the scene into the public variable named `mainCam`.

Press play to test if the script works, the `Player` should look towards the cursor.

## 3. Bullets
This code allows to detect collision between the bullet and anything it hits.

Crate a new Script and call it `BulletScript`.

Locate your `Prefabs` folder and double click on the `BulletPrefab`, this should open the Prefab editor.

Drag and drop the script onto the `BulletPrefab`.

Now put the following code into the `BulletScript` script.

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Health>())
        {
            collision.gameObject.BroadcastMessage("ApplyDamage", damage);
            Destroy(gameObject);
        }

        else
            Destroy(gameObject);
    }
}

```

Leave the `damage` variable 0, as this will be changed through other code.

## 4. Health 
This code gives objects a set amount of Hit Points, any object with this code attached will take damage from the bullet.

Create a new script and name it `Health`.

Paste the following code into the `Health` script.

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;

    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void ApplyDamage(float damage)
    {
        health -= damage;
    }
}

```

Now you put this script onto any object in the scene that can be damaged by a bullet [Make sure the object has a `Collider`].

You have to set the `health` variable to anything above 0, else it will destroy itself.

## 5. Guns guns guns!
This code will allow the player to shoot the previously made `BulletPrefab` and apply damage to objects.

Create a new script and call it `BulletScript`.

Paste the following code into the `BulletScript`.

```
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
        if(Input.GetMouseButton(0) && !isShooting && curAmmo > 0 && !isReloading)
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

```

Drag and drop the code onto the `Gun` object inside of the `Player`.

Now we have to set up the variables. Make sure that the you have the `Gun` selected and that you see the `GunScript` in Inspector.

Drag and drop the `Gun` GameObject into the `Shoot Pos` variable, this tells the script where to shoot bullets from.

Drag and drop the `BulletPrefab` into the `Projectile Prefab` variable.

Change `Bullet Speed` to what ever speed you want the bullet to travel at, I recomment `10` for now.

Change `Bullet Damage` to any amount of damage you want the bullet to apply to objects with the `Health` script. I recommend `25`.

Change `Cur Ammo` to the number of current bullets the player has.

Change `Max Ammo` to the number of bullets you want the player to have once they have reloaded.

`Reload Time` is a delay that takes place once the player runs out of ammo, it simulates reloading. I recommned having this as `1.5` for now.

`Shoot Delay` is the delay between each shot, I recommend seting this to `0.2`. [The lower the number the faster the fire rate]

`Destroy Timer` is the life span of the bullet, once the 10 seconds have passed, the bullet will destroy itself.

## 5. Finished
You have finished the Shooting tutorial, you should now be able to shoot bullets that damage objects.

You can create a large `Cube`, then put the `Health` script onto the `Cube`, and set the health to `100`.

Now if you press play, you should be able to shoot the `Cube` and depending on how much damage you do and how much health it has, it should destroy itself after a few hits.

