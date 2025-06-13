using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Clase que describe el comportamiento del selector de color 
 * Hace uso de los valores hue, saturation y brightness
 */
public class ColorSelector : MonoBehaviour
{

    [SerializeField] private Image eventImage;

    [Header("Sliders")]
    [SerializeField] private Slider sliderHue;
    [SerializeField] private Slider sliderSaturation;
    [SerializeField] private Slider sliderBrightness;

    private float hue;
    private float saturation;
    private float brightness;

    // Start is called before the first frame update
    void Start()
    {
        hue = 0.5f;
        saturation = 1;
        brightness = 1;
        ChangeColor();
    }

    /*
     * Establece un nuevo color seleccionado con el eyedropper
     * @param   color   color seleccionado
     */
    public void SetColorPicked(Color color)
    {
        
        Color.RGBToHSV(color,out hue,out saturation,out brightness);

        sliderHue.value = hue;
        sliderSaturation.value = saturation;
        sliderBrightness.value = brightness;

        ChangeColor();
        
    }

    /*
     * Cambia el color de la imagen que muestra el color seleccionado
     */
    public void ChangeColor()
    {
        Color newColor = Color.HSVToRGB(hue, saturation, brightness);
        eventImage.color = newColor;
    }

    public void SetHue(float hue)
    {
        this.hue = hue;
        ChangeColor();
    }

    public void SetSaturation(float saturation) 
    {
        this.saturation = saturation;
        ChangeColor();

    }

    public void SetBrightness(float brightness) 
    { 
        this.brightness = brightness;
        ChangeColor();

    }
}
