using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RobotSensor : MonoBehaviour
{
    [Header("Tilemaps")]
    [SerializeField] private Tilemap goalTilemap;
    [SerializeField] private Tilemap floorTilemap;
    [SerializeField] private Tilemap obstaclesTilemap;

    [Header("GameEvents")]
    [SerializeField] private GameEvent sendTileData;
    [SerializeField] private GameEvent showVictoryScreenEvent;
    [SerializeField] private GameEvent changeColorEvent;


    [Header("Change color")]
    private Color colorUnderRobot;
    private ColorPicker colorPicker;

    private Camera floorCamera;

    private void Awake()
    {
        colorPicker = new ColorPicker();
        GameObject camGameObject = GameObject.Find("Floor Camera");

        if(camGameObject != null)
        {
            floorCamera = camGameObject.GetComponent<Camera>();
        }
        else
        {
            floorCamera = null;
        }

    }

    public void OnStartExecution(GameObject sender, object data)
    {
        //StartCoroutine(GetStartColor());
        SendColorUnderRobot(this.gameObject, null);
    }

    /*
     * Comprueba si el robot ha llegado a la victoria y enseña la pantalla correspondiente
     */
    public void OnFinishedEvent()
    {
        if (ThereIsGoal(this.transform.position))
        {
            showVictoryScreenEvent.RaiseEvent(this.gameObject, null);
        }
    }

    /*
     * Indica si hay un obstaculo en la posicion indicada
     * @param   pos     posicion que comprobar
     */
    public bool ThereIsObstacle(Vector3 pos)
    {
        if (obstaclesTilemap != null)
        {
            return obstaclesTilemap.HasTile(GetGridPosition(pos));
        }
        return false;
    }

    /*
     * Indica si hay un objetivo en la posicion indicada
     * @param   pos     posicion que comprobar
     */
    public bool ThereIsGoal(Vector3 pos)
    {
        if(goalTilemap != null)
        {
            return goalTilemap.HasTile(GetGridPosition(pos));
        }
        return false;
    }

    public void GetFloorTileData()
    {
        TileBase tileBase = floorTilemap.GetTile(GetGridPosition(this.transform.position));
        if (tileBase is IDataTile)
        {
            IDataTile tileData = (IDataTile)tileBase;
            sendTileData.RaiseEvent(this.gameObject, tileData.GetData());

        }
    }

    public Vector3Int GetGridPosition(Vector3 pos)
    {
        Vector3Int gridPos = goalTilemap.WorldToCell(pos);
        return gridPos;
    }



    /*
     * Obtiene una screenshot y lee el color debajo del robot
     */
    public IEnumerator GetStartColor()
    {
        if (floorCamera != null)
        {

            //robotSpriteManager.ChangeSpriteVisibility(false);
            yield return StartCoroutine(colorPicker.TakeScreenShot(floorCamera));
            //robotSpriteManager.ChangeSpriteVisibility(true);

            GetColorUnderRobot(this.gameObject, null);
        }
    }

    /*
     * Envia a los listeners del evento changeColorEvent el color sobre el que esta el robot
     */
    public void SendColorUnderRobot(GameObject sender, object data)
    {
        changeColorEvent.RaiseEvent(this.gameObject, colorUnderRobot);
    }

    /*
     * @param   data    Screenshot de la que obtener el color sobre el que está el robot
     */
    public void GetColorUnderRobot(GameObject sender, object data)
    {
        if (floorCamera != null)
        {

            if (data != null && data is Texture2D)
            {
                colorPicker.SetScreenshot((Texture2D)data);
            }
            Vector3 position = floorCamera.WorldToScreenPoint(this.transform.position);
            colorUnderRobot = colorPicker.GetColorPicked(position);

            SendColorUnderRobot(this.gameObject, null);
        }
    }
}
