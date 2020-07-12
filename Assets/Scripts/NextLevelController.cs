using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelController : MonoBehaviour
{

    public GameController gameController;
    int activeSceneIndex;


    void Awake()
    {
        activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        
        SceneManager.LoadScene(activeSceneIndex+1);            
        
    }
}
