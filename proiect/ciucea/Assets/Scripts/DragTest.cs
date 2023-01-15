using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTest : MonoBehaviour
{
    public enum DragMode {
        TransformPosition,
        MovePosition,
        Velocity,
        BoxCast
    }

    [Tooltip("Choose how to move the object to the cursor")]
    public DragMode mode;
    [Tooltip("Optional limit on how fast the object can follow")]
    public float maxSpeed = float.PositiveInfinity;
    [Tooltip("Select which layers should block the boxcast drag mode")]
    public LayerMask obstacleLayers;

    Rigidbody2D _body;
    BoxCollider2D _collider;

    delegate YieldInstruction dragMethod(Vector2 destination);    
    bool interzis;
    // Start a drag using the selected method when clicked.
    private void OnTriggerStay2D(Collider2D other) {
        interzis=true;
    }
    void OnMouseDown() {
        dragMethod method = Velocity;
        // Start a function that will run each frame/physics step
        // to update our dragged position until the button is released.
        StartCoroutine(Drag(method));
    }

    // Update the dragged position as long as the mouse button is held.
    IEnumerator Drag(dragMethod dragTo) {
        // Turn off our gravity while we're being dragged.
        float cachedGravityScale = _body.gravityScale;
        _body.gravityScale = 0f;

        // Stash our current offset from the cursor, 
        // so we can preserve it through the move.
        var offset = transform.InverseTransformPoint(ComputeCursorPosition());

        while (Input.GetMouseButton(0) && interzis==false) {
            // Keep the object from accumulating velocity while dragging.
            _body.velocity = Vector2.zero;
            _body.angularVelocity = 0f;

            // Calculate desired drag position.
            var cursor = ComputeCursorPosition();
            var destination = cursor - transform.TransformVector(offset);

            var travel = Vector2.ClampMagnitude(
                destination - transform.position,
                maxSpeed * Time.deltaTime);

            // Let our chosen drag method choose how to get us there.
            yield return dragTo(_body.position + travel);
        }

        // Re-enable gravity as before.
        _body.gravityScale = cachedGravityScale;
    }



    // Effectively the same results as MovePosition.
    YieldInstruction Velocity(Vector2 destination) {
        var velocity = (destination - _body.position) / Time.deltaTime;
        _body.velocity = velocity;
        return new WaitForFixedUpdate();
    }

    
    // Initialize component dependencies.
    void Start() {
        _body = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }

    // Utility functions to compute dragged position.
    float GetDepthOffset(Transform relativeTo) {
        Vector3 offset = transform.position - relativeTo.position;
        return Vector3.Dot(offset, relativeTo.forward);
    }

    Vector3 ComputeCursorPosition() {
        var camera = Camera.main;
        var screenPosition = Input.mousePosition;
        screenPosition.z = GetDepthOffset(camera.transform);
        var worldPosition = camera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}