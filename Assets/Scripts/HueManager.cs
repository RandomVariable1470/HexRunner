using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HueManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private PlayerController controller;

    private RectTransform dragArea;
    private RectTransform rectTransform;
    private Vector2 initialPosition;
    private bool isDragging = false;

    private void Start()
    {
        dragArea = transform.parent.GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();
        initialPosition = transform.localPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameManager.instance.UpdateState(GameState.SelectColor);
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(dragArea, eventData.position, eventData.pressEventCamera, out localPoint))
            {
                float minX = dragArea.rect.xMin + rectTransform.sizeDelta.x * 0.5f;
                float maxX = dragArea.rect.xMax - rectTransform.sizeDelta.x * 0.5f;
                float minY = dragArea.rect.yMin + rectTransform.sizeDelta.y * 0.5f;
                float maxY = dragArea.rect.yMax - rectTransform.sizeDelta.y * 0.5f;

                localPoint.x = Mathf.Clamp(localPoint.x, minX, maxX);
                localPoint.y = Mathf.Clamp(localPoint.y, minY, maxY);

                transform.localPosition = localPoint;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameManager.instance.UpdateState(GameState.Play);
        isDragging = false;
        transform.localPosition = initialPosition;
    }

    public void ChangeToRed()
    {
        controller.RedColor.SetActive(true);
        controller.BlueColor.SetActive(false);
        controller.GreenColor.SetActive(false);
        controller.OrangeColor.SetActive(false);

        controller.red = true;
        controller.blue = false;
        controller.green = false;
        controller.orange = false;
    }

    public void ChangeToBlue()
    {
        controller.RedColor.SetActive(false);
        controller.BlueColor.SetActive(true);
        controller.GreenColor.SetActive(false);
        controller.OrangeColor.SetActive(false);

        controller.red = false;
        controller.blue = true;
        controller.green = false;
        controller.orange = false;
    }

    public void ChangeToOrange()
    {
        controller.RedColor.SetActive(false);
        controller.BlueColor.SetActive(false);
        controller.GreenColor.SetActive(false);
        controller.OrangeColor.SetActive(true);

        controller.red = false;
        controller.blue = false;
        controller.green = false;
        controller.orange = true;
    }

    public void ChangeToGreen()
    {
        controller.RedColor.SetActive(false);
        controller.BlueColor.SetActive(false);
        controller.GreenColor.SetActive(true);
        controller.OrangeColor.SetActive(false);

        controller.red = false;
        controller.blue = false;
        controller.green = true;
        controller.orange = false;
    }
}