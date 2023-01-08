using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyToSpawn;
    private GameObject[] enemies;
    public float difficultyLevel;
    [SerializeField] private LayerMask farmerLayer;

    public void SpawnEnemy()
    {
        int a = Random.Range(0, 5);
        Vector2 pos = new Vector2(transform.position.x, transform.position.y + a * 6);
        if (!Physics2D.OverlapCircle(pos, 1, farmerLayer))
        {
            Instantiate(enemyToSpawn, pos, Quaternion.identity);
        }
    }
}
