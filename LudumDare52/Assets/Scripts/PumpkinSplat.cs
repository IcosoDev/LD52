using DG.Tweening;
using UnityEngine;
using System.Collections;
public class PumpkinSplat : MonoBehaviour
{
    private SpriteRenderer pumpkinSplatSprite;
    private void Start()
    {
        pumpkinSplatSprite = GetComponentInChildren<SpriteRenderer>();
        StartCoroutine(Anim());
    }

    private IEnumerator Anim()
    {
        pumpkinSplatSprite.transform.localScale = Vector3.zero;
        pumpkinSplatSprite.transform.DOScale(1.15f, 0.08f);
        pumpkinSplatSprite.transform.DORotate(new Vector3(0, 0, 120), 2f);
        yield return new WaitForSeconds(0.4f);
        pumpkinSplatSprite.transform.DOScale(0, 0.4f);
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(collision.gameObject.GetComponent<FarmerScript>().TakeDamage(2));
        }
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(collision.gameObject.GetComponent<FarmerScript>().TakeDamage(2));
        }
        GetComponent<BoxCollider2D>().enabled = false;
    }
}