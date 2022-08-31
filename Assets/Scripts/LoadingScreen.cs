using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] string sceneName;
    [SerializeField] Animator anim;

    public void Opened(){
        player.enabled = true;
    }

    public void Closed(){
        SceneManager.LoadScene(sceneName);
    }

    public void Close(){
        anim.SetTrigger("Close");
    }
}
