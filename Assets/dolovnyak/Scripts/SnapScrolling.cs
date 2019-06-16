using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapScrolling   : MonoBehaviour
{
    [Range(1, 50)]
    [Header("Controllers")]
    public int panelCount;
    [Range(0, 500)]
    public int panelIndent;
    [Range(0f, 20f)]
    public float snapSpeed;
    [Range(0f, 5f)]
    public float scaleCoef;
    [Header("Other Objects")]
    public GameObject panelPrefab;
    public ScrollRect scrollRect;


    public float scrollSpeed;

    public bool instantiatePrefabs;
    public float distance = 400;
    
    [SerializeField] private GameObject[] instPans;
    [SerializeField] private Vector3[] panelPos;
    [SerializeField] private Vector3[] panelScale;

    private RectTransform contentRect;
    private Vector3 contentVector;

    private int selectPanelID;
    private bool isScrolling;

    private void Start()
    {
        contentRect = GetComponent<RectTransform>(); 
        
        if (!instantiatePrefabs)
            return;
        
        instPans = new GameObject[panelCount];
        panelPos = new Vector3[panelCount];
        panelScale = new Vector3[panelCount];

        instPans[0] = Instantiate(panelPrefab, transform, false);

        for (int i = 1; i < panelCount; i++)
        {
            instPans[i] = Instantiate(panelPrefab, transform, false);
            instPans[i].transform.localPosition = new Vector3(instPans[i].transform.localPosition.x,
                instPans[i - 1].transform.localPosition.y - panelPrefab.GetComponent<RectTransform>().sizeDelta.y - panelIndent,
                instPans[i].transform.localPosition.z);
            panelPos[i] = -instPans[i].transform.localPosition;
        }
    }

    private void FixedUpdate()
    {
        float nearestPos = float.MaxValue;
        float dist;
        float scale;
        float scrollVelocity;
        if (contentRect.anchoredPosition.y <= panelPos[0].y
            || contentRect.anchoredPosition.y >= panelPos[panelPos.Length - 1].y)
        {
            isScrolling = false;
            scrollRect.inertia = false;
        }
        for (int i = 0; i < panelCount; i++)
        {
            dist = Mathf.Abs(contentRect.anchoredPosition.y - panelPos[i].y);
            if (dist < nearestPos)
            {
                nearestPos = dist;
                selectPanelID = i;
            }
            scale = Mathf.Clamp(1 / (dist / panelIndent) * scaleCoef, 0.7f, 0.9f);
            panelScale[i].x = Mathf.SmoothStep(instPans[i].transform.localScale.x, scale, 6 * Time.fixedDeltaTime); 
            panelScale[i].y = Mathf.SmoothStep(instPans[i].transform.localScale.y, scale, 6 * Time.fixedDeltaTime);
            panelScale[i].z = instPans[i].transform.localScale.z;
            instPans[i].transform.localScale = panelScale[i];
        }
        scrollVelocity = Mathf.Abs(scrollRect.velocity.y);
        if ((scrollVelocity < 400) && (!isScrolling))
            scrollRect.inertia = false;
        if (isScrolling || scrollVelocity > 400)
            return;
        contentVector.y = Mathf.SmoothStep(contentRect.anchoredPosition.y, panelPos[selectPanelID].y, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }

    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
        if (scroll)
            scrollRect.inertia = true;
    }


    public void Scroll(bool isDown)
    {
        if (isDown)
            contentRect.anchoredPosition = new Vector2(contentRect.anchoredPosition.x, contentRect.anchoredPosition.y + distance);
        else
            contentRect.anchoredPosition = new Vector2(contentRect.anchoredPosition.x, contentRect.anchoredPosition.y - distance);
    }
}
