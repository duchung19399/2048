using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour {
    public TileRow[] _rows { get; private set; }
    public TileCell[] _cells { get; private set; }

    public int size => _cells.Length;
    public int height => _rows.Length;
    public int width => size / height;

    private void Awake() {
        _rows = GetComponentsInChildren<TileRow>();
        _cells = GetComponentsInChildren<TileCell>();
    }

    private void Start() {
        for (int y = 0; y < _rows.Length; y++) {
            for (int x = 0; x < _rows[y]._cells.Length; x++) {
                _rows[y]._cells[x]._coordinates = new Vector2Int(x, y);
            }
        }
    }

    public TileCell GetRandomEmptyCell() {
        int index = Random.Range(0, _cells.Length);
        int startIndex = index;
        while(_cells[index].Occupied) {
            index++;
            if(index >= _cells.Length) {
                index = 0;
            }
            if(index == startIndex) {
                return null;
            }
        }
        return _cells[index];
    }

    public TileCell GetCell(int x, int y) {
        if(x < 0 || x >= width || y < 0 || y >= height) {
            return null;
        }
        return _rows[y]._cells[x];
    }

    public TileCell GetCell(Vector2Int coordinates) {
        return GetCell(coordinates.x, coordinates.y);
    }

    public TileCell GetAdjacentCell(TileCell cell, Vector2Int direction) {
        Vector2Int coordinates = cell._coordinates;
        coordinates.x += direction.x;
        coordinates.y -= direction.y;
        return GetCell(coordinates);
    }
}
