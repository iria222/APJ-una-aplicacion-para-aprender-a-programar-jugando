using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * Clase que maneja el comportamiento de los recipientes en los que colocar eventos
 */

public class EventDrop : Drop
{


    public override void OnDrop(PointerEventData eventData)
    {
        GameObject selected = selectedCommand.GetSelectedCommand();
        if (selected == null)
            return;
        if (this.transform.childCount>1||!selected.CompareTag("Evento")) 
        {
            selected.transform.GetComponent<CommandDrag>().SetOriginalParent();
            selectedCommand.SetSelectedCommand(null);
            return; 
        }

        base.OnDrop(eventData);
        
    }



    public override void OnPointerMove(PointerEventData eventData)
    {
        GameObject selected = selectedCommand.GetSelectedCommand();

        if(selected == null ||this.transform.childCount>1|| !isInside || !selected.CompareTag("Evento"))
        {
            placeHolder.SetActive(false);
            return;
        }
        placeHolder.SetActive(true);

        RectTransform rectTransform = (RectTransform)selectedCommand.GetSelectedCommand().transform;

        ModifyPlaceHolder(rectTransform);
    }

}
