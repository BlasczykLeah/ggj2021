using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager inst;

    public List<AudioClip> audioClips;

    public AudioSource audioSource;
    public AudioSource walkSource;

    private void Awake()
    {
        if (inst != null)
        {
            Destroy(this);
        }
        else
            inst = this;
    }

    private void Start()
    {
        inst = this;
    }

    public void PlayBark()
    {
        int rand = Random.Range(0, 4);

        switch (rand)
        {
            case 0:
                audioSource.clip = audioClips[0];
                audioSource.Play();
                return;
            case 1:
                audioSource.clip = audioClips[1];
                audioSource.Play();
                return;
            case 2:
                audioSource.clip = audioClips[2];
                audioSource.Play();
                return;
            default:
                audioSource.clip = audioClips[3];
                audioSource.Play();
                return;
        }
    }
    
    public void PlayWalk(bool play)
    {
        walkSource.clip = audioClips[4];

        if (play)
            walkSource.Play();
        else
            walkSource.Stop();
    }

    public void PlayDig()
    {
        audioSource.clip = audioClips[5];
        audioSource.Play();
    }

    public void PlaySniff()
    {
        audioSource.clip = audioClips[6];
        audioSource.Play();
    }

    public void OpenMap()
    {
        audioSource.clip = audioClips[7];
        audioSource.Play();
    }

    public void CloseMap()
    {
        audioSource.clip = audioClips[8];
        audioSource.Play();
    }

    public void PersonSaved()
    {
        audioSource.clip = audioClips[9];
        audioSource.Play();
    }
}
