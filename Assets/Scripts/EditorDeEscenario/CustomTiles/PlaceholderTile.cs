using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
 * Tiles placeholder para objetos que ocupan más de un cuadro del grid
 * Señalan al espacio que contiene realmente el tile del objeto
 */
public class PlaceholderTile : Tile
{

    private Vector3Int originPosition;
    private Vector3Int thisGridPosition;

    private void Awake()
    {
        this.colliderType = ColliderType.None;
    }

    public void SetOriginPosition(Vector3Int originPosition)
    {
        this.originPosition = originPosition;
    }

    public Vector3Int GetOriginPosition()
    {
        return this.originPosition;
    }

    public void SetThisGridPosition(Vector3Int thisGridPosition)
    {
        this.thisGridPosition = thisGridPosition;
    }

    public Vector3Int GetThisGridPosition()
    {
        return this.thisGridPosition;
    }

}

