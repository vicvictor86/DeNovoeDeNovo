using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIrstScene : MonoBehaviour
{

    [SerializeField] private Animator animatorOptions;
    private bool optionClicked = false;

    // Start is called before the first frame update
    void Start()
    {
        animatorOptions = GameObject.Find("OptionsPane").GetComponent<Animator>();
        PlayerPrefs.SetInt("Level1", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenOptions()
    {
        if (optionClicked)
        {
            animatorOptions.Play("AnimationOptionPaneReverse");
            optionClicked = false;
        }
        if (!optionClicked)
        {
            animatorOptions.Play("AnimationOptionPane");
            optionClicked = true;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
