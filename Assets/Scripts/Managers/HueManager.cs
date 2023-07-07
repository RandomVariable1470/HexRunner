using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading.Tasks;
using UnityEditor;

public class HueManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region Variables
    [SerializeField] private PlayerController controller;
    [SerializeField] private Material[] skybox;
    [SerializeField] private Animator GrayScaleAnim;
    [SerializeField] private float speed;
    [field: SerializeField] public bool red { get; set; }
    [field: SerializeField] public bool blue { get; set; }
    [field: SerializeField] public bool green { get; set; }
    [field: SerializeField] public bool orange { get; set; }
    [field:SerializeField] public bool canInteract {  get; private set; }

    private Material[] mats1;
    private Material[] mats2;

    private RectTransform dragArea;
    private RectTransform rectTransform;
    private Vector2 initialPosition;

    private readonly int OUT_TAG = Animator.StringToHash("Out");

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

    public async void OnBeginDrag(PointerEventData eventData)
    {
        GameManager.instance.UpdateState(GameState.SelectColor);
        await Task.Delay(50);
        isDragging = true;
        GrayScaleAnim.SetBool(OUT_TAG, true);
        canInteract = true;
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

    public async void OnEndDrag(PointerEventData eventData)
    {
        canInteract = false;

        UIManager.Instance.HueWheelIn();

        transform.localPosition = initialPosition;

        GrayScaleAnim.SetBool(OUT_TAG, false);

        await Task.Delay(75);

        ColorHandler();

        await Task.Delay(75);

        GameManager.instance.UpdateState(GameState.Play);
        UIManager.Instance.HueWheelOut();

        isDragging = false;

        DynamicGI.UpdateEnvironment();
    }

    #endregion

    #region Colorrs

    private void ColorHandler()
    {
        if (red)
        {
            ChangeToRed();
        }
        else if (green)
        {
            ChangeToGreen();
        }
        else if (blue)
        {
            ChangeToBlue();
        }
        else if (orange)
        {
            ChangeToOrange();
        }
    }

    private void ChangeToRed()
    {
        controller.RedColor.SetActive(true);
        controller.BlueColor.SetActive(false);
        controller.GreenColor.SetActive(false);
        controller.OrangeColor.SetActive(false);

        RenderSettings.skybox = skybox[0];

        foreach(var item in controller.platform)
        {
            mats1 = item.materials;
            mats1[1] = controller.groundMat[0];
            item.materials = mats1;
        }
        mats2 = controller.platformEnd.materials;
        mats2[2] = controller.groundMat[0];
        controller.platformEnd.materials = mats2;

        controller.red = true;
        controller.blue = false;
        controller.green = false;
        controller.orange = false;
    }

    private void ChangeToBlue()
    {
        controller.RedColor.SetActive(false);
        controller.BlueColor.SetActive(true);
        controller.GreenColor.SetActive(false);
        controller.OrangeColor.SetActive(false);

        RenderSettings.skybox = skybox[1];

        foreach (var item in controller.platform)
        {
            mats1 = item.materials;
            mats1[1] = controller.groundMat[1];
            item.materials = mats1;
        }
        mats2 = controller.platformEnd.materials;
        mats2[2] = controller.groundMat[1];
        controller.platformEnd.materials = mats2;

        controller.red = false;
        controller.blue = true;
        controller.green = false;
        controller.orange = false;
    }

    private void ChangeToOrange()
    {
        controller.RedColor.SetActive(false);
        controller.BlueColor.SetActive(false);
        controller.GreenColor.SetActive(false);
        controller.OrangeColor.SetActive(true);

        RenderSettings.skybox = skybox[2];

        foreach (var item in controller.platform)
        {
            mats1 = item.materials;
            mats1[1] = controller.groundMat[2];
            item.materials = mats1;
        }
        mats2 = controller.platformEnd.materials;
        mats2[2] = controller.groundMat[2];
        controller.platformEnd.materials = mats2;

        controller.red = false;
        controller.blue = false;
        controller.green = false;
        controller.orange = true;
    }

    private void ChangeToGreen()
    {
        controller.RedColor.SetActive(false);
        controller.BlueColor.SetActive(false);
        controller.GreenColor.SetActive(true);
        controller.OrangeColor.SetActive(false);

        RenderSettings.skybox = skybox[3];

        foreach (var item in controller.platform)
        {
            mats1 = item.materials;
            mats1[1] = controller.groundMat[3];
            item.materials = mats1;
        }
        mats2 = controller.platformEnd.materials;
        mats2[2] = controller.groundMat[3];
        controller.platformEnd.materials = mats2;

        controller.red = false;
        controller.blue = false;
        controller.green = true;
        controller.orange = false;
    }

    #endregion
}