using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    public static GameManager Instance;
    private void OnEnable()
    {
        Instance = this;
    }
    private void OnDisable()
    {
        Instance = null;
    }

    public int keys = 0;
    public Text keyText;
    [SerializeField] private GameObject gameOverUi;
    private bool isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        UpdateKey();
        gameOverUi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddKeys(int key)
    {
        keys += key;
        UpdateKey();
    }
    private void UpdateKey()
    {
        keyText.text = keys.ToString();
    }
    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
        gameOverUi.SetActive(true);
    }
    public void GameCompleted()
    {
        Debug.Log("Pass");
    }
    public void RestartGame()
    {
        isGameOver = false;
        keys = 0;
        UpdateKey();
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }
}
