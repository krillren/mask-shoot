using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MaskTargetDisplay : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderers;

    public void Render(List<Mask> masks)
    {
        if (masks.Count > spriteRenderers.Count)
        {
            return;
        }
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.sprite = null;
        }
        for (int i = 0; i < masks.Count; i++) 
        {
            spriteRenderers[i].sprite = masks[i].maskSprite; 
        }
    }
}
