using UnityEngine;
using System.Collections.Generic;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField]
    Transform reference = null;

    private void LateUpdate()
    {
        var animator = GetComponentInParent<Animator>();
        var camera = Camera.main.transform;
        var facing = Mathf.Sign(camera.InverseTransformDirection(reference.forward).x);
        var angle = Vector2.Dot(reference.forward.Flatten().normalized, camera.forward.Flatten().normalized);
        var direction = (angle * -.5f + .5f) * facing;
        animator?.SetFloat("direction", direction);
        transform.LookAt(transform.position + camera.forward, Vector3.up);

//        var speed = GetComponentInParent<Character>()?.speed ?? 1;
//        animator?.SetFloat("speed", UpdatePosition() * speed);
    }

    float UpdatePosition()
    {
        var position = transform.position.Flatten();
        var moved = Vector2.Distance(position, previous);
        previous = position;
        return moved / Time.deltaTime;
    }

    Vector2 previous = Vector2.zero;
}
