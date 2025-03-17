using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonBiggernator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scaleFactor = 1.2f; 
    public float speed = 5f; 
    private Vector3 originalScale;
    private Vector3 targetScale;

    void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
    }

    void Update()
    {
        if (GetComponent<Button>().interactable)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * scaleFactor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
    }
}