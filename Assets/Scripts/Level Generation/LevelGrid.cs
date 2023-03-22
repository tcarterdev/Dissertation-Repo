using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{

    public Room[,] roomGrid;
    [SerializeField] float cellSize;
    [SerializeField] int gridSize;

    [Header("Room Prefabs")]
    [SerializeField] GameObject hubRoomPrefabs;
    [SerializeField] GameObject[] standardRoomsPrefabs;
    [SerializeField] GameObject bossRoomPrefabs;
    [SerializeField] GameObject[] chestRoomPrefabs;

    [Header("Doors and Hallways")]
    public List<Door> doors;
    [SerializeField] private LayerMask doorLayer;
    [SerializeField] private float maxNeighborDistance;

    [Header("Generator Settings")]
    [Range(0, 100)] public int roomSpawnChance;
    public int minChestRooms;
    public int maxChestRooms;

    private void Start()
    {
        GridSetUp();
        GenerateRoomMap();
        RemoveNullDoors();
        GenerateHallwayMap();
        


    }
    private Room RoomSpawn(GameObject room, Vector2Int posInGrid)
    {
        Debug.Log(posInGrid);
        Room newRoom = Instantiate(room, this.transform, true).GetComponent<Room>();
        Vector3 roomWorldPos = new Vector3(posInGrid.x * cellSize, 0f, posInGrid.y * cellSize);
        newRoom.transform.position = roomWorldPos;
        return newRoom;
    }
    private void GridSetUp()
    {
        roomGrid = new Room[gridSize - 1, gridSize - 1];
    }

    private void GenerateRoomMap()
    {
        //Spawn HUB Room
        Vector2Int hubRoomPos = new Vector2Int(roomGrid.GetLength(0) / 2, roomGrid.GetLength(1) / 2);
       Room hubRoom = RoomSpawn(hubRoomPrefabs, hubRoomPos);
        roomGrid[hubRoomPos.x, hubRoomPos.y] = hubRoom;

        //Spawn Boss Room
        Vector2Int bossRoomPos = GetSpawnPosition();
        Room bossRoom = RoomSpawn(bossRoomPrefabs, bossRoomPos);
        roomGrid[bossRoomPos.x, bossRoomPos.y] = bossRoom;

        //Spawn Chest Room
        int numberOfChestRooms = Random.Range(minChestRooms, maxChestRooms + 1);
        for (int chestRooms = 0; chestRooms < numberOfChestRooms; chestRooms++)
        {
            Vector2Int chestRoomPos = GetSpawnPosition();
            Room chestRoom = RoomSpawn(chestRoomPrefabs[Random.Range(0, chestRoomPrefabs.Length)], chestRoomPos);
            roomGrid[chestRoomPos.x, chestRoomPos.y] = chestRoom;
        }

        for (int x = 0; x < roomGrid.GetLength(0); x++)
        {
            for (int y = 0; y < roomGrid.GetLength(1); y++)
            {
                //Check if Hub Room is Center
                if (roomGrid[x, y] != true)
                {
                    int randnum = Random.Range(0, 100);
                    if (randnum <= roomSpawnChance)
                    {
                        GameObject roomToSpawn = standardRoomsPrefabs[Random.Range(0, standardRoomsPrefabs.Length)];
                        Room newroom = RoomSpawn(roomToSpawn, new Vector2Int(x, y));
                        roomGrid[x,y] = newroom;
                    }

                }
            }

        }
    }

    public bool CanSpawnHere(Vector2Int spawnPos)
    {
        if (roomGrid[spawnPos.x, spawnPos.y] == null)
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
        Vector2Int bossRoomPos = new Vector2Int(Random.Range(0, roomGrid.GetLength(0)), Random.Range(0, roomGrid.GetLength(1)));
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

    public void SpawnHallway()
    { 
        
    }

    public void GenerateHallwayMap()
    {
        for (int x = 0; x < roomGrid.GetLength(0); x++)
        {
            for (int y = 0; y < roomGrid.GetLength(1); y++)
            {
               
                if (roomGrid[x, y] == null)
                {
                    continue;
                }
                //Destroy Edge Doors
                if (x == 0)
                {

                    Destroy(roomGrid[x, y].doors[3].gameObject);
                   
                }
                else if (x == roomGrid.GetLength(0) -1 )
                {
                    Destroy(roomGrid[x, y].doors[1].gameObject);
                }

                //Create North and South Border
                if (y == 0)
                {

                    Destroy(roomGrid[x, y].doors[2].gameObject);
                    
                }
                else if (y == roomGrid.GetLength(0) -1)
                {
                    Destroy(roomGrid[x, y].doors[0].gameObject);
                }

                //Add Doors to List
                foreach (Transform door in roomGrid[x, y].doors)
                {
                    if (door != null)
                    {
                        doors.Add(door.GetComponent<Door>());

                    }

                    
                }

            }
        }


    }

    public void RemoveNullDoors()
    {
        foreach (Door door in doors)
        {
            Debug.Log("1");
            door.CheckForNeighbor(doorLayer, maxNeighborDistance);

        }
    }
}
