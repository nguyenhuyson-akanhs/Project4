using UnityEngine;

public class CharacterCollectCollectableItems : MonoBehaviour
{
    private bool CollectCollectableItemsRunning = false;
    public void CollectCollectableItems()
    {
        if (!CollectCollectableItemsRunning)
        {
            CollectCollectableItemsRunning = true;
            _= DoCollectCollectableItems();
        }
        
    }
    private async Awaitable DoCollectCollectableItems()
    {
        GameObject go = transform.parent.gameObject;
        if (go == null)
        {
            Debug.LogWarning("null parent");
            return;
        }

        Transform container = go.GetComponent<FloorLocalData>().CollectableObjectContainer;
        for (int index = container.childCount - 1, step = 1; index >= 0; index--, step++)
        {
            container.GetChild(index).GetComponent<Collectable>().BeCollected();
            if (step >= 20)
            {
                step = 0;
                await Awaitable.WaitForSecondsAsync(0.1f,destroyCancellationToken);
            }
        }

        CollectCollectableItemsRunning = false;
    }
}
