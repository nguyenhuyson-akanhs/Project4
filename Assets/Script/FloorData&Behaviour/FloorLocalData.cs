using UnityEngine;

public class FloorLocalData : GenericLocalData
{
    [field: SerializeField] public Transform Model { get; private set; }
    [field: SerializeField] public Transform CollectableObjectContainer { get; private set; }
    [field: SerializeField] public Transform CharacterContainer {  get; private set; }
    [field: SerializeField] public Transform EnemisContainer {  get; private set; }

    private void Awake()
    {
        TruePosition = transform.position;
    }
}

public class GenericLocalData : MonoBehaviour
{
    [HideInInspector] public Vector3 TruePosition;
}
