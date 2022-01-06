using System;
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

    public IEnumerator FixRocket(GameObject _playerGameObject, PlayerMovement _playerMovement)
    {
        _playerGameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(2);
        ScoreManager.Instance.AddRocketScore(RocketValue);
        _playerGameObject.GetComponent<SpriteRenderer>().enabled = true;
        _playerMovement.isFixing = false;
        Destroy(gameObject);
    }
}
