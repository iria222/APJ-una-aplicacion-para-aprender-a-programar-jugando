using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Clase que describe el comportamiento del evento Robot sobre color
 */
public class EventRobotSobreColor : Event
{
    private Color colorUnderRobot;
    [SerializeField] Image buttonImage;

    // Start is called before the first frame update
    void Start()
    {
        colorUnderRobot = Color.white;
    }

    
    /*
     * Establece el color sobre el que se encuentra el robot
     * @param   sender  Objeto que inicia el evento
     * @param   data    color sobre el que está el robot
     */
    public void SetColorUnderRobot(GameObject sender, object data)
    {
        if(data is Color)
        {
            colorUnderRobot = (Color)data;
        }
    }

    public override bool IsEventHappening()
    {
        return colorUnderRobot == buttonImage.color;
    }
}
