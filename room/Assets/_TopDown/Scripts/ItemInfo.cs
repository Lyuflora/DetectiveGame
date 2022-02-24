using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dec
{
[CreateAssetMenu(fileName = "NewItemInfo", menuName = "Dec/ItemInfo", order = 1)]
public class ItemInfo : ScriptableObject
{
        public ClueType type;
        public int clueId;
        public string title;
        public bool available = false;
        public string description;
        public Sprite image;
        public AudioClip audioClip;
        public bool grabbable;

        public bool linkNode = false;
        public bool requiredItem;

        [Header("Inventory")]
        public bool inventoryItem;
        public string collectMessage;
    }
}