using System;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SlideNoteScript : MonoBehaviour
{
    [Header("Settings")]
    [Range(0, 2)]
    [SerializeField] private int beatType; // 0 = J, 1 = K, 2 = L
    [SerializeField] private float speed = 5f;
    [SerializeField] private Vector2 feedbackPosition;

    [Header("References")]
    [SerializeField] private Transform endZone;
    [SerializeField] private TextMeshProUGUI feedbackTextPrefab;
    [SerializeField] private Canvas canvas;

    private Rigidbody2D rb;
    public LineRenderer lineRenderer;
    private KeyCode[] keys = { KeyCode.J, KeyCode.K, KeyCode.L };
    private KeyCode currentKey;

    [SerializeField, Range(0, 2)] private int BeatType;

    private bool isInStartZone = false;
    private bool isInEndZone = false;
    private bool isHolding = false;

    private bool isInRange = false;
    private bool isSliding = false;
    private bool isTouchingEnd = false;

    [SerializeField] private Text scoreText;

    private Transform lineCenter;
    
    private Canvas canv;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentKey = keys[beatType];
        
        lineCenter = GameObject.FindGameObjectWithTag("Line").transform;
        canv = GameObject.FindGameObjectWithTag("Canv").GetComponent<Canvas>();

        // Setup LineRenderer
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        lineRenderer.positionCount = 2;
        
        currentKey = keys[beatType];
    }

    private void Update()
    {
        rb.velocity = Vector2.left * speed;

        // Update the tail
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endZone.position);

        if (isInRange && Input.GetKey(currentKey))
        {
            speed = 0f;
            isSliding = true;
        }
        else
        {
            speed = 6f;
            isSliding = false;
        }

        if (Input.GetKeyUp(currentKey) && isInRange)
        {
            float accuracy = CalculateAccuracy();
            string displayText = accuracy > 0.8f ? "PERFECT" :
                accuracy > 0.6f ? "good" :
                accuracy > 0.2f ? "bad" : "MISS";
            
            if (accuracy > 0.8f)
            {
                ScoreManager.TotalScore += 1f;
            }
            else if (accuracy > 0.6f)
            {
                ScoreManager.TotalScore += 0.6f;
            }
            else if (accuracy > 0.3)
            {
                ScoreManager.TotalScore += 0.3f;
            }
            else
            {
                ScoreManager.TotalScore += 0.2f;
            }
            
            // Instantiate the text
            var t = Instantiate(scoreText, canv.transform);
            t.text = displayText;

            // **FORCE the text to spawn at the same position every time**
            t.rectTransform.anchoredPosition = new Vector2(0, 0);
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Line")
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
            isInRange = true;
        }
        if (other.tag == "slide_end")
        {
            isTouchingEnd = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Line")
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            isInRange = false;
        }
    }
    private float CalculateAccuracy()
    {
        // Distance from circle to the center of the line
        float maxDistance = 1.0f; // Adjust based on the line's size
        
        float distanceFromCenter = Mathf.Abs(transform.position.x - lineCenter.position.x);
        float distanceFromCenter2 = Mathf.Abs(endZone.position.x - lineCenter.position.x);

        // Normalize accuracy: 1.0 (center) to 0.1 (edge)
        float accuracy = Mathf.Clamp01(1f - (distanceFromCenter / maxDistance));
        float accuracy2 = Mathf.Clamp01(1f - (distanceFromCenter2 / maxDistance));
        return Mathf.Max((accuracy + accuracy2) / 2, 0.1f); // Minimum score of 0.1
    }
}