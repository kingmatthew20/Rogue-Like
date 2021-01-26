using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    North,
    East,
    South,
    West
}

public class Room : MonoBehaviour
{
    public bool NorthDoor = false;
    public bool EastDoor = false;
    public bool SouthDoor = false;
    public bool WestDoor = false;

    public int OpenDoorCount
    {
        get
        {
            int count = 0;
            if (NorthDoor) count += 1;
            if (EastDoor) count += 1;
            if (SouthDoor) count += 1;
            if (WestDoor) count += 1;
            return count;
        }
    }

    [SerializeField] Vector2Int gridPos;
    public Vector2Int GridPos { get { return gridPos; } }

    public void Spawn(Vector2Int position)
    {
        gridPos = position;
        NorthDoor = NorthDoor || Random.value < 0.5f;
        EastDoor = EastDoor || Random.value < 0.5f;
        SouthDoor = SouthDoor || Random.value < 0.5f;
        WestDoor = WestDoor || Random.value < 0.5f;
    }

    public void ForEachDoor(System.Action<Direction, bool, Vector2Int> fn)
    {
        fn(Direction.North, NorthDoor, GridPos + Vector2Int.up);
        fn(Direction.East, EastDoor, GridPos + Vector2Int.right);
        fn(Direction.South, SouthDoor, GridPos + Vector2Int.down);
        fn(Direction.West, WestDoor, GridPos + Vector2Int.left);
    }
}
