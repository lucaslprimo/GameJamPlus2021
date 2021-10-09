using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



namespace gamejamplus2020_t9
{
    public class EnemyEraser : MonoBehaviour
    {
        [SerializeField] NavMeshAgent navAgent;
        [SerializeField] float minDistanceToErase = 0.5f;

        [SerializeField] TilePainter painter;

        Tile targetTile = null;
        State state;

        enum State
        {
            SEKKING, ERASING, ERASE
        }

        void Start()
        {
            state = State.SEKKING;
        }

        void Update()
        {
            switch (state)
            {
                case State.SEKKING:
                    float minDistance = 0;

                    GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Tile");

                    foreach (GameObject gameObj in gameObjects)
                    {
                        Tile tile = gameObj.GetComponent<Tile>();

                        if (tile.isPainted)
                        {
                            if (minDistance == 0)
                            {
                                minDistance = GetDistanteFromTile(tile);
                                targetTile = tile;
                            }
                            else
                            {
                                if(Vector3.Distance(tile.transform.position, transform.position) < minDistance)
                                {
                                    minDistance = GetDistanteFromTile(tile);
                                    targetTile = tile;
                                }
                            }
                        }
                    }

                    if(targetTile!=null)
                        GoToTile(targetTile);

                    break;
                case State.ERASE:
                    painter.OnFinishPaiting.AddListener(OnFinishPainting);
                    painter.OnInterruptPainting.AddListener(OnFinishPainting);
                    painter.EraseTile(targetTile);
                    state = State.ERASING;
                    break;
            }
        }

        void OnFinishPainting()
        {
            state = State.SEKKING;
            targetTile = null;

            painter.OnFinishPaiting.RemoveListener(OnFinishPainting);
            painter.OnInterruptPainting.RemoveListener(OnFinishPainting);
        }

        private float GetDistanteFromTile(Tile tile)
        {
            return Vector3.Distance(tile.transform.position, transform.position);
        }

        void GoToTile(Tile tile)
        {
            navAgent.SetDestination(tile.transform.position);

            if(GetDistanteFromTile(tile) <= minDistanceToErase)
            {
                state = State.ERASE;
            }
        }
    }
}
