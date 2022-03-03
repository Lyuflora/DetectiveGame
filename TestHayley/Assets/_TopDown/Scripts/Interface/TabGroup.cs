using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dec
{
    public class TabGroup : MonoBehaviour
    {
        public List<TabButton> tabButtonList;
        public TabButton selectedTab;
        public Sprite tabId;
        public Sprite tabHover;
        public Sprite tabActive;
        public List<GameObject> objectToSwap;

        public void Subscribe(TabButton tabButton)
        {
            if (tabButtonList == null)
            {
                tabButtonList = new List<TabButton>();
            }

            tabButtonList.Add(tabButton);
        }

        public void OnTabEnter(TabButton tabButton)
        {
            ResetTabs();
            if (selectedTab == null || tabButton != selectedTab) { 
                tabButton.background.sprite = tabHover;
            }
        }

        public void OnTabExit(TabButton tabButton)
        {
            ResetTabs();
            tabButton.background.sprite = tabId;
        }

        public void OnTabSelected(TabButton tabButton)
        {
            if (selectedTab != null)
            {
                selectedTab.DeSelect();
            }

            ResetTabs();
            tabButton.background.sprite = tabActive;
            selectedTab = tabButton;
            selectedTab.Select();

            int index = tabButton.transform.GetSiblingIndex();
            for(int i=0; i<objectToSwap.Count; i++)
            {
                if (i == index)
                {
                    objectToSwap[i].SetActive(true);
                }
                else
                {
                    objectToSwap[i].SetActive(false);
                }
            }
        }

        public void ResetTabs()
        {
            foreach(TabButton button in tabButtonList)
            {
                if (selectedTab != null && button == selectedTab) continue;
                button.background.sprite = tabId;
            }
        }
    }
}