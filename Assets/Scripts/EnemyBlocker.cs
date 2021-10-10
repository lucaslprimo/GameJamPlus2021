using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


namespace gamejamplus2020_t9
{

    public class EnemyBlocker : MonoBehaviour
    {
        [SerializeField] NavMeshAgent navAgent;
        [SerializeField] float minDistanceToHit = 0.5f;
        [SerializeField] float wanderRadius = 50;

        public UnityEvent<GameObject> OnTryHitPlayer;
        private Vector3 wanderPosition;
        Player player;

        // Start is called before the first frame update
        void Start()
        {
            wanderPosition = this.transform.position;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        // Update is called once per frame
        void Update()
        {
            if (player.isEnable)
            {
                if (player.playerState == Player.PlayerState.Runner)
                {
                    navAgent.SetDestination(player.transform.position);
                    wanderPosition = transform.position;
                }
                else
                {
                    if (Vector3.Distance(transform.position, wanderPosition) < 1)
                    {
                        wanderPosition = RandomNavSphere(transform.position, wanderRadius, -1);
                        navAgent.SetDestination(wanderPosition);
                    }
                }
            }
        }

        public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
        {
            Vector3 randDirection = Random.insideUnitSphere * dist;

            randDirection += origin;

            NavMeshHit navHit;

            NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

            return navHit.position;
        }
    }
}
