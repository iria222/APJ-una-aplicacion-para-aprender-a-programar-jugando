using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Clase general de botón de ejecución
 */
public class ExecutionButton : MonoBehaviour
{
    [SerializeField] private GameEvent executionEvent;
    private bool canBePressed;
    private Button executionButton;

    private void Awake()
    {
        executionButton = GetComponent<Button>();
    }

    public void OnButtonPressed()
    {
        if (canBePressed)
        {
            executionEvent.RaiseEvent(this.gameObject, null);
            SetCanBePressed(false);
        }
    }


    public void SetCanBePressed(bool canBePressed)
    {
        this.canBePressed = canBePressed;
        executionButton.interactable = canBePressed;
    }
}
