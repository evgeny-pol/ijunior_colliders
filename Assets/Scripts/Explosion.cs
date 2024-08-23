using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Tooltip("Сила взрыва.")]
    [SerializeField, Min(0f)] private float _explosionForce;
    [Tooltip("Радиус взрыва.")]
    [SerializeField, Min(0f)] private float _explosionRadius;

    public void Explode(IEnumerable<GameObject> objectsToExplode)
    {
        foreach (GameObject go in objectsToExplode)
        {
            if (go.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }
    }
}
