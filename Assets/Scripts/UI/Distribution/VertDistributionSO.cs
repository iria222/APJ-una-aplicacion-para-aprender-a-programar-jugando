using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ScriptableObject con los valores de la interfaz
 * distribuida en filas
 */

[CreateAssetMenu(menuName = "myCustomUI/VertDistributionSO", fileName = "VertDistributionSO")]
public class VertDistributionSO : MyDistributionSO
{
    [Header("Panel Size")]
    [SerializeField] private float topSize;
    [SerializeField] private float bottomSize;

    public float GetTopSize() {  return topSize; }
    public float GetBottomSize() {  return bottomSize; }
}
