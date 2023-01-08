using UnityEngine;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public float difficultyLevel;
    private EnemySpawner enemySpawner;
    public float timeBetweenSpawns;

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        Invoke("StartStuffLmao", 2);
    }
    void StartStuffLmao()
    {
        StartCoroutine(SpawnLoop());
    }
    private IEnumerator SpawnLoop()
    {
        float a = Mathf.Pow(2, -(0.1f * difficultyLevel) - 1.6f);
        timeBetweenSpawns = a + 2.5f;
        SpawnFarmer();
        yield return new WaitForSeconds(timeBetweenSpawns);
        StartCoroutine(SpawnLoop());
    }

    //private bool CheckForRemainingEnemies()
    //{
    //    if(FindObjectsOfType<FarmerScript>().Length > 0)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    private void SpawnFarmer()
    {
        difficultyLevel += 0.02f;
        enemySpawner.SpawnEnemy();
    }
}