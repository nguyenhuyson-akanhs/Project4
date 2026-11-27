//using System;
//using UnityEngine;

//public class SelectTesting : MonoBehaviour
//{
//    [SerializeField] private float MaxSelectingDistance = 10f;
//    [SerializeField] private LayerMask SelectableLayer;

//    [field: Header("SelectBoxOptions")]
//    [SerializeField] private Transform SelectBox;
//    [SerializeField][Range(0f, 5f)] private float Duration;
//    private Vector3 SelectBoxDefaultPos;
//    private CharacterStatus BoxStats;
//    public static event Action<Transform> GameObjectSelected;

//    private void Awake()
//    {
//        SelectBoxDefaultPos = SelectBox.position;
//        BoxStats = CharacterStatus.Idling;
//    }

//    private void Update()
//    {
//        if (Input.GetMouseButtonDown(0) && BoxStats == CharacterStatus.Idling)
//        {
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


//            if (Physics.Raycast(ray, out var hit, MaxSelectingDistance, SelectableLayer))
//            {
//                GameObjectSelected?.Invoke(hit.transform);

//                _ = SelectBoxMove(hit.transform);
//            }
//            else
//            {
//                _ = SelectBoxMove(SelectBoxDefaultPos);
//            }
//        }
//    }

//    private async Awaitable SelectBoxMove(Transform objSelected)
//    {
//        BoxStats = CharacterStatus.Move;

//        float currentTime = 0f;
//        float percent = 0f;

//        Vector3 startPos = SelectBox.position;

//        while (true)
//        {
//            percent = Mathf.Clamp01(currentTime / Duration);
//            SelectBox.position = Vector3.Lerp(startPos, objSelected.position, percent);

//            await Awaitable.NextFrameAsync(destroyCancellationToken);

//            if (percent == 1f) break;
//            currentTime += Time.deltaTime;
//        }

//        BoxStats = CharacterStatus.Idling;
//    }

//    private async Awaitable SelectBoxMove(Vector3 objSelected)
//    {
//        BoxStats = CharacterStatus.Move;

//        float currentTime = 0f;
//        float percent = 0f;

//        Vector3 startPos = SelectBox.position;

//        while (true)
//        {
//            percent = Mathf.Clamp01(currentTime / Duration);
//            SelectBox.position = Vector3.Lerp(startPos, objSelected, percent);

//            await Awaitable.NextFrameAsync(destroyCancellationToken);

//            if (percent == 1f) break;
//            currentTime += Time.deltaTime;
//        }

//        BoxStats = CharacterStatus.Idling;
//    }
//}
