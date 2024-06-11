using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextOscillator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float amplitude;
    [SerializeField] private float frequencyRadians;
    [SerializeField] private float amplitudeOffset;
    [SerializeField] private float frequencyOffset;
    private float baseX;
    private float baseY;

    private void Start()
    {
        baseX = text.transform.localScale.x;
        baseY = text.transform .localScale.y;
    }


    // Update is called once per frame
    void Update()
    {
        float newX = baseX * (amplitude * Mathf.Sin(frequencyRadians * Mathf.PI * Time.time + frequencyOffset) + amplitudeOffset);
        float newY = baseY * (amplitude * Mathf.Sin(frequencyRadians * Mathf.PI * Time.time + frequencyOffset) + amplitudeOffset);
        Vector2 newScale = new Vector2(newX, newY);
        text.transform.localScale = newScale;
    }
}
