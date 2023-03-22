using UnityEngine;

public class LevelAnimationManager : MonoBehaviour
{
    [SerializeField]
    private Animator rocketAnimator;

    [SerializeField]
    private Animator chickenAnimator;

    [SerializeField]
    private Animator cowAnimator;

    [SerializeField]
    private Animator pigAnimator;

    [SerializeField]
    private Animator sheepAniamtor;

    private const string ROCKET_FLY_AWAY    = "RocketFlyAway";
    private const string ROCKET_FLY_BACK    = "RocketFlyBack";
    private const string IDLE               = "isIdle";
    private const string WALKING             = "isWalking";

    private void Start()
    {
        CollectableBasket.OnGoodiesSentToEarth += PlayRocketFlyAwayAnimation;
        AnimalMovement.OnCowIdling += PlayCowIdleAnimation;
        AnimalMovement.OnCowWalking += PlayCownWalkingAnimation;
        AnimalMovement.OnChickenIdling += PlayChickenIdleAnimation;
        AnimalMovement.OnChickenWalking += PlayChickenWalkingAnimation;
        AnimalMovement.OnPigIdling += PlayPigIdleAnimation;
        AnimalMovement.OnPigWalking += PlayPigWalkingAnimation;
        AnimalMovement.OnSheepIdling += PlaySheepIdleAnimation;
        AnimalMovement.OnSheepWalking += PlaySheepWalkingAnimation;
    }

    private void OnDestroy()
    {
        CollectableBasket.OnGoodiesSentToEarth -= PlayRocketFlyAwayAnimation;
        AnimalMovement.OnCowIdling -= PlayCowIdleAnimation;
        AnimalMovement.OnCowWalking -= PlayCownWalkingAnimation;
        AnimalMovement.OnChickenIdling -= PlayChickenIdleAnimation;
        AnimalMovement.OnChickenWalking -= PlayChickenWalkingAnimation;
        AnimalMovement.OnPigIdling -= PlayPigIdleAnimation;
        AnimalMovement.OnPigWalking -= PlayPigWalkingAnimation;
        AnimalMovement.OnSheepIdling -= PlaySheepIdleAnimation;
        AnimalMovement.OnSheepWalking -= PlaySheepWalkingAnimation;
    }

    private void PlayChickenIdleAnimation()
    {
        PlayIdleAnimation(chickenAnimator);
    }

    private void PlayChickenWalkingAnimation()
    {
        PlayWalkAnimation(chickenAnimator);
    }

    private void PlayCowIdleAnimation()
    {
        PlayIdleAnimation(cowAnimator);
    }

    private void PlayCownWalkingAnimation()
    {
        PlayWalkAnimation(cowAnimator);
    }

    private void PlayPigIdleAnimation()
    {
        PlayIdleAnimation(pigAnimator);
    }

    private void PlayPigWalkingAnimation()
    {
        PlayWalkAnimation(pigAnimator);
    }

    private void PlaySheepIdleAnimation()
    {
        PlayIdleAnimation(sheepAniamtor);
    }

    private void PlaySheepWalkingAnimation()
    {
        PlayWalkAnimation(sheepAniamtor);
    }

    private void PlayIdleAnimation(Animator animator)
    {

        animator.SetBool(IDLE, true);
        animator.SetBool(WALKING, false);
    }

    private void PlayWalkAnimation(Animator animator)
    {
        animator.SetBool(WALKING, true);
        animator.SetBool(IDLE, false);
    }

    private void PlayRocketFlyAwayAnimation()
    {
        rocketAnimator.SetBool(ROCKET_FLY_BACK, false);
        rocketAnimator.SetBool(ROCKET_FLY_AWAY, true);
        Invoke(nameof(PlayRocketFlyBackAnimation), 4f);
    }

    private void PlayRocketFlyBackAnimation()
    {
        rocketAnimator.SetBool(ROCKET_FLY_AWAY, false);
        rocketAnimator.SetBool(ROCKET_FLY_BACK, true);
    }
}
