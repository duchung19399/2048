using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileState", menuName = "2048/TileState", order = 0)]
public class TileState : ScriptableObject {
    public Color backgroundColor;
    public Color textColor;
}
