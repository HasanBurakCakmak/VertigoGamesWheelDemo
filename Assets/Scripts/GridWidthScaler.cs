using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridWidthScaler : MonoBehaviour
{
    [SerializeField] private RectTransform parentTransform;
    [SerializeField] private GridLayoutGroup layoutGroup;
    [SerializeField] private int columnCount=4;
    [SerializeField] private int spacing=30;
    [SerializeField] private int heigth=200;

    private void OnRectTransformDimensionsChange()
    {
        float currentWidth = parentTransform.rect.width;
        currentWidth = currentWidth / columnCount;
        layoutGroup.cellSize = new Vector2(currentWidth-spacing-10, heigth);
        layoutGroup.spacing = new Vector2(spacing, spacing);

    }

}
