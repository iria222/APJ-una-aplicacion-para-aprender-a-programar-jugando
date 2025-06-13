using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NumberTile : Tile, IDataTile
{
    [SerializeField] private int number;
    public object GetData()
    {
        return number;
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/2D/Custom Tiles/NumberTile")]
    public static void CreateVariableTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save NumberTile", "New NumberTile", "Asset", "Save NumberTile", "Assets");
        if (path == "") return;

        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<NumberTile>(), path);
    }
#endif
}
