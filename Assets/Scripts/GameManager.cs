using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private int score;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private TextMeshProUGUI curScoreText;
    [SerializeField] private GameObject showHighScore;
    private void Awake()
    {
        instance = this;

        
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void Start()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        score = 0;
        gameOverText.SetActive(false);
    }

    public void AddScore()
    {
        score++;
        scoreText.text = score.ToString();
    
    }

    public void GameOver()
    {
        curScoreText.text = "Score: " + scoreText.text;
       
        if (score > PlayerPrefs.GetInt("score"))
        {
            PlayerPrefs.SetInt("score", score);
            showHighScore.SetActive(true);
        }
            
        else showHighScore.SetActive(false);
        gameOverText.SetActive(true);
        Time.timeScale = 0;
    }

    public void Restart()
    {
        SceneController.instance.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Back()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        SceneController.instance.LoadScene("Splash");
    }
}
