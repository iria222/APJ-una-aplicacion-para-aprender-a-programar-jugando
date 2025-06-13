using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Clase que describe el comportamiento del comando Esperar hasta
 */
public class CommandEsperarHasta : Command
{
    
    [Header("")]
    [SerializeField] private GameObject panel;

   

    public override IEnumerator ExecuteCommand()
    {
        Event eventInCommand = GetEvent(panel.transform);
        
        if (eventInCommand != null)
        {
            yield return StartCoroutine(WaitForEvent(eventInCommand));
        }
    }

    /*
     * @param   eventInCommand  evento por el que hay que esperar
     */
    IEnumerator WaitForEvent(Event eventInCommand)
    {

        while(!eventInCommand.IsEventHappening())
        {
            if (GetStopCoroutine())
            {
                yield break;
            }
            yield return null;
        }

    }

    /*
     * Obtiene el evento por el que esperar
     * @parent  panel en el que se coloca el evento
     */
    public Event GetEvent(Transform eventContainer)
    {
        foreach (Transform child in eventContainer)
        {
            if (!child.CompareTag("PlaceHolder"))
            {
                return child.gameObject.GetComponent<Event>();
            }
        }
        return null;
    }

    

}
