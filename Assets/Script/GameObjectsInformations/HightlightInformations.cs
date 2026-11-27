using UnityEngine;

public class HighlightInformations : MonoBehaviour
{
    [field: SerializeField] public Material ShowHighlightMat { get; private set; }
    [field: SerializeField] public Material HideHighlightMat { get; private set; }
    //[field: SerializeField] public GameObject HighlightByObject { get; private set; }
}