using System;
using System.Collections.Generic;
using Gtk;

enum Direction { North, East, South, West }

public class Vector
{
    public int X { get; set; }
    public int Y { get; set; }
}

static class Map
{
    static Room[][] rooms;
    static Vector mapSize;
    public static int roomCount;
    public static int GetID { get { roomCount++; return roomCount - 1; } }
    
    public static void Generate(Vector size)
    {
        mapSize = size;
        
        for (int i = 0; i < mapSize.Y; i++)
        {
            for (int j = 0; j < mapSize.X; j++)
            {
                Console.WriteLine(j + ", " + i);
            }
        }
    }
}

public class Room
{
    int id;
    byte doors;
    
    public Room()
    {
        id = Map.roomCount;
        Map.roomCount++;
    }
    
    public void SetDoorStates(byte doorState)
    {
        if ((doorState & Doors.NorthLocked) != 0)
        {
            doors = (byte)(doors - (doors & Doors.NorthLocked) | doorState);
        }
        if ((doorState & Doors.EastLocked) != 0)
        {
            doors = (byte)(doors - (doors & Doors.EastLocked) | doorState);
        }
        if ((doorState & Doors.SouthLocked) != 0)
        {
            doors = (byte)(doors - (doors & Doors.SouthLocked) | doorState);
        }
        if ((doorState & Doors.WestLocked) != 0)
        {
            doors = (byte)(doors - (doors & Doors.WestLocked) | doorState);
        }
    }
    
    public byte GetDoorStates()
    {
        return doors;
    }
}

public static class Doors
{
    public const byte NorthOpen =   1;  //0b00000001;
    public const byte NorthClosed = 2;  //0b00000010;
    public const byte NorthLocked = 3;  //0b00000011;
    
    public const byte EastOpen =    4;  //0b00000100;
    public const byte EastClosed =  8;  //0b00001000;
    public const byte EastLocked =  12; //0b00001100;
    
    public const byte SouthOpen =   16; //0b00010000;
    public const byte SouthClosed = 32; //0b00100000;
    public const byte SouthLocked = 48; //0b00110000;
    
    public const byte WestOpen =    64; //0b01000000;
    public const byte WestClosed =  128;//0b10000000;
    public const byte WestLocked =  192;//0b11000000;
}