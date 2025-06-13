using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * Clase que describe el comando Establecer Posicion
 */
public class CommandEstablecerPos : Command
{
    private int maxX = 7;
    private int maxY = 5;
    private int differenceX = 1;
    private int differenceY = -4;

    private int newXPosition;
    private int newYPosition;

    [Header("Input Fields")]
    [SerializeField] private TMP_InputField inputFieldX;
    [SerializeField] private TMP_InputField inputFieldY;
    // Start is called before the first frame update
    void Start()
    {
        newXPosition = differenceX;
        newYPosition = differenceY;
    }

    /*
     * Cambia manualmente la posición del robot
     * Usado para cambiar la posición inicial en el modo edición
     */
    public void ChangePosition()
    {
        StartCoroutine(ExecuteCommand());
    }

    public override IEnumerator ExecuteCommand()
    {
        Vector3 newPosition = new Vector3(newXPosition, newYPosition, 0);
        onExecution.RaiseEvent(this.gameObject, newPosition);
        yield return new WaitForSeconds(0.4f);
    }

    public void SetNewXPosition()
    {
        if (string.IsNullOrEmpty(inputFieldX.text))
        {
            inputFieldX.text = 0.ToString();
        }
        else if(int.Parse(inputFieldX.text) > maxX)
        {
            inputFieldX.text = maxX.ToString();
        }
        newXPosition = int.Parse(inputFieldX.text) + differenceX;
    }

    public void SetNewYPosition()
    {
        
        if (string.IsNullOrEmpty(inputFieldY.text))
        {
            inputFieldY.text = 0.ToString();
        }
        else if (int.Parse(inputFieldY.text) > maxY)
        {
            inputFieldY.text = maxY.ToString();
        }

        newYPosition = int.Parse(inputFieldY.text) + differenceY;

    }

}
