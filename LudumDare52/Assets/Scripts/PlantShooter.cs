using UnityEngine;
public class PlantShooter : Plant
{
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private Transform shootPoint;
    private float shootTimer;
    [SerializeField] private Rigidbody2D bullet;
    [SerializeField] private float shootForce;
    [SerializeField] private ParticleSystem shootParitcles;
    [SerializeField] private bool pea, pumpkin;

    private new void Start()
    {
        base.Start();
        shootTimer = timeBetweenShots;
    }

    private void Update()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0 && canSeeFarmer() == true)
        {
            Shoot();
            shootTimer = timeBetweenShots;
        }
    }

    private void Shoot()
    {
        gameManager.PopSFX();
        if(!pumpkin)
        {
            shootParitcles.Play();
            Rigidbody2D b = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
            b.GetComponent<Bullet>().dmg = damage;
            b.velocity = Vector2.right * shootForce;
            StartCoroutine(Squish(1.15f, 0.85f, 0.05f));
            StartCoroutine(Wiggle(2f, 0.12f));

            if (pea)
            {
                Rigidbody2D bu = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
                bu.GetComponent<Bullet>().dmg = damage / 2;
                bu.velocity = (3 * Vector2.right + Vector2.up).normalized * shootForce;

                Rigidbody2D bd = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
                bd.GetComponent<Bullet>().dmg = damage / 2;
                bd.velocity = (3 * Vector2.right + Vector2.down).normalized * shootForce;
            }
        }
        else
        {
            shootParitcles.Play();
            Rigidbody2D b = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
            b.GetComponent<Bullet>().dmg = damage;
            b.velocity = (Vector2.up + Vector2.right) * shootForce;
            StartCoroutine(Squish(1.15f, 0.85f, 0.05f));
            StartCoroutine(Wiggle(2f, 0.12f));
        }
    }
}
