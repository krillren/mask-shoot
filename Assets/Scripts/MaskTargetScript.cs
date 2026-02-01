using System.Collections.Generic;
using UnityEngine;

public class MaskTargetScript : MonoBehaviour
{
    [Header("UI")]
    public RectTransform targetContainer;

    private Vector2 baseSize;
    private Dictionary<int, GameObject> targetsById = new();

    private void Awake()
    {
        baseSize = targetContainer.sizeDelta;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Takes the mask and add the sprite to the target UI
    public void AddTarget(Mask mask)
    {
        // just add to the dictionary for now
        targetsById[mask.maskID] = null;
        ResizeContainer();
    }

    // check with the ID
    public void RemoveTarget(int maskID)
    {
        targetsById.Remove(maskID);
        ResizeContainer();
    }

    private void ResizeContainer()
    {
        if (targetContainer == null) return;
        int count = targetsById.Count;

        float widthPerTarget = baseSize.x;
        targetContainer.sizeDelta = new Vector2(
            baseSize.x + widthPerTarget * Mathf.Max(0, count - 1),
            baseSize.y
        );
    }
}
