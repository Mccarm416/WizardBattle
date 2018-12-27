using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusicController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip music1;
    public AudioClip music2;
    public AudioClip music3;
    public AudioClip music4;
    public AudioClip music5;

    private void Start()
    {
        int musicCount = 0;
        if (music1 != null)
            musicCount++;
        if (music2 != null)
            musicCount++;
        if (music3 != null)
            musicCount++;
        if (music4 != null)
            musicCount++;
        if (music5 != null)
            musicCount++;
        int randomMusicNumber = Random.Range(1, musicCount);

        switch(randomMusicNumber)
        {
            case 1:
                audioSource.clip = music1;
                break;
            case 2:
                audioSource.clip = music2;
                break;
            case 3:
                audioSource.clip = music3;
                break;
            case 4:
                audioSource.clip = music4;
                break;
            case 5:
                audioSource.clip = music5;
                break;
        }
        audioSource.Play();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
