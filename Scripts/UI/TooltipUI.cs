using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance { get; private set; }

    [SerializeField] private RectTransform canvasRectTransform; //parent rect
    private TextMeshProUGUI textMeshPro;
    private RectTransform backgroundRectTransform;
    private RectTransform rectTransform;
    private TooltipTimer tooltipTimer;

    private Transform tooltipTransform;
    private void Awake()
    {
        Instance = this;
        rectTransform = transform.GetComponent<RectTransform>();
        textMeshPro = transform.Find(Datalarimiz.TEXT).GetComponent<TextMeshProUGUI>();
        backgroundRectTransform = transform.Find(Datalarimiz.BACKGROUND).GetComponent<RectTransform>();
        Hide();
    }
    private void Update()
    {
        HandleFollowTarget(tooltipTransform);

        if (tooltipTimer != null)
        {
            tooltipTimer.timer -= Time.deltaTime;
            if (tooltipTimer.timer <= 0)
            {
                Hide();
            }
        }
    }
    private void HandleFollowTarget(Transform tooltipTransform = null)
    {
        //null'sa mouse'u takip eder
        Vector2 anchoredPosition = tooltipTransform == null ? Input.mousePosition / canvasRectTransform.localScale.x :
             UtilsClass.GetWorldToScreenPosition(tooltipTransform)/ canvasRectTransform.localScale.x;

        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }
        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }

        rectTransform.anchoredPosition = anchoredPosition;
    }
    private void SetText(string tooltipText)
    {                                          //yazacak yazý
        textMeshPro.SetText(tooltipText);
        textMeshPro.ForceMeshUpdate();
        //arka planý çizdiririz
        Vector2 padding = new Vector2(8, 8);
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        backgroundRectTransform.sizeDelta = textSize + padding; //sizedelta: anchorlar arasýndaki mesafe
    }
    public void Show(string tooltipText, TooltipTimer tooltipTimer = null, Transform tooltipTransform = null)
    {
        this.tooltipTimer = tooltipTimer;
        this.tooltipTransform = tooltipTransform;

        if (tooltipTimer == null)
        {
            this.tooltipTimer = new TooltipTimer { timer = 3f };
        }
        gameObject.SetActive(true);
        SetText(tooltipText);
        //HandleFollowTarget(this.tooltipTransform);
        // 1 frame kaçýrmamak için bunu ekledik diðer türlü ilk show sonra update çalýþýyordu
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public class TooltipTimer
    {
        public float timer;
    }
}
