using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Clase que describe el bot�n de una pesta�a de comandos
 */
public class TabButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TabGroup tabGroup;
    [SerializeField] private GameObject page;

    [SerializeField] private int siblingOrder;

    private void OnEnable()
    {
        this.gameObject.transform.SetSiblingIndex(siblingOrder);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    /*
     *@return   pagina asociada a esta pesta�a
     */
    public GameObject GetPage()
    {
        return page;
    }

}
