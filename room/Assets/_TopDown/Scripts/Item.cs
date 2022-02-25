using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dec
{
[CreateAssetMenu(fileName = "NewItemInfo", menuName = "Dec/ItemInfo", order = 1)]
public class Item : ScriptableObject
{
        [Header("Item Details")]
        public ItemType type;
        public string title;
        public bool available = false;  // no use currently
        public string description;
        public Sprite image;
        public AudioClip audioClip;


        [HideInInspector]
        public bool linkNode = false;


        [Header("Inventory")]
        public bool inventoryItem;
        public string collectMessage;
        public bool grabbable;
        public bool requiredItem;   // no use
        public bool isFound = false;

        [Header("Clue")]
        public List<Clue> clueList;

    }
}