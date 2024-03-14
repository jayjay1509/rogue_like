using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

[ExecuteInEditMode]
public class gameoflife_generetor : MonoBehaviour
{
    private BoundsInt _startZone;
    [Header("Tilemap")] [SerializeField] private Tilemap _floormap;
    [SerializeField] private Tilemap _floormap_1;
    [SerializeField] private Tilemap _floormap_2;
    [SerializeField] private Tilemap _floormap_3;
    [SerializeField] private Tilemap _floormap_4;
    [SerializeField] private Tilemap _floormap_final;

    [Header("Tilebase")] [SerializeField] private TileBase _floorBase;
    [SerializeField] private TileBase _floorBase_1;
    [SerializeField] private TileBase _floorBase_2;
    [SerializeField] private TileBase _floorBase_3;
    [SerializeField] private TileBase _floorBase_4;
    


    [SerializeField] private Vector2Int _center;
    [SerializeField] private Vector2Int _size;
    
    [Header("mineral")]
    [SerializeField] [Range(0, 10)] private int _perlin = 1;
    [SerializeField] [Range(0, 1000)] private int _perlinscale = 100;
    [SerializeField] [Range(0, 1)] private float _magic;
    
    [Header("granit")]
    [SerializeField] [Range(0, 10)] private int _perlin_granit_y = 1;
    [SerializeField] [Range(0, 1000)] private int _perlinscale_granit_y = 100;
    [SerializeField] [Range(0, 10)] private int _perlin_granit_x = 1;
    [SerializeField] [Range(0, 1000)] private int _perlinscale_granit_x = 100;
    [SerializeField]  private float _magic_granit;

    [Header("gameoflife")] [SerializeField]
    private int[] avlive;

    [SerializeField] private int[] dead;
    [SerializeField] private int tentative = 1;

    [Header("gameoflife_1")] [SerializeField]
    private int[] avlive_1;

    [SerializeField] private int[] dead_1;
    [SerializeField] private int tentative_1 = 1;

    [Header("gameoflife_2")] [SerializeField]
    private int[] avlive_2;

    [SerializeField] private int[] dead_2;
    [SerializeField] private int tentative_2 = 1;
    
    
    [Header("gameoflife_3")] [SerializeField]
    private int[] avlive_3;

    [SerializeField] private int[] dead_3;
    [SerializeField] private int tentative_3 = 1;

    public bool ok;


    HashSet<Vector2Int> _undergroundMap = new HashSet<Vector2Int>();
    HashSet<Vector2Int> _oreMap = new HashSet<Vector2Int>();
    HashSet<Vector2Int> _oreMap_2 = new HashSet<Vector2Int>();
    HashSet<Vector2Int> _stoneMap = new HashSet<Vector2Int>();
    
    HashSet<Vector2Int> _nullMap = new HashSet<Vector2Int>();


    public void Generate()
    {
        //Generateperlin();
        GenerateperlinForStone();
        GenerateperlinForMineral();
        GenerateperlinForGranit();
        for (int i = 0; i < tentative; i++)
        {
            Gameoflifeinteration(_undergroundMap, dead, avlive, _floormap, _floorBase);
        }

        for (int i = 0; i < tentative_1; i++)
        {
            Gameoflifeinteration(_oreMap, dead_1, avlive_1, _floormap_1, _floorBase_1);
        }

        for (int i = 0; i < tentative_2; i++)
        {
            Gameoflifeinteration(_stoneMap, dead_2, avlive_2, _floormap_2, _floorBase_2);
        }
        
        generateNull();
        
        

        draw.Drawmaps(_floormap, _floorBase, _undergroundMap);
        draw.Drawmaps(_floormap_1, _floorBase_1, _oreMap);
        draw.Drawmaps(_floormap_2, _floorBase_2, _stoneMap);
        draw.Drawmaps(_floormap_3, _floorBase_3, _nullMap);
        draw.Drawmaps(_floormap_4, _floorBase_4, _oreMap_2);
    }
    
    public void active()
    {
        _floormap.gameObject.SetActive(true);
        _floormap_1.gameObject.SetActive(true);
        _floormap_2.gameObject.SetActive(true);
        _floormap_3.gameObject.SetActive(true);
        _floormap_4.gameObject.SetActive(true);
        
    }
    
    public void Deactive()
    {
        _floormap.gameObject.SetActive(false);
        _floormap_1.gameObject.SetActive(false);
        _floormap_2.gameObject.SetActive(false);
        _floormap_3.gameObject.SetActive(false);
        _floormap_4.gameObject.SetActive(false);
        
    }
    
    
    
    
    public void Generate_Final()
    {
       Generate();
       MergeMap();
    }

    public void Clear()
    {
        _floormap.ClearAllTiles();
        _floormap_1.ClearAllTiles();
        _floormap_2.ClearAllTiles();
        _floormap_3.ClearAllTiles();
        _floormap_4.ClearAllTiles();
        _floormap_final.ClearAllTiles();


        _undergroundMap.Clear();
        _oreMap.Clear();
        _stoneMap.Clear();
        _nullMap.Clear();
        _oreMap_2.Clear();
    }

    public void generatestone()
    {
        GenerateperlinForStone();
        Gameoflifeinteration(_stoneMap, dead_2, avlive_2, _floormap_2, _floorBase_2);
        draw.Drawmaps(_floormap_2, _floorBase_2, _stoneMap);
    }

    public void generateminerale()
    {
        GenerateperlinForMineral();
        Gameoflifeinteration(_oreMap, dead_1, avlive_1, _floormap_1, _floorBase_1);
        Gameoflifeinteration(_oreMap_2, dead_1, avlive_1, _floormap_4, _floorBase_4);
        draw.Drawmaps(_floormap_1, _floorBase_1, _oreMap);
        draw.Drawmaps(_floormap_4, _floorBase_4, _oreMap_2);
    }

    public void generateGranit()
    {
        GenerateperlinForGranit();
        Gameoflifeinteration(_undergroundMap, dead, avlive, _floormap, _floorBase);
        draw.Drawmaps(_floormap, _floorBase, _undergroundMap);
    }
    
    
    public void generateNull()
    {
        _floormap_3.ClearAllTiles();
        _nullMap.Clear();
        GenerateperlinForNull();
        for (int i = 0; i < tentative_3; i++)
        {
            GameoflifeinterationForNull();
        }
        draw.Drawmaps(_floormap_3, _floorBase_3, _nullMap);
    }
    


    private int Neigbourcounts(Vector2Int startepostion, HashSet<Vector2Int> map)
    {
        int count = 0;
        foreach (Vector2Int neighbour in draw.MooreNeighbours)
        {
            if (map.Contains(startepostion + neighbour))
            {
                count++;
            }
        }

        return count;
    }

    public void Gameoflifeinteration(HashSet<Vector2Int> map, int[] dead, int[] avlive, Tilemap _floormap,
        TileBase _floorBase)
    {
        HashSet<Vector2Int> alavietile = new HashSet<Vector2Int>(map);
        for (int x = 0; x < _size.x; x++)
        {
            for (int y = 0; y < _size.y; y++)
            {
                Vector2Int cellpostition = new Vector2Int(x, y);
                int nbNeighbours = Neigbourcounts(cellpostition, map);
                if (map.Contains(cellpostition))
                {
                    if (dead.Contains(nbNeighbours))
                    {
                        alavietile.Remove(cellpostition);
                    }
                }
                else
                {
                    if (avlive.Contains(nbNeighbours))
                    {
                        alavietile.Add(cellpostition);
                    }
                }
            }
        }

        map = new HashSet<Vector2Int>(alavietile);
        draw.Drawmaps(_floormap, _floorBase, map);
    }
    
    
    public void GameoflifeinterationForNull()
    {
        HashSet<Vector2Int> alavietile = new HashSet<Vector2Int>(_nullMap);
        for (int x = 0; x < _size.x; x++)
        {
            for (int y = 0; y < _size.y; y++)
            {
                Vector2Int cellpostition = new Vector2Int(x, y);
                int nbNeighbours = Neigbourcounts(cellpostition, _nullMap);
                if (_nullMap.Contains(cellpostition))
                {
                    if (dead_3.Contains(nbNeighbours))
                    {
                        alavietile.Remove(cellpostition);
                    }
                }
                else
                {
                    if (avlive_3.Contains(nbNeighbours))
                    {
                        alavietile.Add(cellpostition);
                    }
                }
            }
        }

        _nullMap = new HashSet<Vector2Int>(alavietile);
        draw.Drawmaps(_floormap_3, _floorBase_3, _nullMap);
    }

    public void Generateperlin()
    {
        for (int x = -1 * _size.x / 2; x < _size.x / 2; x++)
        {
            for (int y = -1 * _size.y / 2; y < _size.y / 2; y++)
            {
                float noisecoordX = x / (float)_size.x + Random.value;
                float noisecoordy = y / (float)_size.y + Random.value;

                float rng = Mathf.PerlinNoise(noisecoordX * (_perlin * _perlinscale),
                    noisecoordy * (_perlin * _perlinscale));

                if (y < -0.2f * _size.y)
                {
                    if (rng > (_magic * 1.1f))
                    {
                        _undergroundMap.Add(new Vector2Int(_center.x + x, _center.y + y));
                    }
                }
                else
                {
                    if (rng < 0.3f)
                    {
                        _oreMap.Add(new Vector2Int(_center.x + x, _center.y + y));
                    }
                    else if (rng < 0.3f || rng > 0.5f)
                    {
                        _stoneMap.Add(new Vector2Int(_center.x + x, _center.y + y));
                    }
                    else
                    {
                    }
                }
            }
        }
    }

    public void GenerateperlinForMineral()
    {
        for (int x = -1 * _size.x / 2; x < _size.x / 2; x++)
        {
            for (int y = -1 * _size.y / 2; y < _size.y / 2; y++)
            {
                float noisecoordX = x / (float)_size.x + Random.value;
                float noisecoordy = y / (float)_size.y + Random.value;

                float rng = Mathf.PerlinNoise(noisecoordX * (_perlin * _perlinscale),
                    noisecoordy * (_perlin * _perlinscale));

                if (y > -0.15f * _size.y)
                {
                    if (rng > 0.8f)
                    {
                        _oreMap.Add(new Vector2Int(_center.x + x, _center.y + y));
                    }
                }
                else
                {
                    if (rng > 0.8f)
                    {
                        _oreMap_2.Add(new Vector2Int(_center.x + x, _center.y + y));
                    }
                }

                
            }
        }
    }
    
    
    public void GenerateperlinForNull()
    {
        for (int x = -1 * _size.x / 2; x < _size.x / 2; x++)
        {
            for (int y = -1 * _size.y / 2; y < _size.y / 2; y++)
            {
                float noisecoordX = x / (float)_size.x + Random.value;
                float noisecoordy = y / (float)_size.y + Random.value;

                float rng = Mathf.PerlinNoise(noisecoordX * (_perlin * _perlinscale),
                    noisecoordy * (_perlin * _perlinscale));

                if (rng > 0.5f)
                {
                    _nullMap.Add(new Vector2Int(_center.x + x, _center.y + y));
                }
            }
        }
    }

    public void GenerateperlinForGranit()
    {
        for (int y = -1 * _size.y / 2; y < _size.y / 2; y++)
        {
            for (int x = -1 * _size.x / 2; x < _size.x / 2; x++)
            {
                float noisecoordX = x / (float)_size.x + Random.value;
                float noisecoordy = y / (float)_size.y + Random.value;

                float rng = Mathf.PerlinNoise(noisecoordX * (_perlin_granit_x * _perlinscale_granit_x),
                    noisecoordy * (_perlin_granit_y * _perlinscale_granit_y));

                if (y < -0.2f * _size.y + rng * _magic_granit)
                {
                    _undergroundMap.Add(new Vector2Int(_center.x + x, _center.y + y));
                }
            }
        }
    }
    
    public void GenerateperlinForStone()
    {
        for (int x = -1 * _size.x / 2; x < _size.x / 2; x++)
        {
            for (int y = -1 * _size.y / 2; y < _size.y / 2; y++)
            {
                _stoneMap.Add(new Vector2Int(_center.x + x, _center.y + y));
            }
        }
    }
    

    public void MergeMap()
    {
        for (int x = -_size.x / 2; x < _size.x / 2; x++)
        {
            for (int y = -_size.y / 2; y < _size.y / 2; y++)
            {
                Vector3Int cellPosition = new Vector3Int(_center.x + x, _center.y + y, 0);
                if (_floormap_1.GetTile(cellPosition) != null && _floormap_3.GetTile(cellPosition) != null)
                {
                    _floormap_final.SetTile(cellPosition, _floorBase_1);
                }
                else if (_floormap_4.GetTile(cellPosition) != null && _floormap_3.GetTile(cellPosition) != null)
                {
                    _floormap_final.SetTile(cellPosition, _floorBase_4);
                }
                else
                {
                    if (_floormap.GetTile(cellPosition) != null && _floormap_3.GetTile(cellPosition) != null)
                    {
                        _floormap_final.SetTile(cellPosition, _floorBase);
                    }
                    else
                    {
                        if (_floormap_2.GetTile(cellPosition) != null && _floormap_3.GetTile(cellPosition) != null && _floormap.GetTile(cellPosition) == null)
                        {
                            _floormap_final.SetTile(cellPosition, _floorBase_2);
                        }
                    }
                }
            }
        }
    }
}