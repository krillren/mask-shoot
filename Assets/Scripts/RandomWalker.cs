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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;

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

        // Smoothly rotate moveDirection toward targetDirection
        moveDirection = Vector2.Lerp(moveDirection, targetDirection, turnSpeed * Time.deltaTime).normalized;

        // Rotate rectangle to face movement
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        ScreenWrap();
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
