using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Configura la interfaz distribuida en columnas
 */
public class HorDistribution : MyCustomUI
{
    //Scriptable Object
    [SerializeField] private HorDistributionSO distData;  

    [Header("Canvas")]
    [SerializeField] private GameObject rightCanvas;
    [SerializeField] private GameObject leftCanvas;

    [Header("Paneles")]
    [SerializeField] private GameObject rightPanel;
    [SerializeField] private GameObject leftPanel;

    private Image leftImage;
    private Image rightImage;

    private HorizontalLayoutGroup horizontalLayoutGroup;

    private LayoutElement leftLayoutElement;
    private LayoutElement rightLayoutElement;

    /*
     * Cambia la distribución de la interfaz según lo que se establezca en el ScriptableObject
     */
    public override void Configure()
    {
        horizontalLayoutGroup.padding=distData.GetPadding();
        horizontalLayoutGroup.spacing=distData.GetSpacing();

        leftLayoutElement.flexibleWidth = distData.GetLeftSize();
        rightLayoutElement.flexibleWidth =distData.GetRightSize();

        leftImage.color=distData.GetLeftColor();
        rightImage.color=distData.GetRightColor();

    }

    /*
     * Inicializa las variables
     */
    public override void SetUp()
    {
        horizontalLayoutGroup = this.GetComponent<HorizontalLayoutGroup>();

        leftLayoutElement = leftCanvas.GetComponent<LayoutElement>();
        rightLayoutElement = rightCanvas.GetComponent<LayoutElement>();

        leftImage = leftPanel.GetComponent<Image>();
        rightImage = rightPanel.GetComponent<Image>(); 
    }

}
