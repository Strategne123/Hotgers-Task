using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Controller : Game_Controller
{

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SetDifficulty(int dif)
    {
        difficulty=dif;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void UpPress()
    {
        up = -1;
    }

    public void UpUnpress()
    {
        up = 1;
    }
    
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void OnActivate(float lifetime)
    {
        int attempts=0;
        attempts = PlayerPrefs.GetInt("attempts");
        attempts++;
        PlayerPrefs.SetInt("attempts", attempts);
        float record = PlayerPrefs.GetFloat("record");
        if(lifetime>record)
        {
            GameObject.Find("Record").GetComponent<Text>().text = "Новый рекорд: " + lifetime;
            PlayerPrefs.SetFloat("record", lifetime);
        }
        else
        {
            GameObject.Find("Record").GetComponent<Text>().text = "Рекорд: " + record;
        }
        GameObject.Find("Time").GetComponent<Text>().text= "Продолжтельность последней попытки: " + lifetime + " с";
        GameObject.Find("Attempts").GetComponent<Text>().text = "Попыток совершено: " + attempts;
        

    }
}
