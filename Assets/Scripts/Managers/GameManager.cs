using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Player player;
    [SerializeField] private bool isPaused = false;
    private int levelMax = 2;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
        isPaused = !isPaused;
        //Missing UI Pause
    }

    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void WinLevel(int nextLevel)
    {
        if (nextLevel < levelMax)
        {
            PlayerPrefs.SetInt("Level_" + (nextLevel - 1), 1);
            SceneManager.LoadScene(nextLevel);
        }
    }
}
