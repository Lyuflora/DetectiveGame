using TMPro;
using UnityEngine;

namespace Dec
{
    public class ClueSlot : MonoBehaviour
    {
        public TMP_Text description;

        public void GenerateClueEntry(Clue clue)
        {
            description.text = clue.description;
        }
    }
}