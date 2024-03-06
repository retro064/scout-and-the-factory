using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    public enum PlayerState
    {
        idle,
        walk,
        jump,
        interact,
        attack
    }
}
