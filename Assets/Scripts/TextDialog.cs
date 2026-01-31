using System.Collections;
using TMPro;
using UnityEngine;

public class TextDialog : MonoBehaviour
{
    [SerializeField] TextMeshPro text;
    [SerializeField] SpriteRenderer spriteRenderer;
    public void Awake()
    {
        text.gameObject.SetActive(false);
        spriteRenderer.gameObject.SetActive(false);
    }
    public void ShowText(string message, float duration)
    {
        StartCoroutine(ShowMessage(message, duration));
    } 
    
    private IEnumerator ShowMessage(string message,float duration)
    {
        text.gameObject.SetActive(true);
        spriteRenderer.gameObject.SetActive(true);
        text.text = message;
        yield return new WaitForSeconds(duration);
        text.gameObject.SetActive(false);
        spriteRenderer.gameObject.SetActive(false);
    }

}
