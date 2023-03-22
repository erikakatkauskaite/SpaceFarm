using UnityEngine;

public class Ghost : MonoBehaviour
{
    private int hitCount;

    private static float DESTROY_TIME       = 20f;
    private static int HIT_NEEDED           = 8;
    private static float MINIMIZE_KOEF      = 0.05f;
         

    private void Start()
    {
        hitCount = 0;
        Destroy(this.gameObject, DESTROY_TIME);
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MinimizeEnemy();
            hitCount++;
            if (hitCount >= HIT_NEEDED)
            {
                Destroy(this.gameObject);
            }            
        }
    }

    private void MinimizeEnemy()
    {
        this.transform.localScale = new Vector3(this.transform.localScale.x - MINIMIZE_KOEF, this.transform.localScale.y - MINIMIZE_KOEF, this.transform.localScale.z - MINIMIZE_KOEF);
    }
}
