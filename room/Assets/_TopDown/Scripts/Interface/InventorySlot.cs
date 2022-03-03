using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dec
{
    public class InventorySlot : MonoBehaviour
    {
        public Image icon;
        public Image typeIcon;
        public TMP_Text title;

        public Item item;

        private void Start()
        {
            if (item!=null && item.isFound)
            {
                DisplayItemUI(item);
            }
            else
            {
                return;
            }
        }

        public void DisplayItemUI(Item newItem)
        {
            item = newItem;
            icon.sprite = item.icon;
            typeIcon.sprite = TestAPP.m_Instance.m_Manifest.ItemTypeSprites[(int)newItem.type];
            typeIcon.enabled = true;
            icon.enabled = true;
            title.text = newItem.title;
        }
        // display self item by default
        public void DisplayItemUI()
        {
            if (item != null)
            {
                icon.sprite = item.icon;
                typeIcon.sprite = TestAPP.m_Instance.m_Manifest.ItemTypeSprites[(int)item.type];
                typeIcon.enabled = true;
                icon.enabled = true;
                title.text = item.title;
            }

        }

        public void LockItemUI(Item newItem)
        {
            icon.sprite = null;
            icon.enabled = false;
            typeIcon.enabled = false;
        }

        public void ClearSlot()
        {
            item = null;

            icon.sprite = null;
            icon.enabled = false;
        }

        public void ItemSlotOnClick()
        {
            if (item != null)
            {
                Debug.Log("Click Item Slot " + item.title);
                InventoryUI.m_Instance.SetCurrentSlot(this);
                InventoryUI.m_Instance.RefreshInventory();
            }


        }
    }
}