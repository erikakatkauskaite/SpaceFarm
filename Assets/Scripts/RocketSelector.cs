using System;
using UnityEngine;

public class RocketSelector : MonoBehaviour
{
    public static event Action OnRocketSelected;
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnRocketSelected?.Invoke();
        }
    }
}
