using DG.Tweening;
using UnityEngine;

public class ButtonAnimations : MonoBehaviour
{
    private Vector3 startScale;
    private MenuManager menuManager;
    private void Start()
    {
        startScale = transform.localScale;
        menuManager = FindObjectOfType<MenuManager>();
    }
    public void MouseOver()
    {
        transform.DOScale(startScale * 1.06f, 0.1f);
    }

    public void MouseExit()
    {
        transform.DOScale(startScale, 0.1f);
    }

    public void MouseClick(string name)
    {
        menuManager.TransitionOut(name);
    }
}
