using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public static DamagePopup Create(Vector3 createPosition, int damageAmount, bool isCriticalHit)
    {
        GameObject a, b;

        Transform damagePopupTransform = Instantiate(GameAssets.Instance.pfDamagePopup, createPosition, Quaternion.identity);

        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, isCriticalHit);
        return damagePopup;
    }
    private static int sortingOrder;

    private TextMeshPro textMesh;
    private Color textColor;

    [ReadOnly] public Vector3 moveVector;
    [ReadOnly] public float disappearTimer;
    [ReadOnly] public float textMoveSpeedY = 0.5f;
    [ReadOnly] public float textDisappearSpeed = 3f;

    private const float DISAPPEAR_TIMER_MAX = 1f;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }
    private void Update()
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
    }
    public void Setup(int damageAmount, bool isCriticalHit)
    {
        if (isCriticalHit)
        {
            textMesh.SetText("<b>" + damageAmount.ToString() + "</b>");
            textMesh.fontSize = 20;
            textColor = UtilsClass.GetColorFromString("FF2B00");
        }
        else
        {
            textMesh.SetText(damageAmount.ToString());
            textMesh.fontSize = 16;
            textColor = UtilsClass.GetColorFromString("FFC500");
        }
        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;

        //son çýkan yazý en üstte gözüksün
        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;

        //yazý çýktýðý yerden hafifçe saða ya da sola kaysýn
        int attackDirection = UtilsClass.GetMouseViewportPosition().x >= 0.5f ? 1 : -1;
        moveVector = new Vector3(attackDirection, 1) * 10f;


    }
}
