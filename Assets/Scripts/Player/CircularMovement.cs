using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    public static CircularMovement Instance;

    [SerializeField] Transform center;
    [SerializeField] float radius;
    [SerializeField] float angularSpeed;
    [SerializeField]
    [Range(0f, 360f)] float startingAngle;

    [SerializeField]
    private List<Rigidbody2D> toMove = new List<Rigidbody2D>();

    [Header("Debug")]
    [SerializeField]
    int direction = 1;
    [SerializeField]
    bool directionChangeEnabled = true;
    [SerializeField]
    float currentAngle;
    [SerializeField]
    Vector3 center_position;

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
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        currentAngle = startingAngle;
        Center = center;
    }

    private void Start()
    {
        Move(currentAngle);
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void MoveThis(Rigidbody2D rb)
    {
        if (!toMove.Contains(rb)) {
            toMove.Add(rb);
        }
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
        if (toMove.Count < 1) { return; }
        float rad = Mathf.Deg2Rad * degrees;
        float x = Mathf.Cos(rad);
        float y = Mathf.Sin(rad);
        Vector3 velocity = radius * new Vector3(x, y, 0);
        foreach(Rigidbody2D rb in toMove)
        {
            rb.MovePosition(center_position + velocity);
        }
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

    public Vector3 GetPoint(float degrees)
    {
        float rad = Mathf.Deg2Rad * degrees;
        float x = Mathf.Cos(rad);
        float y = Mathf.Sin(rad);
        return new Vector3(x, y, 0) * radius + center_position;
    }

    public Vector3 GetRandomPoint()
    {
        float degrees = Random.Range(0f, 360f);
        return GetPoint(degrees);
    }
}
