using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GridGenerator
{
   private static readonly TileTypes[][] Rules =
   {
      new[] { TileTypes.Camp, TileTypes.Field, TileTypes.SmallForest, TileTypes.BigForest, TileTypes.SmallMountain, TileTypes.BigMountain, TileTypes.Water }, //Empty
      new[] { TileTypes.Field, TileTypes.SmallForest, TileTypes.SmallMountain }, //Camp
      new[] { TileTypes.Field, TileTypes.SmallForest, TileTypes.SmallMountain, TileTypes.Water }, // Field
      new[] { TileTypes.Field, TileTypes.SmallForest, TileTypes.BigForest, TileTypes.SmallMountain, TileTypes.Water }, //SmallForest
      new[] { TileTypes.SmallForest }, //BigForest
      new[] { TileTypes.Field, TileTypes.SmallForest, TileTypes.SmallMountain, TileTypes.BigMountain, TileTypes.Water }, //SmallMountain
      new[] { TileTypes.SmallMountain }, //BigMountain
      new[] { TileTypes.Field, TileTypes.SmallForest, TileTypes.SmallMountain, TileTypes.Water } //Water
   };

   public static TileTypes[,] GenerateGrid(int x, int y)
   {
      var size = new Vector2Int(x, y);
      
      var grid = new TileTypes[size.y, size.x];

      for (var i = 0; i < size.y; i++)
      {
         for (var j = 0; j < size.x; j++)
         {
            grid[i, j] = TileTypes.Empty;
         }
      }

      var campPosition = new Vector2Int(Random.Range(0, size.x), Random.Range(0, size.y));
      
      grid[campPosition.y, campPosition.x] = TileTypes.Camp;

      var emptyTiles = new Queue<Vector2Int>();
      
      PushTileToFillStack(emptyTiles, campPosition.y + 1, campPosition.x, size);
      PushTileToFillStack(emptyTiles, campPosition.y - 1, campPosition.x, size);
      PushTileToFillStack(emptyTiles, campPosition.y, campPosition.x + 1, size);
      PushTileToFillStack(emptyTiles, campPosition.y, campPosition.x - 1, size);

      while (true)
      {
         var position = emptyTiles.Dequeue();

         var tile = grid[position.y, position.x];
         
         var possibleTiles = new List<TileTypes>(Rules[(int)tile]);
         
         CheckTile(grid, size, position.y + 1, position.x, emptyTiles, ref possibleTiles);
         CheckTile(grid, size, position.y - 1, position.x, emptyTiles, ref possibleTiles);
         CheckTile(grid, size, position.y, position.x + 1, emptyTiles, ref possibleTiles);
         CheckTile(grid, size, position.y, position.x - 1, emptyTiles, ref possibleTiles);

         if (possibleTiles.Count == 0)
         {
            grid[position.y, position.x] = TileTypes.Empty;
            
            continue;
         }

         var randomTile = possibleTiles[Random.Range(0, possibleTiles.Count)];
         
         grid[position.y, position.x] = randomTile;

         if (emptyTiles.Count == 0)
         {
            break;
         }
      }
      
      return grid;
   }

   private static void CheckTile(TileTypes[,] grid, Vector2Int size, int y, int x, Queue<Vector2Int> emptyTiles, ref List<TileTypes> possibleTiles)
   {
      if (IsPositionInGrid(y, x, size) == false)
      {
         return;
      }

      var tile = grid[y, x];
      
      if (tile == TileTypes.Empty)
      {
         emptyTiles.Enqueue(new Vector2Int(x, y));
         
         return;
      }

      possibleTiles = possibleTiles.Intersect(Rules[(int)tile]).ToList();
   }

   private static void PushTileToFillStack(Queue<Vector2Int> emptyTiles, int y, int x, Vector2Int size)
   {
      if (IsPositionInGrid(y, x, size) == false)
      {
         return;
      }

      emptyTiles.Enqueue(new Vector2Int(x, y));
   }

   private static bool IsPositionInGrid(int y, int x, Vector2Int size)
   {
      if (y < 0 || y >= size.y)
      {
         return false;
      }
      
      if (x < 0 || x >= size.x)
      {
         return false;
      }

      return true;
   }
   
   public enum TileTypes
   {
      Empty,
      Camp,
      Field,
      SmallForest,
      BigForest,
      SmallMountain,
      BigMountain,
      Water
   }
}
