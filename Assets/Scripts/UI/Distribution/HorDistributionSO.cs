using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ScriptableObject con los valores de la interfaz distribuida en columnas
 */

[CreateAssetMenu(menuName = "myCustomUI/HorDistributionSO", fileName = "HorDistributionSO")]
public class HorDistributionSO : MyDistributionSO
{
    [Header("Panel Size")]
    [SerializeField] private float leftSize;
    [SerializeField] private float rightSize;

    [Header("Panel Color")]
    [SerializeField] private Color leftColor;
    [SerializeField] private Color rightColor;

    public float GetLeftSize() {  return leftSize; }
    public float GetRightSize() {  return rightSize; }
    public Color GetLeftColor() {  return leftColor; }
    public Color GetRightColor() {  return rightColor; }


}
