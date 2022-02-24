using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager m_Instance;
    private bool isPanelOn = false;
    public GameObject mainPanel;
    public GameObject handCursor;

    public TMP_Text clueTitle;
    public TMP_Text clueDescription;

    private void Awake()
    {
        m_Instance = this;
    }

    private void Update()
    {
        
    }

    public void RefreshWiki()
    {

    }
    public void SetHandCursor(bool isOn)
    {
        handCursor.SetActive(isOn);
    }

    public void SetBackImage(bool isOn)
    {
        Debug.Log("Set Back Image");
    }

    internal void ShowCluePopup(string title, string description)
    {
        if (clueDescription && clueTitle)
        {
            clueTitle.text = title;
            clueDescription.text = description;
        }

    }
}
