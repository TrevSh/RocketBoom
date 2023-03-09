using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMusicPlayer : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    void Start()
    {
        audio=GetComponent<AudioSource>();
        audio.Play();
    }

}
