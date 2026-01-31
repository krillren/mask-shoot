using UnityEngine;

public class AdjustTextSize : MonoBehaviour
{
    [SerializeField] private SpriteRenderer background;
    void ResizeTextToSprite()
    {
        Vector2 bgSize = background.size;
    }
}
