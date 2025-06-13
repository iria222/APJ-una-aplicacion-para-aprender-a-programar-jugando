using UnityEngine;

/*
 * Clase que maneja el targetPoint
 */

public class TargetPointManager : MonoBehaviour
{
    private LayerMask collidersLayer;
    private bool collisionDetected;


    // Start is called before the first frame update
    void Start()
    {
        collidersLayer = LayerMask.GetMask("Colliders");
        collisionDetected = false;
        this.transform.SetParent(null);
    }

    /*
     * @param   steps       numero de pasos que se mueve el targetPoint
     * @param   orientation sentido en el que se mueve el targetPoint
     * @return  indica si el robot se choca contra un obstáculo o no
     */
    public void MoveTargetPoint(int steps, Vector3 orientation)
    {
        
        Vector3 newPosition = this.transform.position + orientation*steps;
        if (Physics2D.OverlapCircle(newPosition, 0.2f, collidersLayer))
        {
            collisionDetected = true;
            return;
        }
        this.transform.position = newPosition;
        
        collisionDetected = false;
    }

    public bool GetCollisionDetected()
    {
        return collisionDetected;
    }

    public void SetCollisionDetected(bool collisionDetected)
    {
        this.collisionDetected = collisionDetected;
    }

    public void SetPosition(Vector3 position)
    {
        this.transform.position = position;
    }

    public Vector3 GetPosition() 
    { 
        return this.transform.position; 
    }
}
