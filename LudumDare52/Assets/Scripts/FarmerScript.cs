using System.Collections;
using DG.Tweening;
using UnityEngine;

public class FarmerScript : MonoBehaviour
{
    //HALLO YOU WONDERFUL HUMAN BEING I HOPE YOU HAVE A FANTASTICALLY GROOVY DAY!!! -icobo
    //pls do not judge my messy spaghetti code thanku
    public SpriteRenderer farmerSprite;
    public float health;
    [SerializeField] private Sprite normalSprite, hitSprite;
    [SerializeField] private ParticleSystem hitParticles;
    [SerializeField] private GameObject deadObject;
    [SerializeField] private GameObject slowedFlowers;
    [SerializeField] private Sprite[] hats, hatsHit;
    [SerializeField] private Sprite[] weirdHats, weirdHatsHit;
    [SerializeField] private SpriteRenderer hat;
    [SerializeField] private Sprite[] allHats;
    [SerializeField] private Sprite[] allHatsHit;
    [SerializeField] private int hatID;
    private GameManager gameManager;
    private Sprite hatSprite, hatSpriteHit;
    private float startHealth;
    private bool lostHat;

    public enum State
    {
        Moving,
        Attacking,
        Slowed
    }

    public State state;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        health += gameManager.difficultyLevel + Random.Range(-1f, 2f);
        startHealth = health;
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.26f);
        attackTimer = timeBetweenAttacks;
        state = State.Moving;

        int a = Random.Range(0, 100);
        if(a > 90)
        {
            int i = Random.Range(0, weirdHats.Length);
            hat.sprite = weirdHats[i];
            hatID = i + 3;
        }
        else
        {
            int b = Random.Range(0, 100);
            if(b < 80)
            {
                hat.sprite = hats[0];
                hatID = 0;
            }
            else
            {
                int w = Random.Range(1, hats.Length);
                hat.sprite = hats[w];
                hatID = w;
            }
        }
        if (a > 50)
        {
            hat.transform.localScale = new Vector3(-1, 1, 1);
        }
        hatSprite = allHats[hatID];
        hatSpriteHit = allHatsHit[hatID];
    }

    public IEnumerator TakeDamage(float damage)
    {
        health -= damage;
        hitParticles.Play();
        if (health <= 0)
        {
            StartCoroutine(Death());
        }
        if (health < (startHealth / 2))
        {
            if (lostHat == false)
            {
                GameObject h = Instantiate(deadObject, hat.transform.position, Quaternion.identity);
                h.GetComponent<Rigidbody2D>().AddForce(new Vector2(694.20f + Random.Range(-10, 50), 420.69f + Random.Range(-10, 50)));
                h.GetComponent<Rigidbody2D>().AddTorque(-500 + Random.Range(-50, 10));
                h.GetComponentInChildren<SpriteRenderer>().sprite = allHats[hatID];
                h.GetComponentInChildren<SpriteRenderer>().gameObject.transform.localPosition = new Vector3(0, 0, 0);
                Destroy(hat);
                lostHat = true;
            }
        }

        if(lostHat == false)
        {
            hat.sprite = allHatsHit[hatID];
        }
        farmerSprite.sprite = hitSprite;
        int a = Random.Range(0, 2) * 2 - 1;
        farmerSprite.transform.localEulerAngles = new Vector3(0, 0, 15 * a + Random.Range(0, 8) * a);
        farmerSprite.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f);

        yield return new WaitForSeconds(0.2f);

        farmerSprite.sprite = normalSprite;
        if (lostHat == false)
        {
            hat.sprite = allHats[hatID];
        }
    }
    public IEnumerator Death()
    {
        GameObject d = Instantiate(deadObject, transform.position, Quaternion.identity);
        d.GetComponent<Rigidbody2D>().AddForce(new Vector2(694.20f + Random.Range(-10, 50), 420.69f + Random.Range(-10, 50)));
        d.GetComponent<Rigidbody2D>().AddTorque(-500 + Random.Range(-50, 10));
        d.GetComponentInChildren<SpriteRenderer>().sprite = normalSprite;
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

    //public void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawRay(transform.position, Vector2.left * 6);
    //}

    [Header("Attacking")]
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private float attackDamage;
    [SerializeField] private LayerMask plantLayer, farmerLayer;
    private float attackTimer;

    private void Update()
    {
        attackTimer -= Time.deltaTime;
        if (Physics2D.Raycast(transform.position, Vector2.left, 6f, plantLayer))
        {
            Debug.Log("hitting");
            state = State.Attacking;
            if (attackTimer <= 0)
            {
                attackTimer = timeBetweenAttacks;
                StartCoroutine(Attack());
            }
        }
        else if (Physics2D.Raycast(transform.position, Vector2.left, 10f, farmerLayer))
        {
            state = State.Attacking;
        }
        else
        {
            if(state != State.Slowed)
            {
                state = State.Moving;
            }
        }
    }

    private IEnumerator Attack()
    {
        farmerSprite.transform.localEulerAngles = new Vector3(0, 0, 15);
        farmerSprite.transform.DORotate(new Vector3(0, 0, 0), 0.15f);
        yield return new WaitForEndOfFrame();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 6f, plantLayer);
        Debug.Log(hit.collider);
        hit.collider.gameObject.GetComponent<PlantHealth>().TakeHit(attackDamage);
    }

    public void SlowDown()
    {
        state = State.Slowed;
        slowedFlowers.transform.DOScale(1, 0.2f);
        Invoke("ReturnFromSlow", 2f);
    }

    private void ReturnFromSlow()
    {
        state = State.Moving;
        slowedFlowers.transform.DOScale(0, 0.2f);
    }
}