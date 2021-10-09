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

        List<Tile> tiles;
        Tile targetTile = null;
        State state;

        enum State
        {
            SEKKING, IDLE, ERASING, ERASE
        }

        void Start()
        {
            painter.OnFinishPaiting.AddListener(OnFinishPainting);
            painter.OnInterruptPainting.AddListener(OnFinishPainting);

            state = State.IDLE;
           
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Tile");
            tiles = new List<Tile>();
            foreach (GameObject gameObject in gameObjects)
            {
                tiles.Add(gameObject.GetComponent<Tile>());
            }
        }

        void Update()
        {
            switch (state)
            {
                case State.IDLE:
                    float minDistance = 0;
                   
                    foreach (Tile tile in tiles)
                    {
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

                    if (targetTile != null)
                        state = State.SEKKING;

                    break;
                case State.SEKKING:
                    GoToTile(targetTile);

                    break;
                case State.ERASE:
                    painter.EraseTile(targetTile);
                    state = State.ERASING;
                    break;
            }
        }

        void OnFinishPainting()
        {
            state = State.IDLE;
            targetTile = null;
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
