using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeContainer : MonoBehaviour
{
    [Header("Node information")]
    public Node originalNode;
    public int numberNode;
    public float distance;

    [Header("Objects")]
    public Node[] listNodes;
    private Dictionary<int, int> _tileInPlaceInfo;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        _tileInPlaceInfo = new Dictionary<int, int>();
        // init nodes
        // get margin left 
        float marginLeft = 0 - ((numberNode / 2) * distance);
        listNodes = new Node[numberNode];

        for (int i = 0; i < numberNode; i++)
        {
            Node newNode = Instantiate(originalNode, transform);
            newNode.name = "Node_" + i;
            Vector3 newPosition = new Vector3(marginLeft + (i * distance), 0.001f, 0);
            newNode.transform.localPosition = newPosition;
            listNodes[i] = newNode;
        }
    }

    public void ResetGame()
    {
        foreach (Node node in listNodes)
        {
            node.ResetNode();
        }
        _tileInPlaceInfo.Clear();
    }

    public Node GetNode()
    {
        foreach (Node node in listNodes)
        {
            if (node.currentTile == null && node.state == NodeState.AVAILABLE)
            {
                return node;
            }
        }
        return null;
    }

    public void CheckNode(int id)
    {
        if (!_tileInPlaceInfo.ContainsKey(id))
        {
            _tileInPlaceInfo.Add(id, 1);
        }
        else
        {
            _tileInPlaceInfo[id] += 1;
            if (_tileInPlaceInfo[id] == MasterInstance.GameManager.numberForEachTile)
            {
                MasterInstance.GameManager.UpdatePoint();
                RemoveCompleteTilesFromNodes(id);
            }
        }
   
    }

    public void RemoveCompleteTilesFromNodes(int id)
    {
        int totalRemove = 0;
        foreach (Node node in listNodes)
        {
            Node nodeCheck = node;
            if (nodeCheck.currentTileID == id && totalRemove < MasterInstance.GameManager.numberForEachTile)
            {
                nodeCheck.currentTile.SetTileState(TileState.DONE); // hide current tile of node
                nodeCheck.ResetNode();
                totalRemove++;
            }
            if (totalRemove == MasterInstance.GameManager.numberForEachTile)
            {
                break;
            }
        }
        _tileInPlaceInfo.Remove(id);

        MasterInstance.AudioManager.PlayEffect(AUDIO.CORRECT);
    }
}
