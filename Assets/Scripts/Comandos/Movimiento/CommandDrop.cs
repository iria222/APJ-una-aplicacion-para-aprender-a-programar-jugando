using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

/*
 * Clase que maneja el comportamiento de los recipientes en los que colocar los comandos
 */
public class CommandDrop : Drop
{

    private bool draggingInChild;


    public bool IsContainerComenzar(GameObject selected)
    {
        return selected.name.Contains("ContainerComenzar");
    }

    /*
     * Coloca el comando seleccionado en el contenedor cuando se suelta dentro
     */
    public override void OnDrop(PointerEventData eventData)
    {
        GameObject selected = selectedCommand.GetSelectedCommand();

        
        if (selected == null) {  return; }
        //El contenedor Comenzar no se puede colocar dentro de otros
        else if(IsContainerComenzar(selected) || selected.CompareTag("Evento")) 
        {
            selected.GetComponent<CommandDrag>().SetOriginalParent();
            selectedCommand.SetSelectedCommand(null);
            return; 
        }

        base.OnDrop(eventData);

        ManageDraggingInChild(false);
    }


    /*
     * Indica si se está arrastrando un comando dentro de un contenedor hijo
     */
    public void SetDraggingInChild(bool inChild) {  this.draggingInChild = inChild; }
    
    /*
     * Le indica a todos los contenedores padre si se está o no arrastrando un comando dentro de su hijo
     * para que activen o no el placeHolder
     * @param   isInChild    indica si se está o no arrastrando un comando dentro
     */
    public void ManageDraggingInChild(bool isInChild)
    {
        Transform fatherContainer = this.transform.parent.parent;
        bool isChildOfContainer = fatherContainer.CompareTag("panelContenedor");

        if (isChildOfContainer)
        {
            fatherContainer.GetComponent<CommandDrop>().SetDraggingInChild(isInChild);

            fatherContainer.GetComponent<CommandDrop>().ManageDraggingInChild(isInChild);
        }
    }
    /*
     * @parameter   sel     comando a comprobar
     * @return              si el comando es válido o no
     */
    public bool IsValidCommand(GameObject sel)
    {
        return sel != null && sel != this.gameObject;
    }

    /*
     * Describe el comportamiento al entrar en el contenedor
     */
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        GameObject selected = selectedCommand.GetSelectedCommand();

        if (IsValidCommand(selected))
        {
            ManageDraggingInChild(true);
        }
    }

    /*
     * Describe el comportamiento al salir del contenedor
     */
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);

        GameObject selected = selectedCommand.GetSelectedCommand();

        Transform fatherContainer = this.transform.parent.parent;
        bool isChildContainer = fatherContainer.CompareTag("panelContenedor");

        if (IsValidCommand(selected) && isChildContainer)
        {
            fatherContainer.GetComponent<CommandDrop>().SetDraggingInChild(false);


        }
    }

    /*
     * Reorganiza el layout group del contenedor cuando se arrastra un comando por encima
     * insertando el placeholder donde irá dicho comando.
     */
    public override void OnPointerMove(PointerEventData eventData)
    {
        GameObject selected = selectedCommand.GetSelectedCommand();
        
        if(draggingInChild || !isInside || !IsValidCommand(selected) || IsContainerComenzar(selected) || selected.CompareTag("Evento")) 
        {
            placeHolder.SetActive(false);
            return; 
        }

        int index = selectedCommand.GetIndex();

        placeHolder.SetActive(true);
        
        indexManager.AssignSiblingIndex(placeHolder, this.gameObject.transform, index);
        
        RectTransform rectTransform = (RectTransform)selectedCommand.GetSelectedCommand().transform;
        ModifyPlaceHolder(rectTransform);
        
    }
   

}
