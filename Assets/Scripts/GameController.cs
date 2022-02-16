using UnityEngine;
using UnityEngine.SceneManagement;

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

    private const string GameOverTag = "PlayerIsDead";

    private int _activeSceneIndex;


    private void Start()
    {
        AudioController.Instance.PlaySoundEffect(AudioController.SoundEffect.GameSong);

        gameOverText.SetActive(false);

        retryText.SetActive(false);

        winText.SetActive(false);

        _activeSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (_activeSceneIndex == 2)
        {
            InvokeRepeating("MakeControlFallFromTheSky", 0, 0.35f);   
        }

    }


    private void Update()
    {
        DecrementTime();
    }


    private void MakeControlFallFromTheSky()
    {
        if (!player.CompareTag(GameOverTag))
        {
            Instantiate(fallingControlPrefab, new Vector3(Random.Range(minimumX, maximumX),maximumControlFallingHeight,0), Quaternion.identity);   
        }
    }


    private void DecrementTime()
    { 
        if (!player.CompareTag(GameOverTag) && !player.CompareTag("Finish"))
        {
            currentTime = currentTime > 0 ? currentTime - 1 * Time.deltaTime : 0;
        
            timerText.text = currentTime.ToString("0");   
        }


        if (currentTime == 0 && !player.CompareTag(GameOverTag))
        {
            gameOverText.SetActive(true);

            retryText.SetActive(true);

            AudioController.Instance.PlaySoundEffect(AudioController.SoundEffect.GameOver);

            player.tag = GameOverTag;            
        }
    }


    public void GameOverByFall()
    {
        gameOverText.SetActive(true);

        retryText.SetActive(true);

        AudioController.Instance.PlaySoundEffect(AudioController.SoundEffect.GameOver);

        player.tag = GameOverTag;
    }


    public void Win()
    {
        winText.SetActive(true);

        retryText.SetActive(true);

        AudioController.Instance.PlaySoundEffect(AudioController.SoundEffect.Win);

        player.tag = "Finish";
    }
}
