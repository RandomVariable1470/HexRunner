using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Type
{
    Red,
    Green,
    Blue,
    Orange
}
public class Obstacle : MonoBehaviour
{
    [SerializeField] private Type type;
    [SerializeField] private GameObject axe;

    private readonly string PLYAER_TAG = "Player";

    private void Update()
    {
       ColorStatementUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerReference controller = other.GetComponent<PlayerReference>();

        if (controller != null && other.tag == PLYAER_TAG)
        {
            ColorStatmentTrigger(controller);
        }
    }

    #region ColorStatements

    private void ColorStatmentTrigger(PlayerReference controller)
    {
        switch (type)
        {
            case Type.Red:
                if (controller.red)
                {
                    return;
                }
                else
                {
                    GameManager.instance.RestartLevel();
                }
                break;

            case Type.Green:
                if (controller.green)
                {
                    return;
                }
                else
                {
                    GameManager.instance.RestartLevel();
                }
                break;

            case Type.Blue:
                if (controller.blue)
                {
                    return;
                }
                else
                {
                    GameManager.instance.RestartLevel();
                }
                break;

            case Type.Orange:
                if (controller.orange)
                {
                    return;
                }
                else
                {
                    GameManager.instance.RestartLevel();
                }
                break;
        }
    }
    private void ColorStatementUpdate()
    {
        switch (type)
        {
            case Type.Red:
                if (GameManager.instance.playerRef.red)
                {
                    axe.SetActive(false);
                }
                else
                {
                    axe.SetActive(true);
                }
                break;

            case Type.Green:
                if (GameManager.instance.playerRef.green)
                {
                    axe.SetActive(false);
                }
                else
                {
                    axe.SetActive(true);
                }
                break;

            case Type.Blue:
                if (GameManager.instance.playerRef.blue)
                {
                    axe.SetActive(false);
                }
                else
                {
                    axe.SetActive(true);
                }
                break;

            case Type.Orange:
                if (GameManager.instance.playerRef.orange)
                {
                    axe.SetActive(false);
                }
                else
                {
                    axe.SetActive(true);
                }
                break;

        }
    }
    #endregion
}