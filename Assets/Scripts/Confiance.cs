using UnityEngine;

public class Confiance : MonoBehaviour
{
    public float maxConfiance = 100;
    public float confiance;
    
    void Start()
    {
        confiance = maxConfiance;
    }

    // Update is called once per frame
    void Update()
    {
        if (confiance <= 0) GameManagerScript.Instance.EndGame();
    }
}
