using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class DesktopSizeManager : MonoBehaviour
{
    private bool isColliding;
    private RectTransform scrollContent;
    private int objectsColliding;

    private Transform pointA;
    private Transform pointB;

    [SerializeField] private LayerMask layerMask;

    private void Awake()
    {
        scrollContent = this.transform.parent.GetComponent<RectTransform>();
        pointA = this.transform.GetChild(0);
        pointB = this.transform.GetChild(1);
    }

    private void Start()
    {
        isColliding = false;
        objectsColliding = 0;
    }

    public int GetNumberOfCollisions()
    {
        Vector2 positionA = new Vector2(GetPointA().position.x, GetPointA().position.y);
        Vector2 positionB = new Vector2(GetPointB().position.x, GetPointB().position.y);

        Collider2D[] colliderList = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        filter.useLayerMask = true;
        filter.layerMask = GetLayerMask();

        return Physics2D.OverlapArea(positionA, positionB, filter, colliderList);
    }

    public LayerMask GetLayerMask()
    {
        return layerMask;
    }

    public RectTransform GetScrollContent()
    {
        return scrollContent;
    }

    public Transform GetPointA() 
    { 
        return pointA; 
    }

    public Transform GetPointB() 
    {  
        return pointB; 
    }

    public void SetIsColliding(bool isColliding)
    {
        this.isColliding = isColliding;
    }

    public bool IsColliding()
    {
        return isColliding;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetIsColliding(true);
        objectsColliding++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objectsColliding--;
        if (objectsColliding == 0)
        {
            SetIsColliding(false);
        }
    }

    public abstract void ChangeDesktopSize();
}
