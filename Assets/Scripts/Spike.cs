using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] Sprite[] looks;
    private int curLooks;

    void Start()
    {
        curLooks = Random.Range(0, looks.Length);
        GetComponent<SpriteRenderer>().sprite = looks[curLooks];
    }

    void OnTriggerEnter2D(Collider2D col){
        if(curLooks % 2 == 0){
            curLooks++;
            GetComponent<SpriteRenderer>().sprite = looks[curLooks];
        }
    }
}
