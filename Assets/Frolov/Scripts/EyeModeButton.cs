using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeModeButton : MonoBehaviour
{
    public GameObject open;
    
    public GameObject closed;

    public GameObject[] onlyEyeMode;

    public bool isOpen;
    
    private void Start()
    {
        isOpen = false;
        open.SetActive(false);
        closed.SetActive(true);

//        GameObject[] onlyEyeMode = GameObject.FindGameObjectsWithTag("EyeMode");
//        Debug.Log(onlyEyeMode.Length);
        foreach(var go in onlyEyeMode)
            go.SetActive(isOpen);
    }

    public void SwitchMode()
    {
        isOpen = !isOpen;
        open.SetActive(isOpen);
        closed.SetActive(!isOpen);
        EyesInput.Instance.enabled = isOpen;
//        GameObject[] onlyEyeMode = GameObject.FindGameObjectsWithTag("EyeMode");
        foreach(var go in onlyEyeMode)
            go.SetActive(isOpen);
    }

    public void UpdateObjects()
    {
//        GameObject[] onlyEyeMode = GameObject.FindGameObjectsWithTag("EyeMode");
//        Debug.Log(onlyEyeMode[0].name);
        foreach(var go in onlyEyeMode)
            go.SetActive(isOpen);
    }
}
