using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeatScript : MonoBehaviour
{
    // Internal variables
    private Rigidbody2D _rb;
    private bool isInRange;
    
    [Range(0, 2), SerializeField] int beatType;
    
    KeyCode[] types = {KeyCode.J, KeyCode.K, KeyCode.L};

    private KeyCode beatPressButton;

    // Inspector "public" variables
    [SerializeField] private float speed;
    [SerializeField] private Transform lineCenter; // Reference to the line's center
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        scoreText = GameObject.FindGameObjectWithTag("HitInfo").GetComponent<TextMeshProUGUI>();
        lineCenter = GameObject.FindGameObjectWithTag("Line").transform;

        beatPressButton = types[beatType];
    }

    void Update()
    {
        _rb.velocity = new Vector2(-speed, 0);

        if (isInRange && Input.GetKeyDown(beatPressButton))
        {
            float accuracy = CalculateAccuracy();

            Debug.Log($"Hit! Accuracy: {accuracy:F2}");

            if (accuracy > 0.8f)
            {
                scoreText.text = "H I T";
            }

            if (accuracy > 0.6f && accuracy < 0.8f)
            {
                scoreText.text = "eh";
            }

            if (accuracy <= 0.6f)
            {
                scoreText.text = "S H I T";
            }

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Line"))
        {
            GetComponent<SpriteRenderer>().color = Color.red; // Change color when in range
            isInRange = true;
        }
        else if (other.CompareTag("End"))
        {
            Debug.Log("Miss!");
            scoreText.text = "Miss";
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Line"))
        {
            GetComponent<SpriteRenderer>().color = Color.white; // Reset color when out of range
            isInRange = false;
        }
    }

    private float CalculateAccuracy()
    {
        // Distance from circle to the center of the line
        float maxDistance = 1.0f; // Adjust based on the line's size
        float distanceFromCenter = Mathf.Abs(transform.position.x - lineCenter.position.x);

        // Normalize accuracy: 1.0 (center) to 0.1 (edge)
        float accuracy = Mathf.Clamp01(1f - (distanceFromCenter / maxDistance));
        return Mathf.Max(accuracy, 0.1f); // Minimum score of 0.1
    }

    
}