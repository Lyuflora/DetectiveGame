using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cam;
    private Canvas idCanvas;
    [SerializeField] private TMP_Text idText;
    void Start()
    {
        cam = Camera.main.transform;
        idCanvas = GetComponentInChildren<Canvas>();
    }

    void LateUpdate()
    {
        if(idCanvas!=null)
            idCanvas.transform.LookAt(transform.position + cam.forward);
    }

    public void SetIdText(int id)
    {
        if(idText != null)
            idText.text = id.ToString();
    }
}
