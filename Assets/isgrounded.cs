using System;
using System.Collections;
using System.Collections.Generic;
using Platformer;
using UnityEngine;

public class isgrounded : MonoBehaviour
{

    private PlayerController Controller;
    private void Start()
    {
        Controller = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Terrain"))
        {
            Controller.isGrounded = true;
            Debug.Log("ta touche");
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Terrain"))
        {
            Controller.isGrounded = false;
        }
    }
   
}
