using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 2f;

    private Rigidbody2D rb;
    private Vector2 target;

    public Vector2 DeltaThisStep { get; private set; }
    private Vector2 prevPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        target = pointA.position;
        prevPos = rb.position;
    }

    private void FixedUpdate()
    {
        prevPos = rb.position;

        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        DeltaThisStep = newPos - prevPos;

        if (Vector2.Distance(newPos, target) < 0.05f)
        {
            target = (target == (Vector2)pointA.position) ? (Vector2)pointB.position : (Vector2)pointA.position;
        }
    }
}
