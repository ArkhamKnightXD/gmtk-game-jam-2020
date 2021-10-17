using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryController : MonoBehaviour
{
    private string _currentSceneName;

    private void Start()
    {
        _currentSceneName = SceneManager.GetActiveScene().name;
    }

    private void OnMouseDown()
    {
        SceneManager.LoadScene(_currentSceneName);
    }
}
