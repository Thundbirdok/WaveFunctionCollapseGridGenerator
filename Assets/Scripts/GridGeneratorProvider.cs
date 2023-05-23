using UnityEngine;

public class GridGeneratorProvider : MonoBehaviour
{
    [SerializeField]
    private TileUiProvider tilePrefab;

    [SerializeField]
    private Canvas canvas;
    
    private void Start()
    {
        var x = 5;
        var y = 5;
        
        var grid = GridGenerator.GenerateGrid(x, y);

        for (var i = 0; i < y; i++)
        {
            for (var j = 0; j < x; j++)
            {
                var tile = Instantiate(tilePrefab, canvas.transform);

                if (tile.transform is RectTransform rectTransform)
                {
                    var tileSize = rectTransform.rect.size;

                    rectTransform.anchoredPosition = new Vector2(j * tileSize.x, i * tileSize.y);
                }

                tile.Type = grid[i, j];
            }
        }
    }
}
