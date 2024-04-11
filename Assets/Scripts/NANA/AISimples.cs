using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AISimples : MonoBehaviour
{

    public FieldOfView FOV;
    public RandomMovement patrol;
    NavMeshAgent navMeshAgent;
    Transform target;
    Vector3 lastKnownPosition;
    float searchTimer;

    enum AIState
    {
        patrolling, following, searchingLostTarget
    };
    AIState aiState = AIState.patrolling;

    void Start()
    {
        if (FOV == null)
            FOV = GetComponent<FieldOfView>();
        if (patrol == null)
            patrol = GetComponent<RandomMovement>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = null;
        lastKnownPosition = Vector3.zero;
        aiState = AIState.patrolling;
        searchTimer = 0;
    }

    void Update()
    {
        if (FOV)
        {
            switch (aiState)
            {
                case AIState.patrolling:
                    patrol.Patrol();
                    if (FOV.visibleEnemies.Count > 0)
                    {
                        target = FOV.visibleEnemies[0];
                        lastKnownPosition = target.position;
                        aiState = AIState.following;
                    }
                    break;
                case AIState.following:
                    navMeshAgent.SetDestination(target.position);
                    if (!FOV.visibleEnemies.Contains(target))
                    {
                        lastKnownPosition = target.position;
                        aiState = AIState.searchingLostTarget;
                    }
                    break;
                case AIState.searchingLostTarget:
                    navMeshAgent.SetDestination(lastKnownPosition);
                    searchTimer += Time.deltaTime;
                    if (searchTimer > 5)
                    {
                        searchTimer = 0;
                        aiState = AIState.patrolling;
                        break;
                    }
                    if (FOV.visibleEnemies.Count > 0)
                    {
                        target = FOV.visibleEnemies[0];
                        lastKnownPosition = target.position;
                        aiState = AIState.following;
                    }
                    break;
            }
        }
    }
}