using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageTextController : MonoBehaviour {

    public Text damageText;

    [HideInInspector]
    public int damageDisplayed;
    
    public float lifetime, riseDuration, fadeDuration, riseDistance;
    private float riseElapsedTime, fadeElapsedTime;

    private RectTransform rectTransform;
    private Vector2 textStartPosition, textEndPosition;
    private Color textStartColor, textEndColor;
    private Coroutine TextRiseCoroutine, TextFadeCoroutine;


    void Start() {

        //This has to somehow set the damage displayed from the enemy script...blech
        //damageText.text = damageDisplayed.ToString();

        damageText.text = "Ouch!";

        rectTransform = GetComponent<RectTransform>();
        textStartPosition = rectTransform.anchoredPosition;
        textEndPosition = new Vector2(textStartPosition.x, (textStartPosition.y + riseDistance));

        textStartColor = damageText.color;
        textEndColor = new Color(textStartColor.r, textStartColor.g, textStartColor.b, 0f);

        TextRiseCoroutine = StartCoroutine(RiseText());
        TextFadeCoroutine = StartCoroutine(FadeText());

        Destroy(gameObject, lifetime);

    } //end Start() 
   

    IEnumerator RiseText() {

        while (riseElapsedTime < riseDuration) {
            float t = riseElapsedTime / riseDuration;
            rectTransform.anchoredPosition = Vector2.Lerp(textStartPosition, textEndPosition, t);
            damageText.color = Color.Lerp(textStartColor, textEndColor, t);
            riseElapsedTime += Time.deltaTime;
            yield return null;
        }

    } //end RiseText()

    IEnumerator FadeText() {

        while(fadeElapsedTime < fadeDuration) {
            float t = fadeElapsedTime / fadeDuration;
            damageText.color = Color.Lerp(textStartColor, textEndColor, t);
            fadeElapsedTime += Time.deltaTime;
            yield return null;
        }

    } //end RiseText()

} //end DamageTextController
