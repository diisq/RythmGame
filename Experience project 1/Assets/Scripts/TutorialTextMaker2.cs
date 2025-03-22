using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextMaker2 : MonoBehaviour
{
    public Text uiText;
    public string[] messages = 
    { 
        "Press the letter on your keyboard corresponding to the circles", 
        "Try to hit the circles when they are in the center of the line, Have fun!" 
    };
    public float fadeDuration = 2f;
    public float moveUpDistance = 50f;
    public float delayBetweenTexts = 0.5f;
    public AudioSource aud;

    private void Start()
    {
        StartCoroutine(PlayTextSequence());
    }

    IEnumerator PlayTextSequence()
    {
        // If audio exists, wait until aud.time >= 33
        if (aud != null)
        {
            while (aud.time < 33f)
            {
                yield return null; // waits one frame, keeps checking
            }
        }

        // Once ready, start showing text messages
        foreach (string msg in messages)
        {
            uiText.text = msg;
            uiText.color = new Color(1, 1, 1, 1);

            Vector3 startPos = uiText.rectTransform.anchoredPosition;
            Vector3 endPos = startPos + Vector3.up * moveUpDistance;

            float timer = 0f;

            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                float t = timer / fadeDuration;

                // Fade out
                uiText.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
                

                yield return null;
            }

            // Reset position & transparency
            uiText.color = new Color(1, 1, 1, 0);
            uiText.rectTransform.anchoredPosition = startPos;

            yield return new WaitForSeconds(delayBetweenTexts);
        }
    }
}