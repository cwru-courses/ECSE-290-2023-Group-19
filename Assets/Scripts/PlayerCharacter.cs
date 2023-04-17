using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    RelativeMovement relativeMovement;

    private Animator _animator;

    private bool canMove = true;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public bool CanMove()
    {
        return canMove;

    }

    void Update()
    {
    }
}

