using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreButton : MonoBehaviour
{
    public GameObject wallDad;
    [SerializeField] Image sr;
    [SerializeField] Color deselected;
    [SerializeField] Color selected;
    
    public void Selected(){
        sr.color = selected;
    }

    public void Deselected(){
        sr.color = deselected;
    }
}
