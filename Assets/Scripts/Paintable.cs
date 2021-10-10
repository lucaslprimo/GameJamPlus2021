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

        Renderer rend;

        // Start is called before the first frame update
        void Start()
        {
            rend = GetComponent<Renderer>();
            originalMaterial = rend.material;
            rend.material = grayMaterial;
        }

        public void Fill()
        {
            rend.material = originalMaterial;
            isPainted = true;
        }

        // Update is called once per frame
        void Update()
        {
           
        }
    }
}
