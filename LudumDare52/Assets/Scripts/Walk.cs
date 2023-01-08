using UnityEngine;
using System.Collections;
public class Walk : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float stepForce;
    [SerializeField] private float timeBetweenSteps;
    [SerializeField] private float stepTime;
    private FarmerScript farmerScript;
    private GameManager gameManager;
    private float stepTimer;
    private Rigidbody2D rb;
    void Start()
    {
        direction.Normalize();
        rb = GetComponent<Rigidbody2D>();
        farmerScript = GetComponent<FarmerScript>();
        gameManager = FindObjectOfType<GameManager>();
        timeBetweenSteps -= gameManager.difficultyLevel / 10;
        stepTimer = timeBetweenSteps;
    }

    void Update()
    {
        stepTimer -= Time.deltaTime;
        if (stepTimer <= 0 && farmerScript.state == FarmerScript.State.Moving)
        {
            Step();
            stepTimer = timeBetweenSteps;
        }
    }

    public void Step()
    {
        StartCoroutine(TakeStep());
        StartCoroutine(farmerScript.Squish(1.18f, 0.7f, 0.08f));
        StartCoroutine(farmerScript.Wiggle(5f, 0.14f));
    }

    IEnumerator TakeStep()
    {
        rb.velocity = direction * stepForce;
        yield return new WaitForSeconds(stepTime);
        rb.velocity = Vector2.zero;
    }
}