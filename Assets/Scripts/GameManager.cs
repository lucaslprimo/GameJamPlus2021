using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace gamejamplus2020_t9
{
    public class GameManager : MonoBehaviour
    {
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

        void CheckWin()
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Tile");
            bool win = true;
            foreach (GameObject obj in objs)
            {
                Paintable paintable = obj.GetComponent<Paintable>();
                if (!paintable.isPainted)
                {
                    win = false;
                    break;
                }
            }

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
    }
}
