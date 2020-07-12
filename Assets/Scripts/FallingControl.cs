using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingControl : MonoBehaviour
{
    string suicideTag = "PlayerSuicide";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.tag = suicideTag; 

            Destroy(gameObject);
        }
    }
}
