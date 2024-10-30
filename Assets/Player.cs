using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement movement;


    void Awake()
    {
        CharacterManager.Instance.Player = this;
        movement = GetComponent<PlayerMovement>();
    }

}
