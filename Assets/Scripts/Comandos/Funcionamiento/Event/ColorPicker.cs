using System.Collections;
using UnityEngine;

/*
 * Describe el comportamiento del colorPicker
 */
public class ColorPicker
{
    private Texture2D texture;

    public ColorPicker() { }

    public Texture2D GetScreenshot()
    {
        return texture;
    }

    public void SetScreenshot(Texture2D texture)
    {
        this.texture = texture;
    }

    public IEnumerator TakeScreenShot(Camera cam)
    {
        if (cam != null)
        {
            //Destruyo la anterior textura
            Object.Destroy(texture);
            texture = new Texture2D(Screen.width, Screen.height);
            Rect viewRect = new Rect();
            viewRect.x = 0;
            viewRect.y = 0;
            viewRect.width = Screen.width;
            viewRect.height = Screen.height;

            if (cam.gameObject.name.Equals("Floor Camera"))
            {
                RenderTexture rt = RenderTexture.GetTemporary(Screen.width, Screen.height);
                cam.targetTexture = rt;
                yield return new WaitForEndOfFrame();


                RenderTexture previousTexture = RenderTexture.active;
                RenderTexture.active = rt;
                cam.Render();

                
                //Rect viewRect = cam.pixelRect;
                texture.ReadPixels(viewRect, 0, 0);
                RenderTexture.ReleaseTemporary(rt);
                RenderTexture.active = previousTexture;
                cam.targetTexture = null;
            }
            else
            {
                yield return new WaitForEndOfFrame();
                //Rect viewRect = cam.pixelRect;
                texture.ReadPixels(viewRect, 0, 0);
            }


            //texture.Apply();
        }
    }

    /*
     * Obtiene el color de una posición concreta
     * @param   position    posición de la que coger el color
     * @return              color en la posición introducida
     */
    public Color GetColorPicked(Vector3 position)
    {
        
        return texture.GetPixel((int)position.x, (int)position.y);
    }

}
