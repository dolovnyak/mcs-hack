using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EyesInput))]
public class EyesInputModule : StandaloneInputModule
{
    protected override void Awake()
    {
        inputOverride = GetComponent<EyesInput>();

        base.Awake();
    }
}
