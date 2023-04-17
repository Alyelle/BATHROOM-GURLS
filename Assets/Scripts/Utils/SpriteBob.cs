using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBob : MonoBehaviour
{
    public float Freqency;
    public float Amplitude;

    float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        transform.localPosition = new Vector3(0f, Mathf.Sin(timer * Freqency) * Amplitude, 0f);
    }
}
