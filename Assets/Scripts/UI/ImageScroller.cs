using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Mueve una imagen en diagonal
 */
public class ImageScroller : MonoBehaviour
{
    private RawImage rawImage;
    private float x = 0.01f;
    private float y = 0.01f;

    private void Awake()
    {
        rawImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        rawImage.uvRect = new Rect(rawImage.uvRect.position + new Vector2(x, y) *Time.deltaTime, rawImage.uvRect.size);
    }
}
