using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace EditorScripts
{
    public class GridSystem : MonoBehaviour
    {
        public float width = 32f;
        public float height = 32f;
        
        
        public List<Transform> PlacablePositions = new List<Transform>();
        
        private void Awake()
        {
            //PlacablePositions.Clear();
            Debug.Log(PlacablePositions.Count);
            
        }
        
        private void OnEnable()
        {
            Debug.Log(PlacablePositions.Count);
        }

      

        private void OnDisable()
        {
            
        }

        private void OnDrawGizmos()
        {
            Vector3 pos = Camera.current.transform.position;
     
            for (float y = pos.y - 800.0f; y < pos.y + 800.0f; y+= height)
            {
                Gizmos.DrawLine(new Vector3(-1000000.0f, Mathf.Floor(y/height) * height, 0.0f),
                    new Vector3(1000000.0f, Mathf.Floor(y/height) * height, 0.0f));
            }
    
            for (float x = pos.x - 1200.0f; x < pos.x + 1200.0f; x+= width)
            {
                Gizmos.DrawLine(new Vector3(Mathf.Floor(x/width) * width, -1000000.0f, 0.0f),
                    new Vector3(Mathf.Floor(x/width) * width, 1000000.0f, 0.0f));
            }
            
        }
    }
}