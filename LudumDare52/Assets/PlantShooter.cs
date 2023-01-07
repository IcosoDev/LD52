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
        shootTimer = timeBetweenShots;
    }

    private new void Update()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0/* && canSeeFarmer == true*/)
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
        StartCoroutine(Squish(1.2f, 0.75f, 0.08f));
        StartCoroutine(Wiggle(2f, 0.14f));
    }
}
