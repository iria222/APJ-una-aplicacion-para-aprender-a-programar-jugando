using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

/*
 * Clase que describe el comportamiento de los botones de edicion de escenario
 */

public class PlaceableButton : MonoBehaviour
{
    [SerializeField] private PlaceableObjectSO placeableObjectSO;

    [SerializeField] private PlaceableManager placeableManager;

    private PlaceableObject placeable;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        if(placeableObjectSO != null)
        {
            CreateBuildingObject(placeableObjectSO.GetCategory(), placeableObjectSO.GetTileBase(), placeableObjectSO.GetTileSize());
        }
    }


    public void StartObjectPlacement()
    {
        placeableManager.SetSelectedObject(placeable);
        StartCoroutine(placeableManager.UpdateGridPosition());
    }

    private void OnDisable()
    {
        placeableManager.SetSelectedObject(null);
    }

    public void CreateBuildingObject(PlaceableCategory category, TileBase tileBase, Vector2Int tileSize)
    {
        
        placeable = new PlaceableObject(category, tileBase, tileSize);
        
    }

    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void SetBuildableManager(PlaceableManager buildableManager)
    {
        this.placeableManager = buildableManager;
    }
}
