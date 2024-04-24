using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Door : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent(out IPlayer player))
        {
            player.DoorInteraction(true);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.TryGetComponent(out IPlayer player))
        {
            player.DoorInteraction(false);
        }
    }
}
