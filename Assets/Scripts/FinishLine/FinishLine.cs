using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private readonly string PLAYER_TAG = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == PLAYER_TAG)
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                player.StartDancing();
            }
        }
    }
}