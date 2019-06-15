using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FadeAllButtons : MonoBehaviour
{
    public FadeButton[] buttons;
    public float timerLimit = 2.5f;
    public VideoPlayer videoPlayer;

    private float timer = 0;
    private bool isFaded = false;

    public void FadeOrUnfade()
    {
        if (videoPlayer.isPlaying)
        {
            isFaded = !isFaded;
            foreach (FadeButton b in buttons)
            {
                b.FadeOrUnfade();
            }
        }
    }

    public void Unfade()
    {
        isFaded = false;
        foreach (FadeButton b in buttons)
        {
            b.Unfade();
        }
    }

    public void Fade()
    {
        if (videoPlayer.isPlaying)
        {
            isFaded = true;
            foreach (FadeButton b in buttons)
            {
                b.Fade();
            }
        }
    }

    public void UpdateTimer()
    {
        timer = 0;
    }

    private void Update()
    {
        if (isFaded == false && timer < timerLimit)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (timer >= timerLimit)
                FadeOrUnfade();
            timer = 0;
        }
    }
}
