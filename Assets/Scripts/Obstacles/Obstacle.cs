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
        switch (type)
        {
            case Type.Red:
                if (GameManager.instance.playerController.red)
                {
                    axe.SetActive(false);
                }
                else
                {
                    axe.SetActive(true);
                }
                break;

            case Type.Green:
                if (GameManager.instance.playerController.green)
                {
                    axe.SetActive(false);
                }
                else
                {
                    axe.SetActive(true);
                }
                break;

            case Type.Blue:
                if (GameManager.instance.playerController.blue)
                {
                    axe.SetActive(false);
                }
                else
                {
                    axe.SetActive(true);
                }
                break;

            case Type.Orange:
                if (GameManager.instance.playerController.orange)
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

    private void OnTriggerEnter(Collider other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null && other.tag == PLYAER_TAG)
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
    }
}