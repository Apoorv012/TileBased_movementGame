using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rocket1 : BaseRocket
{
    [SerializeField] private GameObject ExplosionEffect;
    public GameObject timerCanvasGO;
    public Image timerImage;
    public bool isFixed = false;

    private bool isBlasted = false;
    float BlastTimer = 10f;
    Animator cameraAnimator;

    private void Update()
    {
        if (!isBlasted && !isFixed)
        {
            BlastTimer -= Time.deltaTime;

            float t = scale(0, 10, 0, 1, BlastTimer);
            timerImage.fillAmount = t;

            if (BlastTimer <= 0)
            {
                RocketBlast();
                isBlasted = true;
            }
        }

    }

    private void RocketBlast()
    {
        HealthManager.Instance.health--;
        HealthManager.Instance.UpdateHearts();
        cameraAnimator = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();

        StartCoroutine(CameraShake());
        Debug.Log("BOOM");
        Instantiate(ExplosionEffect, transform.position, Quaternion.identity);

        Destroy(gameObject, 0.02f);
    }

    IEnumerator CameraShake()
    {
        cameraAnimator.SetBool("DoCameraShake", true);
        yield return new WaitForEndOfFrame();
        cameraAnimator.SetBool("DoCameraShake", false);
    }

    public float scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }
}
