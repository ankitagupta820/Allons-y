using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponentInChildren<AudioSource>().Play();

    }

    public void playMusic() {
        gameObject.GetComponentInChildren<AudioSource>().Play();
    }

}
