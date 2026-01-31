using System;
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

    public float radius;
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
        RaycastHit(position);
        isShooting = false;
    }

    private void RaycastHit(Vector3 pos)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            pos,
            radius
        );
        foreach (Collider2D hit in hits)
        {
            CharacterMask mask = hit.GetComponent<CharacterMask>();
            mask?.Hit();

            Mechant mechant = hit.GetComponent<Mechant>();
            mechant?.Hit();
        }
    }
}
