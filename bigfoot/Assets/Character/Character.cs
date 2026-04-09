using UnityEngine;
using UnityEngine.InputSystem;

static class PointExtension
{
    public static Vector3 Expand(this Vector2 v) => new Vector3(v.x, 0, v.y);
    public static Vector2 Flatten(this Vector3 v) => new Vector2(v.x, v.z);

    public static Quaternion RotatedTowards(this Quaternion t, Quaternion goal, float limit = float.PositiveInfinity)
        => Quaternion.RotateTowards(t, goal, limit);

    public static Quaternion LookDirection(this Vector2 v)
        => Quaternion.LookRotation(v.Expand(), Vector3.up);
}

public class Character : MonoBehaviour
{
    [SerializeField]
    float _speed = 1;
    public float speed => _speed;
    [SerializeField]
    float turn = 100;
    public Vector2 dir;

    
    // public void Move(InputAction.CallbackContext context)
    // {
    //     direction = context.ReadValue<Vector2>().normalized;
    //     print(direction);
    // }

    async void Start()
    {
        var body = GetComponent<Rigidbody>();
        while (this)
        {
            await Awaitable.FixedUpdateAsync();
            if (dir.sqrMagnitude > 0)
            {
                body.rotation = body.rotation.RotatedTowards(dir.LookDirection(), turn * Time.deltaTime);
                body.position = Vector3.MoveTowards(body.position, body.position + dir.Expand(), speed * Time.deltaTime);
            }
            body.linearVelocity.Scale(Vector3.up);
        }
    }
}
