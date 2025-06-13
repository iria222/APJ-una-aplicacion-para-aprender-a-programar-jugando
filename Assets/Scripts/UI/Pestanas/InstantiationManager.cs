using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Clase que se encarga de instanciar el comando requerido al pulsar en el boton de una pagina
 */
public class InstantiationManager : MonoBehaviour
{

    [SerializeField] private Canvas canvasEscritorio;
    [SerializeField] private GameObject scrollContent;

    [SerializeField] private SelectedObject selectedCommand;




    /*
     * Instancia el comando indicado por el ID
     * @param   comandID            ID del boton que indica el comando a instanciar
     * @param   eventData           eventData necesario para arrastrar el comando creado
     * @param   instantiateButton   boton pulsado para instanciar el comando
     */
    public void InstantiateCommand(GameObject commandPrefab, PointerEventData eventData, Vector3 instantiatePosition)
    {
            GameObject command = Instantiate(commandPrefab, instantiatePosition, Quaternion.identity, canvasEscritorio.transform);

            SetUpCommand(canvasEscritorio, scrollContent, command);

            //Permite arrastrar el comando creado sin tener que soltar y clicar otra vez
            eventData.pointerPress = command;
            eventData.pointerDrag = command;
        
    }

    /*
     * Asigna los valores necesarios al comando creado
     * @param   canvasEscritorio    Canvas en el que se mueve el comando
     * @param   scrollContent       Panel en el que se puede soltar el comando
     * @param   command             Comando creado
     */
    public void SetUpCommand(Canvas canvasEscritorio, GameObject scrollContent, GameObject command)
    {
        CommandDrag drag = command.GetComponent<CommandDrag>();
        drag.SetCanvasEscritorio(canvasEscritorio);
        drag.SetScrollContent(scrollContent);
        
    }

}
