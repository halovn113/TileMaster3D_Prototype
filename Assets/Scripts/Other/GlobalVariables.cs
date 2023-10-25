using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// define enum
public enum NodeState
{
    NONE = 0,
    AVAILABLE = 1,
    USED = 2,
}

public enum GameState
{
    NONE = 0,
    INIT = 1,
    START = 2,
    PLAYING = 3,
    END = 5,
    PAUSE = 6,
}

public enum TileState
{
    NONE = 0,
    CAN_INTERACT = 1,
    MOVING = 2,
    DONE = 3,
}

public class LAYER_NAME
{
    public static string GAMEPLAY = "GAMEPLAY";
    public static string POPUP = "POPUP";
    public static string MAIN_MENU = "MAIN_MENU";
    public static string PROTOTYPE_END = "PROTOTYPE_END";
}

public class AUDIO
{
    public static string CLICK = "CLICK";
    public static string WIN = "WIN";
    public static string LOSE = "LOSE";
    public static string PLACED = "PLACED";
    public static string CORRECT = "CORRECT";

}

public class MUSIC
{
    public static string BGM = "BGM";
    public static string MENU = "MENU";
    public static string END = "END";

}