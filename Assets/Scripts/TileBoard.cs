using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static GameInput;

public class TileBoard : MonoBehaviour {
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private TileState[] _tileStates;
    private TileGrid _grid;
    private List<Tile> _tiles;

    private void Awake() {
        _grid = GetComponentInChildren<TileGrid>();
        _tiles = new List<Tile>(16);
    }

    private void Start() {
        GameInput.Instance.OnMoveAction += GameInput_OnMoveAction;

        CreateTile();
        CreateTile();
    }

    private void GameInput_OnMoveAction(object sender, GameInput.OnMoveEventArgs e) {
        PlayerAction action = e.action;
        switch (action) {
            case PlayerAction.Up:
                MoveTiles(Vector2Int.up, 0, 1, 1, 1);
                break;
            case PlayerAction.Down:
                MoveTiles(Vector2Int.down, 0, 1, _grid.height - 2, -1);
                break;
            case PlayerAction.Left:
                MoveTiles(Vector2Int.left, 1, 1, 0, 1);
                break;
            case PlayerAction.Right:
                MoveTiles(Vector2Int.right, _grid.width - 2, -1, 0, 1);
                break;
        }
    }

    private void MoveTiles(Vector2Int direction, int startX, int startY, int xStep, int yStep) {
        for(int y = startY; y < _grid.height && y >= 0; y += yStep) {
            for(int x = startX; x < _grid.width && x >= 0; x += xStep) {
                TileCell cell = _grid.GetCell(x, y);
                if(cell != null) {
                    MoveSingleTile(cell, direction);
                }
            }
        }
    }

    private void MoveSingleTile(TileCell cell, Vector2Int direction) {

    }

    private void OnDestroy() {
        GameInput.Instance.OnMoveAction -= GameInput_OnMoveAction;
    }

    private void CreateTile() {
        Tile tile = Instantiate(tilePrefab, _grid.transform);
        tile.SetState(_tileStates[0], 2);
        tile.SetCell(_grid.GetRandomEmptyCell());
        _tiles.Add(tile);
    }

}
