using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{

    public Room[,] roomGrid;
    public float cellSize;
    public int gridSize;

    public void GridSetUp()
    {
        roomGrid = new Room[gridSize - 1, gridSize - 1];
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
}
