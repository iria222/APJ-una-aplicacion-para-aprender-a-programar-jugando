using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/*
 * Maneja lo relacionado con mostrar el dialogo correspondiente
 */
public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Image characterImage;
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text dialogue;
    [SerializeField] private Button continueButton;

    [Header("Game events")]
    [SerializeField] private GameEvent onEndDialogue;
    [SerializeField] private GameEvent playAudioEvent;
    [SerializeField] private GameEvent stopAudioEvent;


    private Queue<DialogueLine> lineQueue;

    private float typeSpeed = 0.05f;

    private Canvas dialogueCanvas;
    private Animator dialogueAnimator;

    private string hideDialogueTrigger = "HideDialogue";
    private string showDialogueTrigger = "ShowDialogue";

    private bool finishImmediately;

    private GameEvent currentEvent;
    private string lastReceiverName;
    private string currentReceiverName;

    

    private void Awake()
    {
        dialogueCanvas = GetComponent<Canvas>();
        dialogueAnimator = GetComponent<Animator>();
        
    }

    private void Start()
    {
        lastReceiverName = string.Empty;
        currentReceiverName = string.Empty;
        continueButton.enabled = true;
    }

    public void SetFinishImmediately(bool finishImmediately)
    {
        this.finishImmediately = finishImmediately;
    }

    public void StartDialogue(GameObject sender, object data)
    {
        if(data is Dialogue)
        {
            continueButton.enabled = true;
            Dialogue dialogue = (Dialogue)data;
        
            lineQueue = new Queue<DialogueLine>();

            dialogueCanvas.enabled = true;
            PlayAnimation(showDialogueTrigger);

            foreach(DialogueLine line in dialogue.GetDialogueLines())
            {
                lineQueue.Enqueue(line);
            }
            DisplayNextLine();
        }
    }

    public void DisplayNextLine()
    {

        if (lineQueue.Count == 0)
        {
            continueButton.enabled = false;
            EndDialogue();
            return;
        }

        StopAudioClip();

        SetFinishImmediately(false );

        DialogueLine line = lineQueue.Dequeue();
        lastReceiverName = currentReceiverName;
        currentReceiverName = line.GetReceiverName();
        dialogue.spriteAsset = line.GetSpriteAsset();
        characterImage.sprite = line.GetCharacter().GetIcon();
        characterName.text = line.GetCharacter().GetName();

        currentEvent = line.GetGameEvent();
        //Indica que la acción activada por el anterior GameEvent debe terminar
        SendGameEvent(lastReceiverName);

        //Envía una nueva notificación
        SendGameEvent(currentReceiverName);

        StopAllCoroutines();
        StartCoroutine(TypeSentence(line));

        
        PlayAudioClip(line.GetAudioClip());
        
    }

    /*
     * @param   name    nombre del objeto destinatario
     */
    private void SendGameEvent(string name)
    {
        if (currentEvent != null)
        {
            currentEvent.RaiseEvent(this.gameObject,  name);
        }
    }

    public IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogue.text = "";
        bool isTag = false;
        string tagText = "";
        foreach(char character in dialogueLine.GetLine().ToCharArray())
        {
            if (character.Equals('<'))
            {
                isTag = true;
            }

            if(isTag)
            {
                tagText += character;
            }
            else
            {
                dialogue.text += character;
                yield return new WaitForSeconds(typeSpeed);
            }

            if(character.Equals('>')) 
            {
                isTag = false;
                dialogue.text += tagText;
                tagText = "";
                yield return new WaitForSeconds(typeSpeed);
            }

            if(finishImmediately)
            {
                dialogue.text = dialogueLine.GetLine();
                finishImmediately = false;
                break;
            }
        }

    }

    public void PlayAudioClip(AudioClip clip)
    {
        if(clip != null)
        {
            playAudioEvent.RaiseEvent(this.gameObject, clip);

        }
    }

    public void StopAudioClip()
    {
        stopAudioEvent.RaiseEvent(this.gameObject, null);
    }

    public void EndDialogue()
    {
        PlayAnimation(hideDialogueTrigger);
        stopAudioEvent.RaiseEvent(this.gameObject, null);
        onEndDialogue.RaiseEvent(this.gameObject, null);
    }

    public void OnEndHideAnimation()
    {
        //ChangeStartColorEvent.RaiseEvent(this.gameObject, null);
        dialogueCanvas.enabled = false;

    }

    /*
     * @param   trigger     trigger que inicia la animacion
     */
    public void PlayAnimation(string trigger)
    {
        dialogueAnimator.SetTrigger(trigger);
    }

}
