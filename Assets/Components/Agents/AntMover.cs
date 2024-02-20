using Antymology.Terrain;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Antymology.Agents
{
    public class AntMover : MonoBehaviour
    {
        /// <summary>
        /// Random number generator.
        /// </summary>
        private System.Random RNG;

        // Timer to control movement
        private float timer = 0f;

        // Time delay between each movement
        public float movementDelay = 0.5f;

        private WorldManager WorldManagerInstance;

        // block types
        private Vector2 acidic = new Vector2(0, 3);
        private Vector2 container = new Vector2(3, 3);
        private Vector2 grass = new Vector2(0, 2);
        private Vector2 mulch = new Vector2(0, 1);
        private Vector2 nest = new Vector2(1, 3);
        private Vector2 stone = new Vector2(3, 1);

        // health information
        public float health { get; set; }
        public float maxHealth { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            RNG = new System.Random(ConfigurationManager.Instance.Seed);
        }

        public void SetWorldManagerInstance(WorldManager instance)
        {
            WorldManagerInstance = instance;
        }

        void Update()
        {
            // Increment timer
            timer += Time.deltaTime;
            // Check if enough time has passed for movement
            if (timer >= movementDelay)
            {
                // Move the object along the y-axis with reduced speed
                moveAnt();

                AbstractBlock currentBlock = WorldManagerInstance.GetBlock((int)transform.position.x, (int)transform.position.y-1, (int)transform.position.z);
                
                if(currentBlock.tileMapCoordinate() == acidic)
                {
                    health += -2;
                }
                else
                {
                    health += -1;
                }

                if(currentBlock.tileMapCoordinate() == mulch && health < maxHealth)
                {
                    health += 5;
                    AirBlock airBlockInstance = new AirBlock();
                    WorldManagerInstance.SetBlock((int)transform.position.x, (int)transform.position.y - 1, (int)transform.position.z, airBlockInstance);
                    transform.position += new Vector3(0, -1, 0);
                }

                if(health <= 0)
                {
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                }

                AbstractBlock currentAirBlock = WorldManagerInstance.GetBlock((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
                currentAirBlock.pheromoneAmount += 1;
                // Reset timer
                timer = 0f;
            }

        }

        void moveAnt()
        {
            Vector3 newPosition = transform.position;
            bool moveComplete = false;
            int neighbor = RNG.Next(0, 4);
            // try to move forward, back, left or right
            switch (neighbor)
            {
                case 0:
                    newPosition += new Vector3(0, 0, 1);
                    moveComplete = movePosition(newPosition);
                    break;
                case 1:
                    newPosition += new Vector3(1, 0, 0);
                    moveComplete = movePosition(newPosition);
                    break;
                case 2:
                    newPosition += new Vector3(0, 0, -1);
                    moveComplete = movePosition(newPosition);
                    break;
                case 3:
                    newPosition += new Vector3(-1, 0, 0);
                    moveComplete = movePosition(newPosition);
                    break;
            }
            
        }

        bool movePosition(Vector3 position)
        {
            AbstractBlock nextBlock = WorldManagerInstance.GetBlock((int)position.x, (int)position.y, (int)position.z);
            AbstractBlock nextBlockBelow = WorldManagerInstance.GetBlock((int)position.x, (int)position.y - 1, (int)position.z);

            // check is next block is air and there is a block to stand on
            if (!nextBlock.isVisible() && nextBlockBelow.isVisible())
            {
                transform.position = position;
                return true;
            }

            AbstractBlock nextBlockTwoBelow = WorldManagerInstance.GetBlock((int)position.x, (int)position.y - 2, (int)position.z);
            if(!nextBlockBelow.isVisible() && nextBlockTwoBelow.isVisible())
            {
                transform.position = position;
                transform.position += new Vector3(0, -1, 0);
                return true;
            }

            AbstractBlock nextBlockAbove = WorldManagerInstance.GetBlock((int)position.x, (int)position.y + 1, (int)position.z);
            if(nextBlock.isVisible() && !nextBlockAbove.isVisible())
            {
                transform.position = position;
                transform.position += new Vector3(0, 1, 0);
                return true;
            }

            return false;
        }
    }
}
