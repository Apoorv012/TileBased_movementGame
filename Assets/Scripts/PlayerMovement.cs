using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private BaseUnit playerUnit;

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
        if (GameManager.Instance.State != GameState.Gameplay)
            return;
        Tile tile = GridManager.Instance.GetTileAtPosition(new Vector2(transform.position.x + _x, transform.position.y + _y));
        if (tile)
        {
            tile.SetUnit(playerUnit);
        }
    }
}
