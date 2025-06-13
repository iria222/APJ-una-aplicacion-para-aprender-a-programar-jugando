using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Clase que representa el borde de los comandos
 */
public class CommandBorder : MonoBehaviour, IPointerEnterHandler
{
    private IndexManager indexManager;

    [SerializeField] private SelectedObject selectedCommand;
    [SerializeField] private GameObject father;

    

    // Start is called before the first frame update
    void Start()
    {
        indexManager = new IndexManager(selectedCommand);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        int index = father.transform.GetSiblingIndex();
        if (this.CompareTag("BordeInf"))
        {
            index++;
        }
        indexManager.AssignTemporalIndex(index);
    }


}
