using UnityEngine;

public class Mechant : MonoBehaviour
{
    public Sprite FirstHit;
    public Sprite SecondHit;
    public Sprite Dead;
    public TextDialog textDialog;

    public Sprite CurrentSprite;
    public SpriteRenderer target_sr;
    public int Life = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textDialog.ShowText("Bienvenue ! Tire sur les ennemis portant le masque indiqué en bas à droite de ta vision.", 3);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Hit()
    {
        Life --;
        
        switch (Life)
        {
            case 0:
                target_sr.sprite = Dead;
                GameManagerScript.Instance.KillBadGuy();
                textDialog.ShowText("Soupir", 3);
                break;
            case 1 :
                textDialog.ShowText("Arrête ça connard !", 3);
                target_sr.sprite = SecondHit;
            break;
            case 2 :
                target_sr.sprite = FirstHit;
                textDialog.ShowText("C'est pas du vrai boulot, ça ! Il va falloir vous ressaisir, et vite !", 3);
                break;
        }
    }

    public void UpdateNewTargets()
    {
        textDialog.ShowText("Attention ! Nous avons reçu de nouvelles informations de nos agents ! Les terroristes ont modifié leur camouflage.", 3);
    }

    public void CongratulateTargetAchieved()
    {
        textDialog.ShowText("Bravo ! Vous avez éliminé tous les terroristes utilisant un des masques de camouflage.", 2);
    }

    public void WarnForCasualties()
    {
        textDialog.ShowText("On vous a dit de ne pas tuer d'innocents...", 4);
    }
}
