using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;


public class generetor : MonoBehaviour
{
    [SerializeField] private Tilemap _floormap;
    [SerializeField] private TileBase _floorBase;
    
    
    [SerializeField] private TileBase _stoneBase;
    
    [SerializeField] private Vector2Int _center;
    [SerializeField] private Vector2Int _size;
    [SerializeField] private int _perlin = 1;
    [SerializeField] private int _perlinscale = 100;


    public void Generate()
    {
        _floormap.ClearAllTiles();
        for (int x = -1 * _size.x / 2 ; x < _size.x/2 ; x++)
        {
            for (int y = -1 * _size.y / 2 ; y < _size.y/2 ; y++)
            {
                float noisecoordX = x / (float)_size.x + Random.value;
                float noisecoordy = y / (float)_size.y + Random.value;

                float rng = Mathf.PerlinNoise(noisecoordX * (_perlin * _perlinscale), noisecoordy * (_perlin * _perlinscale));

                if (rng > 0.5f)
                {
                    _floormap.SetTile(new Vector3Int(_center.x+x,_center.y+y),_floorBase);
                }
                else
                {
                    _floormap.SetTile(new Vector3Int(_center.x+x,_center.y+y),_stoneBase);
                }
                
            }
            
        }
        
    }




}
