using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;


/*
 * Clase que describe el movimiento del robot
 */
public class RobotManager : MonoBehaviour
{
    private TargetPointManager targetPointManager;
    private RobotSpriteManager robotSpriteManager;
    private RobotSensor robotSensor;

    private float movementSpeed;
    private float rotationSpeed;

    private bool isMoving;

    private Vector3 initialPosition;
    private Quaternion initialRotation;


    [Header("GameEvents")]
    [SerializeField] private GameEvent robotStoppedEvent;
    [SerializeField] private GameEvent stopExecutionEvent;
    [SerializeField] private GameEvent setNewInitialPositionEvent;
    

    private const int targetChild = 0;
    private const int spriteChild = 1;


    private void Awake()
    {
        targetPointManager = this.transform.GetChild(targetChild).GetComponent<TargetPointManager>();
        robotSpriteManager = this.transform.GetChild(spriteChild).GetComponent<RobotSpriteManager>();
        robotSensor = GetComponent<RobotSensor>();
    }

    // Start is called before the first frame update
    void Start()
    {

        isMoving = false;
        ChangeSpeed(this.gameObject, 5f);

        initialPosition = this.transform.position;
        initialRotation = this.transform.rotation;

        StartCoroutine(robotSensor.GetStartColor());
        setNewInitialPositionEvent.RaiseEvent(this.gameObject, this.transform.position);

    }

    public GameObject GetTargetPoint()
    {
        return targetPointManager.gameObject;
    }

    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

    public void SetInitialPosition()
    {
        initialPosition = this.transform.position;
        initialRotation = this.transform.rotation;
    }

    

    public void OnGetStartColorEvent(GameObject sender, object data)
    {
        StartCoroutine(robotSensor.GetStartColor());
    }


    /*
     * Detiene las coroutines activas y cambia los valores del robot a los iniciales
     */
    public void StopExecution(GameObject sender, object data)
    {
        StopAllCoroutines();
        robotStoppedEvent.RaiseEvent(this.gameObject, true);
        RestartPosition();
        ChangeSpeed(this.gameObject, 5f);
        robotSpriteManager.RestartParameters();
        isMoving = false;
    }

    /*
     * Cambia la posición y rotación del robot a los iniciales
     */
    public void RestartPosition()
    {
        this.transform.rotation = initialRotation;
        ChangePosition(this.gameObject, initialPosition);
    }

    /*
     * @param   data    velocidad de movimiento
     */
    public void ChangeSpeed(GameObject sender, object data)
    {
        if(data is float)
        {
            movementSpeed = (float)data;
            rotationSpeed = (float)data*60;
        }
    }

    /*
     * Cambia la posicion del robot
     * @param   data    nueva posición
     */
    public void ChangePosition(GameObject sender, object data)
    {
        if (data is Vector3)
        {
            Vector3 newPosition = (Vector3) data;

            //No puede cambiar la posicion
            if ((robotSensor.ThereIsObstacle(newPosition)) ||
                (sender != null && sender.CompareTag("PositionChanger") && robotSensor.ThereIsGoal(newPosition)))
            {
                StartCoroutine(OnCantChangePosition());
            }
            //Cambia la posicion
            else
            {
                this.transform.position = newPosition;
                targetPointManager.SetPosition(newPosition);
                robotSensor.GetColorUnderRobot(this.gameObject, null);
            }

            if (sender !=null && sender.CompareTag("PositionChanger"))
            {
                setNewInitialPositionEvent.RaiseEvent(this.gameObject, this.transform.position);
            }
        }
    }

    public IEnumerator OnCantChangePosition()
    {
        yield return StartCoroutine(robotSpriteManager.ColorFlickering(Color.red));
        stopExecutionEvent.RaiseEvent(this.gameObject, null);
    }

    /*
     * @param   data    pasos que moverse
     */
    public void StartMovement(GameObject gameObject, object data)
    {
        if(data is int)
        {
            int steps = (int)data;
            targetPointManager.MoveTargetPoint(steps, GetOrientation());

            StartCoroutine(MoveRobot(targetPointManager.GetPosition(), targetPointManager.GetCollisionDetected()));
            
            targetPointManager.SetCollisionDetected(false);

        }
    }

    /*
     * @param   data    angulo que rotar
     */
    public void StartRotation(GameObject gameObject, object data)
    {
        if (data is int)
        {
            int angle = (int)data;
            StartCoroutine(RotateRobot(angle));
        }
    }

    /*
     * Mueve el robot arrastrándolo hasta el targetPoint
     * @param   endpoint  destino del robot
     * @param   collisionDetected   indica si el robot choca contra un obstáculo
     */
    public IEnumerator MoveRobot(Vector3 endPoint, bool collisionDetected)
    {
        if(isMoving) { yield break; }
        isMoving = true;

        robotStoppedEvent.RaiseEvent(this.gameObject, false);
        
        //El robot se mueve hacia el target point
        while(Vector3.Distance(this.transform.position, endPoint) != 0.0f)
        {
            Vector3 startPoint = this.transform.position;

            this.transform.position = Vector3.MoveTowards(startPoint, endPoint, movementSpeed * Time.deltaTime);
            robotSensor.GetColorUnderRobot(this.gameObject, null);

            yield return null;
        }

        //Si el robot choca se ejecuta la animación correspondiente
        if(collisionDetected)
        {
            yield return StartCoroutine(RobotHittedObstacle());
            yield break;
        }


        //Se asigna la posición final a la del robot para evitar pequeñas diferencias tras varios movimientos
        this.transform.position = endPoint;

        isMoving=false;

        robotStoppedEvent.RaiseEvent(this.gameObject, true);
        
    }

    public IEnumerator RobotHittedObstacle()
    {
        yield return StartCoroutine(robotSpriteManager.PlayHitAnimation());
        yield return new WaitForSeconds(0.1f);
        stopExecutionEvent.RaiseEvent(this.gameObject, null);
    }

    /*
     * Rota el robot suavemente
     * @param   angle   Angulo que gira el robot
     */
    public IEnumerator RotateRobot(int angle)
    {
        if (isMoving) { yield break; }
        isMoving = true;

        if (robotStoppedEvent != null)
        {
            robotStoppedEvent.RaiseEvent(this.gameObject, false);
        }

        int newAngle = angle + (int)this.transform.rotation.eulerAngles.z;
        Quaternion newRotation = Quaternion.Euler(0, 0, newAngle);
        
        while (Quaternion.Angle(newRotation, this.transform.rotation) != 0.0f)
        {
            Quaternion startRotation = this.transform.rotation;

            this.transform.rotation = Quaternion.RotateTowards(startRotation, newRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        //Se asigna la rotación final a la del robot para evitar pequeñas diferencias
        this.transform.rotation = newRotation;

        isMoving = false;

        if (robotStoppedEvent != null)
        {
            robotStoppedEvent.RaiseEvent(this.gameObject, true);
        }

    }


    public Vector3 GetOrientation()
    {
        switch ((int)this.transform.localRotation.eulerAngles.z)
        {
            case 0:
                return new Vector3(0,1,0);

            case 90:
                return new Vector3(-1,0,0);

            case 180:
                return new Vector3(0,-1,0);

            case 270:
                return new Vector3(1,0,0);

            default:
                return new Vector3(0,0,0);
        }
    }



}
