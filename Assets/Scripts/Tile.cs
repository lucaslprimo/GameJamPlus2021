using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace gamejamplus2020_t9
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] public bool isPainted = false;
        [SerializeField] public Color fillColor;
        [SerializeField] public Color originalColor;
        [SerializeField] public Color highlightColor;
        
        public bool isHighlighted = false;

        Renderer rend;

        // Start is called before the first frame update
        void Start()
        {
            rend = GetComponent<Renderer>();
        }

        public void Highlight()
        {
            rend.material.color = highlightColor;
            isHighlighted = true;
        }

        public void ResetColor()
        {
            isHighlighted = false;
            rend.material.color = originalColor;
        }

        public void Fill()
        {
            isHighlighted = false;
            rend.material.color = fillColor;
            isPainted = true;
        }

        // Update is called once per frame
        void Update()
        {

        }

        internal void EraseColor()
        {
            isPainted = false;
            rend.material.color = originalColor;
        }
    }
}
