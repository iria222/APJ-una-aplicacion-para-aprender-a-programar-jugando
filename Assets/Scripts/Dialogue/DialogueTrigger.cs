using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private GameEvent startDialogue;
    [SerializeField] private Dialogue dialogue;


    private void Start()
    {
        if (dialogue.PlayOnStart())
        {
            InitiateDialogue();
        }
    }

    public void InitiateDialogue()
    {
        startDialogue.RaiseEvent(this.gameObject, dialogue);
    }

}
