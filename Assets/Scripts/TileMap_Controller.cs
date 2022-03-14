using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TileMap_Controller : Game_Controller
{
    private Tilemap tileMap;
    private int obstacleX=0;
    private Vector3Int gridPos;
    private const int WindowWidth = 16;
    private Material material;
    private Vector2 offset = Vector2.zero;
    private readonly Dictionary<int,int> obstacles=new Dictionary<int, int>();

    [SerializeField]
    private int distance;
    [SerializeField]
    private TileBase wall;
    [SerializeField]
    private Transform ball_pos;
    [SerializeField]
    private GameObject background;

    private void Start()
    {
        material = background.GetComponent<Renderer>().material;
        offset = material.GetTextureOffset("_MainTex");
        tileMap = GetComponent<Tilemap>();
        for (var x = -WindowWidth; x < WindowWidth; x++)
        {
            tileMap.SetTile(new Vector3Int(x, -5, 0), wall);
            tileMap.SetTile(new Vector3Int(x, 4, 0), wall);
        }
    }

    void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {
            offset.x += hspeed*Time.fixedDeltaTime;
            material.SetTextureOffset("_MainTex",offset);
            gridPos = tileMap.WorldToCell(ball_pos.position);
            if(gridPos.x % distance == 0 && gridPos.x != obstacleX)
            {
                ObstacleChange();
            }
        }
    }

    /// <summary>
    /// Создает новые препядствия, удаляет старые
    /// </summary>
    private void ObstacleChange()
    {
        obstacleX = gridPos.x;
        var y = Random.Range(-4, 4);
        tileMap.SetTile(new Vector3Int(gridPos.x + WindowWidth, y, 0), wall);
        obstacles.Add(gridPos.x + WindowWidth, y);

        //если препядствие вышло за левую границу окна
        if (obstacles.TryGetValue(gridPos.x - WindowWidth, out y))
        {
            var x = gridPos.x - WindowWidth;
            tileMap.SetTile(new Vector3Int(x, y, 0), null);
            obstacles.Remove(x);
        }
    }
}
