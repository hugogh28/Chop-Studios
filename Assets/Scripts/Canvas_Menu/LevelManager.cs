using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BotonPlay()
    {
        SceneManager.LoadScene(3);
    }
    public void BotonExit()
    {
        Application.Quit();
    }
    public void Menu()
    {
        SceneManager.LoadScene(1);
    }

}
