using UnityEngine;

public class ExplodingObjectActivator : MonoBehaviour
{
    [SerializeField] private LayerMask _explodingObjectsLayer;

    private void Update()
    {
        if (!Input.GetMouseButtonUp(0))
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _explodingObjectsLayer))
            return;

        if (!hitInfo.transform.TryGetComponent(out ExplodingObject explodingObject))
            return;

        explodingObject.Explode();
    }
}
