using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject _highlight;

    public void init(bool _isOffset)
    {
        spriteRenderer.color = _isOffset ? _offsetColor : _baseColor;

    }

    private void OnMouseEnter()
    {
        Debug.Log("Enter");
    }

    private void OnMouseExit()
    {
        Debug.Log("Exit");
    }
}
