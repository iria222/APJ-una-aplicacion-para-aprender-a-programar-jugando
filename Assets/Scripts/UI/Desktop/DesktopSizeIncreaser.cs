using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesktopSizeIncreaser : DesktopSizeManager
{
    public override void ChangeDesktopSize()
    {
        RectTransform scrollContent = GetScrollContent();

        int numberOfCollisions = GetNumberOfCollisions();
        if (numberOfCollisions>0)
        {
            scrollContent.sizeDelta += new Vector2(0, 200);
        }
    }

}
