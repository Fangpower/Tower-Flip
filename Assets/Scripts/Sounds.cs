using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sounds : MonoBehaviour
{
    //[SerializeField] AudioListener audioListener;
    [SerializeField] Sprite soundOn;
    [SerializeField] Sprite soundOff;
    [SerializeField] Image soundImage;

    private bool isSound = true;

    public void SwitchSound(){
        if(isSound){
            AudioListener.pause = true;
            soundImage.sprite = soundOff;
            isSound = false;
        } else {
            AudioListener.pause = false;
            soundImage.sprite = soundOn;
            isSound = true;
        }
    }
}
