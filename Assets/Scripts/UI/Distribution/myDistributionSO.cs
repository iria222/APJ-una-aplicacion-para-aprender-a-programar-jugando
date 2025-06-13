using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/*
 * Clase padre de las distribuciones horizontal y vertical
 */

public class MyDistributionSO : ScriptableObject
{
    [SerializeField] private RectOffset padding;
    [SerializeField] private float spacing;
    
    public RectOffset GetPadding() { return padding; }
    public float GetSpacing() { return spacing; }
}
