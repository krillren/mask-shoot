using UnityEngine;

public class Mechant : MonoBehaviour
{
    public Sprite FirstHit;
    public Sprite SecondHit;
    public Sprite Dead;

    public Sprite CurrentSprite;

    public int Life = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Hit()
    {
        Life --;
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        switch (Life)
        {
            case 0: 
                sr.sprite = Dead;
                GameManagerScript.Instance.KillBadGuy();
            break;
            case 1 : 
                sr.sprite = SecondHit;
            break;
            case 2 : 
                sr.sprite = FirstHit;
            break;
        }
    }
}
