using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    private BoxCollider2D col;
    //[SerializeField] private GameObject hitParticle;
    [HideInInspector] public float dmg;
    void Start()
    {
        Destroy(gameObject, 5);
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.gameObject.CompareTag("Farmer"))
        //{
        //    StartCoroutine(Death());
        //}
        StartCoroutine(collision.gameObject.GetComponent<FarmerScript>().TakeDamage(dmg));
        StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        ////destroy the bullet on impact
        //GameObject hp = Instantiate(hitParticle, transform.position, transform.rotation);
        //Destroy(hp, 1);

        transform.position = new Vector3(0, 0, -100);
        col.enabled = false;

        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
