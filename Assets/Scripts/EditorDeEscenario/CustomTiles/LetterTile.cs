using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class LetterTile : Tile, IDataTile
{
    [SerializeField] private char letter;

    public object GetData()
    {
        return letter;
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/2D/Custom Tiles/LetterTile")]
    public static void CreateVariableTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save LetterTile", "New LetterTile", "Asset", "Save LetterTile", "Assets");
        if (path == "") return;

        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<LetterTile>(), path);
    }
#endif
}
