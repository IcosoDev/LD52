using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class MovingPanel : MonoBehaviour/*, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IInitializePotentialDragHandler*/
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask uiLayer;
    [SerializeField] private Sprite normalSprite, outlinedSprite;
    [SerializeField] private Image spriteImage;
    public int id; //0 corn, //1 wheat, //2 cotton, //3 pea, //4 potato, //5 pumpkin
    private PlantPlacerManager placerManager;

    private void Start()
    {
        placerManager = FindObjectOfType<PlantPlacerManager>();
    }

    private void Update()
    {
        if (Physics2D.Raycast(transform.position + new Vector3(-3, 0, 0), Vector2.left, 3f, uiLayer) == false)
        {
            transform.position -= Vector3.right * moveSpeed;
        }
    }

    public void Selected()
    {
        if(!placerManager.isAPlantSelected)
        {
            placerManager.PopSFX();
            spriteImage.sprite = outlinedSprite;
            spriteImage.transform.DOScale(1.08f, 0.14f);
        }
    }

    public void Deselected()
    {
        if (!placerManager.isAPlantSelected)
        {
            spriteImage.sprite = normalSprite;
            spriteImage.transform.DOScale(1f, 0.14f);
        }
    }

    public void SelectThisCrop()
    {
        if (!placerManager.isAPlantSelected)
        {
            placerManager.isAPlantSelected = true;
            placerManager.IDOfSelectedPlant = id;
            Destroy(gameObject);
        }
    }










    //[SerializeField] private Canvas canvas;
    //private CanvasGroup canvasGroup;
    //private RectTransform rectTransform;
    //private void Awake()
    //{
    //    rectTransform = GetComponent<RectTransform>();
    //    canvasGroup = GetComponent<CanvasGroup>();
    //    canvas = GetComponentInParent<Canvas>();
    //    col = GetComponent<BoxCollider2D>();
    //}

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    canvasGroup.blocksRaycasts = false;
    //    canvasGroup.alpha = 0.6f;
    //    col.enabled = false;
    //    moveSpeed = 0;
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    canvasGroup.blocksRaycasts = true;
    //    canvasGroup.alpha = 1f;
    //}

    //public void OnPointerDown(PointerEventData eventData)
    //{

    //}

    //public void OnInitializePotentialDrag(PointerEventData eventData)
    //{
    //    eventData.useDragThreshold = false;
    //}

    //public void OnDrop(PointerEventData eventData)
    //{
    //    throw new System.NotImplementedException();
    //}
}
