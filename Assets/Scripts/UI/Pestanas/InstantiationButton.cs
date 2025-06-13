using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Clase que describe un boton para instanciar un comando
 */
public class InstantiationButton : MonoBehaviour, IInitializePotentialDragHandler, IDragHandler
{
    [SerializeField] private GameObject commandPrefab;
    [SerializeField] private InstantiationManager manager;
    [SerializeField] private int numberOfInstances =-10;
    private ButtonHighlighter buttonHighlighter;

    private void Awake()
    {
        buttonHighlighter = GetComponent<ButtonHighlighter>();
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        if(numberOfInstances != 0)
        {
            manager.InstantiateCommand(commandPrefab, eventData, this.transform.position);
            numberOfInstances--;
            if(numberOfInstances == 0)
            {
                buttonHighlighter.SetIsActive(false);
            }
        }
    }

    public void AddInstance(GameObject sender, object data)
    {
        if(sender.name.Contains(commandPrefab.name))
        {
            numberOfInstances++;
            buttonHighlighter.SetIsActive(true);
        }
    }

    /*
     * No funciona sin el IDragHandler
     */
    public void OnDrag(PointerEventData eventData) { }

}
