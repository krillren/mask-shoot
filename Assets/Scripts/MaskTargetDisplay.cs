using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MaskTargetDisplay : MonoBehaviour
{
    public Dictionary<Mask,SpriteRenderer> maskTargets;
    public List<SpriteRenderer> spriteRenderers;
    public List<Mask> masksDisplayed;
    private void Awake()
    {
        masksDisplayed = new();
    }
    public void AddTarget(Mask mask)
    {
        //if (maskTargets.Count >= 3) return;
        masksDisplayed.Add(mask);
        Render(masksDisplayed);
    }

    public void RemoveTarget(Mask mask)
    {
        masksDisplayed.Remove(mask);
        Render(masksDisplayed);
    }

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
