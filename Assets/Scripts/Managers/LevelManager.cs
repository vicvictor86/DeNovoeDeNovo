using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public GameObject botao;
    public Transform localBtn;
   
    void Start()
    {
        ListaAdd();
    }

    void ListaAdd()
    {
        foreach (Level level in levelList)
        {
            //Criação do botão
            GameObject btnNovo = Instantiate(botao) as GameObject;
            btnNovo.transform.SetParent(localBtn, false);

            //Características do botão 
            if (PlayerPrefs.GetInt("Level" + level.levelText) == 1)
            {
                level.desbloqueado = 1;
                level.habilitado = true;
            }

            btnNovo.GetComponentInChildren<Text>().text = level.levelText;
            btnNovo.GetComponentInChildren<Text>().enabled = level.habilitado;
            btnNovo.GetComponent<Button>().interactable = level.habilitado;

            //Lógica para acessar as fases
            btnNovo.GetComponent<Button>().onClick.AddListener(() => ClickLevel(level.levelText));
        }
    }

    void ClickLevel(string level)
    {
        GameManager.instance.LoadLevel(level);
    }

    
}
