using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : Game_Controller
{
    private void Start()
    {
        var buttons = GameObject.FindGameObjectsWithTag("difficulty");
        buttons[difficulty - 1].GetComponent<Button>().Select();
    }

    public void SetDifficulty(int dif)
    {
        difficulty = dif;
        PlayerPrefs.SetInt("difficulty", dif);
        Play();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Info()
    {
        Application.OpenURL("https://strategne123.github.io/Task/");
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}