using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCell : MonoBehaviour {
    public Vector2Int _coordinates {get; set;}
    public Tile _tile {get; set;}

    public bool Empty => _tile == null;
    public bool Occupied => !Empty;
}
