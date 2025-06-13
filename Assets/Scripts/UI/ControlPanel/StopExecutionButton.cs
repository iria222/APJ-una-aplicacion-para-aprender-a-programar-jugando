using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Botón que detiene la ejecución del programa creado
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
