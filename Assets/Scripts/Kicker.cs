using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;


namespace gamejamplus2020_t9
{
    public class Kicker : MonoBehaviour
    {
        [SerializeField] float kickForce = 200f;
        [SerializeField] float cooldownKick = 1f;
        [SerializeField] KeyCode paintActionKey;
        [SerializeField] bool isPlayer;

        [SerializeField] float minVelocityTakingBackControl;

        [SerializeField] Crosshair crosshair;

        private float timeToKick;

        private bool shouldKick;

        GameObject target;

        private void Update()
        {
            if (isPlayer)
            {
                if (Input.GetKeyDown(paintActionKey))
                {
                    GameObject gameObject = GetTarget();
                    if (gameObject != null)
                        Kick(GetTarget());
                }
            }
        }

        private GameObject GetTarget()
        {
            GameObject gameObject = null;

            if (crosshair.GetHitObject().collider != null)
            {
                gameObject = crosshair.GetHitObject().collider.gameObject;
            }

            return gameObject;
        }

        public void Kick(GameObject target)
        {
            if (Time.time >= timeToKick)
            {
                this.target = target;
                timeToKick = Time.time + cooldownKick;

                TilePainter tilePainter = target.GetComponent<TilePainter>();
                if (tilePainter != null)
                {
                    tilePainter.InterruptPainting();
                }

                shouldKick = true;
            }
        }


        private void FixedUpdate()
        {
            if (shouldKick)
            {
                Vector3 direction = target.transform.position - transform.position;

                if (isPlayer)
                {
                    target.GetComponent<NavMeshAgent>().enabled = false;
                    target.GetComponent<Rigidbody>().isKinematic = false;

                    target.GetComponent<Rigidbody>().AddForce(direction.normalized * kickForce, ForceMode.Impulse);
                }
                else
                {
                    target.GetComponent<Rigidbody>().isKinematic = false;
                    target.GetComponent<CharacterController>().enabled = false;

                    target.GetComponent<Rigidbody>().AddForce(direction.normalized * kickForce, ForceMode.Impulse);
                }

                shouldKick = false;
            }
            else
            {
                if (target != null)
                {
                    float targetVelocity = target.GetComponent<Rigidbody>().velocity.magnitude;

                    if (targetVelocity < minVelocityTakingBackControl)
                    {
                        if (isPlayer)
                        {
                            target.GetComponent<NavMeshAgent>().enabled = true;
                            target.GetComponent<Rigidbody>().isKinematic = true;
                        }
                        else
                        {
                            target.GetComponent<CharacterController>().enabled = true;
                            target.GetComponent<Rigidbody>().isKinematic = true;
                        }
                    }
                }
            }
        }
    }
}