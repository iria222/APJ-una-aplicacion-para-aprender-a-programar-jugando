using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * Clase que describe el evento tecla pulsada
 */
public class EventTeclaPulsada : Event
{
    private string selectedKey;
    [SerializeField] private TMP_Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
        selectedKey = "cualquiera";
    }

    public void SetSelectedKey()
    {
        selectedKey = dropdown.options[dropdown.value].text;
        selectedKey = selectedKey.ToLower();

        switch (selectedKey)
        {
            case "espacio":
                selectedKey = "space";
                break;
            case "flecha arriba":
                selectedKey = "up";
                break;
            case "flecha abajo":
                selectedKey = "down";
                break;
            case "flecha izquierda":
                selectedKey = "left";
                break;
            case "flecha derecha":
                selectedKey = "right";
                break;
        }
    }


    public override bool IsEventHappening()
    {
        bool pressed = false;
        
        if (selectedKey.Equals("cualquiera"))
        {
            if (Input.anyKey && !IsMousePressed())
            {
                pressed = true;
            }
        }
        else if (Input.GetKey(selectedKey))
        {
            pressed = true;
        }
        return pressed;        
    }

    public bool IsMousePressed()
    {
        return Input.GetMouseButton(0)||Input.GetMouseButton(1) || Input.GetMouseButton(2);
    }
}
