using UnityEngine;
using System.Collections;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyToSpawn;
    private GameObject[] enemies;
    public float difficultyLevel;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        int a = Random.Range(0, 5);
        Vector2 pos = new Vector2(transform.position.x, transform.position.y + a * 6);
        Instantiate(enemyToSpawn, pos, Quaternion.identity);
        yield return new WaitForSeconds(3f);
        StartCoroutine(SpawnEnemy());
    }
}
