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

        // Timer to control movement
        private float timer = 0f;

        // Time delay between each movement
        public float movementDelay = 0.5f;

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
            // Increment timer
            timer += Time.deltaTime;
            // Check if enough time has passed for movement
            if (timer >= movementDelay)
            {
                if(health > maxHealth*0.33)
                {
                    //transform.position += new Vector3(0, 1, 0);
                    NestBlock nestBlockInstance = new NestBlock();
                    WorldManagerInstance.SetBlock((int)transform.position.x, (int)transform.position.y, (int)transform.position.z, nestBlockInstance);
                    transform.position += new Vector3(0, 1, 0);
                    health += (float)(-maxHealth * 0.33);
                }
                // Reset timer
                timer = 0f;
            }
        }
    }
}
