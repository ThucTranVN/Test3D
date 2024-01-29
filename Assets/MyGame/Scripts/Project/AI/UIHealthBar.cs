using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public Canvas canvas;
    public Image foreGroundImg;
    private Transform target;

    void Start()
    {
        canvas.worldCamera = Camera.main;
        target = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (target)
        {
            this.transform.rotation = target.transform.rotation;
        }
    }

    public void SetHealthBarPercentage(float percentage)
    {
        float parentWidth = GetComponent<RectTransform>().rect.width;
        float width = parentWidth * percentage;
        foreGroundImg.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }

    public void Deactive()
    {
        this.gameObject.SetActive(false);
    }
}
