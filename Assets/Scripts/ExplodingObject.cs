using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Spawner))]
[RequireComponent(typeof(Explosion))]
public class ExplodingObject : MonoBehaviour
{
    [Tooltip("Вероятность создания объектов при взрыве")]
    [SerializeField, Range(0f, 1f)] private float _newObjectsSpawnProbability = 1f;
    [Tooltip("Коэффициент уменьшения вероятности создания объектов после взрыва.")]
    [SerializeField, Min(0f)] private float _spawnProbabilityReduceCoeff = 0.5f;
    
    private Spawner _spawner;
    private Explosion _explosion;

    private void Awake()
    {
        _spawner = GetComponent<Spawner>();
        _explosion = GetComponent<Explosion>();
    }

    private void Start()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
    }

    public void Explode()
    {
        TrySpawnObjects();
        Destroy(gameObject);
    }

    private void TrySpawnObjects()
    {
        float randomValue = Random.value;
        Debug.Log($"Trying to spawn objects, random value: {randomValue}, spawn probability: {_newObjectsSpawnProbability}");
        
        if (randomValue > _newObjectsSpawnProbability)
            return;

        GameObject[] newObjects = _spawner.Spawn(gameObject, transform.localScale.x * 0.5f);
        float newSpawnProbability = _newObjectsSpawnProbability * _spawnProbabilityReduceCoeff;

        foreach (GameObject newObject in newObjects)
        {
            newObject.GetComponent<ExplodingObject>()._newObjectsSpawnProbability = newSpawnProbability;
        }

        _explosion.Explode(newObjects);
    }
}
