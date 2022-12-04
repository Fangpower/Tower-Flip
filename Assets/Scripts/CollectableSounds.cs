using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectableSounds : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioSource ad;
    [SerializeField] TMP_Text coins;

    void Start()
    {
        ad.clip = clips[Random.Range(0, clips.Length)];
        ad.pitch = 1 + Random.Range(-0.1f, 0.1f);
        ad.Play();
        coins.text = FindObjectOfType<Score>().coins.ToString();
        coins.gameObject.GetComponent<Animator>().SetTrigger("Collect");
    }
}
