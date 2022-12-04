using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] Animator storeAnim;
    [SerializeField] GameObject currentWallDad;

    [SerializeField] StoreButton sb;

    public void OpenStore(){
        storeAnim.SetBool("Open", true);
    }

    public void CloseStore(){
        storeAnim.SetBool("Open", false);
    }

    public void SetWallDad(StoreButton storeButton){
        sb.Deselected();
        sb = storeButton;
        sb.Selected();
        GameObject.Destroy(currentWallDad);
        var temp = Instantiate(storeButton.wallDad, Vector3.zero, Quaternion.identity);
        currentWallDad = temp;
    }
}
