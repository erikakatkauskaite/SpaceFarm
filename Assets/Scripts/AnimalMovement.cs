using System;
using UnityEngine;
using UnityEngine.AI;

public class AnimalMovement : MonoBehaviour
{
    public static event Action OnDangerSoundNeeded;
    public static event Action OnDangerSoundStopNeeded;
    public static event Action OnCowWalking;
    public static event Action OnCowIdling;
    public static event Action OnChickenWalking;
    public static event Action OnChickenIdling;
    public static event Action OnPigWalking;
    public static event Action OnPigIdling;
    public static event Action OnSheepWalking;
    public static event Action OnSheepIdling;

    [SerializeField]
    private AnimalData animalData;

    public NavMeshAgent agent;
    public LayerMask whatIsGround;
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    private float touchTime;
    private Vector3 animalPosition;

    private const float DESTROY_TIME        = 1.5f;
    private const float IDLE_TIME           = 4f;
    private const float WALK_TIME           = 4f;
    private const string ENEMY_TAG          = "Ghost";

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Patroling();
    }

    private void ResetAnimalAnimation()
    {
        InvokeAnimalTypeIdle();
        Invoke(nameof(InvokeAnimalTypeWalk), IDLE_TIME);
    }

    private void InvokeAnimalTypeWalk()
    {
        if (animalData.animalType == AnimalData.AnimalType.Chicken)
        {
            OnChickenWalking?.Invoke();
        }
        else if (animalData.animalType == AnimalData.AnimalType.Cow)
        {
            OnCowWalking?.Invoke();
        }
        else if (animalData.animalType == AnimalData.AnimalType.Pig)
        {
            OnPigWalking?.Invoke();
        }
        else if (animalData.animalType == AnimalData.AnimalType.Sheep)
        {
            OnSheepWalking?.Invoke();
        }
    }

    private void InvokeAnimalTypeIdle()
    {
        if (animalData.animalType == AnimalData.AnimalType.Chicken)
        {
            OnChickenIdling?.Invoke();
        }
        else if (animalData.animalType == AnimalData.AnimalType.Cow)
        {
            OnCowIdling?.Invoke();
        }
        else if (animalData.animalType == AnimalData.AnimalType.Pig)
        {
            OnPigIdling?.Invoke();
        }
        else if (animalData.animalType == AnimalData.AnimalType.Sheep)
        {
            OnSheepIdling?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(ENEMY_TAG))
        {
            touchTime = 0;
            OnDangerSoundNeeded?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(ENEMY_TAG))
        {
            touchTime += Time.deltaTime;

            if (touchTime > DESTROY_TIME)
            {
                OnDangerSoundStopNeeded?.Invoke();
                Destroy(this.gameObject);
            }
        }
    }

    private void Standing(Vector3 pos)
    {
        this.transform.position = pos;
    }

    private void Patroling()
    {
        if(!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude <1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ =UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
}
