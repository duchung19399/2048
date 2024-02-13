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

    private bool isMoving = false;

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
        if(isMoving) return;
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

    private void MoveTiles(Vector2Int direction, int startX, int xStep, int startY, int yStep) {
        bool isChanged = false;
        for (int y = startY; y < _grid.height && y >= 0; y += yStep) {
            for (int x = startX; x < _grid.width && x >= 0; x += xStep) {
                TileCell cell = _grid.GetCell(x, y);
                if (cell.Occupied) {
                    isChanged |= MoveSingleTile(cell, direction);
                }
            }
        }

        if(isChanged) {
            isMoving = true;
            StartCoroutine(WaitForMove());
        }
    }

    private IEnumerator WaitForMove() {
        yield return new WaitForSeconds(0.2f);
        isMoving = false;
    }

    private bool MoveSingleTile(TileCell cell, Vector2Int direction) {
        TileCell newCell = null;
        TileCell adjacentCell = _grid.GetAdjacentCell(cell, direction);

        while (adjacentCell != null) {
            if (adjacentCell.Occupied) {
                //Merge cell

                break;
            }

            newCell = adjacentCell;
            adjacentCell = _grid.GetAdjacentCell(newCell, direction);
        }

        if (newCell != null) {
            cell._tile.MoveTo(newCell);
            return true;
        }
        return false;
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
