using UnityEngine;
public class PlantPlacerManager : MonoBehaviour
{
    public bool isAPlantSelected = true;
    public Sprite[] plantSprites; //corn 0, other veggie 1, idk
    public Plant[] plant;
    public int IDOfSelectedPlant;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    public AudioSource sfxPop;
    public AudioClip[] pop;


    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void PopSFX()
    {
        if (!sfxPop.isPlaying)
        {
            sfxPop.clip = pop[Random.Range(0, pop.Length)];
            sfxPop.pitch = Random.Range(0.95f, 1.05f);
            sfxPop.Play();
        }
    }
}
