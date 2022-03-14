using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class UI_Game : UI
{
    private Button button;
    private GameObject info;
    private static UI_Game instance;

    [SerializeField]
    private Text time;
    [SerializeField]
    private Text record;
    [SerializeField]
    private GameObject endGame;
    [SerializeField]
    private GameObject goButton;
    
    private void Start()
    {
        instance = this;
        info = goButton.transform.GetChild(0).gameObject;
        button = goButton.GetComponent<Button>();
        Invoke("HideInfo",2);
    }

    private void HideInfo()
    {
        info.SetActive(false);
    }

    public void MoveCat(int gravity)
    {
        up = gravity;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public static void OnDeath(float lifetime)
    {
        instance.endGame.SetActive(true);
        instance.button.gameObject.SetActive(false);
        var attempts = PlayerPrefs.GetInt("attempts",0);
        var _record = PlayerPrefs.GetFloat("record");
        PlayerPrefs.SetInt("attempts", ++attempts);
        if(lifetime>_record)
        {
            PlayerPrefs.SetFloat("record", lifetime);
            instance.record.text = "NEW RECORD: " + lifetime.ToString();
        }
        else
        {
            instance.record.text = "BEST SCORE: " + _record.ToString();
        }
        instance.time.text= "YOUR RESULT: " + lifetime.ToString();
    }
}
