using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Controla el color de una imagen
 */
public class ManageColorImage : MonoBehaviour
{

    [SerializeField] private Image image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeImageColor(GameObject sender, object data)
    {
        if(data is Color)
        {
            image.color = (Color)data;
        }
    }
}
