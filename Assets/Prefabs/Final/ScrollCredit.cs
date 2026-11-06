using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCredit : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 50f;
    [SerializeField] RectTransform creditsRect;
    private Vector2 startPos;
    private Vector2 endPos;
    private float contentHeight;
    private float canvasHeight;
    private float resetThresholdY;
    private void Start()
    {
        contentHeight = creditsRect.rect.height;
        canvasHeight = creditsRect.parent.GetComponent<RectTransform>().rect.height;
        //inicio de creditos debajo de la pantalla
        startPos = new Vector2(creditsRect.anchoredPosition.x, -canvasHeight);
        endPos = new Vector2(creditsRect.anchoredPosition.x, contentHeight);

        //creditsRect.anchoredPosition = startPos;
        resetThresholdY = contentHeight;
    }
    private void Update()
    {
        creditsRect.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
        if (creditsRect.anchoredPosition.y >= resetThresholdY)
        {
            //enabled = false;
            creditsRect.anchoredPosition = startPos;
        }
    }
}
