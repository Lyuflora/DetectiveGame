using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dec
{

    public class ClueTrigger : MonoBehaviour
    {
        public Clue m_Clue;
        
        public void GainClue()
        {
            if(m_Clue.m_Owner == PlayerInteraction.m_Instance.GetCurrentItem())
            {
                Debug.Log("Find Clue" + m_Clue.title);
                TestAPP.m_Instance.m_ClueManager.AddClue(m_Clue);
                UIManager.m_Instance.ShowCluePopup(m_Clue.title, m_Clue.description);
            }
            
        }
    }

    
}