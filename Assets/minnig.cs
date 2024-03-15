using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class minnig : MonoBehaviour
{

    private gameoflife_generetor generetor;
    private StarterAssetsInputs _inputs;
    [SerializeField] private GameObject gauche;
    [SerializeField] private GameObject haut;
    [SerializeField] private GameObject bas;

    private bool _gauche;
    private bool _haut;
    private bool _bas;
    


    private void Start()
    {
        generetor = FindFirstObjectByType<gameoflife_generetor>();
        _inputs = GetComponentInParent<StarterAssetsInputs>();
    }

    private void Update()
    {
        Vector3Int cellpostion = generetor._floormap_final.WorldToCell(transform.position);
        TileBase tile =  generetor._floormap_final.GetTile(cellpostion);
        if (tile != null)
        {
            Debug.Log("mining tile :" + tile.name);
            if (_inputs.Minnig)
            {
                generetor._floormap_final.SetTile(cellpostion,null);
            }
        }

        if (_inputs.CheckUp)
        {
            _haut = true;
            _gauche = false;
            _bas = false;
        }
        if (_inputs.CheckDown)
        {
            _haut = false;
            _gauche = false;
            _bas = true;
        }
        if (Mathf.Abs(_inputs.Move) > Mathf.Epsilon)
        {
            _haut = false;
            _gauche = true;
            _bas = false;
        }

        if (_haut)
        {
            this.transform.position = haut.transform.position;
        }
        if (_gauche)
        {
            this.transform.position = gauche.transform.position;
        }
        if (_bas)
        {
            this.transform.position = bas.transform.position;
        }
        
    }

    
}
