using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] Sprite[] looks;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = looks[Random.Range(0, looks.Length)];
    }
}
