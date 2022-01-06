using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private BaseUnit basePlayerUnit;
    [SerializeField] private BasePlayer playerUnit;
    public bool isFixing = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(0, 1);
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(-1, 0);
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(0, -1);
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(1, 0);
        }
        
    }

    private void Move(int _x, int _y)
    {
        if (!isFixing)
        {
            if (GameManager.Instance.State != GameState.Gameplay)
                return;
            Tile tile = GridManager.Instance.GetTileAtPosition(new Vector2(transform.position.x + _x, transform.position.y + _y));
            if (tile && tile.isWalkable)
            {
                if (tile.OccupiedUnit != null)
                {
                    // Rocket here
                    // IEnumerator FixRocketCoroutine = tile.OccupiedUnit.gameObject.GetComponent<BaseRocket>().FixRocket(gameObject);
                    // StartCoroutine(FixRocketCoroutine);
                    StartCoroutine(tile.OccupiedUnit.gameObject.GetComponent<BaseRocket>().FixRocket(gameObject, this));
                    isFixing = true;
                    //StartCoroutine(tile.OccupiedUnit.gameObject.GetComponent<BaseRocket>().something());

                }
                tile.SetUnit(basePlayerUnit);
                playerUnit.UpdateSortingLayer();
            }
        }
    }
}
