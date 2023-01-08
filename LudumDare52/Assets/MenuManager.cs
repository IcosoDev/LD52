using DG.Tweening;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject gridBackground;
    [SerializeField] private GameObject title, playButton, infoButton, corn, farmer;
    [SerializeField] private float timeBeforeStart;
    [SerializeField] private Transition transition;
    [SerializeField] private GameObject epic;
    [SerializeField] private Text farmersKilled;
    public bool main = false, start = false, lose = false;
    private float timer;
    private void Start()
    {
        StartCoroutine(Animate());
        if(start == true)
        {
            StartCoroutine(AnimateStart());
        }
        if(lose == true)
        {
            farmersKilled.text = PlayerPrefs.GetInt("FarmersKilled").ToString();
        }
    }

    private IEnumerator AnimateStart()
    {
        epic.transform.localScale = Vector3.zero;
        //epic.transform.localEulerAngles = new Vector3(0, 0, 10);
        yield return new WaitForSeconds(0.5f);
        epic.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack);
        //epic.transform.DORotate(new Vector3(0, 0, -10), 1f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(1.5f);
        //epic.transform.DORotate(new Vector3(0, 0, 10), 1f).SetEase(Ease.OutBack);
        //epic.transform.DOScale(Vector3.zero, 1f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(0.25f);
        TransitionOut("MainMenu");
    }

    private void Update()
    {
        timer += Time.deltaTime;
        gridBackground.transform.position = new Vector3(2 * Mathf.Sin(0.5f * timer), 0, 0);
        gridBackground.transform.localEulerAngles = new Vector3(0, 0, 0.2f * Mathf.Cos(2 * timer));
        //float sinTime = Mathf.Sin(timer);
        //float cosTime = Mathf.Cos(timer);
        if (start == false)
        {
            corn.transform.localEulerAngles = new Vector3(0, 0, 2 * Mathf.Cos(1.4f * timer));
            if (main == true)
            {


                title.transform.localEulerAngles = new Vector3(0, 0, 2 * Mathf.Cos(1.1f * timer));
                playButton.transform.localEulerAngles = new Vector3(0, 0, 2 * Mathf.Sin(1.2f * timer));
                infoButton.transform.localEulerAngles = new Vector3(0, 0, 2 * Mathf.Cos(1.3f * timer));
                farmer.transform.localEulerAngles = new Vector3(0, 0, 2 * Mathf.Sin(1.5f * timer));
            }
        }
        if (lose == true)
        {
            title.transform.localEulerAngles = new Vector3(0, 0, 2 * Mathf.Cos(1.1f * timer));
            farmer.transform.localEulerAngles = new Vector3(0, 0, 2 * Mathf.Sin(1.5f * timer));
        }
    }

    private IEnumerator Animate()
    {
        if(main)
        {
            yield return new WaitForEndOfFrame();
            title.transform.localScale = Vector3.zero;
            playButton.transform.localScale = Vector3.zero;
            infoButton.transform.localScale = Vector3.zero;
            corn.transform.localScale = Vector3.zero;
            farmer.transform.localScale = Vector3.zero;
            yield return new WaitForSeconds(timeBeforeStart);
            title.transform.DOScale(Vector3.one, 0.25f);
            yield return new WaitForSeconds(0.06f);
            playButton.transform.DOScale(new Vector2(0.25f, 0.25f), 0.25f);
            yield return new WaitForSeconds(0.06f);
            infoButton.transform.DOScale(new Vector2(0.25f, 0.25f), 0.25f);
            yield return new WaitForSeconds(0.2f);
            corn.transform.DOScale(Vector3.one, 0.25f);
            yield return new WaitForSeconds(0.06f);
            farmer.transform.DOScale(Vector3.one, 0.25f);
        }
    }

    public void TransitionOut(string sceneName)
    {
        Transition t = Instantiate(transition, transform.position, transform.rotation);
        StartCoroutine(t.Transit(sceneName));
    }
}
