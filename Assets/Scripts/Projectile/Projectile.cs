using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float timeAlive;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, timeAlive);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 delta = speed * Time.fixedDeltaTime * transform.up;
        rb.MovePosition(delta + rb.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
