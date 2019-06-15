using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerVisualizer : MonoBehaviour
{
    [SerializeField] private Transform pointer;
    [SerializeField] private Transform averagedPointer;
    
    private void LateUpdate()
    {
        averagedPointer.position = EyesInput.Instance.mousePosition;
        pointer.position = EyesInput.Instance.rawMousePosition;
        
        Debug.Log($"av: {averagedPointer.position} ptr: {pointer.position}");
    }
}
