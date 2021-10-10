using System;
using UnityEngine;
using UnityEngine.Events;

namespace gamejamplus2020_t9
{

    public class Painter : MonoBehaviour
    {
        [SerializeField] LayerMask groundMask;

        Paintable workingTile;


        public UnityEvent OnInterruptPainting;
        public UnityEvent OnStartPainting;
        public UnityEvent OnFinishPaiting;

        private void Update()
        {
            CheckSurfacePainted();
            if (workingTile != null && !workingTile.isPainted)
            {
                workingTile.Fill();
            }
        }

        public void CheckSurfacePainted()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 5f, groundMask))
            {
                workingTile = hit.collider.GetComponent<Paintable>();
            }
        }
    }
}
