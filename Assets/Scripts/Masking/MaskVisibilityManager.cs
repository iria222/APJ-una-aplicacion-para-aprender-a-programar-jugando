using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskVisibilityManager : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void ChangeMaskVisibility(GameObject sender, object data)
    {
        if (data is string name && name.Contains(this.gameObject.name))
        {
            image.enabled = !image.enabled;
        }
    }
}
