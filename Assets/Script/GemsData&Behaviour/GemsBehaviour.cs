using UnityEngine;

public class GemsBehaviour : MonoBehaviour, Collectable
{
    [field: SerializeField] public GemsLocalData GemsData { get; private set; }

    public void BeCollected()
    {
        GemsGlobalData.Instance.ChangeGemsCountRequest(GemsGlobalData.Instance.GemsCount + GemsData.GemsValue);

        Destroy(gameObject);
    }
}

public interface Collectable
{
    public void BeCollected();
}
