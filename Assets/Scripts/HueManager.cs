using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HueManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region Variables
    [SerializeField] private PlayerController controller;
    [SerializeField] private Material[] skybox;
    [SerializeField] private Image image;
    [SerializeField] private float changeSpeed = 2f;
    [SerializeField] private float maxSize = 1.5f;
    [SerializeField] private float minSize = 0.5f;

    private bool increaseSize = false;
    private bool decreaseSize = false;
    private float currentSize = 0f;

    private RectTransform dragArea;
    private RectTransform rectTransform;
    private Vector2 initialPosition;
    public bool isDragging = false;
    #endregion

    #region Initilization

    private void Start()
    {
        dragArea = transform.parent.GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();
        initialPosition = transform.localPosition;
    }
    #endregion

    #region Handlers

    private void Update()
    {
        if (increaseSize)
        {
            currentSize += changeSpeed * Time.deltaTime;
            if (currentSize >= maxSize)
            {
                currentSize = maxSize;
                increaseSize = false;
            }
        }
        else if (decreaseSize)
        {
            currentSize -= changeSpeed * Time.deltaTime;
            if (currentSize <= minSize)
            {
                currentSize = minSize;
                decreaseSize = false;
            }
        }

        image.rectTransform.localScale = new Vector3(currentSize, currentSize, 0f);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameManager.instance.UpdateState(GameState.SelectColor);
        isDragging = true;
        StartIncreasingSize();
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
        StartDecreasingSize();

    }

    public void StartIncreasingSize()
    {
        increaseSize = true;
        decreaseSize = false;
    }

    public void StartDecreasingSize()
    {
        decreaseSize = true;
        increaseSize = false;
    }
    #endregion

    #region Colorrs

    public void ChangeToRed()
    {
        controller.RedColor.SetActive(true);
        controller.BlueColor.SetActive(false);
        controller.GreenColor.SetActive(false);
        controller.OrangeColor.SetActive(false);

        RenderSettings.skybox = skybox[0];
        DynamicGI.UpdateEnvironment();

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

        RenderSettings.skybox = skybox[1];
        DynamicGI.UpdateEnvironment();

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

        RenderSettings.skybox = skybox[2];
        DynamicGI.UpdateEnvironment();

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

        RenderSettings.skybox = skybox[3];
        DynamicGI.UpdateEnvironment();

        controller.red = false;
        controller.blue = false;
        controller.green = true;
        controller.orange = false;
    }

    #endregion
}