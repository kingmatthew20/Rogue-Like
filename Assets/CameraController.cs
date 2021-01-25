using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float angle = 45.0f;
    public float distance = 10.0f;
    public float zoom = 10.0f;

    Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!target) return;

        Vector3 pos = target.position;
        Vector3 direction = Quaternion.AngleAxis(angle, Vector3.right) * -Vector3.forward;
        transform.position = pos + direction * distance;
        transform.forward = -direction;

        camera.orthographicSize = zoom;
    }
}
