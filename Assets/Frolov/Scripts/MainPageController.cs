using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPageController : MonoBehaviour
{
    public GameObject[] pages;

    public SnapScrolling ss;

    public void SelectPage()
    {
        pages[ss.SelectPanelID].SetActive(true);
        gameObject.SetActive(false);
    }
}
