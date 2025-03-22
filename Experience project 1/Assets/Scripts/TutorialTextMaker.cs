using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialTextMaker : MonoBehaviour
{
    public Text uiText;
    public string[] messages = { "Press the letter on your keyboard corresponding to the circles", "Try to hit the circles when they in the center of the line, Have fun!" };
    public float fadeDuration = 2f;
    public float moveUpDistance = 50f;
    public float delayBetweenTexts = 0.5f;
    [SerializeField] private AudioSource aud;

    private void Start()
    {
        StartCoroutine(PlayTextSequence());
    }

    IEnumerator PlayTextSequence()
    {
        
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

                // Move up
                uiText.rectTransform.anchoredPosition = Vector3.Lerp(startPos, endPos, t);

                yield return null;
            }

            // Ensure it's fully faded and moved
            uiText.color = new Color(1, 1, 1, 0);
            uiText.rectTransform.anchoredPosition = startPos;

            yield return new WaitForSeconds(delayBetweenTexts);
        }
    }
}