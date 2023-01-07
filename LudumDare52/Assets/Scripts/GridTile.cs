using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
public class GridTile : MonoBehaviour
{
    public Sprite spriteToDisplay, blank;
    private Image image;
    private PlantPlacerManager placerManager;
    public bool available = true;
    [SerializeField] private SpriteRenderer grassSpriteRenderer;
    [SerializeField] private Sprite[] grassSprites;
    private void Awake()
    {
        image = GetComponent<Image>();
        placerManager = FindObjectOfType<PlantPlacerManager>();
        image.color = new Color32(255, 255, 255, 0);

        //grassSpriteRenderer.sprite = grassSprites[Random.Range(0, grassSprites.Length)];
        //grassSpriteRenderer.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));
    }

    public void ShowDisplay()
    {
        if(placerManager.isAPlantSelected == true && available == true)
        {
            placerManager.transform.position = transform.position;
            placerManager.spriteRenderer.sprite = placerManager.plantSprites[placerManager.IDOfSelectedPlant];
            //image.sprite = spriteToDisplay;
            //image.DOFade(1, 0.04f);
        }

    }

    public void RemoveDisplay()
    {
        placerManager.spriteRenderer.sprite = blank;
    }

    public void PlacePlant()
    {
        if (placerManager.isAPlantSelected == true && available == true)
        {
            Instantiate(placerManager.plant[placerManager.IDOfSelectedPlant], transform.position, Quaternion.identity);
            RemoveDisplay();
            placerManager.isAPlantSelected = false;
            available = false;
        }
    }
}