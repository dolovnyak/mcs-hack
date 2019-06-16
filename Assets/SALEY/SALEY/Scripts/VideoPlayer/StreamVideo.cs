using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{
    public bool isLocal;


    public float waitLimit = 5f;
    public float waitDelay = 0.1f;
    public Camera mainCamera;
    public Camera fullScreenCamera;

    private VideoPlayer videoPlayer;
    private RawImage normalRawImage;

    private WaitForSeconds wait;
    private float waitTime = 0f;
    private bool isPrepared = false;

   // [SerializeField] private AlertButton alertButton;

    private enum PlayMod
    {
        Normal,
        FullScreen
    }

    private PlayMod playMod = PlayMod.Normal;

    private void Awake()
    {
        wait = new WaitForSeconds(waitDelay);
    }

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        normalRawImage = GetComponent<RawImage>();
        if (isLocal)
        {
            videoPlayer.source = VideoSource.VideoClip;
        }
        else
        {
            videoPlayer.source = VideoSource.Url;
        }
    }

    void StartPlaying()
    {
        Debug.Log("start preparing");
        StartCoroutine(PrepareVideo());
        Debug.Log("end preparing");
        videoPlayer.Play();
    }

    IEnumerator PrepareVideo()
    {
        videoPlayer.Prepare();
        waitTime = 0f;
        isPrepared = false;
        while (!videoPlayer.isPrepared)
        {
            yield return wait;
            waitTime += waitDelay;
            if (waitTime >= waitLimit)
                break;
        }
        if (videoPlayer.isPrepared)
        {
            normalRawImage.texture = videoPlayer.texture;
            isPrepared = true;
        }
    }

    public void PlayOrPause()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
        else
        {
            StartPlaying();
        }
    }

    private void PrepareVideoPlayerToFullScreenMod()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        mainCamera.depth = -2;
        fullScreenCamera.depth = -1;
        StartCoroutine(WaitingScreenChange());
        videoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
        playMod = PlayMod.FullScreen;
    }

    IEnumerator WaitingScreenChange()
    {
        ScreenOrientation target =
            Screen.orientation == ScreenOrientation.Portrait ?
            ScreenOrientation.LandscapeLeft : ScreenOrientation.Portrait;
        while (Screen.orientation != target)
        {
            yield return new WaitForSeconds(.1f);
        }
    }

    private void PrepareVideoplayerToNormalMod()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        mainCamera.depth = -1;
        fullScreenCamera.depth = -2;
        StartCoroutine(WaitingScreenChange());
        playMod = PlayMod.Normal;
        videoPlayer.renderMode = VideoRenderMode.APIOnly;
    }

    public void SwitchPlayMod()
    {
        bool isPlaying = videoPlayer.isPlaying;
        if (isPlaying)
        {
            videoPlayer.Pause();
        }

        if (playMod == PlayMod.Normal)
        {
            PrepareVideoPlayerToFullScreenMod();
        }
        else
        {
            PrepareVideoplayerToNormalMod();
        }

        if (isPlaying)
        {
            StartPlaying();
        }
    }


    private void Update()
    {
        //if (videoPlayer.isPlaying && !IsPlayble() && playMod == PlayMod.Normal)
        //{
        //    videoPlayer.Pause();
        //}
        //if (!alertButton.isAlert && Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (playMod == PlayMod.Normal)
        //    {
        //        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        //    }
        //    else
        //    {
        //        SwitchPlayMod();
        //    }
        //}
    }

    private bool IsPlayble()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPos.y > Screen.height)
        {
            return false;
        }
        return true;
    }
}