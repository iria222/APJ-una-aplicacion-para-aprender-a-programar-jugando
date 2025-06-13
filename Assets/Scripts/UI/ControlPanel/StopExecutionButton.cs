using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Bot�n que detiene la ejecuci�n del programa creado
 */
public class StopExecutionButton : ExecutionButton
{


    // Start is called before the first frame update
    void Start()
    {
        this.SetCanBePressed(false);
        
    }

    private void OnEnable()
    {
        this.SetCanBePressed(false );
    }

    public void OnStartButtonPressed()
    {
        this.SetCanBePressed(true);
    }

}
