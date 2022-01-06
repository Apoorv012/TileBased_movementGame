using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRocket : BaseUnit
{
    public int RocketValue;
    public bool isSorted = true;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rocket1 _rocket1;

    private void LateUpdate()
    {
        if (!isSorted)
        {
            const float precisionMultiplier = 10f;
            spriteRenderer.sortingOrder = (int)(-transform.position.y * precisionMultiplier);

            isSorted = true;
        }
    }

    public void RocketTakeOff()
    {
        rb.velocity += new Vector2(0, 10);
        Debug.Log("Destroying");
        Destroy(gameObject, 1f);
    }

    public IEnumerator FixRocket(GameObject _playerGameObject, PlayerMovement _playerMovement)
    {
        _rocket1.isFixed = true;
        _rocket1.timerImage.color = Color.blue;
        _playerGameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(2);
        ScoreManager.Instance.AddRocketScore(RocketValue);
        _playerGameObject.GetComponent<SpriteRenderer>().enabled = true;
        _playerMovement.isFixing = false;
        Destroy(_rocket1.timerCanvasGO);
        RocketTakeOff();
    }
}
