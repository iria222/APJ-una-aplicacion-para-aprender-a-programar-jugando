using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
 * Permite arrastrar los comandos con el ratón
 */
public class CommandDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler, IPointerUpHandler
{
    [SerializeField] private Canvas canvasEscritorio;
    [SerializeField] private GameObject scrollContent;
    [SerializeField] private SelectedObject selectedCommand;

    [Header("GameEvents")]
    [SerializeField] private GameEvent changeDesktopSizeEvent;

    private Vector3 offset;

    private CanvasGroup canvasGroup;

    private Camera mainCamera;

    private void Awake()
    {
        canvasGroup=this.gameObject.GetComponent<CanvasGroup>();
    }

    void Start()
    {
        canvasGroup.blocksRaycasts = true;
        mainCamera = Camera.main;

    }

    public void SetCanvasEscritorio(Canvas canvasEscritorio)
    {
        this.canvasEscritorio = canvasEscritorio;
    }

    public void SetScrollContent(GameObject scrollContent)
    {
        this.scrollContent = scrollContent;
    }

    public void SetOriginalParent()
    {
        this.transform.SetParent(scrollContent.transform);
    }

    /*
     * Permite arrastrar el comando
     */
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 worldPosition = GetMousePosition();

        transform.position = new Vector3(worldPosition.x, worldPosition.y, 0) - offset;
    }

    /*
     * @return  posición del cursor
     */
    private Vector2 GetMousePosition()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    
        return mainCamera.ScreenToWorldPoint(mousePosition);
    }


    /*
     * Destruye el comando si se suelta fuera del escritorio
     */
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!selectedCommand.IsDroppable())
        {
            Destroy(this.gameObject);
        }

        BlockRaycast(true);
        changeDesktopSizeEvent.RaiseEvent(this.gameObject, null);
    }



    /*
     * Acciones realizadas al comenzar a arrastrar un comando
     */
    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 worldPosition = GetMousePosition();

        //Diferencia entre el centro del comando y por donde se agarra
        offset = new Vector3(worldPosition.x, worldPosition.y, 0) - gameObject.transform.position;

        selectedCommand.SetSelectedCommand(this.gameObject);

        transform.SetParent(canvasEscritorio.transform);

        BlockRaycast(false);

    }

    
    public void BlockRaycast(bool blockRaycast)
    {
        canvasGroup.blocksRaycasts = blockRaycast;
    }

    /*
     * Si se clica y suelta el boton de instanciar comando sin llegar a arrastrar el comando creado, este se elimina
     */
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!selectedCommand.IsDroppable())
        {
            Destroy(this.gameObject);
        }
        BlockRaycast(true);

    }
}