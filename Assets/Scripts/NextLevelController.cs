using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelController : MonoBehaviour
{
    public GameController gameController;

    private int _activeSceneIndex;


    private void Awake()
    {
        _activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CompareTag("EndGame"))
        {
            other.gameObject.SetActive(false);
            gameController.Win();    
        }
        else
            SceneManager.LoadScene(_activeSceneIndex+1);            
        
    }
}
