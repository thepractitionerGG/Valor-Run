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
    public void PlayAudioOnce(AudioClip clip,Transform transform)
    {
        StartCoroutine(PlayAudioOnceCourtine(clip, transform));
    }

    IEnumerator PlayAudioOnceCourtine(AudioClip clip,Transform transform)
    {

        GameObject audioSorceObject = new GameObject();
        audioSorceObject.transform.position = Vector3.zero;
        audioSorceObject.name = "audioObject";
        audioSorceObject.AddComponent<AudioSource>();
        audioSorceObject.GetComponent<AudioSource>().clip = clip;
        audioSorceObject.GetComponent<AudioSource>().volume = .5f;
        Instantiate(audioSorceObject, transform);

        while (audioSorceObject.GetComponent<AudioSource>().isPlaying)
        {
            yield return null;
        }

        Destroy(audioSorceObject);

    }
}
