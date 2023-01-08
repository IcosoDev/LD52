using System.Collections;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public int numOfTiles()
    {
        return FindObjectsOfType<MovingPanel>().Length;
    }
    public float maxTilesAllowed;
    [SerializeField] private float usedMaxTiles;
    public float timeBetweenTiles;
    [SerializeField] private float usedTimeTiles;
    private float gameTime;
    public GameObject spawnPoint;
    public MovingPanel[] tiles; //0 corn, 1 wheat, 2 cotton, 3 pea, 4 potato, 5 pumpkin

    private void Start()
    {
        maxTilesAllowed = 4;
        usedMaxTiles = maxTilesAllowed;
        usedTimeTiles = timeBetweenTiles;
        Invoke("StartStuffs", 8);
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
        usedMaxTiles = maxTilesAllowed + gameTime / 60;
        if(usedMaxTiles > 9)
        {
            usedMaxTiles = 9;
        }
    }

    void StartStuffs()
    {
        StartCoroutine(SpawnTile());
    }

    private IEnumerator SpawnTile()
    {
        if(numOfTiles() <= usedMaxTiles)
        {
            usedTimeTiles = timeBetweenTiles + Random.Range(-2f, 2f);
            yield return new WaitForEndOfFrame();
            int r = Random.Range(0, tiles.Length);
            MovingPanel t = Instantiate(tiles[r], spawnPoint.transform.position, Quaternion.identity);
            t.gameObject.transform.SetParent(spawnPoint.transform);
        }
        yield return new WaitForSeconds(usedTimeTiles);
        StartCoroutine(SpawnTile());
    }
}
