using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    public float moveSpeed = 50f;     // How fast the text moves upward
    public float fadeDuration = 1f;   // How long it takes for the text to fade out

    private Text textMesh;
    private Color originalColor;

    private void Awake()
    {
        textMesh = GetComponent<Text>();
        originalColor = textMesh.color;
    }

    private void Start()
    {
        StartCoroutine(MoveAndFade());
    }

    private IEnumerator MoveAndFade()
    {
        float elapsedTime = 0f;
        Vector3 startPos = transform.position;

        while (elapsedTime < fadeDuration)
        {
            // Move upward over time
            transform.position = startPos + new Vector3(0, moveSpeed * (elapsedTime / fadeDuration), 0);

            // Fade out over time
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}

