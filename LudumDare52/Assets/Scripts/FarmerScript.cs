using System.Collections;
using DG.Tweening;
using UnityEngine;

public class FarmerScript : MonoBehaviour
{
    //HALLO YOU WONDERFUL HUMAN BEING I HOPE YOU HAVE A FANTASTICALLY GROOVY DAY!!! -icobo
    public SpriteRenderer farmerSprite;
    public float health;
    [SerializeField] private Sprite normalSprite, hitSprite;
    [SerializeField] private ParticleSystem hitParticles;
    [SerializeField] private GameObject deadObject;

    public enum State
    {
        Moving,
        Attacking
    }

    public State state;

    private void Start()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.26f);
        attackTimer = timeBetweenAttacks;
        state = State.Moving;
    }

    public IEnumerator TakeDamage(float damage)
    {
        health -= damage;
        hitParticles.Play();
        if (health <= 0)
        {
            StartCoroutine(Death());
        }

        farmerSprite.sprite = hitSprite;
        int a = Random.Range(0, 2) * 2 - 1;
        farmerSprite.transform.localEulerAngles = new Vector3(0, 0, 15 * a + Random.Range(0, 8) * a);
        farmerSprite.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f);

        yield return new WaitForSeconds(0.2f);

        farmerSprite.sprite = normalSprite;
    }
    public IEnumerator Death()
    {
        GameObject d = Instantiate(deadObject, transform.position, Quaternion.identity);
        d.GetComponent<Rigidbody2D>().AddForce(new Vector2(694.20f, 420.69f));
        d.GetComponent<Rigidbody2D>().AddTorque(-500);
        transform.position = new Vector3(0, 0, -100);

        yield return new WaitForSeconds(0.25f);

        Destroy(gameObject);
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

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + new Vector3(-3, 0, 0), Vector2.left * 3);
    }

    [Header("Attacking")]
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private float attackDamage;
    [SerializeField] private LayerMask plantLayer;
    private float attackTimer;

    private void Update()
    {
        attackTimer -= Time.deltaTime;
        if (Physics2D.Raycast(transform.position + new Vector3(-3, 0, 0), Vector2.left, 3f, plantLayer))
        {
            state = State.Attacking;
            if (attackTimer <= 0)
            {
                attackTimer = timeBetweenAttacks;
                StartCoroutine(Attack());
            }
        }
        else
        {
            state = State.Moving;
        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForEndOfFrame();
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(-3, 0, 0), Vector2.left, 3f, plantLayer);
        hit.collider.gameObject.GetComponent<Plant>().health -= attackDamage;
    }
}