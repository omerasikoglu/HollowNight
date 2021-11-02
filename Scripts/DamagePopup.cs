using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public static DamagePopup Create(Vector3 createPosition, int damageAmount, bool isCriticalHit, bool isBlocked = false)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.Instance.pfDamagePopup, createPosition, Quaternion.identity);

        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, isCriticalHit, isBlocked);
        return damagePopup;
    }
    
    private static int sortingOrder;

    private TextMeshPro textMesh;
    private Color textColor;

    [ReadOnly] public Vector3 moveVector;
    [ReadOnly] public float disappearTimer;
    [ReadOnly] public float textMoveSpeedY = 0.5f;
    [ReadOnly] public float textDisappearSpeed = 3f;

    //Frame Optimisation => Pixel art hissini artýrýr text gösteriminde stop motion görüntü elde edersin
    private float frameIntervalTimer = 0.0f;
    private float frameIntervalDuration = 0.034f; // 1saniye / 0.034 = 30fps
    private bool canShowFrame => frameIntervalTimer <= 0f;

    private const float DISAPPEAR_TIMER_MAX = 1f;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }
    private void Update()
    {
        Timers();
        ShowText();

    }

    private void Timers()
    {
        if (frameIntervalTimer > 0)
        {
            frameIntervalTimer -= Time.deltaTime;
        }
    }

    private void ShowText()
    {
        if (canShowFrame)
        {
            transform.position += moveVector * Time.deltaTime;
            moveVector -= moveVector * 5f * Time.deltaTime;

            disappearTimer -= Time.deltaTime;

            //popup'ýn ilk yarýsý
            if (disappearTimer >= DISAPPEAR_TIMER_MAX * 0.5f)
            {
                float increaseScaleAmount = 0.5f;
                transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
            }
            //popup'ýn son yarýsý
            else
            {
                float decreaseScaleAmount = 0.5f;
                transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;

            }
            if (disappearTimer <= 0)
            {
                textColor.a -= textDisappearSpeed * Time.deltaTime;
                textMesh.color = textColor;

                if (textColor.a <= 0) Destroy(gameObject);
            }
            frameIntervalTimer += frameIntervalDuration;
        }
    }

    public void Setup(int damageAmount, bool isCriticalHit, bool isBlocked)
    {
        if (isBlocked)
        {
            textMesh.SetText("<b> 0! </b>");
            textMesh.fontSize = 40;
            textColor = UtilsClass.GetColorFromString("FFFFFF");
        }
        else
        {
            if (isCriticalHit)
            {
                textMesh.SetText("<b>" + damageAmount.ToString() + "</b>");
                textMesh.fontSize = 26;
                textColor = UtilsClass.GetColorFromString("FF2B00"); //red
            }
            else
            {
                textMesh.SetText(damageAmount.ToString());
                textMesh.fontSize = 16;
                textColor = UtilsClass.GetColorFromString("DDAD0D"); //yellow
            }
        }
        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;

        //son çýkan yazý en üstte gözüksün
        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;

        //yazý çýktýðý yerden hafifçe saða ya da sola kaysýn
        int attackDirection = UtilsClass.GetScreenToViewportPosition().x >= 0.5f ? 1 : -1;
        moveVector = new Vector3(attackDirection, 1) * 10f;


    }
}
