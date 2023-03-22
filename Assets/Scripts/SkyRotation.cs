using UnityEngine;

public class SkyRotation : MonoBehaviour
{
    [SerializeField]
    private Material skybox;

    public float x;

    private const float SPEED           = 0.001f;
    private const string PROPERTY_NAME  = "_Rotation";
    private void Start()
    {
        skybox.SetFloat(PROPERTY_NAME, x);
    }

    private void Update()
    {
        x = x + SPEED;
        skybox.SetFloat(PROPERTY_NAME,  x);
    }   
}
