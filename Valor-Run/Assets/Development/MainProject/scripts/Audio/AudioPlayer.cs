using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer audioPlayerSingle;

    private void Start()
    {
        audioPlayerSingle = this;
    }
    public void PlayAudioOnce(AudioClip clip,Transform transform,float volume)
    {
        StartCoroutine(PlayAudioOnceCourtine(clip, transform,volume));
    }

    IEnumerator PlayAudioOnceCourtine(AudioClip clip,Transform transform,float volume)
    {

        GameObject audioSorceObject = new GameObject();
        audioSorceObject.transform.position = Vector3.zero;
        audioSorceObject.name = "audioObject";
        audioSorceObject.AddComponent<AudioSource>();
        audioSorceObject.GetComponent<AudioSource>().clip = clip;
        audioSorceObject.GetComponent<AudioSource>().volume = volume;
        Instantiate(audioSorceObject, transform);

        while (audioSorceObject.GetComponent<AudioSource>().isPlaying)
        {
            yield return null;
        }

        Destroy(audioSorceObject);

    }
}
