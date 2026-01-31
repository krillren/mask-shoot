using UnityEngine;

public class CharacterMask : MonoBehaviour
{
    public Mask equippedMask; // Current mask
    public Transform maskSlot; // Where the mask will appear (child transform)
    private GameObject maskVisual; // Instantiated sprite

    // Equip a mask
    public void EquipMask(Mask newMask)
    {
        UnequipMask(); // Remove current mask if any
        equippedMask = newMask;

        if (maskSlot != null && newMask.maskSprite != null)
        {
            maskVisual = new GameObject("MaskVisual");
            maskVisual.transform.SetParent(maskSlot, false);
            var sr = maskVisual.AddComponent<SpriteRenderer>();
            sr.sprite = newMask.maskSprite;
            sr.sortingOrder = 10; // On top of character
        }
    }

    // Remove current mask
    public void UnequipMask()
    {
        equippedMask = null;

        if (maskVisual != null)
        {
            Destroy(maskVisual);
            maskVisual = null;
        }
    }

    public void Hit()
    {
        
    }
}
