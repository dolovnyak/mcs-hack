using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EyesInputModule : StandaloneInputModule
{
    protected override void Awake()
    {
        inputOverride = GameObject.Find("Controller").GetComponent<EyesInput>();

        base.Awake();
    }
}
