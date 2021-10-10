using System;
using UnityEngine;
using UnityEngine.Events;

namespace gamejamplus2020_t9
{

    public class Painter : MonoBehaviour
    {
        [SerializeField] LayerMask groundMask;

        Paintable workingTile;
        Player player;

        public UnityEvent OnInterruptPainting;
        public UnityEvent OnStartPainting;
        public UnityEvent OnFinishPaiting;


        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        private void Update()
        {
            CheckSurfacePainted();
            if (workingTile != null && !workingTile.isPainted && player.playerState == Player.PlayerState.Runner)
            {
                workingTile.Fill();
            }
        }

        public bool CheckSurfacePainted()
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, Vector3.down * 5f, Color.red);
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 3f, groundMask))
            {
                workingTile = hit.collider.GetComponent<Paintable>();
            }

            if (workingTile != null && workingTile.isPainted)
                return true;
            else return false;
        }
    }
}
