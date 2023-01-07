using UnityEngine;
public class PlantShooter : Plant
{
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private Transform shootPoint;
    private float shootTimer;
    [SerializeField] private Rigidbody2D bullet;
    [SerializeField] private float shootForce;
    [SerializeField] private ParticleSystem shootParitcles;

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
        shootParitcles.Play();
        Rigidbody2D b = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        b.GetComponent<Bullet>().dmg = damage;
        b.velocity = Vector2.right * shootForce;
        StartCoroutine(Squish(1.15f, 0.85f, 0.05f));
        StartCoroutine(Wiggle(2f, 0.12f));
    }
}
