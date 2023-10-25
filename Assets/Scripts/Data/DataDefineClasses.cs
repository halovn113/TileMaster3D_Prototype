using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileData
{
    public int Id;
    public Sprite Sprite;
    public int Chances;
}

[System.Serializable]
public class Map
{
    public string Name;
    public string DisplayName;
    public int Level;
    public int Time;
    public List<TileData> ListTileData;
}
