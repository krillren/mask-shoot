using System.Collections;
using UnityEngine;

public class BloodDecay : MonoBehaviour
{
    public float lifeTime = 5f;
    public float fadeDuration = 1f;

    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeAndDestroy());
    }

    IEnumerator FadeAndDestroy()
    {
        yield return new WaitForSeconds(lifeTime - fadeDuration);

        float t = 0;
        Color c = sr.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            sr.color = c;
            yield return null;
        }

        Destroy(gameObject);
    }
}
