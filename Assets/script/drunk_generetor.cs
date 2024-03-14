using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class drunk_generetor : MonoBehaviour
{
    [SerializeField] private Vector2Int _startposition = new Vector2Int(0,0);
    
    [SerializeField] private Tilemap _floormap;
    [SerializeField] private TileBase _floorBase;

    [SerializeField] private float _leghtFactor = 1;

    [SerializeField] private int _carreuxmax = 50;

    


    public void Generate()
    {
        
        
        Vector2Int position = _startposition;
        

        HashSet<Vector2Int> positions = new HashSet<Vector2Int>();
        
        positions.Add(position);
        do
        {
            Vector2Int direction = draw._vonNeuannNeighbours[Random.Range(0, draw._vonNeuannNeighbours.Length)];


            int leght = Mathf.CeilToInt(_leghtFactor * Random.Range(0, 1f));
            for (int n = 0; n < leght; n++)
            {
                position += direction;
                positions.Add(position);
            }

            
        } while (positions.Count < _carreuxmax);
        
        draw.Drawmaps(_floormap,_floorBase,positions);
    }

    
}
