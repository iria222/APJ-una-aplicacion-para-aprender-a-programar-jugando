
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * Comprueba si el comando está sobre el escritorio o no
 * para saber si se debe eliminar al soltarlo
 */
public class CommandOverDesktop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    [SerializeField] private SelectedObject selectedCommand;
    [SerializeField] private GameObject scrollContent;

    public void OnDrop(PointerEventData eventData)
    {
        if (selectedCommand.GetSelectedCommand() != null)
        {
            //Se suelta el comando en el escritorio
            selectedCommand.GetSelectedCommand().transform.SetParent(scrollContent.transform);
            selectedCommand.SetSelectedCommand(null);
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        selectedCommand.SetDroppable(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {       
        selectedCommand.SetDroppable(false); 
    }


}
