using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class DialogueCharacter
{
    [SerializeField] private string name;
    [SerializeField] private Sprite icon;

    public string GetName() {  return name; }

    public Sprite GetIcon() { return icon; }
}

[System.Serializable]
public class DialogueLine
{
    [SerializeField] private DialogueCharacter character;
    [TextArea(1,10)]
    [SerializeField] private string line;

    [Header("Initial GameEvent")] //GameEvent que iniciar al comenzar la línea de diálogo
    [SerializeField] private GameEvent gameEvent;
    [SerializeField] private string receiverName;

    [SerializeField] private TMP_SpriteAsset spriteAsset;

    [SerializeField] private AudioClip audioClip;
    public string GetLine() {  return line; }
    public DialogueCharacter GetCharacter() { return character;}

    public GameEvent GetGameEvent() {  return gameEvent;}

    public string GetReceiverName() {  return receiverName;}

    public TMP_SpriteAsset GetSpriteAsset() { return spriteAsset;}

    public AudioClip GetAudioClip() {  return audioClip;}

}

[CreateAssetMenu (fileName = "MyDialogue", menuName ="Dialogue/Create dialogue")]
public class Dialogue : ScriptableObject
{
    [SerializeField] private List<DialogueLine> lines = new List<DialogueLine>();
    [SerializeField] private bool playOnStart;
    public List<DialogueLine> GetDialogueLines() { return lines; }
    public bool PlayOnStart() { return playOnStart; }
}
