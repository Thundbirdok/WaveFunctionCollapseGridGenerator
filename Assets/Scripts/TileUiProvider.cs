using TMPro;
using UnityEngine;

public class TileUiProvider : MonoBehaviour
{
    private GridGenerator.TileTypes _type;
    public GridGenerator.TileTypes Type
    {
        get
        {
            return _type;
        }
    
        set
        {
            text.text = value.ToString();
            
            _type = value;
        }
    }
    
    [SerializeField]
    private TextMeshProUGUI text;
}
