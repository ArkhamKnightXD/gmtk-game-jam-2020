using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    public GameController gameController;


    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);

        gameController.GameOverByFall();
    }
}
