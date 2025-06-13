using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Cuenta el número de comandos en el escritorio que formen programas (están dentro de Contenedor comenzar)
 */

public class CommandCounter : MonoBehaviour
{
    [SerializeField] private GameEvent changeCommandCounterEvent;
    
    public bool IsContainerComenzar(GameObject command)
    {
        return command.CompareTag("Contenedor") && command.name.Contains("Comenzar");
    }

    public bool IsATypeOfCommand(GameObject command)
    {
        return command.CompareTag("Comando") || command.CompareTag("Contenedor") || command.CompareTag("Evento");
    }

    public void ChangeText()
    {
        changeCommandCounterEvent.RaiseEvent(this.gameObject, GetDesktopCommandCount());
    }

    /*
     * Devuelve el número de comandos que formen programas en el escritorio
     * @return  número de comandos
     */
    public int GetDesktopCommandCount()
    {
        int count = 0;
        int childCount = this.transform.childCount;

        if(childCount > 0)
        {
            for(int i = 0; i < childCount; i++)
            {
                GameObject childCommand = this.transform.GetChild(i).gameObject;
                if(IsContainerComenzar(childCommand))
                {
                    count += GetCommandInContainerCount(childCommand);
                }
            }
        }

        return count;
    }

    /*
     * Devuelve el número de comandos en un contenedor
     * @param   command     contenedor que contiene los comandos
     * @return  número de comandos en el contenedor
     */
    public int GetCommandInContainerCount(GameObject command)
    {
        int count = 0;
        if (command.CompareTag("PlaceHolder"))
        {
            return count;
        }

        if (IsATypeOfCommand(command))
        {
            count = 1;
            if ((command.CompareTag("Comando") && !command.name.Contains("EsperarHasta")) || command.CompareTag("Evento"))
            {
                return count;
            }
        }

        int containerChildCount = command.transform.childCount;

        for(int i = 0;i < containerChildCount;i++)
        {
            count += GetCommandInContainerCount(command.transform.GetChild(i).gameObject);
        }
        return count;
    }
}
