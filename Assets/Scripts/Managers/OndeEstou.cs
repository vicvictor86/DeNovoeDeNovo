using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OndeEstou : MonoBehaviour
{

    public static OndeEstou instance;

    public int fase = -1;

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

        SceneManager.sceneLoaded += VerificaFase;
    }


    void VerificaFase(Scene cena, LoadSceneMode modo)
    {
        //Até o index 1 serão scenes que não são fase(pode mudar isso a qualquer momento, então tem que mudar aqui)
        if(SceneManager.GetActiveScene().buildIndex > 1)
        {
            fase = Int16.Parse(SceneManager.GetActiveScene().name);
        }

        //Vai ser usado dps quando começar a criar o UImanager e GameManager
        if (fase != 0 && fase != 1 && fase != 2 && fase != 7)
        {
            //Instantiate(UiManagerGO);
            //Instantiate(GameManagerGo);
            //Camera.main.projectionMatrix = Matrix4x4.Ortho(-orthoSize * aspect, orthoSize * aspect, -orthoSize, orthoSize, Camera.main.nearClipPlane, Camera.main.farClipPlane);
        }
    }
}
