using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FIrstScene : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Level1", 1);
    }

    public void LevelScene()
    {
        SceneManager.LoadScene("MenuLevels");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
