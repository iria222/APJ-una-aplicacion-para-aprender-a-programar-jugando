using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Drop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler, IPointerMoveHandler
{

    [SerializeField] protected SelectedObject selectedCommand;
    [SerializeField] protected GameObject placeHolder;
    protected bool isInside;
    protected IndexManager indexManager;
    // Start is called before the first frame update
    void Start()
    {
        indexManager = new IndexManager(selectedCommand);
        isInside = false;
        placeHolder.SetActive(false);
    }

    /*
     * Modifica el tamaño del placeHolder
     * @param   rectTransform   rect transform del objeto del que obtener el nuevo tamaño
     */
    public void ModifyPlaceHolder(RectTransform rectTransform)
    {
        float height = rectTransform.rect.height;
        float width = rectTransform.rect.width;

        placeHolder.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        isInside = true;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        isInside = false;
        placeHolder.SetActive(false);
        
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        GameObject selected = selectedCommand.GetSelectedCommand();

        selected.transform.SetParent(this.transform);

        int index = selectedCommand.GetIndex();
        indexManager.AssignSiblingIndex(selected, this.transform, index);

        placeHolder.SetActive(false);
        selectedCommand.SetSelectedCommand(null);
        
    }


    public abstract void OnPointerMove(PointerEventData eventData);
}
