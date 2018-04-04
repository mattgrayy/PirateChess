using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoSingleton<TileGrid> {

    [SerializeField] List<GameObject> Tiles = new List<GameObject>();

    public void flipTile(int _index, bool _inverted = false)
    {
        if(_inverted)
            Tiles[invertTileIndex(_index)].GetComponent<TileControl>().UpdateTile(true);
        else
            Tiles[_index].GetComponent<TileControl>().UpdateTile(true);
    }

    int invertTileIndex(int _index)
    {
        return (Tiles.Count-1) - _index;
    }
}
