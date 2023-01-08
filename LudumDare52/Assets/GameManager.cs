using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    public float difficultyLevel;
    private EnemySpawner enemySpawner;
    public float timeBetweenSpawns;
    public AudioSource sfxPop;
    public AudioClip[] pop;
    public AudioSource sfxHit;
    public AudioClip[] hit;
    public Text hintText;

    private void Start()
    {
        PlayerPrefs.SetInt("FarmersKilled", 0);
        enemySpawner = FindObjectOfType<EnemySpawner>();
        Invoke("StartStuffLmao", 2);
        StartCoroutine(TextStuff());
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

    public void PopSFX()
    {
        if(!sfxPop.isPlaying)
        {
            sfxPop.clip = pop[Random.Range(0, pop.Length)];
            sfxPop.pitch = Random.Range(0.95f, 1.05f);
            sfxPop.Play();
        }
    }

    public void HitSFX()
    {
        if (!sfxHit.isPlaying)
        {
            sfxHit.clip = hit[Random.Range(0, hit.Length)];
            sfxHit.pitch = Random.Range(0.95f, 1.05f);
            sfxHit.Play();
        }
    }

    private IEnumerator TextStuff()
    {
        hintText.color = new Color32(255, 255, 255, 0);
        yield return new WaitForSeconds(0.4f);
        hintText.DOFade(1, 0.8f);
        hintText.transform.DOScale(0.85f, 7f);
        yield return new WaitForSeconds(5f);
        hintText.DOFade(0, 0.8f);
        yield return new WaitForSeconds(0.3f);
        Destroy(hintText);
    }
}