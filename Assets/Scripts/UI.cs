using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : Game_Controller
{
    [SerializeField]
    private Button[] buttons;

    private void Start()
    {
        buttons[difficulty - 1].GetComponent<Button>().Select();
    }

    public void SetDifficulty(int _difficulty)
    {
        difficulty = _difficulty;
        PlayerPrefs.SetInt("difficulty", _difficulty);
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