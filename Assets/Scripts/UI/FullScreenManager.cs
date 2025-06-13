using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Permite cambiar entre pantalla completa y ventana
 */
public class FullScreenManager : MonoBehaviour
{
    [SerializeField] private Toggle toggle;


    // Start is called before the first frame update
    void Start()
    {
        if(Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }
    }

    public void ChangeScreenSize()
    {
        Screen.fullScreen = toggle.isOn;
        Resolution currentResolution = Screen.currentResolution;

        if (!toggle.isOn )
        {
            int newWidth = currentResolution.width - currentResolution.width / 8;
            int newHeight = newWidth * 9 / 16;
            
            Screen.SetResolution(newWidth, newHeight, false );
        }
        else
        {
            Screen.SetResolution(currentResolution.width, currentResolution.height, true );
        }
    }

    public void Interact()
    {
        if (toggle.isOn)
        {
            toggle.isOn= false;
        }
        else
        {
            toggle.isOn= true;
        }
        ChangeScreenSize();
    }
}
