using SimpleFileBrowser;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static SimpleFileBrowser.FileBrowser;

/*
 * Clase que se encarga de cargar una imagen del ordenador y crear un buildable a partir de ella
 */

public abstract class PlaceableLoader : MonoBehaviour
{
    [SerializeField] protected GameObject placeableButton;

    private void Start()
    {
        FileBrowser.SetFilters(false, new FileBrowser.Filter("Imagenes", ".jpg", ".png", ".jpeg"));
    }

    /*
     * Muesta la pestaña para elegir el archivo que cargar
     */
    public virtual void LoadDialog()
    {
        FileBrowser.ShowLoadDialog(OnSuccess, OnCancel, FileBrowser.PickMode.Files, true, null, null, "Cargar imagen", "Seleccionar");
    }

    /*
     * Crea un nuevo BuildableButton con la imagen seleccionada
     * @param   paths   ruta de la imagen/es seleccionada/s en el pc
     */
    public void OnSuccess(string[] paths)
    {
        for (int i = 0; i < paths.Length; i++)
        {
            Sprite sprite = LoadImageAsSprite(paths[i]);
            if(sprite != null) 
            { 
            
                GameObject aux = Instantiate(placeableButton, this.transform.parent.transform);
                aux.transform.SetSiblingIndex(1);

                EditCreatedButton(sprite, aux);
            }
        }
    }


    /*
     * Necesario para la pestaña de seleccion de archivos
     */
    public void OnCancel() { }

    /*
     * Asigna los valores necesarios al boton
     * @param   sprite  sprite del boton
     * @param   gameObject  boton que editar
     */
    public abstract void EditCreatedButton(Sprite sprite, GameObject gameObject);

    /*
     * Carga la imagen seleccionada y la devuelve como Texture2D
     * @param   path    ruta de la imagen seleccionada en el pc
     * @return          Texture2D de imagen seleccionada
     */
    public Texture2D LoadImage(string path)
    {
        if (File.Exists(path))
        {
            byte[] bytes = File.ReadAllBytes(path);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(bytes);
            
            return tex;
        }
        else
        {
            return null;
        }
    }

    /*
     * Carga la imagen seleccionada como Sprite
     * @param   path    ruta de la imagen seleccionada
     * @return          Sprite de imagen seleccionada
     */
    public Sprite LoadImageAsSprite(string path)
    {
        Texture2D image = LoadImage(path);

        if(image == null)
        {
            return null;
        }
        float max = Mathf.Max(image.width, LoadImage(path).height);

        Sprite sprite = Sprite.Create(image, new Rect(0.0f, 0.0f, image.width,
        image.height), new Vector2(0.5f, 0.5f), max);

        return sprite;
    }
}
