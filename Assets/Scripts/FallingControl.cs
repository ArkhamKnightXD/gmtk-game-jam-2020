using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingControl : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.tag = "PlayerSuicide"; 

            Destroy(gameObject);
        }
    }
}
