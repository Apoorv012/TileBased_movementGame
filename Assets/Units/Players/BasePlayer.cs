using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : BaseUnit
{
    private SpriteRenderer spriteRenderer;
    const float precisionMultiplier = 10f;

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void UpdateSortingLayer()
    {
        spriteRenderer.sortingOrder = (int)(-transform.position.y * precisionMultiplier);
    }
}
