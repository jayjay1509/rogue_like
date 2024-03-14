using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;



    public class draw
    {
        
        public static Vector2Int[] _vonNeuannNeighbours =
        {
            new Vector2Int(0, 1),
            new Vector2Int(1, 0),
            new Vector2Int(-1, 0),
            new Vector2Int(0, -1)
        };
        
        public static Vector2Int[] MooreNeighbours =
        {
            new Vector2Int(0, 1),
            new Vector2Int(1, 1),
            new Vector2Int(1, 0),
            new Vector2Int(1, -1),
            new Vector2Int(0, -1),
            new Vector2Int(-1, -1),
            new Vector2Int(-1, 0),
            new Vector2Int(-1, 1)
        };
        

        public static void Drawmaps(Tilemap map,TileBase _base,HashSet<Vector2Int> positions)
        {
            map.ClearAllTiles();

            foreach (Vector2Int position in positions)
            {
                map.SetTile(new Vector3Int(position.x,position.y),_base);
            }
        } 
    }
