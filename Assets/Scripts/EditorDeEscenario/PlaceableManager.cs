using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
 * Clase que maneja la edicion del escenario
 */

public class PlaceableManager: MonoBehaviour
{
    private static PlaceableObject selectedObject;

    [Header("Tilemaps")]
    [SerializeField] private Tilemap defaultTilemap;
    [SerializeField] private Tilemap floorPreviewTilemap;
    [SerializeField] private Tilemap floorTilemap;
    [SerializeField] private Tilemap obstaclesTilemap;
    [SerializeField] private Tilemap obstaclesPreviewTilemap;
    [SerializeField] private Tilemap eraserPreviewTilemap;
    [SerializeField] private Tilemap goalsTilemap;

    private TileBase tileBase;
    private Vector2Int tileSize;

    private Vector3Int currentGridPosition;
    private Vector3Int lastGridPosition;

    private Camera cam;

    private Vector3Int robotPosition;

    private void Awake()
    {
        cam = Camera.main;
        
    }

    private void Start()
    {
        selectedObject = null;
        tileBase = null;
        tileSize = new Vector2Int(0, 0);
        currentGridPosition = new Vector3Int(0, 0, 0);
        lastGridPosition = new Vector3Int(0, 0, 0);
        robotPosition = defaultTilemap.WorldToCell(new Vector3(1, -4, 0));
    }


    /*
     * Indica la posición actual del robot para no poder colocar objetos en ella
     * @param   sender  Objeto que envía el mensaje
     * @param   data    Posición del robot
     */
    public void SetRobotPosition(GameObject sender, object data)
    {
        if(data is Vector3)
        {
            Vector3 position = (Vector3)data;
            robotPosition = defaultTilemap.WorldToCell(position);
        }
    }

    /*
     * Indica la Tile seleccionada para colocar
     * @param   obj  Tile seleccionada
     */
    public void SetSelectedObject(PlaceableObject obj)
    {
        selectedObject = obj;

        if(selectedObject != null)
        {
            TileBase auxTile = selectedObject.GetTileBase();
            if(auxTile is ObjectTile)
            {
                //Se crea un clon para que los ObjectTile no compartan la lista de placeholders
                ObjectTile objectTile = (ObjectTile)auxTile;
                auxTile = objectTile.GetEmptyClone();

            }
            tileBase = auxTile;
            tileSize = selectedObject.GetTileSize();
        }
        else
        {
            tileBase = null;
            tileSize = new Vector2Int(0, 0);
        }
        UpdatePreview();
    }

    /*
     * Establece la posicion actual y anterior del grid sobre la que está el ratón
     *
     */
    public IEnumerator UpdateGridPosition()
    {
        List<Tilemap> tilemapToErase = new List<Tilemap>();
        Vector3Int gridPos;

        while(selectedObject != null)
        {
            gridPos = GetGridPosition();

            if(gridPos != currentGridPosition)
            {
                lastGridPosition = currentGridPosition;
                currentGridPosition = gridPos;

                UpdatePreview();
            }

            CheckMouseInput();

            yield return null;
        }
    }

    /*
     * Comprueba si se ha pulsado el ratón y actua en consecuencia
     */
    public void CheckMouseInput()
    {

        //Click izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedObject.GetCategory().Equals(PlaceableCategory.Eraser))
            {
                if(GetErasingTilemap().Equals(floorTilemap))
                {
                    EraseFloorTile(GetErasingTilemap());
                }
                else
                {
                    EraseObjectTile(GetErasingTilemap());

                }
            }
            else if (selectedObject.GetCategory().Equals(PlaceableCategory.Floor))
            {
                DrawFloorTile(GetDrawingTilemap());
            }
            //Si es obstaculo u objetivo
            else
            {
                DrawObjectTile(GetDrawingTilemap());
            }
        }

        //Click derecho
        if (Input.GetMouseButtonDown(1))
        {
            if(tileBase is ObjectTile)
            {
                ObjectTile objectTile = (ObjectTile)tileBase;
                objectTile.color = Color.white;
                objectTile.DestroyTile();
            }
            //Se deselecciona el Tile
            SetSelectedObject(null);

        }
    }
    
    /*
     * Obtiene la posicion de la casilla del grid bajo el raton
     * @return  posicion de la casilla bajo el raton
     */
    public Vector3Int GetGridPosition()
    {
        if (cam == null) { return new Vector3Int(0,0,0); }
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPos = defaultTilemap.WorldToCell(mousePos);
        return gridPos;
    }


    public void UpdatePreview()
    {
        List<Tilemap> previewList = new List<Tilemap>();

        if (selectedObject == null) //Si no hay ningun objeto seleccionado se guardan todos los PreviewTilemap para borrar
        {
            previewList.Add(floorPreviewTilemap);
            previewList.Add(obstaclesPreviewTilemap);
            previewList.Add(eraserPreviewTilemap);
        }
        else
        {
            previewList.Add(GetPreviewTilemap());
        }

        foreach (Tilemap tilemap in previewList)
        {
            if(tilemap == null)
            {
                continue;
            }

            tilemap.SetTile(lastGridPosition, null);

            if (defaultTilemap.HasTile(GetGridPosition()))
            {
                if(tileBase is ObjectTile)
                {
                    ObjectTile objectTile = (ObjectTile)tileBase;
                    if (!CanDrawObject())
                    {
                        objectTile.color = Color.red;
                    }
                    else
                    {
                        objectTile.color = Color.white;
                    }
                }

                tilemap.SetTile(currentGridPosition, tileBase);
            }
        }
    }

    /*
     * Coloca la Tile suelo seleccionada en un Tilemap
     */
    public void DrawFloorTile(Tilemap tilemap)
    {
        if (defaultTilemap.HasTile(GetGridPosition()))
        {

            tilemap.SetTile(currentGridPosition, tileBase);
        }
    }

    /*
     * Borra la Tile suelo de un Tilemap
     * @param   Tilemap del que borrar la Tile
     */
    public void EraseFloorTile(Tilemap tilemap)
    {
        tilemap.SetTile(currentGridPosition, null);
    }

    /*
     * @return  Tilemap en el que colocar las Tiles
     */
    public Tilemap GetDrawingTilemap()
    {
        switch (selectedObject.GetCategory())
        {
            case PlaceableCategory.Floor:
                return floorTilemap;
            case PlaceableCategory.Obstacle:
                return obstaclesTilemap;
            default:
                return goalsTilemap;
        }
    }

    /*
     * @return  Tilemap del que borrar las Tiles
     */
    public Tilemap GetErasingTilemap()
    {
        if (goalsTilemap.HasTile(currentGridPosition))
        {
            return goalsTilemap;
        }
        else if (obstaclesTilemap.HasTile(currentGridPosition))
        {
            return obstaclesTilemap;
        }
        else
        {
            return floorTilemap;
        }
    }

    /*
     * @return Tilemap en el que colocar la preview de la Tile seleccionada
     */
    public Tilemap GetPreviewTilemap()
    {
        switch (selectedObject.GetCategory())
        {
            case PlaceableCategory.Floor:
                return floorPreviewTilemap;
            case PlaceableCategory.Obstacle:
                return obstaclesPreviewTilemap;
            case PlaceableCategory.Goal:
                return obstaclesPreviewTilemap;
            default: 
                return eraserPreviewTilemap;
        }
    }

    /*
     * Coloca los objetos en el Tilemap correspondiente
     */
    public void DrawObjectTile(Tilemap tilemap)
    {
        if (CanDrawObject())
        {
            List<PlaceholderTile> placeholderList = new List<PlaceholderTile>();
            ObjectTile objectTile = (ObjectTile)tileBase;

            //Coloco los placeholders
            DrawPlaceholders(tilemap, objectTile);

            //Coloco el ObjectTile
            tilemap.SetTile(currentGridPosition, objectTile);

            //Se crea una nueva instancia que dibujar a continuacion
            ObjectTile auxTile = (ObjectTile)tileBase;
            tileBase = auxTile.GetEmptyClone();

        }
    }

    /*
     * Coloca los placeholders del objeto seleccionado
     * @param   tilemap     tilemap en el que colocar los placeholders
     * @param   objectTile  tile a la que corresponden los placeholders
     */
    private void DrawPlaceholders(Tilemap tilemap, ObjectTile objectTile)
    {

        for (int i = 0; i < tileSize.x; i++)
        {
            for (int j = 0; j < tileSize.y; j++)
            {
                Vector3Int newPos = new Vector3Int(i, j, 0);

                PlaceholderTile placeholderTile = ScriptableObject.CreateInstance<PlaceholderTile>();
                placeholderTile.SetThisGridPosition(currentGridPosition + newPos);
                placeholderTile.SetOriginPosition(currentGridPosition);
                tilemap.SetTile(currentGridPosition + newPos, placeholderTile);
                
                objectTile.AddPlaceholder(placeholderTile);

            }
        }
    }

    /*
     * Comprueba si la posición actual está ocupada para ver si se puede colocar la ObjectTile
     * @return  Devuelve si se puede colocar o no la Tile seleccionada en la posición actual
     */
    public bool CanDrawObject()
    {
        int xSize = tileSize.x;
        int ySize = tileSize.y;
        for(int i = 0; i < xSize; i++)
        {
            for (int j = 0;j < ySize; j++)
            {
                Vector3Int newPos = new Vector3Int(i, j, 0);
   
                if(!defaultTilemap.HasTile(currentGridPosition+newPos) || obstaclesTilemap.HasTile(currentGridPosition+newPos) 
                    || goalsTilemap.HasTile(currentGridPosition+newPos) || (currentGridPosition+newPos).Equals(robotPosition))
                {
                    return false;
                }
            }
        }
        return true;
    }

    /*
     * Borra la Tile en la posición actual
     * @param   tilemap     Tilemap del que borrar la Tile
     */
    public void EraseObjectTile(Tilemap tilemap)
    {

        Vector3Int originPosition;
        TileBase auxTile = tilemap.GetTile(currentGridPosition);

        if (auxTile is PlaceholderTile)
        {
            PlaceholderTile placeholderTile = (PlaceholderTile)auxTile;
            originPosition = placeholderTile.GetOriginPosition();
        }
        else
        {
            originPosition = currentGridPosition;
        }

        ObjectTile originTile = (ObjectTile)tilemap.GetTile(originPosition);

        foreach (PlaceholderTile placeholder in originTile.GetPlaceholderList())
        {
            tilemap.SetTile(placeholder.GetThisGridPosition(), null);
        }
        tilemap.SetTile(originPosition, null);
        originTile.DestroyTile();

        
    }

}
