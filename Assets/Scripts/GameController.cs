using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;

    public GameObject gameOverText;

    public GameObject retryText;

    public GameObject winText;

    public TextMesh timerText;

    public float currentTime;


    void Start()
    {
        AudioController.Instance.PlaySoundEffect(AudioController.SoundEffect.GameSong);

        gameOverText.SetActive(false);

        retryText.SetActive(false);

        winText.SetActive(false);
    }

    void Update()
    {
        DecrementTime();
    }


    public float DecrementTime()
    { 
        if (!player.CompareTag("PlayerIsDead"))
        {
            currentTime = currentTime > 0 ? currentTime - 1 * Time.deltaTime : 0;
        
            timerText.text = currentTime.ToString("0");   
        }


        if (currentTime == 0 && !player.CompareTag("PlayerIsDead"))
        {

            gameOverText.SetActive(true);

            retryText.SetActive(true);

            AudioController.Instance.PlaySoundEffect(AudioController.SoundEffect.GameOver);

            player.tag = "PlayerIsDead";            
        }
       
        return currentTime;
    }


    public void GameOverByFall()
    {
        gameOverText.SetActive(true);

        retryText.SetActive(true);

        AudioController.Instance.PlaySoundEffect(AudioController.SoundEffect.GameOver);

        player.tag = "PlayerIsDead";
    }


    public void Win()
    {
        winText.SetActive(true);

        retryText.SetActive(true);

        AudioController.Instance.PlaySoundEffect(AudioController.SoundEffect.Win);
    }
}
