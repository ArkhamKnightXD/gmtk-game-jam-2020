using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;

    public float minimumX;

    public float maximumX;

    public GameObject gameOverText;

    public GameObject retryText;

    public GameObject winText;

    public TextMesh timerText;

    public float currentTime;

    public GameObject fallingControlPrefab;

    public int maximumControlFallingHeight;

    string gameOverTag = "PlayerIsDead";


    void Start()
    {
        AudioController.Instance.PlaySoundEffect(AudioController.SoundEffect.GameSong);

        gameOverText.SetActive(false);

        retryText.SetActive(false);

        winText.SetActive(false);

       // InvokeRepeating("MakeControlFallFromTheSky", 0, 1.1f);
    }

    void Update()
    {
        DecrementTime();

    }


    void MakeControlFallFromTheSky()
    {
        if (!player.CompareTag(gameOverTag))
        {
            Instantiate(fallingControlPrefab, new Vector3(Random.Range(minimumX, maximumX),maximumControlFallingHeight,0), Quaternion.identity);   
        }
    }


    public float DecrementTime()
    { 
        if (!player.CompareTag(gameOverTag))
        {
            currentTime = currentTime > 0 ? currentTime - 1 * Time.deltaTime : 0;
        
            timerText.text = currentTime.ToString("0");   
        }


        if (currentTime == 0 && !player.CompareTag(gameOverTag))
        {

            gameOverText.SetActive(true);

            retryText.SetActive(true);

            AudioController.Instance.PlaySoundEffect(AudioController.SoundEffect.GameOver);

            player.tag = gameOverTag;            
        }
       
        return currentTime;
    }


    public void GameOverByFall()
    {
        gameOverText.SetActive(true);

        retryText.SetActive(true);

        AudioController.Instance.PlaySoundEffect(AudioController.SoundEffect.GameOver);

        player.tag = gameOverTag;
    }


    public void Win()
    {
        winText.SetActive(true);

        retryText.SetActive(true);

        AudioController.Instance.PlaySoundEffect(AudioController.SoundEffect.Win);
    }
}
