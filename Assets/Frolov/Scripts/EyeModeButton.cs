using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeModeButton : MonoBehaviour
{
    public GameObject open;
    
    public GameObject closed;

    public bool isOpen;

    private GameObject[] onlyEyeMode;
    
    private void Start()
    {
        isOpen = false;
        open.SetActive(false);
        closed.SetActive(true);

        onlyEyeMode = GameObject.FindGameObjectsWithTag("EyeMode");
        foreach(var go in onlyEyeMode)
            go.SetActive(isOpen);
    }

    public void SwitchMode()
    {
        isOpen = !isOpen;
        open.SetActive(isOpen);
        closed.SetActive(!isOpen);
        EyesInput.Instance.enabled = isOpen;
        foreach(var go in onlyEyeMode)
            go.SetActive(isOpen);
        Debug.Log(1);
    }
}
