using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

enum Colorr
{
    Red,
    Green,
    Orange,
    Blue
}
public class Hue : MonoBehaviour
{
    [SerializeField] private Colorr colorr;
    [SerializeField] private RectTransform object2;

    private RectTransform object1; 
    private RectTransform rectTransform1;
    private RectTransform rectTransform2;

    private HueManager manager;
    private Animator anim;
    public bool hover = false;

    private readonly int HOVER_TAG = Animator.StringToHash("Hover");

    private void Start()
    {
        object1 = GetComponent<RectTransform>();
        anim = transform.parent.GetComponent<Animator>();
        manager = object2.GetComponent<HueManager>();
        rectTransform1 = object1;
        rectTransform2 = object2;
    }

    private void Update()
    { 
        bool isColliding = CheckCollision();

        if (isColliding)
        {
            anim.SetBool(HOVER_TAG, true);
            hover = true;
        }
        else
        {
            anim.SetBool(HOVER_TAG, false);
            hover = false;
        }


        if (hover)
        {
            switch (colorr)
            {
                case Colorr.Red:
                    manager.ChangeToRed();
                    break;

                case Colorr.Green:
                    manager.ChangeToGreen();
                    break;

                case Colorr.Blue:
                    manager.ChangeToBlue();
                    break;

                case Colorr.Orange:
                    manager.ChangeToOrange();
                    break;
            }
        }
    }


    private bool CheckCollision()
    {
        Vector3[] object1Corners = new Vector3[4];
        Vector3[] object2Corners = new Vector3[4];
        rectTransform1.GetWorldCorners(object1Corners);
        rectTransform2.GetWorldCorners(object2Corners);

        bool isOverlapping = AreRectanglesOverlapping(object1Corners, object2Corners);

        return isOverlapping;
    }

    private bool AreRectanglesOverlapping(Vector3[] rect1Corners, Vector3[] rect2Corners)
    {
        bool isOverlapping =
            rect1Corners[0].x < rect2Corners[2].x &&
            rect1Corners[2].x > rect2Corners[0].x &&
            rect1Corners[0].y < rect2Corners[2].y &&
            rect1Corners[2].y > rect2Corners[0].y;

        return isOverlapping;
    }


}