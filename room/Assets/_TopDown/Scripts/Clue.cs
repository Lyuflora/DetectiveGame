using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dec
{
[CreateAssetMenu(fileName = "NewClue", menuName = "Dec/Clue", order = 1)]
public class Clue : ScriptableObject
{
        public ClueType type;
        public int clueId;
        public string title;
        public bool available = false;
        public Sprite icon;
        public string description;

        public bool linkNode = false;
}
}