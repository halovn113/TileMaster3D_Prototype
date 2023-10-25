using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node : MonoBehaviour
{
    [HideInInspector]
    public NodeState state;
    [HideInInspector]
    public Tile currentTile;
    public int currentTileID;
    

    public void SetTileToNode(Tile tile)
    {
        currentTile = tile;
        currentTileID = tile.ID;
        state = NodeState.USED;
    }

    public void ResetNode()
    {
        currentTile = null;
        currentTileID = -1;
        state = NodeState.AVAILABLE;
    }
}
