using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyBlocker : MonoBehaviour
{
    [SerializeField] NavMeshAgent navAgent;
    [SerializeField] float minDistanceToHit = 0.5f;

    public UnityEvent<GameObject> OnTryHitPlayer;
    
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        navAgent.SetDestination(player.transform.position);

        if (Vector3.Distance(player.transform.position, transform.position) <= minDistanceToHit)
        {
            OnTryHitPlayer.Invoke(player.gameObject);
        }
    }
}
