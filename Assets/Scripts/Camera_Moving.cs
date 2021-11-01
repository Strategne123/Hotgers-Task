using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Moving : MonoBehaviour
{
    public GameObject ball;
    
    public void Start()
    {
        ball = Instantiate(ball);
        ball.name = "Ball";
    }
    private void Update() 
    {
        transform.position = new Vector3(ball.transform.position.x, 0, transform.position.z);
    }
}
