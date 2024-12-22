using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static bool isGameOver;

    public GameObject gameOverScreen;
    public static Vector2 lastCheckPointPos = new Vector2(-3, 0); 
    public Vector2 startPosition; 

    public static int numberOfCoins;
    public TextMeshProUGUI coinsText;

    public GameObject pauseMenuScreen;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex != PlayerPrefs.GetInt("LastLoadedScene", -1))
        {
            numberOfCoins = 0; 
            PlayerPrefs.SetInt("NumberOfCoins", 0);

            lastCheckPointPos = startPosition; 
            PlayerPrefs.SetInt("LastLoadedScene", SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            numberOfCoins = PlayerPrefs.GetInt("NumberOfCoins", 0);
        }

        isGameOver = false;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = lastCheckPointPos;
        }
    }

    private void Update()
    {
        coinsText.text = numberOfCoins.ToString();

        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
        }
    }

    public void ReplayLevel()
    {
        isGameOver = false;
        gameOverScreen.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenuScreen.SetActive(true);
    }
}
