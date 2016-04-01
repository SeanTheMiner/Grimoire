using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace DamageTextObjects {

    public class DamageTextController : MonoBehaviour {

        public TextMesh damageTextMesh;

        [HideInInspector]
        public int damageDisplayed;

        public float lifetime, riseDuration, fadeDuration, riseDistance;
        private float riseElapsedTime, fadeElapsedTime;
        
        private RectTransform rectTransform;
        private Vector2 textStartPosition, textEndPosition;
        private Color textStartColor, textEndColor;

        public enum HitType {
            Null,
            PhysicalHit,
            PhysicalCrit,
            PhysicalMiss,
            PhysicalBlock,
            MagicalHit,
            MagicalCrit,
            MagicalMiss,
            MagicalBlock,
            Heal,
            HealCrit,
            TrueHit,
            TrueCrit
        }

        public HitType hitType;


        void Start() {

            rectTransform = GetComponent<RectTransform>();

            textStartPosition = rectTransform.anchoredPosition;
            textEndPosition = GetRiseComponents(textStartPosition, hitType);

            textStartColor = damageTextMesh.color;
            textEndColor = new Color(textStartColor.r, textStartColor.g, textStartColor.b, 0f);

            StartCoroutine(RiseText());
            StartCoroutine(FadeText());

            Destroy(gameObject, lifetime);

        } //end Start() 


        private Vector2 GetRiseComponents(Vector2 startVector, HitType hitType) {

            Vector2 workVector;
            Vector2 returnVector;

            //workVector.x = (Random.Range(1, riseDistance));
            //workVector.y = Mathf.Sqrt(riseDistance - workVector.x);
            //Mathf.Pow(workVector.x, 2);
            // Mathf.Pow(workVector.y, 2);

            workVector.x = Mathf.Sqrt(2 * riseDistance);
            workVector.y = Mathf.Sqrt(2 * riseDistance);

            returnVector = new Vector2((workVector.x + startVector.x) , (workVector.y + startVector.y));

            if ((hitType == HitType.PhysicalHit) | (hitType == HitType.PhysicalCrit)) {
                returnVector.x *= -1;
            }
            else if ((hitType == HitType.MagicalHit) | (hitType == HitType.MagicalCrit)) {
                returnVector.x *= 1;
            }
            else if ((hitType == HitType.PhysicalBlock) | (hitType == HitType.PhysicalMiss)) {
                returnVector.x *= -1;
                returnVector.y *= -1;
            }
            else if ((hitType == HitType.MagicalBlock) | (hitType == HitType.MagicalMiss)) {
                returnVector.y *= -1;
            }
            else if ((hitType == HitType.Heal) | (hitType == HitType.HealCrit) 
                | (hitType == HitType.TrueHit) | (hitType == HitType.TrueCrit)) {
                returnVector.x = 0;
                returnVector.y = riseDistance;
            }

            return returnVector;

        } //end GetRiseComponents

 

        IEnumerator RiseText() {

            while(riseElapsedTime < riseDuration) {
                float t = riseElapsedTime / riseDuration;
                rectTransform.anchoredPosition = Vector2.Lerp(textStartPosition, textEndPosition, t);
                riseElapsedTime += Time.deltaTime;
                yield return null;
            }

        } //end RiseText()

        IEnumerator FadeText() {

            while(fadeElapsedTime < fadeDuration) {
                float t = fadeElapsedTime / fadeDuration;
                damageTextMesh.color = Color.Lerp(textStartColor, textEndColor, t);
                fadeElapsedTime += Time.deltaTime;
                yield return null;
            }

        } //end RiseText()

    } //end DamageTextController

} //end namespace
