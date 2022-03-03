using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dec
{
    public interface IClickable
    {
        // �μ�SelectionManager
        void OnLeftClick();
        void OnLeftClickUp();
        void OnRightClickDown();

        void OnHoverEnter();
        void OnHoverExit();
    }
}