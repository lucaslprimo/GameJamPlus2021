using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace gamejamplus2020_t9
{
    public class Paintable : MonoBehaviour
    {
        [SerializeField] public bool isPainted = false;
        [SerializeField] public Material grayMaterial;
        [SerializeField] public Material blinkMaterial;
        [SerializeField] public float blinkInterval;
        private Material originalMaterial;

        private bool blinking = false;
        private float timeToBlink = 0;

        Player player;

        Renderer rend;

        // Start is called before the first frame update
        void Start()
        {
            rend = GetComponent<Renderer>();
            originalMaterial = rend.material;
            rend.material = grayMaterial;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        public void Fill()
        {
            rend.material = originalMaterial;
            isPainted = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (player.playerState == Player.PlayerState.Chaser)
            {
                if (isPainted)
                {
                    if (Time.time > timeToBlink)
                    {
                        timeToBlink = Time.time + blinkInterval;
                        if (blinking)
                        {
                            rend.material = originalMaterial;
                        }
                        else
                        {
                            rend.material = blinkMaterial;
                        }

                        blinking = !blinking;
                    }
                }
            }
            else
            {
                if (isPainted)
                {
                    rend.material = originalMaterial;
                }
                else
                {
                    rend.material = grayMaterial;
                }
            }
        }
    }
}
