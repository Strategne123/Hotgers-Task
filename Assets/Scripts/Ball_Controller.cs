using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class Ball_Controller : Game_Controller
{
    private Rigidbody2D rb;
    private float koef = 1;
    private float lifetime = 0;
    [SerializeField] private float vspeed;

    private void Awake()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("4462951", false);
        }
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Start()
    {
        Time.timeScale = 1;
        StartCoroutine(CoefficientInc());   
    }

    private void FixedUpdate()
    {
        lifetime++;
        rb.velocity = new Vector2(0, vspeed * up * koef);
        var tempPosition = Camera.main.transform.position;
        tempPosition.x = transform.position.x;
        Camera.main.transform.position = tempPosition;

    }

    private IEnumerator CoefficientInc()
    {
        koef += 0.5f;
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
        UI_Game.OnDeath(lifetime);
        Time.timeScale = 0;
    }
}
