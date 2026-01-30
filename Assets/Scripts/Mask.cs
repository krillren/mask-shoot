using UnityEngine;

[CreateAssetMenu(fileName = "NewMask", menuName = "Character/Mask")]
public class Mask : ScriptableObject
{
    public string maskName;
    public int maskID;
    public Sprite maskSprite;
}
