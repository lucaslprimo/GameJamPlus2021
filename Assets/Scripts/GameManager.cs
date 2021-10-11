using FMOD.Studio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace gamejamplus2020_t9
{
    public class GameManager : MonoBehaviour
    {
        EventInstance soundAmbience;
        EventInstance soundMusic;

        void Start()
        {
            if (SceneManager.GetActiveScene().name == "Test")
            {
                soundAmbience = FMODUnity.RuntimeManager.CreateInstance("event:/Ambience");
                soundAmbience.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
                soundAmbience.start();

                soundMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music");
                soundMusic.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
                soundMusic.start();
            }
        }

        // Start is called before the first frame update
        void Awake()
        {

            if (SceneManager.GetActiveScene().name == "Restart Scene" || SceneManager.GetActiveScene().name == "Win" || SceneManager.GetActiveScene().name == "Menu")
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (SceneManager.GetActiveScene().name == "Restart Scene" || SceneManager.GetActiveScene().name == "Win" || SceneManager.GetActiveScene().name == "Menu")
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                CheckWin();
            }            
        }

        public void StopMusic()
        {
            soundMusic.stop(STOP_MODE.IMMEDIATE);
        }

        public void PlayMusic()
        {
            soundMusic.start();
        }

        void CheckWin()
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Tile");
            bool win = true;

            float nPainted = 0;

            foreach (GameObject obj in objs)
            {
                Paintable paintable = obj.GetComponent<Paintable>();
                if (!paintable.isPainted)
                {
                    win = false;

                }
                else
                {
                    nPainted += 1;
                }
            }

            soundAmbience.setParameterByName("WorldPainted", nPainted / objs.Length);

            if (win)
            {
                StartWinScene();
            }
        }

        private void StartWinScene()
        {
            SceneManager.LoadScene(3);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        void OnDestroy()
        {
            soundMusic.stop(STOP_MODE.IMMEDIATE);
            soundAmbience.stop(STOP_MODE.IMMEDIATE);
        }
    }
}
