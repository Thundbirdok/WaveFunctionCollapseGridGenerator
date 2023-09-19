using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "TileType", menuName = "GridGenerator/TileType", order = 1)]
public class TileType : ScriptableObject
{
    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private TileProbability[] tilesProbabilities;
    
    public TileType GetValidNeighborTile()
    {
        var random = Random.Range(0, 1f);
        var sum = 0f;
        
        foreach (var tileProbability in tilesProbabilities)
        {
            sum += tileProbability.Probability;
            
            if (random <= sum)
            {
                return tileProbability.TileType;
            }
        }
        
        return null;
    }
 
    [Serializable]
    private struct TileProbability
    {
        [field: SerializeField]
        public TileType TileType { get; private set; }
        
        [field: SerializeField]
        public float Probability { get; private set; }
    }
}