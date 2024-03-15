using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activecolider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Stone"))
        {
           other.gameObject.SetActive(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Stone"))
        {
            other.gameObject.SetActive(false);
        }
    }
    
}
