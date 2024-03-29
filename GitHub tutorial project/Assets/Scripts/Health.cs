﻿using System.Collections;
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
