using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Transition : MonoBehaviour
{
    private SpriteRenderer sprite;
    public bool fadeIn = false;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        if(fadeIn == true)
        {
            StartCoroutine(FadeIn());
        }
    }
    public IEnumerator Transit(string name)
    {
        sprite.color = new Color32(0, 0, 0, 0);
        sprite.DOFade(1, 0.14f);
        yield return new WaitForSeconds(0.19f);
        DoItLol(name);
    }
    private IEnumerator FadeIn()
    {
        sprite.color = new Color32(0, 0, 0, 255);
        yield return new WaitForSeconds(0.1f);
        sprite.DOFade(0, 0.16f);
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
    public void DoItLol(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
