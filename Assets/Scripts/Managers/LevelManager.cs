using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

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

    [System.Serializable]
    public class Level
    {
        public string levelText;
        public bool habilitado;
        public int desbloqueado;
    }

    public List<Level> levelList;
    private int levelMax = 2;

    void Start()
    {
        
    }

    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void WinLevel(int nextLevel)
    {
        if(nextLevel < levelMax)
        {
            PlayerPrefs.SetInt("Level_" + (nextLevel - 1), 1);
            SceneManager.LoadScene(nextLevel);
        }
    }
}
