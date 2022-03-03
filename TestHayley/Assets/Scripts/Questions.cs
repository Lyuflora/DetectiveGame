using System.Collections.Generic;
using UnityEngine;

namespace Dec
{
    [CreateAssetMenu(fileName = "NewQuestion", menuName = "Dec/Question", order = 5)]

    public class Questions: ScriptableObject
    {
        [TextArea]
        public string question;
        public string title;
        public List<Item> m_CorrectChoices;
        public List<Item> m_DragableChoices;

        public Item m_AnswerItem;
        public bool isFound = false;
        [TextArea][Tooltip("开发者做笔记用")]
        public string description;
        public Sprite icon;
    }
}