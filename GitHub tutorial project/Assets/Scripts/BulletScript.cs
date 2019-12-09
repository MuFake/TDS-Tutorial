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
