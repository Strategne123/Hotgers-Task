using UnityEngine;

public class Game_Controller : MonoBehaviour
{
    protected float hspeed=0.1f;
    protected static int up = 1;
    protected static int difficulty = 1;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("difficulty") != 0)
        {
            difficulty = PlayerPrefs.GetInt("difficulty");
        }
    }

}
