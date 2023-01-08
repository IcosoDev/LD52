using System.Collections;
using UnityEngine;

public class FarmerWinBarrier : MonoBehaviour
{
    private bool gone = false;
    public Transition transition;
    private void Start()
    {
        Debug.Log("lol");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Farmer") && gone == false)
        {
            Debug.Log("a");
            StartCoroutine(yes());
            gone = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Farmer") && gone == false)
        {
            Debug.Log("b");
            StartCoroutine(yes());
            gone = true;
        }
    }

    private IEnumerator yes()
    {
        yield return new WaitForEndOfFrame();
        Transition t = Instantiate(transition, transform.position, transform.rotation);
        StartCoroutine(t.Transit("LoseScreen"));
    }
}
