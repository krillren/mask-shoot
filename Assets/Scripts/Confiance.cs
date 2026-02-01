using UnityEngine;

public class Confiance : MonoBehaviour
{
    public float maxConfiance = 100;
    public float confiance;
    public float lossPerSeconds;
    public ChargeBar progressBar;
    public bool lowTrustStart = false;
    public float lowTrustMax;
    
    void Start()
    {
        confiance = maxConfiance;
        progressBar.InitBar(maxConfiance, maxConfiance);
    }
    public void AddConfiance(float addValue)
    {
        confiance = Mathf.Clamp(confiance + addValue, 0f, maxConfiance);
    }
    // Update is called once per frame
    void Update()
    {
        var loss = lossPerSeconds * Time.deltaTime;

        lowTrustStart = confiance > lowTrustMax && confiance - loss <= lowTrustMax;

        confiance -= loss;
        progressBar.UpdateBar(confiance);
        if (confiance <= 0f) GameManagerScript.Instance.LoseGame();

    }
}
