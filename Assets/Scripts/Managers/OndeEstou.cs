using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OndeEstou : MonoBehaviour
{

    public static OndeEstou instance;

    public int fase = -1;
    [SerializeField] private GameObject UiManagerGO = null, GameManagerGo = null, LevelManagerGo = null;

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

        //PlayerPrefs.DeleteAll();
        SceneManager.sceneLoaded += VerificaFase;
    }


    void VerificaFase(Scene cena, LoadSceneMode modo)
    {
        fase = SceneManager.GetActiveScene().buildIndex;
        
        if(fase == 1)
        {
            Instantiate(LevelManagerGo);
        }

        //Até o index 1 serão scenes que não são fase(pode mudar isso a qualquer momento, então tem que mudar aqui)
        if(SceneManager.GetActiveScene().buildIndex > 1)
        {
            fase = Int16.Parse(SceneManager.GetActiveScene().name);
            Instantiate(UiManagerGO);
            Instantiate(GameManagerGo);
        }

    }
}
