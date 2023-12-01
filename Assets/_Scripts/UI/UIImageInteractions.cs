using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIImageInteractions : MonoBehaviour
{
    [SerializeField] private Vector2 interactionRectSize;
    private Vector2 startRectSize;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        startRectSize = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
    }

    private void OnDisable()
    {
        OnImageExit();
    }

    public void OnImageEnter()
    {
        rectTransform.sizeDelta = interactionRectSize;
    }

    public void OnImageExit()
    {
        rectTransform.sizeDelta = startRectSize;
    }
}
