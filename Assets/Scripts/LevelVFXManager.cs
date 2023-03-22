using UnityEngine;

public class LevelVFXManager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem rocketFlames;

    private void Start()
    {
        DisableRocketFlames();
        CollectableBasket.OnGoodiesSentToEarth += EnableRocketFlames;
    }

    private void OnDestroy()
    {
        CollectableBasket.OnGoodiesSentToEarth -= EnableRocketFlames;
    }

    private void EnableRocketFlames()
    {
        rocketFlames.Play();
        Invoke(nameof(DisableRocketFlames), 17f);
    }

    private void DisableRocketFlames()
    {
        rocketFlames.Stop();
    }
}
