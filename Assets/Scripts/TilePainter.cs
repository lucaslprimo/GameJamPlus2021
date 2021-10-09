using System;
using UnityEngine;

namespace gamejamplus2020_t9
{

    public class TilePainter : MonoBehaviour
    {
        [SerializeField] float channelingTime = 2f;
        [SerializeField] float cooldownTime = 1f;
        [SerializeField] bool isPlayer;
        [SerializeField] KeyCode paintActionKey;

        [SerializeField] Crosshair crosshair;

        [SerializeField] LayerMask groundLayer;
        float paintingFinishedTime;
        private Tile currentTile;

        private bool isPainting;

        public event Action OnInterruptPainting;
        public event Action OnStartPainting;
        public event Action OnFinishPaiting;

        private void Update()
        {
            if (isPlayer)
            {
                if (Input.GetKey(paintActionKey))
                {
                    if (!isPainting)
                    {
                        PaintTile();
                    }
                }
                else
                {
                    if (isPainting)
                    {
                        resetPaintingState();
                    }
                }
            }


            if (isPainting)
            {
                if( Time.time >= paintingFinishedTime)
                {
                    FinishPainting();
                }
            }
        }

        public void PaintTile()
        {
            if (IsAboveUnpaintedTile())
            {
                StartPainting(channelingTime);
            }
        }

        private void StartPainting(float channelingTime)
        {
            paintingFinishedTime = Time.time + channelingTime;
            isPainting = true;
            if(OnStartPainting != null)
                OnStartPainting.Invoke();
        }

        private void FinishPainting()
        {
            currentTile.Fill();
            resetPaintingState();
            if (OnFinishPaiting != null)
                OnFinishPaiting.Invoke();
        }

        private void resetPaintingState()
        {
            isPainting = false;
        }

        public void InterruptPainting()
        {
            resetPaintingState();
            if (OnInterruptPainting != null)
                OnInterruptPainting.Invoke();
        }

        private bool IsAboveUnpaintedTile()
        {
            CheckHit();

            if (currentTile == null)
                return false;

            return !currentTile.isPaintend;
        }

        private void CheckHit()
        {
            RaycastHit hit;
            hit = crosshair.GetHitObject();
            if (hit.collider != null && hit.collider.CompareTag("Tile"))
            {
                currentTile = hit.collider.GetComponent<Tile>();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawRay(transform.position, Vector3.down * 2f);
        }
    }
}
