using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace gamejamplus2020_t9
{
    public class Paintable : MonoBehaviour
    {
        [SerializeField] public bool isPainted = false;

        Renderer rend;

        // Start is called before the first frame update
        void Start()
        {
            rend = GetComponent<Renderer>();
            rend.material.SetFloat("_GrayscaleAmmount", 100);
        }

        public void Fill()
        {
            rend.material.SetFloat("_GrayscaleAmmount", 0);
                       
            isPainted = true;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
