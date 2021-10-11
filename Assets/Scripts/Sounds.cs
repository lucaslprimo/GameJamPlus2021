using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace gamejamplus2020_t9
{
    public class Sounds : MonoBehaviour
    {
        public void deadPlayer()
        {
            //Ativa o Som do FMOD
            //FMODUnity.RuntimeManager.PlayOneShot("event:/PlayerSteps", GetComponent<Transform>().position);

            var sound = FMODUnity.RuntimeManager.CreateInstance("event:/PlayerDeath");
            sound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            sound.start();
            sound.release();
        }

        public void JumpStartPlayer()
        {
            //Ativa o Som do FMOD
            //FMODUnity.RuntimeManager.PlayOneShot("event:/PlayerSteps", GetComponent<Transform>().position);

            var sound = FMODUnity.RuntimeManager.CreateInstance("event:/PlayerJumpStart");
            sound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            sound.start();
            sound.release();
        }

        public void JumpLandPlayer()
        {
            //Ativa o Som do FMOD
            //FMODUnity.RuntimeManager.PlayOneShot("event:/PlayerSteps", GetComponent<Transform>().position);

            var sound = FMODUnity.RuntimeManager.CreateInstance("event:/PlayerJumpLand");
            sound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            sound.start();
            sound.release();
        }

        public void StepsPlayer()
        {
            //Ativa o Som do FMOD
            //FMODUnity.RuntimeManager.PlayOneShot("event:/PlayerSteps", GetComponent<Transform>().position);

            var sound = FMODUnity.RuntimeManager.CreateInstance("event:/PlayerSteps");
            sound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            sound.start();
            sound.release();
        }

        public void StepsEnemy()
        {
            //Ativa o Som do FMOD
            //FMODUnity.RuntimeManager.PlayOneShot("event:/PlayerSteps", GetComponent<Transform>().position);

            var sound = FMODUnity.RuntimeManager.CreateInstance("event:/EnemyFootsteps");
            sound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            sound.start();
            sound.release();
        }

        public void EnemyHitPlayer()
        {
            //Ativa o Som do FMOD
            //FMODUnity.RuntimeManager.PlayOneShot("event:/PlayerSteps", GetComponent<Transform>().position);

            var sound = FMODUnity.RuntimeManager.CreateInstance("event:/EnemyHit");
            sound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            sound.start();
            sound.release();
        }

        public void PlayerHitPlayer()
        {
            //Ativa o Som do FMOD
            //FMODUnity.RuntimeManager.PlayOneShot("event:/PlayerSteps", GetComponent<Transform>().position);

            var sound = FMODUnity.RuntimeManager.CreateInstance("event:/PlayerHit");
            sound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            sound.start();
            sound.release();
        }

        public void PowerUpGetPlayer()
        {
            //Ativa o Som do FMOD
            //FMODUnity.RuntimeManager.PlayOneShot("event:/PlayerSteps", GetComponent<Transform>().position);

            var sound = FMODUnity.RuntimeManager.CreateInstance("event:/PowerUp");
            sound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            sound.start();
            sound.release();

            sound = FMODUnity.RuntimeManager.CreateInstance("event:/PowerUpGet");
            sound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            sound.start();
            sound.release();
        }
    }
}
