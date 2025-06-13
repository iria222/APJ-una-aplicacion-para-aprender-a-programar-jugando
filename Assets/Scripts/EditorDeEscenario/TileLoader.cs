using UnityEngine;
using UnityEngine.Tilemaps;

public class TileLoader : PlaceableLoader
{
    [SerializeField] private PlaceableCategory category;

    [SerializeField] private PlaceableManager placeableManager;

    public override void LoadDialog()
    {
        placeableManager.SetSelectedObject(null);
        base.LoadDialog();
    }

    /*
     * @param   sprite  sprite del tile
     * @return          tile creada
     */
    public TileBase CreateTile(Sprite sprite)
    {
        Tile tile = ScriptableObject.CreateInstance(typeof(Tile)) as Tile;
        tile.sprite = sprite;
        return tile;
        
    }

    public ObjectTile CreateObjectTile(Sprite sprite)
    {
        ObjectTile tile = ScriptableObject.CreateInstance(typeof (ObjectTile)) as ObjectTile;
        tile.SetSprite(sprite);
        return tile;
    }

    /*
     * Asigna los valores necesarios al boton
     * @param   sprite  sprite del boton
     * @param   gameObject  boton que editar
     */
    public override void EditCreatedButton(Sprite sprite, GameObject gameObject)
    {
        PlaceableButton placeableButton = gameObject.GetComponent<PlaceableButton>();
        TileBase tile;
        if(category.Equals(PlaceableCategory.Obstacle) || category.Equals(PlaceableCategory.Goal))
        {
            tile = CreateObjectTile(sprite);

        }
        else
        {
            tile = CreateTile(sprite);
        }
        placeableButton.CreateBuildingObject(category, tile, new Vector2Int(1, 1));

        placeableButton.SetImage(sprite);
        placeableButton.SetBuildableManager(placeableManager);
    }
}
