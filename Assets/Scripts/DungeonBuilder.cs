using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PotentialRoom
{
    float weight = 100.0f;
    public Vector2Int pos;

    public PotentialRoom(DungeonBuilder builder, Vector2Int pos)
    {
        this.pos = pos;

        for (int x = pos.x - 1; x < 3; x++)
        {
            for(int y = pos.y - 1; y < 3; y++)
            {
                Room room = builder.GetRoom(pos + new Vector2Int(x, y));
                if (!room) continue;

                weight += room.OpenDoorCount;
            }
        }
    }
}

public class DungeonBuilder : MonoBehaviour
{
    public const int ROOM_GRID_SIZE = 22;
    public const int ROOM_GRID_DIMENSIONS = 101;

    public Room roomPrefab;

    Room[,] rooms = new Room[ROOM_GRID_DIMENSIONS, ROOM_GRID_DIMENSIONS];

    // Start is called before the first frame update
    void Start()
    {
        List<PotentialRoom> potentialRooms = new List<PotentialRoom>();

        Room spawnRoom = SpawnRoom(Vector2Int.one * (ROOM_GRID_DIMENSIONS / 2));
        spawnRoom.ForEachDoor((_, open, pos) =>
        {
            if (open) potentialRooms.Add(new PotentialRoom(this, pos));
        });

        for(int i = 0; i < 50 && potentialRooms.Count > 0; i++)
        {
            int index = Random.Range(0, potentialRooms.Count);
            var potential = potentialRooms[index];
            Room room = SpawnRoom(potential.pos);
            potentialRooms.RemoveAt(index);
            room.ForEachDoor((_, open, doorPos) =>
            {
                // If door is open and connected room is not in potential rooms already, add it as a potential
                if (!open || !IsPositionValid(doorPos) || GetRoom(doorPos) != null) return;

                var match = potentialRooms.Find(r => r.pos == doorPos);
                if (match == null) potentialRooms.Add(new PotentialRoom(this, doorPos));
            });
        }

        int dupes = 0;
        foreach(Room room in rooms)
        {
            if (!room) continue;
            foreach(Room room2 in rooms)
            {
                if (!room2) continue;
                if (room2 == room) continue;
                if (room.GridPos == room2.GridPos) dupes += 1;
            }
        }

        Debug.Log(dupes);
    }

    public bool IsPositionValid(Vector2Int pos)
    {
        return !(pos.x < 0 || pos.x > ROOM_GRID_DIMENSIONS - 1 || pos.y < 0 || pos.y > ROOM_GRID_DIMENSIONS - 1);
    }

    public Room GetRoom(Vector2Int pos)
    {
        if(!IsPositionValid(pos)) return null;
        return rooms[pos.x, pos.y];
    }

    public Vector3 GetWorldPosition(Vector2Int pos)
    {
        return new Vector3(pos.x * ROOM_GRID_SIZE, 0.0f, pos.y * ROOM_GRID_SIZE);
    }

    Room SpawnRoom(Vector2Int pos)
    {
        Room room = Instantiate(roomPrefab, GetWorldPosition(pos), Quaternion.identity);
        rooms[pos.x, pos.y] = room;
        room.Spawn(pos);
        return room;
    }
}
