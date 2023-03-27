using System.Collections;
using System.Collections.Generic;
// using System.Linq;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    private LevelGrid levelGrid;
    private GenerationSeed generationSeed;

    [Header("Generator Settings")]
    [Range(0, 100)] public int roomSpawnChance;
    public int minChestRooms;
    public int maxChestRooms;

    [Header("Room Prefabs")]
    [SerializeField] GameObject hubRoomPrefabs;
    [SerializeField] GameObject[] standardRoomsPrefabs;
    [SerializeField] GameObject bossRoomPrefabs;
    [SerializeField] GameObject[] chestRoomPrefabs;
    [SerializeField] GameObject hallwayPrefab;


    [Header("Doors & Hallways")]
    public float maxHallWayRange;
    public LayerMask doorsLayer;
    private List<Door> doors;

    private void Awake()
    {
        generationSeed = GetComponent<GenerationSeed>();
        levelGrid = GetComponent<LevelGrid>();
        doors = new List<Door>();

        if (generationSeed.randomSeed) {
            generationSeed.GenerateRandomSeed();
        }
        generationSeed.SetSeed(generationSeed.seed);

        levelGrid.GridSetUp();
        GenerateRoomMap();
        GetNeighborDoors();
        GenerateHallWays();
        bool hasGhostRooms = CheckForGhostRooms();

        if (hasGhostRooms) {
            
        }
    }

    private void GenerateRoomMap()
    {
        // Spawn HUB Room
        Vector2Int hubRoomPos = new Vector2Int(levelGrid.roomGrid.GetLength(0) / 2, levelGrid.roomGrid.GetLength(1) / 2);
        Room hubRoom = RoomSpawn(hubRoomPrefabs, hubRoomPos);
        levelGrid.roomGrid[hubRoomPos.x, hubRoomPos.y] = hubRoom;

        // Spawn Boss Room
        Vector2Int bossRoomPos = GetSpawnPosition();
        Room bossRoom = RoomSpawn(bossRoomPrefabs, bossRoomPos);
        levelGrid.roomGrid[bossRoomPos.x, bossRoomPos.y] = bossRoom;

        // Spawn Chest Rooms
        int numberOfChestRooms = Random.Range(minChestRooms, maxChestRooms + 1);
        for (int chestRooms = 0; chestRooms < numberOfChestRooms; chestRooms++)
        {
            Vector2Int chestRoomPos = GetSpawnPosition();
            Room chestRoom = RoomSpawn(chestRoomPrefabs[Random.Range(0, chestRoomPrefabs.Length)], chestRoomPos);
            levelGrid.roomGrid[chestRoomPos.x, chestRoomPos.y] = chestRoom;
        }

        // Loop through all tiles and spawn the remaining rooms
        for (int x = 0; x < levelGrid.roomGrid.GetLength(0); x++)
        {
            for (int y = 0; y < levelGrid.roomGrid.GetLength(1); y++)
            {
                //Check if Hub Room is Center
                if (levelGrid.roomGrid[x, y] != true)
                {
                    int randnum = Random.Range(0, 100);
                    if (randnum <= roomSpawnChance)
                    {
                        GameObject roomToSpawn = standardRoomsPrefabs[Random.Range(0, standardRoomsPrefabs.Length)];
                        Room newroom = RoomSpawn(roomToSpawn, new Vector2Int(x, y));
                        levelGrid.roomGrid[x,y] = newroom;
                    }
                }
            }
        }
    }

    private Room RoomSpawn(GameObject room, Vector2Int posInGrid)
    {
        
        Vector3 roomWorldPos = new Vector3(posInGrid.x * levelGrid.cellSize, 0f, posInGrid.y * levelGrid.cellSize);

        Room newRoom = Instantiate(room, roomWorldPos, room.transform.rotation, this.transform).GetComponent<Room>();
        // newRoom.transform.position = roomWorldPos;
        foreach (Door d in newRoom.doors)
        {
            doors.Add(d);
        }
        return newRoom;
    }

    public Vector2Int GetSpawnPosition()
    {
        Vector2Int bossRoomPos = new Vector2Int(Random.Range(0, levelGrid.roomGrid.GetLength(0)), Random.Range(0, levelGrid.roomGrid.GetLength(1)));
        if (levelGrid.CanSpawnHere(bossRoomPos))
        {
            //boolGrid[bossRoomPos.x, bossRoomPos.y] = true;
            return bossRoomPos;
        }
        else
        {

            return GetSpawnPosition();
        }
    }

    private void GetNeighborDoors()
    {
        foreach(Door d in doors)
        {
            d.CheckForNeighbor(doorsLayer, maxHallWayRange);
        }

        doors.Clear();
        Door[] doorsAray = GetComponentsInChildren<Door>();
        foreach(Door d in doorsAray)
        {
            doors.Add(d);
        }
    }

    private void GenerateHallWays()
    {
        foreach(Door door in doors)
        {
            if (door == null || door.neighborDoor == null) {
                continue;
            }

            //Vector3 midPoint = (door.transform.position - (door.neighboringDoorPosition.position / 2f));

            int cellsToDoor = (int)Vector3.Distance(door.transform.position, door.neighborDoor.position);
            Vector3 direction = (door.transform.position - door.neighborDoor.position).normalized;
            for (int cell = 0; cell < cellsToDoor; cell++)
            {
                Vector3 pos = door.transform.position - (direction * cell);
                GameObject newCell = Instantiate(hallwayPrefab, pos, door.transform.rotation, this.transform);
            }
        }
    }

    private bool CheckForGhostRooms()
    {
        for (int x = 0; x < levelGrid.roomGrid.GetLength(0); x++)
        {
            for (int y = 0; y < levelGrid.roomGrid.GetLength(1); y++)
            {
                // 
                if (levelGrid.roomGrid[x, y] != false)
                {
                    Room room = levelGrid.roomGrid[x,y];
                    Door[] roomDoors = room.transform.GetComponentsInChildren<Door>();
                    Debug.Log(roomDoors.Length);

                    if (roomDoors.Length == 0) {
                        return true;
                    }
                }
            }
        }

        // Didn't find any ghost rooms in the grid
        return false;
    }
}
