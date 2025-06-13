using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 * Clase que describe el comportamiento del contenedor Comenzar
 */

public class ContainerComenzar : Container
{
    private static int programsInExecution;

    [SerializeField] private GameEvent executionFinishedEvent;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        programsInExecution = 0;
    }

    public void SetProgramsInExecution(int programs)
    {
        programsInExecution = programs;
    }

    public void StartOnClick(GameObject sender, object data)
    {
        StartCoroutine(StartExecution());
    }

    public override IEnumerator StartExecution()
    {
        programsInExecution++;
        List<GameObject> commandList = GetCommands(containerPanel.transform);
        yield return(StartCoroutine(ExecuteChildCommands(commandList)));
        programsInExecution--;
        if(programsInExecution == 0)
        {
            yield return new WaitForSeconds(0.2f);
            //Se lanza este evento para indicar que la ejecución ha terminado y se puede comprobar si el robot alcanzó la meta
            executionFinishedEvent.RaiseEvent(this.gameObject, null);
        }
    }


}
