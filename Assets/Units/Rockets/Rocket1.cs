using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket1 : BaseRocket
{
    [SerializeField] private GameObject ExplosionEffect;

    float BlastTimer = 10f;
    private bool isBlasted = false;
    public bool isFixed = false;

    private void Update()
    {
        if (!isBlasted && !isFixed)
        {
            Debug.Log(BlastTimer);
            BlastTimer -= Time.deltaTime;
            if (BlastTimer <= 0)
            {
                RocketBlast();
                isBlasted = true;
            }
        }

    }

    private void RocketBlast()
    {
        Debug.Log("BOOM");
        Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.2f);
    }
}
