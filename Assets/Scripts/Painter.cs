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
            Debug.DrawRay(transform.position, Vector3.down * 5f, Color.red);
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 3f, groundMask))
            {
                workingTile = hit.collider.GetComponent<Paintable>();
            }
        }
    }
}
