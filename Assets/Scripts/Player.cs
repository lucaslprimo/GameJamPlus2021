using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace gamejamplus2020_t9
{
    public class Player : MonoBehaviour
    {
        [SerializeField] public Camera mainCamera;
        [SerializeField] public bool isEnable = true;
        [SerializeField] public float powerUpDuration;
        [SerializeField] public int maxHp;

        [SerializeField] Text hp;
        private float timeToStopPowerUp;

        public PlayerState playerState;

        private int playerHP;

        public UnityEvent OnPowerUpCatched;
        public UnityEvent OnPlayerDies;
        public UnityEvent OnMonsterKilled;
        public UnityEvent OnPlayerHurt;

        public enum PlayerState
        {
            Chaser, Runner
        }

        void Start()
        {
            
            playerState = PlayerState.Runner;
            playerHP = maxHp;
            hp.text = playerHP.ToString();
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
                    playerHP -= 1;
                    hp.text = playerHP.ToString();
                    if (playerHP == 0)
                    {
                        SceneManager.LoadScene(1);
                        if (OnPlayerDies != null)
                            OnPlayerDies.Invoke();
                    }
                    else
                    {
                        Destroy(other.gameObject);
                        if (OnPlayerHurt != null)
                            OnPlayerHurt.Invoke();
                    }   
                }
                else
                {
                    Destroy(other.gameObject);
                    if (OnMonsterKilled != null)
                        OnMonsterKilled.Invoke();
                }
            }
        }
    }
}
