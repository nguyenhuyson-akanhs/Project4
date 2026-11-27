using UnityEngine;

public class AttackLocalData : MonoBehaviour
{
    [field: SerializeField] public float DamageDeal { get; private set; }
    [field: SerializeField] public float MoveDuration { get; private set; }
    [field: SerializeField] public int AttackStepCost { get; private set; }
    [field: HideInInspector] public Vector3 StartPos { get; private set; }

    private void Awake()
    {
        StartPos = transform.position;
    }
}
