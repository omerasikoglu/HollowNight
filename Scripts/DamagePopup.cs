using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    private const float DISAPPEAR_TIMER_MAX = 1f;

    private TextMeshPro textMesh;
    private Color textColor;

    [ReadOnly] public float disappearTimer;
    [ReadOnly] public float textMoveSpeedY = 5f;
    [ReadOnly] public float textDisappearSpeed = 3f;

    public static DamagePopup Create(Vector3 position, int damageAmount, bool isCriticalHit)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.Instance.pfDamagePopup, position, Quaternion.identity);

        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, isCriticalHit);
        return damagePopup;
    }
    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }
    private void Update()
    {
        transform.position += new Vector3(0, textMoveSpeedY) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;
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
            textMesh.fontSize = 24;
            textColor = UtilsClass.GetColorFromString("FF2B00");
        }
        else
        {
            textMesh.SetText(damageAmount.ToString());
            textMesh.fontSize = 16;
            textColor = UtilsClass.GetColorFromString("FFc500");
        }
        textMesh.color = textColor;
        disappearTimer = 1f;
    }
}
