using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [Header("Main Menu Animation")]
    public Animator spaceshipAnimator;
    public Animator bigPlanetAnimator;
    public Animator earthPlanetAnimator;

    private const string BIG_PLANET_SCALING_ANIMATION   = "bigPlanetScaling";
    private const string ON_PLAY_PRESSED_ANIMATION      = "isPlayPressed";

    private void Start()
    {
        MainMenuUIManager.OnPlaySelectedForAnimations += PlayBigPlanetAnimation;
        MainMenuUIManager.OnPlaySelectedForAnimations += PlaySpaceshipAnimationOnPlayPressed;
        MainMenuUIManager.OnPlaySelectedForAnimations += StopEarthAnimation;

        PlayEarthAnimation();
        PlaySpaceshipRotateAroundEarthAnimation();
    }

    private void PlayEarthAnimation()
    {
        earthPlanetAnimator.enabled = true;
    }

    private void StopEarthAnimation()
    {
        earthPlanetAnimator.enabled = false;
    }

    private void PlayBigPlanetAnimation()
    {
        StopEarthAnimation();
        bigPlanetAnimator.enabled = true;
        bigPlanetAnimator.Play(BIG_PLANET_SCALING_ANIMATION);
    }

    private void StopBigPlanetAnimation()
    {
        bigPlanetAnimator.enabled = false;
    }

    private void PlaySpaceshipRotateAroundEarthAnimation()
    {
        spaceshipAnimator.enabled = true;
    }

    private  void PlaySpaceshipAnimationOnPlayPressed()
    {
        spaceshipAnimator.SetBool(ON_PLAY_PRESSED_ANIMATION, true);
    }
}
