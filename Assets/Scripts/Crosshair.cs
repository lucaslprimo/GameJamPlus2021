using System;
using UnityEngine;
using UnityEngine.UI;

namespace gamejamplus2020_t9
{
    public class Crosshair : MonoBehaviour
    {
        Player player;
        Image image;
        RaycastHit hit;

        private Tile lastHighlightTile;

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
                if (hit.collider.CompareTag("Enemy")) 
                    image.color = Color.red;
                else if (hit.collider.CompareTag("Tile"))
                {
                    if(!GameObject.ReferenceEquals(lastHighlightTile, hit.collider.GetComponent<Tile>())){
                        if(lastHighlightTile != null && lastHighlightTile.isHighlighted)
                        {
                            lastHighlightTile.ResetColor();
                        }

                        if (!hit.collider.GetComponent<Tile>().isPainted)
                        {
                            lastHighlightTile = hit.collider.GetComponent<Tile>();
                            lastHighlightTile.Highlight();                        
                        }
                    }            
                }
                else image.color = Color.white;
            }
            else
            {
                if (lastHighlightTile != null && lastHighlightTile.isHighlighted)
                {
                    lastHighlightTile.ResetColor();
                    lastHighlightTile = null;
                }
                image.color = Color.black;
            }            
        }

        private void CleanedHighlight()
        {
            GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
            foreach(GameObject tileGameObj in tiles){
                Tile tile = tileGameObj.GetComponent<Tile>();
                if (!tile.isPainted && !tile.isHighlighted)
                {
                    tile.ResetColor();
                }
            }
        }

        public RaycastHit GetHitObject()
        {
            return hit;
        }
    }

}