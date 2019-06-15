using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeButton : MonoBehaviour
{
    public float dTime = .05f;
    public float dAlpha = 2.55f;

    private Image image;
    private bool isFaded = false;
    private WaitForSeconds wait;

    private void Awake()
    {
        image = GetComponent<Image>();
        wait = new WaitForSeconds(dTime);
    }

    public void Fade()
    {
        isFaded = true;
        //StartCoroutine(Fading());
        Color color = image.color;
        color.a = 0;
        image.color = color;
        image.raycastTarget = false;
    }

    IEnumerator Fading()
    {
        Color color = image.color;
        while (color.a != 0)
        {
            float a = image.color.a;
            a -= dAlpha;
            if (a < 0)
                a = 0;
            color.a = a;
            Debug.Log(color.ToString());
            image.color = color;
            yield return wait;
        }
        image.raycastTarget = false;
    }

    public void Unfade()
    {
        isFaded = false;
        //StartCoroutine(Unfading());
        Color color = image.color;
        color.a = 255;
        image.color = color;
        image.raycastTarget = true;
    }

    IEnumerator Unfading()
    {
        Color color = image.color;
        image.raycastTarget = true;
        while (color.a != 255)
        {
            float a = image.color.a;
            a += dAlpha;
            if (a > 255)
                a = 255;
            color.a = a;
            Debug.Log(color.ToString());
            image.color = color;
            yield return wait;
        }
    }

    public void FadeOrUnfade()
    {
        if (isFaded)
        {
            //StopAllCoroutines();
            Unfade();
        }
        else
        {
            //StopAllCoroutines();
            Fade();
        }
    }
}