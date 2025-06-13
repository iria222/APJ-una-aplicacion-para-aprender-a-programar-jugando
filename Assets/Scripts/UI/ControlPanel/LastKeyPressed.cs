using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LastKeyPressed : MonoBehaviour
{
    private TMP_Text text;
    [SerializeField] private GameObject arrowImage;
    private string keyPressed;

    private bool arrowActive;
    private int arrowAngle;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        keyPressed = "Tecla";
        arrowActive = false;
        arrowAngle = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space"))
        {
            arrowActive = false;
            keyPressed = "Espacio";
        }
        else if (Input.GetKeyDown("down"))
        {
            keyPressed = "";
            arrowActive = true;
            arrowAngle = 270;
        }
        else if (Input.GetKeyDown("up"))
        {
            keyPressed = "";
            arrowActive = true;
            arrowAngle = 90;

        }
        else if (Input.GetKeyDown("left"))
        {
            keyPressed = "";
            arrowActive= true;
            arrowAngle = 180;
        }
        else if (Input.GetKeyDown("right"))
        {
            keyPressed = "";
            arrowActive = true;
            arrowAngle = 0;
        }
        else if (Input.anyKeyDown && !IsMouseClick())
        {
            arrowActive= false;
            string aux = Input.inputString;
            //Es letra o numero
            if (!string.IsNullOrEmpty(aux) && aux.Length ==1 && char.IsLetterOrDigit(aux[0]))
            {
                keyPressed = Input.inputString.ToUpper();
            }
        }

        arrowImage.transform.eulerAngles = new Vector3(0, 0, arrowAngle);
        arrowImage.SetActive(arrowActive);

        text.text = keyPressed;
    }

    /*
     * Detecta si se ha pulsado el ratón
     * @return  si se ha o no pulsado ún botón del ratón
     */
    public bool IsMouseClick()
    {
        return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1);
    }
}
