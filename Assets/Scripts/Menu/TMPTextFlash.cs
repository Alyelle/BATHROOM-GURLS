using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TMPTextFlash : MonoBehaviour
{
    TMP_Text txt;

    public float Frequency = 1f;

    public float MaxAlpha = 1f;
    public float MinAlpha = 0.2f;

    private void Awake()
    {
        txt = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        Color c = txt.color;

        c.a = Mathf.Lerp(MinAlpha, MaxAlpha, Mathf.Sin(Time.time * Frequency) * 0.5f + 0.5f);

        txt.color = c;
    }
}
