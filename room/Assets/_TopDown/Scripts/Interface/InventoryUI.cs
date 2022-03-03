using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dec
{
    public class InventoryUI : MonoBehaviour
    {
        public Transform itemsParent;
        PlayerInventory inventory;

        InventorySlot[] inventorySlots;
        [SerializeField] private InventorySlot currentSlot;
        Manifest manifest;

        public static InventoryUI m_Instance;

        [Header("ItemDetailArea")]
        public TMP_Text itemName;
        public TMP_Text itemDescription;
        public Image itemImage;
        public Transform clueParent;
        public GameObject clueEntryPrefab;

        [Header("HiddenInventory")]
        public string unknownItemName = "? ? ?";

        private void Awake()
        {
            m_Instance = this;
        }
        private void Start()
        {
            inventory = PlayerInventory.m_Instance;
            inventory.OnItemChangedEvent += UpdateItemUI;

            inventorySlots = itemsParent.GetComponentsInChildren<InventorySlot>();
            manifest = TestAPP.m_Instance.m_Manifest;

            InitInventoryUI();
            currentSlot = inventorySlots[0];    //   默认第一个
        }

        public void InitInventoryUI()
        {
            for(int i = 0; i < inventorySlots.Length; i++)
            {
                if (i < TestAPP.m_Instance.m_Manifest.m_AllItems.Count)
                    inventorySlots[i].item = TestAPP.m_Instance.m_Manifest.m_AllItems[i];
                else
                    return;
            }
        }


        void UpdateItemUI()
        {
            Debug.Log("Updating UI");
            // 固定顺序
            for(int i = 0; i < inventorySlots.Length; i++)
            {
                if(i < manifest.m_AllItems.Count)
                {
                    Item item = manifest.m_AllItems[i];
                    if (item.isFound)
                    {
                        Debug.Log("Add Item to Inventory");
                        inventorySlots[i].DisplayItemUI(item);
                        itemName.text = item.title;
                    }
                    else
                    {
                        inventorySlots[i].LockItemUI(item);
                        itemName.text = unknownItemName;
                    }
                }
                
            }

        }

        public void UpdateClueUI(Item item)
        {
            for(int i = 0; i < item.clueList.Count; i++)
            {
                if (item.clueList[i].isFound)
                {
                    ClueSlot clueSlot = Instantiate(clueEntryPrefab, clueParent).GetComponent<ClueSlot>();
                    clueSlot.GenerateClueEntry(item.clueList[i]);

                }
            }
        }

        public void SetCurrentSlot(InventorySlot slot)
        {
            currentSlot = slot;
        }

        public void DisplayItem(InventorySlot slot)
        {
            Item item = slot.item;
            itemName.text = item.title;
            itemDescription.text = item.description;
            if (item.image!=null)
            {
                itemImage.sprite = item.image;
            }
        }

        public void RefreshInventory()
        {
            UpdateItemUI();
            foreach (Transform child in clueParent)
            {
                GameObject.Destroy(child.gameObject);
            }

            DisplayItem(currentSlot);
            UpdateClueUI(currentSlot.item);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RefreshInventory();
            }
        }
    }
}