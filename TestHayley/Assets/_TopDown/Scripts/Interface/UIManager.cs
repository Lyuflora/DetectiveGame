using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Dec
{

    public class UIManager : MonoBehaviour
    {
        public static UIManager m_Instance;
        private bool isPanelOn = false;
        public GameObject mainPanel;
        public GameObject handCursor;
        public GameObject inductionCanvas;
        public Animator inventoryAnim;
        private bool isInduction;
        private bool isInventory;
        [SerializeField] private InventoryUI inventoryUI;

        public TMP_Text clueTitle;
        public TMP_Text clueDescription;

        private void Awake()
        {
            m_Instance = this;
        }

        private void Start()
        {
            isInduction = false;
            inductionCanvas.SetActive(false);
            isInventory = false;
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

        public void SwitchInductionCanvas()
        {
            if (isInduction)
            {
                inductionCanvas.SetActive(false);
            }
            else
            {
                inductionCanvas.SetActive(true);
            }
            isInduction = !isInduction;
            return;
        }

        public void SwitchInventory()
        {
            isInventory = !isInventory;
            inventoryAnim.SetBool("isShow", isInventory);
            inventoryUI.RefreshInventory();
            return;
        }
    }
}