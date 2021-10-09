using System;
using UnityEngine;
using UnityEngine.Events;

namespace gamejamplus2020_t9
{

    public class TilePainter : MonoBehaviour
    {
        [SerializeField] float channelingTime = 2f;
        [SerializeField] bool isPlayer;
        [SerializeField] KeyCode paintActionKey;

        [SerializeField] Crosshair crosshair;
        float paintingFinishedTime;

        Tile workingTile;

        private bool isPainting;

        public UnityEvent OnInterruptPainting;
        public UnityEvent OnStartPainting;
        public UnityEvent OnFinishPaiting;

        private void Update()
        {
            if (isPlayer)
            {
                if (Input.GetKey(paintActionKey))
                {
                    if (!isPainting)
                    {
                        Tile tile = GetTile();
                        if (tile != null)
                        {
                            PaintTile(tile);
                        }
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

        private Tile GetTile()
        {
            Tile tile = null;
            RaycastHit hit;
            hit = crosshair.GetHitObject();
            if (hit.collider != null && hit.collider.CompareTag("Tile"))
            {
                tile = hit.collider.GetComponent<Tile>();
            }

            return tile;
        }

        public void PaintTile(Tile tile)
        {
            if (!tile.isPainted) {
                workingTile = tile;
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
            if (isPlayer)
            {
                workingTile.Fill();
                resetPaintingState();
            }
            else
            {
                workingTile.EraseColor();
            }
            
            if (OnFinishPaiting != null)
                OnFinishPaiting.Invoke();
        }

        internal void EraseTile(Tile tile)
        {
            if (tile.isPainted)
            {
                workingTile = tile;
                StartPainting(channelingTime);
            }
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
    }
}
