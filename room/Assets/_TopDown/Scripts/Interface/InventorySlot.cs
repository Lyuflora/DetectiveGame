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

        public void AddItemUI(Item newItem)
        {
            item = newItem;
            icon.sprite = item.icon;
            typeIcon.sprite = TestAPP.m_Instance.m_Manifest.ItemTypeSprites[(int)newItem.type];
            typeIcon.enabled = true;
            icon.enabled = true;
            title.text = newItem.title;
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
            Debug.Log("Click Item Slot " + item.title);
            InventoryUI.m_Instance.SetCurrentSlot(this);
            InventoryUI.m_Instance.RefreshInventory();
        }
    }
}