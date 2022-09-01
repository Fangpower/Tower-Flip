using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSounds : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioSource ad;

    void Start()
    {
        ad.clip = clips[Random.Range(0, clips.Length)];
        ad.pitch = 1 + Random.Range(-0.1f, 0.1f);
        ad.Play();
    }
}
