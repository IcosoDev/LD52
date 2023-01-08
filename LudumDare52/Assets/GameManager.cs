using UnityEngine;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public float difficultyLevel;
    private EnemySpawner enemySpawner;
    public float timeBetweenSpawns;

    private void Start()
    {
        PlayerPrefs.SetInt("FarmersKilled", 0);
        enemySpawner = FindObjectOfType<EnemySpawner>();
        Invoke("StartStuffLmao", 2);
    }
    void StartStuffLmao()
    {
        StartCoroutine(SpawnLoop());
    }
    private IEnumerator SpawnLoop()
    {
        float b = 0.12f * difficultyLevel - 1.6f;
        float a = Mathf.Pow(2, -b);
        timeBetweenSpawns = a + 1;
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