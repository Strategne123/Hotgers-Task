using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class Ball_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private float score = 0;
    private float coefficient = 1;

    [SerializeField]
    private float vspeed;


    private void Awake()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("4462951", false);
        }
        if (PlayerPrefs.GetInt("difficulty") != 0)
        {
            difficulty = PlayerPrefs.GetInt("difficulty");
        }
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Start()
    {
        Time.timeScale = 1;
        up = 1;
        StartCoroutine(CoefficientInc());   
    }

    private void FixedUpdate()
    {
        score+=difficulty;
        rb.velocity = new Vector2(0, vspeed * up * coefficient);
    }

    private IEnumerator CoefficientInc()
    {
        coefficient += 0.5f;
        yield return new WaitForSeconds(10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var attempts = PlayerPrefs.GetInt("attempts",0);
        if ((attempts % 3 == 0) && (attempts>2) && Advertisement.IsReady())
        {
            Camera.current.GetComponent<AudioSource>().volume = 0;
            Advertisement.Show("Interstitial_Android");
        }
        UI_Game.OnDeath(score);
        Time.timeScale = 0;
    }
}
