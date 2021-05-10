using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Player player;
    [SerializeField] private bool isPaused = false;
    private int levelMax = 13;

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
       // if(SceneManager.GetActiveScene().buildIndex > 1)
       // {
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
       // }
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
    }

    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void WinLevel(int nextLevel)
    {
        if (nextLevel < levelMax)
        {
            PlayerPrefs.SetInt("Level" + nextLevel, 1);
            SceneManager.LoadScene(nextLevel.ToString());
        }
    }
}
