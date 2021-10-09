using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace gamejamplus2020_t9
{
    public class Kicker : MonoBehaviour
    {
        [SerializeField] float kickForce = 0.1f;
        [SerializeField] float cooldownKick = 1f;
        [SerializeField] KeyCode paintActionKey;
        [SerializeField] bool isPlayer;

        [SerializeField] Crosshair crosshair;

        private float timeToKick;

        private void Update()
        {
            if (isPlayer)
            {
                if (Input.GetKeyDown(paintActionKey))
                {
                    GameObject gameObject = GetTarget();
                    if(gameObject!= null)
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
                Debug.Log("Kicking");
                timeToKick = Time.time + cooldownKick;

                TilePainter tilePainter = target.GetComponent<TilePainter>();
                if (tilePainter != null)
                {
                    tilePainter.InterruptPainting();
                    Debug.Log("Interrupted");
                }

                Vector3 direction = target.transform.position - transform.position;

                target.GetComponent<Rigidbody>().AddForce(direction.normalized * kickForce);
            }
        }
    }
}