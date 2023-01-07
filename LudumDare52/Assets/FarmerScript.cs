using System.Collections;
using DG.Tweening;
using UnityEngine;

public class FarmerScript : MonoBehaviour
{
    public SpriteRenderer farmerSprite;
    public float health;
    [SerializeField] private Sprite normalSprite, hitSprite;

    public IEnumerator TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //Death
            Destroy(gameObject);
        }

        farmerSprite.sprite = hitSprite;
        int a = Random.Range(0, 2) * 2 - 1;
        farmerSprite.transform.localEulerAngles = new Vector3(0, 0, 15 * a + Random.Range(0, 8) * a);
        farmerSprite.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f);

        yield return new WaitForSeconds(0.2f);

        farmerSprite.sprite = normalSprite;

    }

    public IEnumerator Squish(float xSquish, float ySquish, float animationTime)
    {
        Vector3 originalSize = farmerSprite.transform.localScale;
        Vector3 newSize = new Vector3(xSquish, ySquish, originalSize.z);

        float t = 0f;
        while (t <= 1)
        {
            t += Time.deltaTime / animationTime;
            farmerSprite.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            yield return null;
        }

        t = 0f;
        while (t <= 1)
        {
            t += Time.deltaTime / animationTime;
            farmerSprite.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }
    }
    public IEnumerator Wiggle(float angle, float animationTime)
    {
        //angle *= Random.Range(0, 2) * 2- 1;
        //angle += Random.Range(5f, -5f);
        Vector3 originalRotation = Vector3.zero;
        Vector3 newRotation = new Vector3(0, 0, angle);

        float t = 0f;
        //while (t <= 1)
        //{
        //    t += Time.deltaTime / animationTime;
        //    farmerSprite.transform.localEulerAngles = Vector3.Lerp(originalRotation, newRotation, t);
        //    yield return null;
        //}
        t = 0f;

        while (t <= 1)
        {
            t += Time.deltaTime / animationTime;
            farmerSprite.transform.localEulerAngles = Vector3.Lerp(newRotation, originalRotation, t);
            yield return null;
        }
    }
}
