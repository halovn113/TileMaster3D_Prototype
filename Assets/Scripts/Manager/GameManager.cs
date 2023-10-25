using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    [Header("Objects")]
    public NodeContainer nodeContainer;
    public GameObject tileParent;
    public SpawnBox spawnBox;
    public Tile tilePrefab;

    [Header("UI")]
    public UIManager uiManager;

    public GameState state;

    protected List<Tile> tilesPool;

    [Header("Data")]
    public LevelDataSO levelData;

    [Header("Values")]
    public int numberForEachTile;
    public int currentLevel;

    // private 
    private int _currentLevelIndex;
    private float _maxTime;
    private float _timer;
    private int _totalTile;

    // Start is called before the first frame update
    void Start()
    {
        MasterInstance.GameManager = this;
        state = GameState.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.PLAYING)
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }
            else
            {
                _timer = 0;
                EndGame(false);
            }
            uiManager.GetLayer(LAYER_NAME.GAMEPLAY).GetComponent<UIGameplay>().progressBar.current = _timer;
        }
    }

    public void NextLevel()
    {
        if (_currentLevelIndex < levelData.Maps.Count - 1)
        {
            _currentLevelIndex++;
            currentLevel = levelData.Maps[_currentLevelIndex].Level;
            StartLevel(currentLevel);
        }
        else
        {
            uiManager.ShowLayerAndHideOther(LAYER_NAME.PROTOTYPE_END);
        }
    }

    public void ResetCurrentLevel()
    {
        StartLevel(currentLevel);
    }

    public void StartLevel(int level)
    {
        //Camera.main.GetComponent<ScreenKeeper>().ScaleCamInGame();
        MasterInstance.AudioManager.PlayMusic(MUSIC.BGM);

        currentLevel = level;
        for (int i = 0; i < levelData.Maps.Count; i++)
        {
            if (levelData.Maps[i].Level == currentLevel)
            {
                _currentLevelIndex = i;
                break;
            }
        }
        uiManager.ShowLayerAndHideOther(LAYER_NAME.GAMEPLAY);
        InitTiles();
        InitLevel();
    }

    public void MoveTileToNode(Tile tile)
    {
        Node node = nodeContainer.GetNode();

        if (node != null) 
        {
            node.SetTileToNode(tile);
            tile.SetTargetAndMove(node);
        }
        else
        {
            EndGame(false);
        }
    }

    public void InitLevel()
    {
        state = GameState.START;

        _maxTime = levelData.Maps[_currentLevelIndex].Time;

        uiManager.GetLayer(LAYER_NAME.GAMEPLAY).GetComponent<UIGameplay>().progressBar.max = _maxTime;
        _timer = _maxTime;

        uiManager.HideLayer(LAYER_NAME.POPUP);
        uiManager.GetLayer(LAYER_NAME.GAMEPLAY).GetComponent<UIGameplay>().SetLevelName(levelData.Maps[_currentLevelIndex].DisplayName);
        nodeContainer.ResetGame();

        StartCoroutine(DelayStartGame());
    }

    IEnumerator DelayStartGame()
    {
        yield return new WaitForSeconds(0.5f);
        state = GameState.PLAYING;

        foreach (Tile tile in tilesPool)
        {
            if (tile.gameObject.activeSelf)
            {
                tile.ResetTile();
            }
        }
    }

    public void InitTiles()
    {
        var map = levelData.Maps[_currentLevelIndex];
        var listTileData = map.ListTileData;

        if (tilesPool == null)
        {
            tilesPool = new List<Tile>();
        }

        // calculate total tile for level
        _totalTile = 0;
        foreach (TileData data in listTileData)
        {
            _totalTile += data.Chances * numberForEachTile;
        }
        // if tile in pool is not enough for level, spawn more 
        if (_totalTile > tilesPool.Count)
        {
            int tileLeft = _totalTile - tilesPool.Count;
            for (int i = 0; i < tileLeft; i++)
            {
                Tile tile = Instantiate(tilePrefab, tileParent.transform);
                tilesPool.Add(tile);
                tile.gameObject.SetActive(false);
            }
        }

        int index = 0;

        foreach (TileData data in listTileData) // total tile always equals or lesser than tile pool so don't need to worry about of out pool
        {
            for (int i = 0; i < numberForEachTile * data.Chances; i++)
            {
                tilesPool[index].gameObject.SetActive(true);
                tilesPool[index].ID = data.Id;
                tilesPool[index].SetSprite(data.Sprite);
                tilesPool[index].state = TileState.NONE;
                spawnBox.SpawnObjectFromRandomPosition(tilesPool[index].gameObject);
                index++;
            }
        }

    }

    public bool CheckAllTileDone()
    {
        foreach (Tile tile in tilesPool)
        {
            if (tile.state != TileState.DONE)
            {
                return false;
            }
        }
        return true;
    }

    public void UpdatePoint()
    {
        if (CheckAllTileDone())
        {
            EndGame(true);
        }
    }

    public void EndGame(bool win)
    {
        state = GameState.END;
        uiManager.ShowLayerAndHideOther(LAYER_NAME.POPUP);
        uiManager.GetLayer(LAYER_NAME.POPUP).GetComponent<UIPopupManager>().ShowResult(win, CheckRankCondition());
    }

    public int CheckRankCondition()
    {
        int rank = 0;
        if (_timer >= _maxTime * 2/3)
        {
            rank = 3;
        }
        else if (_timer < _maxTime * 2/3 && _timer >= _maxTime * 1/3)
        {
            rank = 2;
        }
        else
        {
            rank = 1;
        }
        return rank;
    }

}
