using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    private BoxCollider2D col;
    //[SerializeField] private GameObject hitParticle;
    [HideInInspector] public float dmg;
    [SerializeField] private bool pierce, slowFarmer;
    [SerializeField] private float rotationSpeed;
    private SpriteRenderer bulletSprite;
    void Start()
    {
        bulletSprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        Destroy(gameObject, 5);
    }

    private void Update()
    {
        bulletSprite.transform.localEulerAngles += new Vector3(0, 0, rotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.gameObject.CompareTag("Farmer"))
        //{
        //    StartCoroutine(Death());
        //}
        if (collision.gameObject.CompareTag("Death"))
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(collision.gameObject.GetComponent<FarmerScript>().TakeDamage(dmg));
            if (!pierce)
            {
                StartCoroutine(Death());
            }
            if(slowFarmer)
            {
                collision.gameObject.GetComponent<FarmerScript>().SlowDown();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.gameObject.CompareTag("Farmer"))
        //{
        //    StartCoroutine(Death());
        //}
        if (collision.gameObject.CompareTag("Death"))
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(collision.gameObject.GetComponent<FarmerScript>().TakeDamage(dmg));
            if (!pierce)
            {
                StartCoroutine(Death());
            }
            if (slowFarmer)
            {
                collision.gameObject.GetComponent<FarmerScript>().SlowDown();
            }
        }
    }

    private IEnumerator Death()
    {
        ////destroy the bullet on impact
        //GameObject hp = Instantiate(hitParticle, transform.position, transform.rotation);
        //Destroy(hp, 1);

        transform.position = new Vector3(0, 10000, -100);
        col.enabled = false;

        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
