using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFileBrowser;
using System.IO;


public class SpriteLoader : PlaceableLoader
{
    /*
     * Asigna los valores necesarios al boton
     * @param   sprite  sprite del boton
     * @param   gameObject  boton que editar
     */
    public override void EditCreatedButton(Sprite sprite, GameObject gameObject)
    {
        ChangeSpriteButton changeSpriteButton = gameObject.GetComponent<ChangeSpriteButton>();
        changeSpriteButton.SetSprite(sprite);
    }

}
