using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SimpleFileBrowser.FileBrowser;

public class DesktopSizeDecreaser : DesktopSizeManager
{

    private const float minHeight = 1092f;


    public override void ChangeDesktopSize()
    {
        Vector2 sizeChange = new Vector2 (0, 50);
        RectTransform scrollContent = GetScrollContent();

        

        int numberOfCollisions = GetNumberOfCollisions();
      
        while (numberOfCollisions== 0 && scrollContent.sizeDelta.y > minHeight)
        {
          
            scrollContent.sizeDelta -= sizeChange;

            numberOfCollisions = GetNumberOfCollisions();
          
        }
    }

}
