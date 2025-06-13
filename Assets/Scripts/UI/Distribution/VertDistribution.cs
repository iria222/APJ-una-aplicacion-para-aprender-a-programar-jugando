using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Clase para configurar la interfaz distribuida en filas
 */
public class VertDistribution : MyCustomUI
{
    [SerializeField] private VertDistributionSO distData;

    [SerializeField] private GameObject contTop;
    [SerializeField] private GameObject contBottom;

    private VerticalLayoutGroup verticalLayoutGroup;

    private LayoutElement topLayoutElement;
    private LayoutElement bottomLayoutElement;

    /*
     * Actualiza los valores de la distribución
     * según lo que se establezca en el ScriptableObject
     */
    public override void Configure()
    {
        verticalLayoutGroup.padding= distData.GetPadding();
        verticalLayoutGroup.spacing= distData.GetSpacing();

        topLayoutElement.flexibleHeight = distData.GetTopSize();
        bottomLayoutElement.flexibleHeight= distData.GetBottomSize();
    }

    /*
     * Inicializa las variables
     */
    public override void SetUp()
    {
        verticalLayoutGroup = this.GetComponent<VerticalLayoutGroup>();

        topLayoutElement = contTop.GetComponent<LayoutElement>();
        bottomLayoutElement = contBottom.GetComponent<LayoutElement>();
    }

}
