using UnityEngine;

public class CharacterMask : MonoBehaviour
{
    public Mask equippedMask; // Current mask

    // Equip a mask
    public void EquipMask(Mask newMask)
    {
        UnequipMask(); // Remove current mask if any
        equippedMask = newMask;

        if ( newMask.maskSprite != null)
        {
            var sr = transform.GetChild(0).GetComponentInChildren<SpriteRenderer>();
            sr.sprite = newMask.maskSprite;
            sr.sortingOrder = 10; // On top of character
        }
    }

    // Remove current mask
    public void UnequipMask()
    {
        equippedMask = null;
    }

    public void Hit()
    {
        GameManagerScript.Instance.OnCharacterHit(this);
    }
}
