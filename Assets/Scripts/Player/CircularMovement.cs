using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CircularMovement : MonoBehaviour
{
    [SerializeField] Transform center;
    [SerializeField] float radius;
    [SerializeField] float angularSpeed;
    [SerializeField]
    [Range(0f, 360f)] float startingAngle;

    [Header("Debug")]
    [SerializeField]
    int direction = 1;
    [SerializeField]
    bool directionChangeEnabled = true;
    [SerializeField]
    float currentAngle;
    [SerializeField]
    Vector3 center_position;

    Rigidbody2D rb;

    [SerializeField]
    private float speedMultiplier = 1f;
    public float SpeedMultiplier { get => speedMultiplier; set => speedMultiplier = (value > 0) ? value : speedMultiplier; }

    public Transform Center
    {
        get { return center; }

        set
        {
            center = (value) ? value : transform;
            center_position = center.position;
        }
    }

    private void Awake()
    {
        currentAngle = startingAngle;
        Center = center;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Move(currentAngle);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        currentAngle += angularSpeed * SpeedMultiplier * direction * Time.fixedDeltaTime;
        float excess = currentAngle % 360f;
        if(excess > 360f) { currentAngle /= Mathf.FloorToInt(excess); }
        Move(currentAngle);
    }

    private void Move(float degrees)
    {
        if (!rb) { return; }
        float rad = Mathf.Deg2Rad * degrees;
        float x = Mathf.Cos(rad);
        float y = Mathf.Sin(rad);
        Vector3 velocity = radius * new Vector3(x, y, 0);
        rb.MovePosition(center_position + velocity);
    }

    public void ChangeDirection()
    {
        if (directionChangeEnabled)
        {
            direction *= -1;
        }
    }

    public void SetDirectionChange(bool active)
    {
        directionChangeEnabled = active;
    }
}
