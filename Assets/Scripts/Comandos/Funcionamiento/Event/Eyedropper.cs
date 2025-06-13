using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyedropper : MonoBehaviour
{
    private ColorSelector colorSelector;
    private ColorPicker colorPicker;
    private Color selectedColor;
    private Camera mainCamera;


    private void Awake()
    {
        mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        colorSelector = transform.parent.GetComponent<ColorSelector>();
        colorPicker = new ColorPicker();
    }

    public void OnClick()
    {
        StartCoroutine(GetColorUnderMouse());
    }

    /*
     * Obtiene el color en la posición del ratón
     */
    public IEnumerator GetColorUnderMouse()
    {
        yield return StartCoroutine(colorPicker.TakeScreenShot(mainCamera));
        while (!Input.GetMouseButtonDown(0))
        {
            Vector3 position = Input.mousePosition;
            selectedColor = colorPicker.GetColorPicked(position);

            colorSelector.SetColorPicked(selectedColor);

            yield return null;
        }
    }
}
