using UnityEngine;
public class PlantHealth : MonoBehaviour
{
    public float health;
    private Plant plantScript;

    private void Awake()
    {
        plantScript = GetComponent<Plant>();
        plantScript.health = health;
    }

    public void TakeHit(float damage)
    {
        StartCoroutine(plantScript.TakeDamage(damage));
    }
}
