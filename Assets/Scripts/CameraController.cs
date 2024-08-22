using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 _rotateAround;
    [SerializeField] private float _moveSensitivity = 1.0f;
    [SerializeField] private float _zoomSensitivity = 1.0f;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            transform.RotateAround(_rotateAround, Vector3.up, Input.GetAxis("Mouse X") * _moveSensitivity);
            transform.RotateAround(_rotateAround, transform.right, Input.GetAxis("Mouse Y") * -_moveSensitivity);
            _camera.fieldOfView -= Input.mouseScrollDelta.y * _zoomSensitivity;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
