using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private Image pauseOptions;
    private Button menuLevels;
    private Button sound;
    private Button play;

    private bool isPaused;
    private Button pauseButton;
    private Animator animatorPause;

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
        pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
        pauseOptions = GameObject.Find("PauseOptions").GetComponent<Image>();
        menuLevels = pauseOptions.transform.Find("MenuLevels").GetComponent<Button>();
        sound = pauseOptions.transform.Find("Sound").GetComponent<Button>();
        play = pauseOptions.transform.Find("Play").GetComponent<Button>();
        animatorPause = GameObject.Find("PauseOptions").GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            animatorPause.Play("PauseOptionsReversed");
            isPaused = false;
        }
        else
        {
            animatorPause.Play("PauseOptions");
            Time.timeScale = 0;
            isPaused = true;
        }
    }

    public void MenuLevels()
    {
        GameManager.instance.LoadLevel("MenuLevels");
    }

    public void FirstScene()
    {
        GameManager.instance.LoadLevel("FirstScene");
    }

    public void Mute()
    {
        //Mutar o jogo
    }
    
    public void Play()
    {
        Time.timeScale = 1;
        animatorPause.Play("PauseOptionsReversed");
        isPaused = false;
    }
}
