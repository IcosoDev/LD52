using UnityEngine;
public class PlantPlacerManager : MonoBehaviour
{
    public bool isAPlantSelected = true;
    public Sprite[] plantSprites; //corn 0, other veggie 1, idk
    public Plant[] plant;
    public int IDOfSelectedPlant;
    [HideInInspector] public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
}
