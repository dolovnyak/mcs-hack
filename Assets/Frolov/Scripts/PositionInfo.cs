using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PositionInfo : MonoBehaviour
{
//    public Text leftEyeLabel;
//    public Text rightEyeLabel;
    public Text eyesLabel;

    public Transform mainCamera;
    
//    public Text allLabel;
//
//    public Transform parent;
//    public Transform camera;
//    public Transform ARcamera;

    public Transform pointer;
    public Transform curPointer;

    [SerializeField]
    private Transform leftEye;
    [SerializeField]
    private Transform rightEye;

    private Queue<Vector3> queue = new Queue<Vector3>();
    
    private Vector2 iPhoneXPointSize = new Vector2(375, 812) * 3;
    private Vector2 iPhoneXMeterSize = new Vector2(0.0623908297f, 0.135096943231532f);
    
    public static Vector3 SetVectorLength(Vector3 vector, float size){
 
        //normalize the vector
        Vector3 vectorNormalized = Vector3.Normalize(vector);
 
        //scale the vector
        return vectorNormalized *= size;
    }
    
    public static bool LinePlaneIntersection(out Vector3 intersection, Vector3 linePoint, Vector3 lineVec, Vector3 planeNormal, Vector3 planePoint){
 
        float length;
        float dotNumerator;
        float dotDenominator;
        Vector3 vector;
        intersection = Vector3.zero;
 
        //calculate the distance between the linePoint and the line-plane intersection point
        dotNumerator = Vector3.Dot((planePoint - linePoint), planeNormal);
        dotDenominator = Vector3.Dot(lineVec, planeNormal);
 
        //line and plane are not parallel
        if(dotDenominator != 0.0f){
            length =  dotNumerator / dotDenominator;
 
            //create a vector from the linePoint to the intersection point
            vector = SetVectorLength(lineVec, length);
 
            //get the coordinates of the line-plane intersection point
            intersection = linePoint + vector;	
 
            return true;	
        }
 
        //output not valid
        else{
            return false;
        }
    }
//    Vector3 ml = Camera.main.transform.InverseTransformPoint((li) / 2.0f);
//    ml.x = ml.x / (iPhoneXMeterSize.x / 2.0f) * (iPhoneXPointSize.x * 3 / 2.0f) + iPhoneXPointSize.x / 2.0f;
//    ml.y = ml.y / (iPhoneXMeterSize.y / 2.0f) * (iPhoneXPointSize.y * 3 / 2.0f) + iPhoneXPointSize.y / 2.0f;
//            
//    Vector3 mr = Camera.main.transform.InverseTransformPoint((ri) / 2.0f);
//    mr.x = mr.x / (iPhoneXMeterSize.x / 2.0f) * (iPhoneXPointSize.x * 3 / 2.0f) + iPhoneXPointSize.x / 2.0f;
//    mr.y = mr.y / (iPhoneXMeterSize.y / 2.0f) * (iPhoneXPointSize.y * 3 / 2.0f) + iPhoneXPointSize.y / 2.0f;

    void ModeEyesTracking(Vector3 mi)
    {
        if (queue.Count < 5)
        {
            queue.Enqueue(mi);
        }
        else
        {
            Vector3 res = mi;
            foreach (Vector3 vec in queue)
            {
                res += vec;
            }
            res /= queue.Count + 1;
            queue.Dequeue();
            queue.Enqueue(res);
            pointer.position = res;
            eyesLabel.text = $"{res}";
        }
    }

    void Update()
    {
        Vector3 li, ri;

        if (LinePlaneIntersection(out li, leftEye.position, leftEye.forward, mainCamera.forward, Vector3.zero) &&
            LinePlaneIntersection(out ri, rightEye.position, rightEye.forward, mainCamera.forward, Vector3.zero))
        {
            Vector3 mi = Camera.main.transform.InverseTransformPoint(li);
            mi.x = mi.x / (iPhoneXMeterSize.x / 2.0f) * (iPhoneXPointSize.x) + iPhoneXPointSize.x / 2.0f;
            mi.y = mi.y / (iPhoneXMeterSize.y / 2.0f) * (iPhoneXPointSize.y) + iPhoneXPointSize.y / 2.0f;

            curPointer.position = mi;
            
            ModeEyesTracking(mi);
        }
        
        
    }
}
