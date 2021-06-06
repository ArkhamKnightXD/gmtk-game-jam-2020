using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    public GameController gameController;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") || other.CompareTag("PlayerSuicide"))
        {
            gameController.GameOverByFall();

            other.gameObject.SetActive(false);            
        }

        else
            Destroy(other.gameObject);
    }
}
