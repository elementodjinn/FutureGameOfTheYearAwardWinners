using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FloorGeneration2 : MonoBehaviour
{
    public Grid grid;
    public Tilemap floor;
    public Tilemap walls;
    public GameObject[] roomExits;
    public GameObject baseRoom;
    public int length = 3;
    public int height = 3;

    //These are the dimensions of the rooms, here temporarily before I add a room sc
    private int roomLength = 16;
    private int roomHeight = 16;
    private RoomModel[,] floorLayout;

    // Start is called before the first frame update
    void Start()
    {
        floorLayout = floorSet(length, height);
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 spawnPosition = new Vector2(roomLength * i, roomHeight * j);
                Object.Instantiate(baseRoom, spawnPosition, Quaternion.identity, grid.transform);
            }
        }
        Debug.Log(roomExits.Length);
        for (int i = 0; i < roomExits.Length; ++i)
        {
            Tilemap[] wallsAndFloor = roomExits[i].GetComponentsInChildren<Tilemap>();
            foreach(Tilemap t in wallsAndFloor)
            {
                int bounds = roomExits[i].GetComponent<Room>().height;
                Debug.Log(t.name);
            }
        }
    }
    void createExits(GameObject roomToEdit, RoomModel roomToMatch)
    {
        List<GameObject> viable = new List<GameObject>();
        Debug.Log(roomToMatch.eastExit);
    }
    public class RoomModel
    {

        public bool northExit { get; set; }
        public bool southExit { get; set; }
        public bool eastExit { get; set; }
        public bool westExit { get; set; }
        public RoomModel(bool n, bool s, bool e, bool w)
        {
            northExit = n;
            southExit = s;
            eastExit = e;
            westExit = w;
        }
    }

    RoomModel[,] floorSet(int length, int height)
    {
        RoomModel[,] floor = new RoomModel[length, height];
        bool[,] visited = new bool[length, height];
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < height; j++)
            {
                visited[i, j] = false;
                floor[i, j] = new RoomModel(false, false, false, false);
            }
        }
        wallDig(ref floor, ref visited, length, height, 0, 0);
        return floor;
    }


    enum directions
    {
        north, south, east, west
    }


    void wallDig(ref RoomModel[,] floor, ref bool[,] visits, int length, int height, int currentXIndex, int currentYIndex)
    {

        visits[currentXIndex, currentYIndex] = true;
        List<directions> possibleWalls = new List<directions>();
        while (true)
        {
            possibleWalls.Clear();
            if (currentXIndex != 0 && !visits[currentXIndex - 1, currentYIndex])
            {
                possibleWalls.Add(directions.west);
            }
            if (currentXIndex != length - 1 && !visits[currentXIndex + 1, currentYIndex])
            {
                possibleWalls.Add(directions.east);
            }
            if (currentYIndex != height - 1 && !visits[currentXIndex, currentYIndex + 1])
            {
                possibleWalls.Add(directions.north);
            }
            if (currentYIndex != 0 && !visits[currentXIndex, currentYIndex - 1])
            {
                possibleWalls.Add(directions.south);
            }
            if (possibleWalls.Count > 0)
            {
                int choice = Random.Range(0, possibleWalls.Count);
                if (directions.north == possibleWalls[choice])
                {
                    floor[currentXIndex, currentYIndex].northExit = true;
                    floor[currentXIndex, currentYIndex + 1].southExit = true;
                    wallDig(ref floor, ref visits, length, height, currentXIndex, currentYIndex + 1);
                }
                if (directions.south == possibleWalls[choice])
                {
                    floor[currentXIndex, currentYIndex].southExit = true;
                    floor[currentXIndex, currentYIndex - 1].northExit = true;
                    wallDig(ref floor, ref visits, length, height, currentXIndex, currentYIndex - 1);
                }
                if (directions.east == possibleWalls[choice])
                {
                    floor[currentXIndex, currentYIndex].eastExit = true;
                    floor[currentXIndex + 1, currentYIndex].westExit = true;
                    wallDig(ref floor, ref visits, length, height, currentXIndex + 1, currentYIndex);
                }
                if (directions.west == possibleWalls[choice])
                {
                    floor[currentXIndex, currentYIndex].westExit = true;
                    floor[currentXIndex - 1, currentYIndex].eastExit = true;
                    wallDig(ref floor, ref visits, length, height, currentXIndex - 1, currentYIndex);
                }
            }
            else
            {
                break;
            }
        }
    }
}
