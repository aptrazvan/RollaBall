using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private const float Y_ANGLE_MIN = -50.0f;
    private const float Y_ANGLE_MAX = 87.0f;

    public Transform lookAt;
    public Transform camTransform;
    private Camera cam;
    private float distance = 7.5f;
	public float currentX;
	public float currentY;
    private float sensivityX = 4.0f;
    private float sensivityY = 1.0f;
	public JoystickController joystick;

    private void Start()
    {
        camTransform = transform;
        cam = Camera.main;
    }

    private void Update()
    {
       // currentX += Input.GetAxis("Mouse X");
       // currentY += Input.GetAxis("Mouse Y");

		currentX += 2 * joystick.Horizontal ();
		currentY -= joystick.Vertical ();

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);
    }
}
  

