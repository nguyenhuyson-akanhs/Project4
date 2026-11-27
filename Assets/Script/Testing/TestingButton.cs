using UnityEngine;

public class TestingButton : MonoBehaviour
{
    public void OnInteract()
    {
        Debug.Log(gameObject.name + " Interacted");
    }
}
