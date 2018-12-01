using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject scoreMenu;

    private void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("Menu"));
        if (PlayerPrefs.GetInt("Menu") == 1)
        {
            scoreMenu.SetActive(true);
            mainMenu.SetActive(false);
        }
    }

    public void PlayGame()
    {
        PlayerPrefs.SetInt("Menu", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
