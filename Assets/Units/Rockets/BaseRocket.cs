using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRocket : BaseUnit
{
    public int RocketValue;
    public bool isSorted = true;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        if (!isSorted)
        {
            const float precisionMultiplier = 10f;
            spriteRenderer.sortingOrder = (int)(-transform.position.y * precisionMultiplier);

            isSorted = true;
        }
    }

    public void FixRocket()
    {
        ScoreManager.Instance.AddRocketScore(RocketValue);
        Destroy(gameObject);
    }
}
