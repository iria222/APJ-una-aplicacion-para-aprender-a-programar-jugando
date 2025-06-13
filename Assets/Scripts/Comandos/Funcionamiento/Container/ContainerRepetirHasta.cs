using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerRepetirHasta : Container
{
    [SerializeField] private GameObject panelEvent;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    
    public override IEnumerator StartExecution()
    {
        Event eventInContainer = GetEvent(panelEvent.transform);
        List<GameObject> list = GetCommands(containerPanel.transform);

        if (eventInContainer != null)
        {
            while (!eventInContainer.IsEventHappening())
            {
                if (GetStopCoroutine())
                {
                    yield break;
                }
                yield return StartCoroutine(ExecuteChildCommands(list));
            }
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
