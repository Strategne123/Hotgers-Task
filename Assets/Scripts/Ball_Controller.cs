using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball_Controller : Game_Controller
{
    Rigidbody2D rb;
    public GameObject endGame;
    float hspeed = 2;
    float vspeed = -1;
    float koef = 1;
    float lifetime = 0;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        endGame = GameObject.Find("EndGame");
        endGame.SetActive(false);  
    }
  
    void Start()
    {
        Time.timeScale = 1;
        koefUp();
    }
    
    void koefUp()
    {
        koef += 0.5f;
        Invoke("koefUp", 15);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        endGame.SetActive(true);
        endGame.GetComponent<UI_Controller>().OnActivate(lifetime);
        Time.timeScale = 0;
    }

    void Update()
    {

        lifetime += Time.deltaTime;
        rb.velocity = new Vector2(hspeed*difficulty, vspeed*up*koef);
    }
}
