using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSpawner : MonoBehaviour
{
    [SerializeField] private GameObject SquarePrefab;

    private void Start()
    {
        Instantiate(SquarePrefab, new Vector3(), Quaternion.identity);
    }
}
