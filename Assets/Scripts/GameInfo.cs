using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameInfo : MonoBehaviour
{
    public static GameInfo GI;

    [HideInInspector]
    public Tilemap map;

    //Beer interactinos
    [HideInInspector]
    public Dictionary<Tile, Tile> BeerTileConversion;

    [HideInInspector]
    public Dictionary<Tile, Tile> BeerItemConversion;

    //Torch Interactions
    [HideInInspector]
    public Dictionary<Tile, Tile> TorchTileConversion;

    private void Awake()
    {
        if (GI != null)
        {
            Destroy(GI);
        }
        else
        {
            GI = this;
        }

        BeerTileConversion = new Dictionary<Tile, Tile>
        {
            //{ Resources.Load<VolaTile>("Tiles/VolaTile"), Resources.Load<VolaTile>("Tiles/NullTile_Beer") }
        };

        TorchTileConversion = new Dictionary<Tile, Tile>
        {
            //{ Resources.Load<VolaTile>("Tiles/NullTile_Beer"), Resources.Load<VolaTile>("Tiles/NullTile_Beer_Burning") }
        };
        
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        map = FindObjectOfType<Tilemap>();
    }
}
