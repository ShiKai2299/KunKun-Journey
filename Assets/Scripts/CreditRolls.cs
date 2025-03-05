using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for UI elements like Image

public class CreditRolls : MonoBehaviour
{
    public float scrollSpeed = 40f;

    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        // Get the RectTransform component of the UI element (e.g., Image)
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the UI element upwards
        rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
    }
}