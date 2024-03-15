using System;
using System.Collections;
using System.Collections.Generic;
using Platformer;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;


public class StarterAssetsInputs : MonoBehaviour
{
    
    
    public int cameraNumber;
    
    public bool Jump;
    public bool Minnig;
    public float Move;
    public bool CheckUp;
    public bool CheckDown;
    
    
    private PlayerController _player;
    
    private void Start()
    {
        _player = GetComponentInChildren<PlayerController>();
    }
    
    
    
    public void OnJump(InputValue value)
    {
        Jump = value.isPressed;
    }
    
    public void OnMinnig(InputValue value)
    {
        Minnig = value.isPressed;
    }
    
    public void OnMove(InputValue value)
    {
        Move = value.Get<float>();
    }
    
    public void OnCheckUp(InputValue value)
    {
        CheckUp = value.isPressed;
    }
    
    public void OnCheckDown(InputValue value)
    {
        CheckDown = value.isPressed;
    }
    
    
    
    

    
    
    
    
    
}
