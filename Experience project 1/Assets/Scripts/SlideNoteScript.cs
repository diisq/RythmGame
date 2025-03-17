using System;
using UnityEngine;
using TMPro;

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

    private bool isInStartZone = false;
    private bool isInEndZone = false;
    private bool isHolding = false;

    private bool isInRange = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentKey = keys[beatType];

        // Setup LineRenderer
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.startColor = Color.cyan;
        lineRenderer.endColor = Color.cyan;
        lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        rb.velocity = Vector2.left * speed;

        // Update the tail
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endZone.position);

        if (isInRange && Input.GetKeyDown(KeyCode.J))
        {
            speed = 0f;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Line")
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
            isInRange = true;
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
}