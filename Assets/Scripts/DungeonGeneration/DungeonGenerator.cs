using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;
    private List<Vector2Int> dungeonRooms;
    public bool go;

    private void Start()
    {   
        dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms);

    }
    

    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        RoomController.instance.LoadRoom("Start", 0, 0);

        foreach (Vector2Int roomLocation in rooms)
        {

            RoomController.instance.LoadRoom(RoomController.instance.GetRandomRoomName(), roomLocation.x, roomLocation.y);
            
        }

    }

    //private void Update()
    //{
    //    if (RoomController.instance.BossIsDead())
    //    {
    //        SpawnNewLevel();
    //    }
    //}

    //public void SpawnNewLevel()
    //{
    //    dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);
    //    if(dungeonRooms == null)
    //    {
    //        Debug.Log("Нет данных о построении Подземелья");
    //    }
    //    SpawnRooms(dungeonRooms);
    //}

    


}
