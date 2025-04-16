using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private Slider progressBar;
    [SerializeField] private Text levelText;
    void Start()
    {
        progressBar.value = 0;
        levelText.text = "Level : " + (ChunkManager.instance.GetLevels() + 1).ToString();

        GameManager.onGameStateChanged += GameStateChangedCallBack;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallBack;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateProgressBar();
    }

    private void GameStateChangedCallBack(GameManager.GameState gameState)
    {
        if(gameState == GameManager.GameState.GameOver)
        {
            Show_GameOver_Panel();
        }
        else if(gameState == GameManager.GameState.LevelComplete)
        {
            Show_LevelComplete_Panel();
        }
    }

    public void Play_Button_Pressed()
    {
        GameManager.instance.SetGameState(GameManager.GameState.Game);
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void Retery_Button_Pressed()
    {
        SceneManager.LoadScene(1);
    }
    public void Complete_Button_Pressed()
    {
        SceneManager.LoadScene(1);
    }
    public void Show_GameOver_Panel()
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);

    }
    public void Show_LevelComplete_Panel()
    {
        gamePanel.SetActive(false);
        levelCompletePanel.SetActive(true);

    }

    public void UpdateProgressBar()
    {
        if (!GameManager.instance.IsGameState())
        {
            return;
        }
        float progress =PlayerController.instance.transform.position.z/ChunkManager.instance.GetFinshZ();
        progressBar.value = progress;
    }


}
