using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


namespace gamejamplus2020_t9
{

    public class EnemyBlocker : MonoBehaviour
    {
        [SerializeField] NavMeshAgent navAgent;

        [SerializeField] Animator animator;

        GameObject[] runPoints;

        public UnityEvent<GameObject> OnTryHitPlayer;
        private Vector3 runPos;
        Player player;
        private EnemyState enemyState;

        public enum EnemyState
        {
            WANDER, CHASE
        }

        // Start is called before the first frame update
        void Start()
        {
            animator.speed = Random.Range(2f, 2.5f);

            runPos = transform.position;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            runPoints = GameObject.FindGameObjectsWithTag("RunAwayPoint");

            enemyState = EnemyState.WANDER;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 playerDir = player.transform.position - transform.position;

            if(player.playerState == Player.PlayerState.Chaser)
            {
                enemyState = EnemyState.WANDER;
            }

            switch (enemyState)
            {
                case EnemyState.WANDER:
                    if(Vector3.Distance(transform.position, runPos) < 5)
                    {
                        runPos = GetNewWanderPoint();  
                    }

                    navAgent.SetDestination(runPos);

                    CheckPlayerInSight();
                    break;
                case EnemyState.CHASE:
                    navAgent.SetDestination(player.transform.position);
                    break;
            }
        }

        private bool CheckPlayerInSight()
        {
            Vector3 playerDir = player.transform.position - transform.position;
            RaycastHit hit;
            if(Physics.Raycast(transform.position, playerDir, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    if(player.playerState!= Player.PlayerState.Chaser)
                        enemyState = EnemyState.CHASE;
                  
                    return true;
                }
            }

            return false;
        }

        public Vector3 GetNewWanderPoint()
        {
            int random = Random.Range(0, runPoints.Length);
            Vector3 result = runPoints[random].transform.position;
            
            return result;
        }
    }
}
