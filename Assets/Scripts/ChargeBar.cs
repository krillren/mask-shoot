using UnityEngine;

public class ChargeBar : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    float _maxValue;
    Vector3 _initialScale;

    void Awake()
    {
        if (!spriteRenderer)
            spriteRenderer = GetComponent<SpriteRenderer>();

        _initialScale = transform.localScale;
    }

    public void InitBar(float initValue, float maxValue)
    {
        _maxValue = maxValue;
        spriteRenderer.color = Color.green;
        UpdateBar(initValue);
    }

    public void UpdateBar(float value)
    {
        float ratio = Mathf.Clamp01(value / _maxValue);
        spriteRenderer.color = Color.Lerp(Color.red, Color.green, ratio);
        transform.localScale = new Vector3(
            _initialScale.x * ratio,
            _initialScale.y,
            _initialScale.z
        );
    }
}
