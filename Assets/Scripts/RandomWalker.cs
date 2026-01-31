using System.Collections;
using UnityEngine;

public class RandomWalker : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f;               // Movement speed
    public float changeDirectionTime = 2f; // Time between random turns
    public float turnSpeed = 2f;           // How fast it turns to new direction

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector2 targetDirection;
    private float timer;

    private Camera mainCam;
    private Vector2 screenBounds;

    private float halfWidth;
    private float halfHeight;
    Animator animator;

    private bool isIdle = false;
    private float idleDuration = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
        animator = GetComponent<Animator>();
        // Half size of sprite
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        halfWidth = sr.bounds.extents.x;
        halfHeight = sr.bounds.extents.y;

        PickRandomDirection();
        CalculateScreenBounds();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            PickRandomDirection();
        }
        float idleChancePerSecond = 0.1f;

        if (Random.value < idleChancePerSecond * Time.deltaTime)
        {
            StartCoroutine(IdleFor(idleDuration));
        }

        // Smoothly rotate moveDirection toward targetDirection
        moveDirection = Vector2.Lerp(moveDirection, targetDirection, turnSpeed * Time.deltaTime).normalized;
        if (isIdle) moveDirection = Vector2.zero;
        if (moveDirection.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(moveDirection.x) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        animator?.SetBool("isWalking", moveDirection != Vector2.zero);
        ScreenWrap();
    }
    public IEnumerator IdleFor(float duration)
    {
        isIdle = true;
        yield return new WaitForSeconds(duration);
        isIdle = false;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed;
    }

    void PickRandomDirection()
    {
        targetDirection = Random.insideUnitCircle.normalized;
        timer = changeDirectionTime;
    }

    void CalculateScreenBounds()
    {
        Vector3 topRight = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        screenBounds = topRight;
    }

    void ScreenWrap()
    {
        Vector3 pos = transform.position;

        // Fully disappear before wrapping horizontally
        if (pos.x - halfWidth > screenBounds.x)
            pos.x = -screenBounds.x - halfWidth;
        else if (pos.x + halfWidth < -screenBounds.x)
            pos.x = screenBounds.x + halfWidth;

        // Fully disappear before wrapping vertically
        if (pos.y - halfHeight > screenBounds.y)
            pos.y = -screenBounds.y - halfHeight;
        else if (pos.y + halfHeight < -screenBounds.y)
            pos.y = screenBounds.y + halfHeight;

        transform.position = pos;
    }
}
