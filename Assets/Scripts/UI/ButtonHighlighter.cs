using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * Cambia el color de un elemento cuando se interactua con el mediante el uso del raton
 */
public class ButtonHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private Image buttonImage;
    private Button button;
    private Color originalColor;
    private Color highlightColor;
    private Color inactiveColor;

    private const float multiplicityValue = 0.9f;
    private bool isActive;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();

        TryGetComponent<Button>(out button);
    }

    private void Start()
    {
        originalColor = buttonImage.color;
        highlightColor = new Color(originalColor.r * multiplicityValue, originalColor.g * multiplicityValue, originalColor.b * multiplicityValue);
        inactiveColor = new Color(highlightColor.r * 0.75f,
                                  highlightColor.g * 0.75f, 
                                  highlightColor.b * 0.75f);
        isActive = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(isActive &&(button == null || button.interactable)) 
        {
            buttonImage.color = highlightColor;

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(isActive)
        {
            buttonImage.color = originalColor;

        }
    }

    public void SetIsActive(bool isActive)
    {
            this.isActive = isActive;
            if(!isActive)
            {
                buttonImage.color = inactiveColor;
            }
            else
            {
                buttonImage.color = originalColor;
            }
        
    }
    

}
