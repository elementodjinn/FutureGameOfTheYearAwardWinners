using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class FloorGeneration : MonoBehaviour
{
    public Grid grid;
    public GameObject[] rooms;
    public int length = 3;
    public int height = 3;
    public GameObject endLevel;

    //These are the dimensions of the rooms, here temporarily before I add a room sc
    private int roomLength = 16;
    private int roomHeight = 16;
    private RoomModel[,] floorLayout;

    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        floorLayout = floorSet(length, height);
        for(int i = 0; i < length; i++)
        {
            for(int j = 0; j < height; j++)
            {
                Vector2 spawnPosition = new Vector2(roomLength * i, roomHeight * j);
                List<GameObject> viableRooms = allViableRooms(rooms, floorLayout[i, j]);
                int room = Random.Range(0, viableRooms.Count-1);
//                Object.Instantiate(viableRooms[room], spawnPosition, Quaternion.identity, grid.transform);
                GameObject spawnedRoom = PhotonNetwork.Instantiate(Path.Combine("Prefab","Rooms", viableRooms[room].name), spawnPosition, Quaternion.identity, 0);
                Room r = spawnedRoom.GetComponent<Room>();
                if (i==0 && j==0)
                {
                    r.roomType = RoomType.SpawnRoom;
                }
                else if(i==length - 1 && j == height -1)
                {
                    r.roomType = RoomType.BossRoom;
                    endLevel.transform.position = r.transform.position;
                }
                else
                {
                    r.roomType = RoomType.EnemyRoom;
                    Transform[] t = new Transform[1];
                    t[0] = r.transform;
                    r.enemySpawnPoints = t;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //This makes a list of all the rooms you put in the room set that have the correct exits.
    List<GameObject> allViableRooms(GameObject[] roomSet, RoomModel roomToMatch)
    {
        List<GameObject> viable = new List<GameObject>();
        foreach(GameObject roomPrefab in roomSet)
        {
            Room room = roomPrefab.GetComponent<Room>();
            if(roomToMatch.northExit == room.northExit &&
                roomToMatch.southExit == room.southExit &&
                roomToMatch.eastExit == room.eastExit &&
                roomToMatch.westExit == room.westExit)
            {
                viable.Add(roomPrefab);
            }
        }
        return viable;
    }

    //The RoomModel class. It's essentially a tuple of 4 booleans, but allowing you to change them.
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

    //Initializes, and returns the floor layout
    RoomModel[,] floorSet(int length, int height)
    {
        RoomModel[,] floor = new RoomModel[length, height];
        bool[,] visited = new bool[length, height];
        for(int i = 0; i<length; i++)
        {
            for(int j = 0; j<height; j++)
            {
                visited[i, j] = false;
                floor[i, j] = new RoomModel(false, false, false, false);
            }
        }
        wallDig(ref floor,  ref visited, length, height, 0, 0);
        return floor;
    }


    enum directions
    {
        north, south, east, west
    }

    //Creates the maze, using the room Models.
    void wallDig(ref RoomModel[,] floor, ref bool[,] visits, int length, int height, int currentXIndex, int currentYIndex)
    {

        visits[currentXIndex, currentYIndex] = true;
        List<directions> possibleWalls = new List<directions>();
        while(true)
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
