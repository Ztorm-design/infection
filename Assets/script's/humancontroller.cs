using UnityEngine;

public enum Status { Friska, Sjuka, Immuna, Döda }

public class HumanController : MonoBehaviour
{
    public float speed = 2f; // Movement speed
    private Vector2 direction; // Movement direction

    public Status status = Status.Friska;

    public float infectionRadius = 2f; // Infection radius
    public float sicknessDuration = 5f; // Duration of sickness
    private float sicknessTimer = 0f; // Timer to track sickness progression

    // Prefab to instantiate when infection occurs
    public GameObject sjukatagPrefab;

    // Sprites for different statuses
    public Sprite FriskaSprite;
    public Sprite SjukaSprite;
    public Sprite ImmunaSprite;
    public Sprite DödaSprite;

    private SpriteRenderer srenderer;

    void Start()
    {
        // Get the SpriteRenderer attached to the game object
        srenderer = GetComponent<SpriteRenderer>();
        if (srenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on the object. Please add one.");
            return;
        }

        ChangeDirection(); // Start with a random direction
        InvokeRepeating(nameof(ChangeDirection), 3f, 3f); // Change direction every 3 seconds
        UpdateStatusVisuals(); // Initialize visuals based on status
    }

    void Update()
    {
        // Handle movement
        transform.Translate(direction * speed * Time.deltaTime);

        // Check for screen boundaries and reverse direction
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0 || pos.x > 1) direction.x = -direction.x;
        if (pos.y < 0 || pos.y > 1) direction.y = -direction.y;

        // Handle sickness progression
        if (status == Status.Sjuka)
        {
            sicknessTimer += Time.deltaTime;
            if (sicknessTimer >= sicknessDuration)
            {
                // Resolve sickness and update visuals
                ResolveInfection();
            }
        }

        // Update visuals based on the current status
        UpdateStatusVisuals();
    }

    void ChangeDirection()
    {
        // Generate a random movement direction
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void ResolveInfection()
    {
        // Determine outcome after sickness duration
        int roll = Random.Range(0, 100);
        if (roll < 10)
        {
            status = Status.Döda;
            Destroy(gameObject, 1f); // Remove object after a delay to show "dead" visuals
        }
        else if (roll < 50)
        {
            status = Status.Friska;
        }
        else
        {
            status = Status.Immuna;
        }
        sicknessTimer = 0f; // Reset sickness timer
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object has a CircleCollider2D component
        CircleCollider2D collider = collision.collider.GetComponent<CircleCollider2D>();

        if (collider != null && collision.gameObject.CompareTag("Sjuka") && CompareTag("Friska"))
        {
            Debug.Log("Collision with Sjuka detected using CircleCollider2D!");

            int roll = Random.Range(0, 100);
            if (roll < 40) // 40% chance to become sick
            {
                status = Status.Sjuka;

                // Instantiate visual effect (if provided)
                if (sjukatagPrefab != null)
                {
                    Instantiate(sjukatagPrefab, transform.position, Quaternion.identity);
                }
            }
        }
    }

    void UpdateStatusVisuals()
    {
        // Update the sprite based on the current status
        if (srenderer == null)
        {
            Debug.LogError("SpriteRenderer is missing. Cannot update visuals.");
            return;
        }

        switch (status)
        {
            case Status.Friska:
                srenderer.sprite = FriskaSprite;
                break;

            case Status.Sjuka:
                srenderer.sprite = SjukaSprite;
                break;

            case Status.Immuna:
                srenderer.sprite = ImmunaSprite;
                break;

            case Status.Döda:
                srenderer.sprite = DödaSprite;
                break;

            default:
                Debug.LogWarning("Unknown status: " + status);
                break;
        }
    }
}