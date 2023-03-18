using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{

    public bool[,] boolGrid;
    [SerializeField] float cellSize;
    [SerializeField] int gridSize;

    [Header("Room Prefabs")]
    [SerializeField] GameObject hubRoom;
    [SerializeField] GameObject[] standardRooms;

    [Header("Generator Settings")]
    [Range(0, 100)] public int roomSpawnChance;

    private void Start()
    {
        GridSetUp();
        GenerateMap();
    }
    private void RoomSpawn(GameObject room, Vector2Int posInGrid)
    {
        Debug.Log(posInGrid);
       GameObject newRoom = Instantiate(room);
        Vector3 roomWorldPos = new Vector3(posInGrid.x * cellSize, 0f, posInGrid.y * cellSize);
        newRoom.transform.position = roomWorldPos;
    }
    private void GridSetUp()
    {
        boolGrid = new bool[gridSize - 1, gridSize - 1];
    }

    private void GenerateMap()
    {
        for (int x = 0; x < boolGrid.GetLength(0); x++)
        {
            for (int y = 0; y < boolGrid.GetLength(1); y++)
            {
                //Check if Hub Room is Center
                if (x == boolGrid.GetLength(0) / 2 && y == boolGrid.GetLength(1) / 2)
                {
                    RoomSpawn(hubRoom, new Vector2Int(x,y));
                }
                else //Spawn Standard Room
                {
                    int randnum = Random.Range(0, 100);
                    if (randnum <= roomSpawnChance)
                    {
                        GameObject roomToSpawn = standardRooms[Random.Range(0, standardRooms.Length)];
                        RoomSpawn(roomToSpawn, new Vector2Int(x, y));
                    }
                   
                }
            }

        }
    }

    //private void ondrawgizmosselected()
    //{
    //    gridsetup();

    //    for (int x = 0; x < boolgrid.getlength(0); x++)
    //    {
    //        for (int y = 0; y < boolgrid.getlength(0); y++)
    //        {
    //            if (boolgrid[x, y] == true)
    //            {
    //                gizmos.color = color.red;

    //            }
    //            else
    //            {
    //                gizmos.color = color.green;
    //            }
    //            vector3 cellpos = new vector3(x * cellsize, 0f, y * cellsize);
    //            gizmos.drawsphere(cellpos, 5);
    //        }
    //    }
    //}
}
