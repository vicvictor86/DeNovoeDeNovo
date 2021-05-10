using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField]private Image pauseOptions;
    [SerializeField]private Button menuLevels;
    private Button sound;
    private Button play;

    private bool isPaused;
    private bool isMuted;
    public Sprite mute, desmute; 

    [SerializeField]private Button pauseButton;
    [SerializeField]private Animator animatorPause;

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
        GetInfo();
    }

    void GetMenu()
    {
        pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
        pauseOptions = GameObject.Find("PauseOptions").GetComponent<Image>();
        menuLevels = pauseOptions.transform.Find("MenuLevels").GetComponent<Button>();
        sound = pauseOptions.transform.Find("Sound").GetComponent<Button>();
        play = pauseOptions.transform.Find("Play").GetComponent<Button>();
        animatorPause = GameObject.Find("PauseOptions").GetComponent<Animator>();

        ClicksButtons();
    }

    void GetInfoSceneLoad(Scene scene, LoadSceneMode mode)
    {
        GetMenu();
    }

    void GetInfo()
    {
        GetMenu();

        SceneManager.sceneLoaded += GetInfoSceneLoad;
    }

    public void ClicksButtons()
    {
        pauseButton.onClick.AddListener(() => PauseGame());
        menuLevels.onClick.AddListener(() => MenuLevels());
        sound.onClick.AddListener(() => Mute());
        play.onClick.AddListener(() => Play());      
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
        Time.timeScale = 1;
        GameManager.instance.LoadLevel("MenuLevels");
    }

    

    public void Mute()
    {
        Play();
        if (isMuted)
        {
            GameObject.Find("PauseOptions").transform.Find("Sound").GetComponent<Image>().sprite = desmute;
            GameObject.Find("Main Camera").GetComponent<AudioSource>().mute = false;
            isMuted = false;
        }
        else
        {
            GameObject.Find("PauseOptions").transform.Find("Sound").GetComponent<Image>().sprite = mute;
            GameObject.Find("Main Camera").GetComponent<AudioSource>().mute = true;
            isMuted = true;
        }
    }
    
    public void Play()
    {
        Time.timeScale = 1;
        animatorPause.Play("PauseOptionsReversed");
        isPaused = false;
    }
}
