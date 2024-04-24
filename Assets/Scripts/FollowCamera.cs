using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // your target is (usually) your player, manually set it in Unity Inspector
    public Transform target;

    // this is how quickly the camera catches up to the target, manually adjust 2f in Unity Inspector
    public float followSpeed = 2f;

    // this is where the target appears within the camera's view, can be dynamically changed in Unity
    public Vector3 offset;

    // here we use "LateUpdate" instead of "Update" to ensure smooth following of the  camera
    void LateUpdate()
    {
        // calculates where the camera needs to be
        Vector3 newPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        // smoothly moves the camera to the new position
        transform.position = Vector3.Lerp(transform.position, newPosition, followSpeed * Time.deltaTime);
    }
}
