using UnityEngine;

public class Confiance : MonoBehaviour
{
    public float maxConfiance = 100;
    public float confiance;
    public float lossPerSeconds;
    public ChargeBar progressBar;
    
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
        confiance -= lossPerSeconds * Time.deltaTime;
        progressBar.UpdateBar(confiance);
        if (confiance <= 0f) GameManagerScript.Instance.EndGame();
    }
}
