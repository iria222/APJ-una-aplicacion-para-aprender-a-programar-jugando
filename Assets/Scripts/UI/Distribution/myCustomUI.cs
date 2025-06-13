using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Clase general para crear interfaces
 */
public abstract class MyCustomUI : MonoBehaviour
{
    

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        SetUp();
        Configure();
    }

    private void OnValidate()
    {
        Init();
    }
    public abstract void SetUp();

    public abstract void Configure();

}
