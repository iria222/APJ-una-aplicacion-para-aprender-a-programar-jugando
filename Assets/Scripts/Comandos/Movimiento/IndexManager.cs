using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Clase que controla la asignación de índices para ordenar los comandos dentro de los contenedores
 */
public class IndexManager 
{
    private SelectedObject selectedCommand;

    public IndexManager(SelectedObject selectedCommand)
    {
        this.selectedCommand = selectedCommand;
    }


    /*
     * Asigna un indice temporal al comando seleccionado
     * @param   index   indice del comando sobre el que se esta moviendo el seleccionado
     */
    public void AssignTemporalIndex(int index)
    {
        GameObject selected = selectedCommand.GetSelectedCommand();

        if (selected != null)
        {
            selectedCommand.SetIndex(index);
        }
    }

    /*
     * Asigna un indice a un gameobject
     * @param   command gameObject al que asignar el indice
     * @param   fatherPanel     panel en el que se esta colocando el comando
     * @param   index           indice que asignar a selectedCommand
     */
    public void AssignSiblingIndex(GameObject command, Transform fatherPanel, int index)
    {
        if (index < 0) { index = 0; }
        
        RearrangeIndexes(index, fatherPanel);
        command.transform.SetSiblingIndex(index);

    }

    /*
     * Reasigna los indices de los comandos de un contenedor tras introducir un comando nuevo
     * @param   selectedIndex   indice del nuevo comando introducido
     * @param   fatherPanel     panel en el que se encuentran los comandos a organizar
     */
    public void RearrangeIndexes(int selectedIndex, Transform fatherPanel)
    {
        for (int i = selectedIndex; i < fatherPanel.childCount; i++)
        {
            if (!fatherPanel.GetChild(i).CompareTag("PlaceHolder"))
            {
                fatherPanel.GetChild(i).SetSiblingIndex(i++);
            }
        }
    }
}
