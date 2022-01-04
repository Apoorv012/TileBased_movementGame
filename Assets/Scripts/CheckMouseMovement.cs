using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMouseMovement : MonoBehaviour
{
    private void OnMouseEnter()
    {
        Debug.Log("Enter");
    }

    private void OnMouseExit()
    {
        Debug.Log("Exit");
    }
}
