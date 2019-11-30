using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitcher : MonoBehaviour
{
    [SerializeField] private AudioClip nextmusic;
    private float fadeOutTime = 2f;
    public void SwitchMusic()
    {
        StartCoroutine(FadeOutAndSwitchMusic());
    }

    private IEnumerator FadeOutAndSwitchMusic()
    {
        var audiosource = GetComponent<AudioSource>();

        float elapsedTime = 0f;
        while (elapsedTime < fadeOutTime)
        {
            audiosource.volume = (1f - (elapsedTime / fadeOutTime));
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        audiosource.Stop();
        audiosource.clip = nextmusic;
        audiosource.volume = 0.5f;
        audiosource.Play();
    }
}
