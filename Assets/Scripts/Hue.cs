using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

enum Colorr
{
    Red,
    Green,
    Orange,
    Blue
}
public class Hue : MonoBehaviour, IDropHandler
{
    [SerializeField] private Colorr colorr;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        HueManager manager = dropped.GetComponent<HueManager>();
        
        switch(colorr)
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
