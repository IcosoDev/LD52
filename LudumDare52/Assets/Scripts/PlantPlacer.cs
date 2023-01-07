using UnityEngine;

public class PlantPlacer : MonoBehaviour
{
    [SerializeField] private float tileSize;
    private int numOfGridTiles = 60;
    [SerializeField] private GameObject l;

    void Start()
    {
        for (int i = 0; i < numOfGridTiles; i++)
        {
            var x = i % 12;
            var y = i / 12;

            Vector2 spawnPos = new Vector2(x * tileSize, y * tileSize);
            Instantiate(l, (Vector2)transform.position + spawnPos, Quaternion.identity);
        }
    }

    void Update()
    {
        
    }
}
