using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dec
{
    [CreateAssetMenu(fileName = "NewClue", menuName = "Dec/Clue", order = 2)]

    public class Clue : ScriptableObject
    {
        public string title;
        [Tooltip("Currently no use.")]
        public int m_ClueId;    // no use
        public Item m_Owner;
        public bool isFound = false;
        [TextArea]
        public string description;
        public Sprite icon;
    }
}