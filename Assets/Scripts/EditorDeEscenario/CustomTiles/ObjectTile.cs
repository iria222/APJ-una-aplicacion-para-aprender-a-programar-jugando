using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
 * Tile para los objetos
 */

#if UNITY_EDITOR
using UnityEditor;
#endif
public class ObjectTile : Tile
{
    [SerializeField] private List<PlaceholderTile> placeholderList = new List<PlaceholderTile>();

    public ObjectTile GetEmptyClone()
    {
        ObjectTile tile = Instantiate(this);
        tile.ClearList();
        return tile;
    }

    public void ClearList()
    {
        placeholderList.Clear();
    }

    public void SetSprite(Sprite sprite)
    {
        this.sprite = sprite;
    }

    public List<PlaceholderTile> GetPlaceholderList()
    {
        return placeholderList;
    }

    public void AddPlaceholder(PlaceholderTile placeholderTile)
    {
        placeholderList.Add(placeholderTile);
    }


    public void DestroyTile()
    {
        foreach(PlaceholderTile placeholderTile in placeholderList)
        {
            Destroy(placeholderTile);
        }
        ClearList();
        Destroy(this);
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/2D/Custom Tiles/ObjectTile")]
    public static void CreateVariableTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save ObjectTile", "New ObjectTile", "Asset", "Save ObjectTile", "Assets");
        if (path == "") return;

        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<ObjectTile>(), path);
    }
#endif
}
