using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ImageGetter : MonoBehaviour
{
    bool grab;
    public Renderer m_Display;
    public Camera targetCamera;
    public string screenShotURL = "http://172.20.10.2:5000/abs";
    Texture2D texture;
    public GameObject ageIcon;
    public GameObject bottomButtons;
    public GameObject mainPage;
    public GameObject logPage;

    private bool isFirst = true;

//    private void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Space))
//            grab = true;
//    }

    private void OnPostRender()
    {
        if (!grab) 
            return;
        FillTexture();
        byte[] image = GetPNG();
        StartCoroutine(UploadPNG(image));
        if (isFirst)
        {
            isFirst = false;
            FillTexture();
            image = GetPNG();
            StartCoroutine(UploadPNG(image));
        }
        grab = false;
    }

    public void FillTexture()
    {
            RenderTexture camText = targetCamera.activeTexture;
            RenderTexture.active = camText;
            texture = new Texture2D(camText.width, camText.height, TextureFormat.RGB24, false);
            texture.ReadPixels(new Rect(0, 0, camText.width, camText.height), 0, 0, false);
            texture.Apply();
            if (m_Display != null)
                m_Display.material.mainTexture = texture;
    }

    byte[] GetPNG()
    {
        byte[] image = texture.EncodeToPNG();
        print(image.Length.ToString());
        return image;
    }

    IEnumerator UploadPNG(byte[] image)
    {
        WWWForm form = new WWWForm();
        //form.AddField("frameCount", Time.frameCount.ToString());
        form.AddBinaryData("file", image);

        using (var w = UnityWebRequest.Post(screenShotURL, form))
        {
            yield return w.SendWebRequest();
            if (w.isNetworkError || w.isHttpError)
            {
                print(w.error);
            }
            else
            {
                string age = w.downloadHandler.text;
                string[] tokens = age.Split(new char[] {' ', ','}, StringSplitOptions.RemoveEmptyEntries);
                double a;
                print($"text: {age}");
                if (double.TryParse(tokens[0], out a))
                {
                    if (a >= 18)
                    {
                        ageIcon.SetActive(true);
                        bottomButtons.SetActive(true);
                        mainPage.SetActive(true);
                        logPage.SetActive(false);
                    }
                }
            }
        }
    }

    public void SetGrabTrue()
    {
        if (!grab)
        {
            grab = true;
        }
    }
}
