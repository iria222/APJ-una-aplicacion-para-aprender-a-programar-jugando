using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
 * Clase que describe los objetos con los que editar el escenario
 */

public class PlaceableObject
{

    private PlaceableCategory category;
    private TileBase tileBase;
    private Vector2Int tileSize;

    public PlaceableObject(PlaceableCategory category, TileBase tileBase, Vector2Int tileSize)
    {
        this.category = category;
        this.tileBase = tileBase;
        this.tileSize = tileSize;
    }

    public PlaceableCategory GetCategory() { return category; }
    public TileBase GetTileBase() {  return tileBase; }

    public Vector2Int GetTileSize() {  return tileSize; }
}
