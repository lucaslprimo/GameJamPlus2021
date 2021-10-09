using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

namespace gamejamplus2020_t9
{
    public class Crosshair : MonoBehaviour
    {
        Player player;
        Image image;
        RaycastHit hit;

        [SerializeField] float offset = 1f;
        [SerializeField] float actionRange = 1f;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            image = GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {
            Debug.DrawRay(Camera.main.ScreenToWorldPoint(this.transform.position + Vector3.forward * offset), player.mainCamera.transform.forward * actionRange, Color.red);
            if (Physics.Raycast(Camera.main.ScreenToWorldPoint(this.transform.position + Vector3.forward * offset), player.mainCamera.transform.forward, out hit,  actionRange))
            {
                if (hit.collider.CompareTag("Tile"))
                    image.color = Color.red;
                else
                    image.color = Color.white;
            }
            else
            {
                image.color = Color.black;
            }            
        }

        public RaycastHit GetHitObject()
        {
            return hit;
        }
    }

}