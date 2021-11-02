using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player örn. bi eve girdiðinde çatýnýn a'sýnýn solmasý ve player'ý görebilmek

public class FadingSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private float targetAlpha = 1f;
    private float defaultAlpha = 1f;
    private float fadedAlpha = .6f;

    private float fadeSpeed = 1f;
    private bool changing = false;

    private void Start()
    {
        spriteRenderer = transform.Find(Datalarimiz.ALIVE).GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (spriteRenderer.color.a == targetAlpha) changing = false;

        if (targetAlpha == defaultAlpha && changing) //sertleþme
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b,
                Mathf.MoveTowards(spriteRenderer.color.a, targetAlpha, fadeSpeed * Time.deltaTime));
        }
        if (targetAlpha == fadedAlpha && changing) //saydamlaþma
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b,
              Mathf.MoveTowards(spriteRenderer.color.a, targetAlpha, fadeSpeed * Time.deltaTime));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            ChangeFadeState();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            ChangeFadeState();
        }
    }
    private void ChangeFadeState()
    {
        targetAlpha = targetAlpha == defaultAlpha ? fadedAlpha : defaultAlpha;
        changing = true;
    }
}
