using Antymology.Terrain;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Antymology.Agents
{
    public class QueenAntMover : MonoBehaviour
    {
        private WorldManager WorldManagerInstance;

        public float health { get; set; }
        public float maxHealth { get; set; }

        // Start is called before the first frame update
        void Start()
        {

        }

        public void SetWorldManagerInstance(WorldManager instance)
        {
            WorldManagerInstance = instance;
        }

        /// <summary>
        /// initialize the gameobject to have a mesh and mesh renderer, and set the references internally.
        /// </summary>
        

        // Update is called once per frame
        void Update()
        {

        }
    }
}
