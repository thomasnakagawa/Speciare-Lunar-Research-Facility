using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OatsUtil;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private Canvas canvas;
    private Image image;

    public AudioSource musicSource;

    private float fadeOutTime = 5f;

    private void Start()
    {
        canvas = this.RequireComponent<Canvas>();
        canvas.enabled = false;
        image = this.RequireComponent<Image>();

    }

    public void End()
    {
        StartCoroutine(EndSequence());
    }

    private IEnumerator EndSequence()
    {
        canvas.enabled = true;
        float elapsedTime = 0f;
        while (elapsedTime < fadeOutTime)
        {
            image.color = new Color(0f, 0f, 0f, elapsedTime / fadeOutTime);
            musicSource.volume = (1f - (elapsedTime / fadeOutTime));
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        SceneManager.LoadScene(0);
    }
}
