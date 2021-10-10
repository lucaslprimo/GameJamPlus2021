using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


namespace gamejamplus2020_t9
{

    public class EnemyBlocker : MonoBehaviour
    {
        [SerializeField] NavMeshAgent navAgent;
        [SerializeField] float minDistanceToHit = 0.5f;
        [SerializeField] float playerDistance = 10;

        [SerializeField] LayerMask layerMask;

        GameObject[] runPoints;

        public UnityEvent<GameObject> OnTryHitPlayer;
        private Vector3 runPos;
        Player player;

        // Start is called before the first frame update
        void Start()
        {
            runPos = transform.position;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            runPoints = GameObject.FindGameObjectsWithTag("RunAwayPoint");
        }

        // Update is called once per frame
        void Update()
        {
            if (player.isEnable)
            {
                if (player.playerState == Player.PlayerState.Runner)
                {
                    navAgent.SetDestination(player.transform.position);
                    runPos = transform.position;
                }
                else
                {
                    runPos = GetFarAwayPoint();
                    navAgent.SetDestination(runPos);   
                }
            }
        }

        public Vector3 GetFarAwayPoint()
        {
            float maxDistance = Vector3.Distance(runPoints[0].transform.position, transform.position);
            Vector3 result = runPoints[0].transform.position;
            
            foreach(GameObject point in runPoints)
            {
                if(maxDistance < Vector3.Distance(point.transform.position, transform.position))
                {
                    maxDistance = Vector3.Distance(point.transform.position, transform.position);
                    result = point.transform.position;
                }
            }

            return result;
        }
    }
}
