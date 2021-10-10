using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



namespace gamejamplus2020_t9
{
    public class Player : MonoBehaviour
    {
        [SerializeField] public Camera mainCamera;
        [SerializeField] public bool isEnable = true;
        [SerializeField] public float powerUpDuration;

        private float timeToStopPowerUp;


        private Painter painter;

        public PlayerState playerState;

        public UnityEvent OnPowerUpCatched;
        public UnityEvent OnPlayerDies;

        public enum PlayerState
        {
            Chaser, Runner
        }

        void Start()
        {
            painter = gameObject.GetComponent<Painter>();
            playerState = PlayerState.Runner;
        }

        void Update()
        {
            if(playerState == PlayerState.Chaser)
            {
                if (Time.time >= timeToStopPowerUp)
                {
                    playerState = PlayerState.Runner;
                }
            }
           
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PowerUp"))
            {
                timeToStopPowerUp = Time.time + powerUpDuration;
                playerState = PlayerState.Chaser;
                Destroy(other.gameObject);
                if (OnPowerUpCatched != null)
                    OnPowerUpCatched.Invoke();
            }

            if (other.CompareTag("Enemy"))
            {
                if (playerState == PlayerState.Runner)
                {
                    Destroy(gameObject);
                }
                else
                {
                    if (painter.CheckSurfacePainted())
                    {
                        Destroy(other.gameObject);
                    }
                }
            }
        }
    }
}
