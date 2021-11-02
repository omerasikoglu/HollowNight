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

    //Frame Optimisation => Pixel art hissini art�r�r text g�steriminde stop motion g�r�nt� elde edersin
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

            //popup'�n ilk yar�s�
            if (disappearTimer >= DISAPPEAR_TIMER_MAX * 0.5f)
            {
                float increaseScaleAmount = 0.5f;
                transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
            }
            //popup'�n son yar�s�
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

        //son ��kan yaz� en �stte g�z�ks�n
        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;

        //yaz� ��kt��� yerden hafif�e sa�a ya da sola kays�n
        int attackDirection = UtilsClass.GetScreenToViewportPosition().x >= 0.5f ? 1 : -1;
        moveVector = new Vector3(attackDirection, 1) * 10f;


    }
}
