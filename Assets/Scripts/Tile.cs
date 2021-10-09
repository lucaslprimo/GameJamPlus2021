using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace gamejamplus2020_t9
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] public bool isPaintend = false;
        [SerializeField] public Color fillColor;
        [SerializeField] public Color originalColor;
        [SerializeField] public Color highlightColor;

        Renderer rend;

        // Start is called before the first frame update
        void Start()
        {
            rend = GetComponent<Renderer>();
        }

        public void Highlight()
        {
            rend.material.color = highlightColor;
        }

        public void ResetColor()
        {
            rend.material.color = originalColor;
        }

        public void Fill()
        {
            rend.material.color = fillColor;
            isPaintend = true;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
