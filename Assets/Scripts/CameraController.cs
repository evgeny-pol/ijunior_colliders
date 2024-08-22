using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 _rotateAround;
    [SerializeField] private float _moveSensitivity = 1.0f;
    [SerializeField] private float _zoomSensitivity = 1.0f;

    private const string HorizontalMouseAxis = "Mouse X";
    private const string VerticalMouseAxis = "Mouse Y";

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
            transform.RotateAround(_rotateAround, Vector3.up, Input.GetAxis(HorizontalMouseAxis) * _moveSensitivity);
            transform.RotateAround(_rotateAround, transform.right, Input.GetAxis(VerticalMouseAxis) * -_moveSensitivity);
            _camera.fieldOfView -= Input.mouseScrollDelta.y * _zoomSensitivity;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
