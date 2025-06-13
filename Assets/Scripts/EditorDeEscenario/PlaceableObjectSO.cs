using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
 * Describe los objetos con los que editar el escenario
 */

public enum PlaceableCategory
{
    Floor,
    Obstacle,
    Goal,
    Eraser
}

[CreateAssetMenu (fileName = "Placeable", menuName ="PlaceableObjects/Create Placeable")]
public class PlaceableObjectSO : ScriptableObject
{
    [SerializeField] private PlaceableCategory category;
    [SerializeField] private TileBase tileBase;
    [SerializeField] private Vector2Int tileSize;
    
    public PlaceableCategory GetCategory()
    {
        return category;
    }

    public TileBase GetTileBase()
    {
        return tileBase;
    }

    public Vector2Int GetTileSize()
    {
        return tileSize;
    }
}
