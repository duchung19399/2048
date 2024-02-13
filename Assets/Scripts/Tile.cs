using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour {
    public TileState _state { get; private set; }
    public TileCell _cell { get; private set; }
    public int number { get; private set; }

    [SerializeField] private Image backgroundImage;
    [SerializeField] private TextMeshProUGUI numberText;

    public void SetState(TileState state, int number) {
        _state = state;
        this.number = number;
        numberText.text = number.ToString();

        backgroundImage.color = state.backgroundColor;
        numberText.color = state.textColor;
    }

    public void SetCell(TileCell cell) {
        if (_cell != null) {
            _cell._tile = null;
        }
        _cell = cell;
        _cell._tile = this;
        transform.position = cell.transform.position;
    }
}
