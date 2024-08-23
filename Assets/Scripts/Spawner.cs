using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Tooltip("Минимальное количество создаваемых объектов.")]
    [SerializeField, Min(0)] private int _newObjectsMin;
    [Tooltip("Максимальное количество создаваемых объектов.")]
    [SerializeField] private int _newObjectsMax;
    [Tooltip("Коэффициент размера создаваемых объектов")]
    [SerializeField, Min(0.1f)] private float _newObjectScaleCoeff = 0.5f;

    private void OnValidate()
    {
        _newObjectsMax = Mathf.Max(_newObjectsMin, _newObjectsMax);
    }

    public GameObject[] Spawn(GameObject original, float offsetRadius = 0f)
    {
        int newObjectsCount = Random.Range(_newObjectsMin, _newObjectsMax + 1);
        var newObjects = new GameObject[newObjectsCount];
        Vector3 currentScale = original.transform.localScale;
        Vector3 newScale = currentScale * _newObjectScaleCoeff;

        for (int i = 0; i < newObjectsCount; ++i)
        {
            Vector3 position = transform.position;

            if (offsetRadius > 0)
                position += Random.onUnitSphere * offsetRadius;

            GameObject newObject = Instantiate(original, position, Quaternion.identity);
            newObject.transform.localScale = newScale;
            newObjects[i] = newObject;
        }

        return newObjects;
    }
}
