using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Clase que evita que la lista del dropdown se coloque en la sortingLayer incorrecta
 * Por defecto se coloca en la sortingLayer UI y al estar por debajo de la sortingLayer Comandos no se vería
 */
public class MyDropdownTemplate : MonoBehaviour
{
    public void OnEnable()
    {
        Canvas canvas = GetComponent<Canvas>();
        if (canvas)
        {
            canvas.sortingLayerName = "Comandos";
        }
    }
}
