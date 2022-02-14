using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TileMap_Controller : Game_Controller
{
    private Tilemap tilemap;
    private int obstacle_x=0;
    private Vector3Int gridPos;
    private int windowWidth = 16;
    private Material material;
    private Vector2 offset = Vector2.zero;
    [SerializeField] private GameObject background;
    private Dictionary<int,int> obstacles=new Dictionary<int, int>();
    [SerializeField] private int distance;
    [SerializeField] private TileBase wall;
    [SerializeField] private Transform ball_pos;

    private void Start()
    {
        material = background.GetComponent<Renderer>().material;
        offset = material.GetTextureOffset("_MainTex");
        tilemap = GetComponent<Tilemap>();
        for (var x = -windowWidth; x < windowWidth; x++)
        {
            tilemap.SetTile(new Vector3Int(x, -5, 0), wall);
            tilemap.SetTile(new Vector3Int(x, 4, 0), wall);
        }
    }

    void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {
            offset.x += hspeed*Time.fixedDeltaTime;
            material.SetTextureOffset("_MainTex",offset);
            gridPos = tilemap.WorldToCell(ball_pos.position);
            if(gridPos.x % distance == 0 && gridPos.x != obstacle_x)
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
        obstacle_x = gridPos.x;
        var y = Random.Range(-4, 4);
        tilemap.SetTile(new Vector3Int(gridPos.x + windowWidth, y, 0), wall);
        obstacles.Add(gridPos.x + windowWidth, y);

        //если препядствие вышло за левую границу окна
        if (obstacles.TryGetValue(gridPos.x - windowWidth, out y))
        {
            var x = gridPos.x - windowWidth;
            tilemap.SetTile(new Vector3Int(x, y, 0), null);
            obstacles.Remove(x);
        }
    }
}
