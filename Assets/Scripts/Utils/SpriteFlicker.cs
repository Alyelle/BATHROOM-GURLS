using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFlicker : MonoBehaviour
{
    public float FlickerRate;

    public Color TargetColor = Color.white;

    Color originalColor = Color.white;

    SpriteRenderer sp;

    float timeElapsed;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();

        originalColor = sp.color;
    }

    private void Update()
    {
        sp.color = Color.Lerp(originalColor, TargetColor, Mathf.Sin(timeElapsed * FlickerRate));

        timeElapsed += Time.deltaTime;
    }
}
