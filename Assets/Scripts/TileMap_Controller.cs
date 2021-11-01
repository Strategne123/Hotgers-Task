using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMap_Controller : MonoBehaviour
{
    Tilemap tilemap;
    public TileBase wall;
    public int n;   //интервал между препядствиями
    int leftSide=16; //ширина окна для создания тайлов
    int[,] map;
    Transform ball_pos;
    Vector3Int gridPos;
    int obstacle_x=0;
    Dictionary<int,int> obstacles=new Dictionary<int, int>(); //сохранение в памяти позиций препядствий

    void ChangeTile(Vector3Int pos)
     {

         tilemap.SetTile(new Vector3Int(pos.x - leftSide, -5, 0), null);
         tilemap.SetTile(new Vector3Int(pos.x - leftSide, 4, 0), null);
         tilemap.SetTile(new Vector3Int(pos.x + leftSide, -5, 0), wall);
         tilemap.SetTile(new Vector3Int(pos.x + leftSide, 4, 0), wall);
     }

    void ObstacleChange()//создание и удаление рандомных препядствий
    {
        obstacle_x = gridPos.x; //позиция х текущего препядствия
        int y = Random.Range(-4, 4);
        tilemap.SetTile(new Vector3Int(gridPos.x + leftSide, y, 0), wall);
        obstacles.Add(gridPos.x + leftSide, y); 
        int x;

        if (obstacles.TryGetValue(gridPos.x - leftSide, out y)) //если препядствие вышло за левую границу окна
        {
            x = gridPos.x - leftSide;
            tilemap.SetTile(new Vector3Int(x, y, 0), null);
            obstacles.Remove(x);
        }
    }

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        ball_pos = GameObject.Find("Ball").transform;
        for (int x = -leftSide; x < leftSide; x++)
        {
            tilemap.SetTile(new Vector3Int(x, -5, 0), wall);
            tilemap.SetTile(new Vector3Int(x, 4, 0), wall);
        }
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            gridPos = tilemap.WorldToCell(ball_pos.position);
            ChangeTile(gridPos); 
            if(gridPos.x%n==0 && gridPos.x!=obstacle_x)  //создание препядствия раз в n тайлов
            {
                ObstacleChange();
            }
        }
    }
}
