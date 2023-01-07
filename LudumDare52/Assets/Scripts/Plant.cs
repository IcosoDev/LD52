using UnityEngine;
using System.Collections;
using DG.Tweening;
public class Plant : MonoBehaviour
{
    public float health;
    public float damage;
    public SpriteRenderer plantSprite;
    [SerializeField] private LayerMask farmerLayer;
    [HideInInspector] public GridTile gridTile;

    public bool canSeeFarmer()
    {
        if (Physics2D.Raycast(transform.position, Vector2.right, 100, farmerLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Start()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.26f);
    }


    public IEnumerator Squish(float xSquish, float ySquish, float animationTime)
    {
        Vector3 originalSize = plantSprite.transform.localScale;
        Vector3 newSize = new Vector3(xSquish, ySquish, originalSize.z);

        float t = 0f;
        while (t <= 1)
        {
            t += Time.deltaTime / animationTime;
            plantSprite.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            yield return null;
        }

        t = 0f;
        while (t <= 1)
        {
            t += Time.deltaTime / animationTime;
            plantSprite.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }
    }

    public IEnumerator Wiggle(float angle, float animationTime)
    {
        //angle *= Random.Range(0, 2) * 2 - 1;
        //angle += Random.Range(2f, -2f);
        Vector3 originalRotation = Vector3.zero;
        Vector3 newRotation = new Vector3(0, 0, angle);

        float t = 0f;
        //while (t <= 1)
        //{
        //    t += Time.deltaTime / animationTime;
        //    plantSprite.transform.localEulerAngles = Vector3.Lerp(originalRotation, newRotation, t);
        //    yield return null;
        //}
        t = 0f;

        while (t <= 1)
        {
            t += Time.deltaTime / animationTime;
            plantSprite.transform.localEulerAngles = Vector3.Lerp(newRotation, originalRotation, t);
            yield return null;
        }
    }
}