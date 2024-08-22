using UnityEngine;

public class ExplodingObject : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [Tooltip("Минимальное количество создаваемых при взрыве объектов.")]
    [SerializeField, Min(0)] private int _newObjectsMin;
    [Tooltip("Максимальное количество создаваемых при взрыве объектов.")]
    [SerializeField] private int _newObjectsMax;
    [Tooltip("Коэффициент размера создаваемых при взрыве объектов")]
    [SerializeField, Min(0.1f)] private float _newObjectScaleCoeff = 0.5f;
    [Tooltip("Вероятность создания объектов при взрыве")]
    [SerializeField, Range(0.0f, 1.0f)] private float _newObjectsSpawnProbability = 1.0f;
    [Tooltip("Коэффициент уменьшения вероятности создания объектов после взрыва.")]
    [SerializeField, Min(0.0f)] private float _spawnProbabilityReduceCoeff = 0.5f;
    [Tooltip("Префаб объекта создаваемого при взрыве")]
    [SerializeField] private ExplodingObject _newObjectPrefab;

    private void OnValidate()
    {
        _newObjectsMax = Mathf.Max(_newObjectsMin, _newObjectsMax);
    }

    private void Start()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
    }

    public void Explode()
    {
        if (Random.value <= _newObjectsSpawnProbability)
        {
            var currentScale = transform.localScale;
            var newScale = currentScale * _newObjectScaleCoeff;
            var newSpawnProbability = _newObjectsSpawnProbability * _spawnProbabilityReduceCoeff;

            for (var i = 0; i < Random.Range(_newObjectsMin, _newObjectsMax + 1); ++i)
            {
                var position = transform.position + currentScale.x * 0.5f * Random.onUnitSphere;
                var newObject = Instantiate(_newObjectPrefab, position, Quaternion.identity);
                newObject.transform.localScale = newScale;
                newObject._newObjectsSpawnProbability = newSpawnProbability;
                newObject.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }

        Destroy(gameObject);
    }
}
