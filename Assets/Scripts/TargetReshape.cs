using System.Collections;
using UnityEngine;

public class TargetReshape : MonoBehaviour
{
    public SpriteRenderer sprite;
    public void ShootReshape(Vector3 position,float duration)
    {
        transform.position = position;
        sprite.enabled = true;
        StartCoroutine(Reshape(duration));
    }
    public IEnumerator Reshape(float duration)
    {
        float time = 0f;

        Vector3 startScale = Vector3.one;
        Vector3 endScale = Vector3.zero;

        transform.localScale = startScale;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }

        transform.localScale = endScale;
        sprite.enabled = false;
    }
}
