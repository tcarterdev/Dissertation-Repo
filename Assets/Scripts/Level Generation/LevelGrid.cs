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
    [SerializeField] GameObject bossRoom;
    [SerializeField] GameObject[] chestRoomPrefabs;

    [Header("Generator Settings")]
    [Range(0, 100)] public int roomSpawnChance;
    public int minChestRooms;
    public int maxChestRooms;

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
        //Spawn HUB Room
        Vector2Int hubRoomPos = new Vector2Int(boolGrid.GetLength(0) / 2, boolGrid.GetLength(1) / 2);
        RoomSpawn(hubRoom, hubRoomPos);
        boolGrid[hubRoomPos.x, hubRoomPos.y] = true;

        //Spawn Boss Room
        Vector2Int bossRoomPos = GetSpawnPosition();
        RoomSpawn(bossRoom, bossRoomPos);
        boolGrid[bossRoomPos.x, bossRoomPos.y] = true;

        //Spawn Chest Room
        int numberOfChestRooms = Random.Range(minChestRooms, maxChestRooms + 1);
        for (int chestRooms = 0; chestRooms < numberOfChestRooms; chestRooms++)
        {
            Vector2Int chestRoomPos = GetSpawnPosition();
            RoomSpawn(chestRoomPrefabs[Random.Range(0, chestRoomPrefabs.Length)], chestRoomPos);
            boolGrid[chestRoomPos.x, chestRoomPos.y] = true;
        }

        for (int x = 0; x < boolGrid.GetLength(0); x++)
        {
            for (int y = 0; y < boolGrid.GetLength(1); y++)
            {
                //Check if Hub Room is Center
                if (boolGrid[x, y] != true)
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

    public bool CanSpawnHere(Vector2Int spawnPos)
    {
        if (boolGrid[spawnPos.x, spawnPos.y] == false)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public Vector2Int GetSpawnPosition()
    {
        Vector2Int bossRoomPos = new Vector2Int(Random.Range(0, boolGrid.GetLength(0)), Random.Range(0, boolGrid.GetLength(1)));
        if (CanSpawnHere(bossRoomPos))
        {
            //boolGrid[bossRoomPos.x, bossRoomPos.y] = true;
            return bossRoomPos;
        }
        else
        {

            return GetSpawnPosition();
        }

        
    }
}
