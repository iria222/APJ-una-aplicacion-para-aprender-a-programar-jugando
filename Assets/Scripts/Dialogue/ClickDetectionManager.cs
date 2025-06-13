using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickDetectionManager : MonoBehaviour, IPointerClickHandler
{
    private DialogueManager dialogueManager;

    private void Awake()
    {
        dialogueManager = transform.parent.GetComponent<DialogueManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        dialogueManager.SetFinishImmediately(true);
    }

}
