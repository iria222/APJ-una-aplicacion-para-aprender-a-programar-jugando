using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Clase que desactiva un panel si se hace clic fuera de el
 */
public class ClosePanel : MonoBehaviour
{
    private EventSystem eventSystem;

    // Start is called before the first frame update
    private void Awake()
    {
        eventSystem = FindAnyObjectByType<EventSystem>();
    }

    private void OnEnable()
    {
        eventSystem.SetSelectedGameObject(this.gameObject);
    }

    public void Desactivate()
    {
        StartCoroutine(DesactivateInNextFrame());
    }

    IEnumerator DesactivateInNextFrame()
    {
        yield return null;
        bool childIsSelected;

        if (eventSystem.currentSelectedGameObject != null)
        {
            childIsSelected = eventSystem.currentSelectedGameObject.transform.IsChildOf(this.transform);
        }
        else
        {
            childIsSelected = false;
        }

        if (!childIsSelected)
        {

            this.gameObject.SetActive(false);
        }
    }

}
