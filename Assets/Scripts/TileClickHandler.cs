using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileClickHandler : MonoBehaviour {

    public int tileX;
    public int tileY;
    public TileMap map;

    // displays path to tile that was clicked on
    private void OnMouseUp () {
        Debug.Log("Click");
        map.GeneratePathTo(tileX, tileY, map.unitSelector);
    }
}
