using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    float moveSpeed;

    public void Move(Vector3 targetPosition) {

        var movementThisFrame = moveSpeed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
    }

    public void SetSpeed(float newSpeed) {

        moveSpeed = newSpeed;
    }
}
