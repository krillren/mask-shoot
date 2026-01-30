using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    private bool isShooting = false;
    public Camera MainCam;
    public ParticleSystem ParticleShoot;
    public TargetReshape targetReshape;
    public float duration;
    private void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (!isShooting && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector2 target = MainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, MainCam.nearClipPlane));

            StartCoroutine(ShootDown(target));
        }
    }

    private IEnumerator ShootDown(Vector3 position)
    {
        isShooting = true;
        targetReshape.ShootReshape(position,duration);
        yield return new WaitForSeconds(duration);
        ParticleShoot.transform.position = position;
        ParticleShoot.Play();
        isShooting = false;
    }

}
