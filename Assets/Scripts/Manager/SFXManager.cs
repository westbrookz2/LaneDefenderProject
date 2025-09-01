using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
    [SerializeField] private AudioSource audioPos;

    public void PlaySFX(AudioClip clip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(audioPos, spawnTransform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);


    }
    public void PlayRandomSFX(AudioClip[] clip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(audioPos, spawnTransform.position, Quaternion.identity);
        int selectedClip = Random.Range(0, clip.Length);
        audioSource.clip = clip[selectedClip];
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);


    }
}
