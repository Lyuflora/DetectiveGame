using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager m_Instance;
    private bool isPanelOn = false;
    public GameObject mainPanel;

    private void Awake()
    {
        m_Instance = this;
    }

    private void Update()
    {
        
    }

    public void DisplayPanel()
    {
        mainPanel.SetActive(!isPanelOn);
        isPanelOn = !isPanelOn;
    }

    public void RefreshWiki()
    {

    }
}
