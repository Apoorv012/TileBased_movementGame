using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Sprite _baseSprite, _offsetSprite;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject _highlight;

    public bool isWalkable = true;

    public BaseUnit OccupiedUnit;
    public bool Walkable()
    {
        return OccupiedUnit == null && isWalkable;
    }

    public void init(bool _isOffset)
    {
        spriteRenderer.sprite = _isOffset ? _offsetSprite : _baseSprite;
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
       _highlight.SetActive(false);
    }

    public void SetUnit(BaseUnit unit)
    {
        if(unit.OccupiedTile != null)
        {
            unit.OccupiedTile.OccupiedUnit = null;
        }
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }

    public void ChangeSpriteTo(Sprite _sprite)
    {
        spriteRenderer.sprite = _sprite;
    }
}
