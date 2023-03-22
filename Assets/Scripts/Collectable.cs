using System;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public static event Action OnEggCollected;
    public static event Action OnMilkCollected;
    public static event Action OnMeatCollected;
    public static event Action OnWoolCollected;

    public CollectableData collectableData;
    private Vector3 startPosition;

    private static float MIN_SCALING_AMOUNT     = 0.1f;
    private static float WARNING_TIME_1         = 15f;
    private static float WARNING_TIME_2         = 15.3f;
    private static float WARNING_TIME_3         = 15.6f;
    private static float WARNING_TIME_4         = 15.9f;
    private static float WARNING_TIME_5         = 16.2f;
    private static float WARNING_TIME_6         = 16.5f;
    private static float WARNING_TIME_7         = 16.8f;
    private static float WARNING_TIME_8         = 17.1f;
    private static float DESTROY_TIME           = 17.4f;

    private void Start()
    {
        Invoke(nameof(MinimizeCollectable), WARNING_TIME_1);
        Invoke(nameof(MaximizeCollectable), WARNING_TIME_2);
        Invoke(nameof(MinimizeCollectable), WARNING_TIME_3);
        Invoke(nameof(MaximizeCollectable), WARNING_TIME_4);
        Invoke(nameof(MinimizeCollectable), WARNING_TIME_5);
        Invoke(nameof(MaximizeCollectable), WARNING_TIME_6);
        Invoke(nameof(MinimizeCollectable), WARNING_TIME_7);
        Invoke(nameof(MaximizeCollectable), WARNING_TIME_8);
        Destroy(this.gameObject, DESTROY_TIME);
    }

    private void MinimizeCollectable()
    {        
        Vector3 _smallScale= new Vector3(this.transform.localScale.x - MIN_SCALING_AMOUNT, this.transform.localScale.y - MIN_SCALING_AMOUNT, this.transform.localScale.z- MIN_SCALING_AMOUNT);
        this.transform.localScale = _smallScale;     
    }

    private void MaximizeCollectable()
    {
        Vector3 _bigScale = new Vector3(this.transform.localScale.x + MIN_SCALING_AMOUNT, this.transform.localScale.y + MIN_SCALING_AMOUNT, this.transform.localScale.z + MIN_SCALING_AMOUNT);
        this.transform.localScale = _bigScale;
    }

    private void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(collectableData.collectableType == CollectableData.CollectableType.Egg)
            {
                OnEggCollected?.Invoke();
                Destroy(this.gameObject, 0.1f);
            }
            else if(collectableData.collectableType == CollectableData.CollectableType.Milk)
            {
                OnMilkCollected?.Invoke();
                Destroy(this.gameObject, 0.1f);
            }
            else if(collectableData.collectableType == CollectableData.CollectableType.Meat)
            {
                OnMeatCollected?.Invoke();
                Destroy(this.gameObject, 0.1f);
            }
            else if (collectableData.collectableType == CollectableData.CollectableType.Wool)
            {
                OnWoolCollected?.Invoke();
                Destroy(this.gameObject, 0.1f);
            }
        }
    }
}
