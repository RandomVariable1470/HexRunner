using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HueManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private PlayerController controller;
    [SerializeField] private float returnSpeed = 5f; 
    [SerializeField] private float smoothTime = 0.2f;

    private RectTransform dragArea;
    private Vector2 initialPosition;
    private Vector2 targetPosition; 
    private Vector2 currentVelocity; 
    private bool isDragging = false;
    private bool isReturning = false; 

    private void Start()
    {
        dragArea = transform.parent.GetComponent<RectTransform>();
        initialPosition = transform.localPosition;
        targetPosition = initialPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isReturning)
        {
            GameManager.instance.UpdateState(GameState.SelectColor);
            isDragging = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isReturning && isDragging)
        {
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(dragArea, eventData.position, eventData.pressEventCamera, out localPoint))
            {
                localPoint.x = Mathf.Clamp(localPoint.x, dragArea.rect.xMin, dragArea.rect.xMax);
                localPoint.y = Mathf.Clamp(localPoint.y, dragArea.rect.yMin, dragArea.rect.yMax);

                transform.localPosition = localPoint;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isReturning)
        {
            GameManager.instance.UpdateState(GameState.Play);
            isDragging = false;

            StartCoroutine(ReturnToInitialPosition());
        }
    }

    private IEnumerator ReturnToInitialPosition()
    {
        isReturning = true;

        while (Vector2.Distance(transform.localPosition, targetPosition) > 0.01f)
        {
            transform.localPosition = Vector2.SmoothDamp(transform.localPosition, targetPosition, ref currentVelocity, smoothTime, returnSpeed);
            yield return null;
        }

        transform.localPosition = targetPosition;
        isReturning = false;
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