using UnityEngine;

public class Mechant : MonoBehaviour
{
    public Sprite FirstHit;
    public Sprite SecondHit;
    public Sprite Dead;
    public TextDialog textDialog;

    public Confiance confiance;

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
        if (confiance.lowTrustStart)
        {
            textDialog.ShowText("Allez, plus vite que ça !!", 3);
        }
    }

    public void Hit()
    {
        Life --;
        
        switch (Life)
        {
            case 0:
                AudioManager.Instance.StopMusicForDuration(2.3f);
                AudioManager.Instance.PlayMechantDeath();
                target_sr.sprite = Dead;
                GameManagerScript.Instance.KillBadGuy();
                textDialog.ShowText("Soupir", 3);
                break;
            case 1 :
                AudioManager.Instance.PlayOuch2();
                textDialog.ShowText("Arrête ça connard !", 3);
                target_sr.sprite = SecondHit;
            break;
            case 2 :
                AudioManager.Instance.PlayOuch1();
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
