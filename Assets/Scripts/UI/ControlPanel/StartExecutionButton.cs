using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Bot�n que inicia la ejecuci�n del programa creado
 */
public class StartExecutionButton : ExecutionButton
{

    // Start is called before the first frame update
    void Start()
    {
        this.SetCanBePressed(true);
    }

    private void OnEnable()
    {
        this.SetCanBePressed(true) ;
    }

    public void OnStopButtonPressed()
    {
        this.SetCanBePressed(true);
    }
}
