using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

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
    public Button buttonBack;
   
    void Start()
    {
        ListaAdd();

        buttonBack = GameObject.Find("BackFirst").GetComponent<Button>();
        buttonBack.onClick.AddListener(() => FirstScene());
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
            btnNovo.GetComponentInChildren<Text>().enabled = true;

            if (!level.habilitado)
            {
                btnNovo.GetComponentInChildren<Text>().color = Color.red;
            }

            btnNovo.GetComponent<Button>().interactable = level.habilitado;

            //Lógica para acessar as fases
            btnNovo.GetComponent<Button>().onClick.AddListener(() => ClickLevel(level.levelText));
        }
    }

    void ClickLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void FirstScene()
    {
        SceneManager.LoadScene("FirstScene");
    }

    public void Awake()
    {
        Destroy(GameObject.Find("UIManager(Clone)"));
        Destroy(GameObject.Find("GameManager(Clone)"));
        localBtn = GameObject.Find("Canvas").transform.Find("Background");
    }
}
