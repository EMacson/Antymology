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

        private Vector2 acidic = new Vector2(0, 3);
        private Vector2 container = new Vector2(3, 3);
        private Vector2 grass = new Vector2(0, 2);
        private Vector2 mulch = new Vector2(0, 1);
        private Vector2 nest = new Vector2(1, 3);
        private Vector2 stone = new Vector2(3, 1);


        public float health { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            RNG = new System.Random(ConfigurationManager.Instance.Seed);
        }

        public void SetWorldManagerInstance(WorldManager instance)
        {
            WorldManagerInstance = instance;
        }

        public float speed = 5f; // Speed at which the object moves
                                 // Update is called once per frame
        void Update()
        {
            // Increment timer
            timer += Time.deltaTime;
            // Check if enough time has passed for movement
            if (timer >= movementDelay)
            {
                // Move the object along the y-axis with reduced speed
                //transform.localPosition += new Vector3(0, 1, 0);
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

                if(health <= 0)
                {
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                }

                // Reset timer
                timer = 0f;
            }



            //health += -1;

            // Move the object along the x-axis
            //transform.localPosition += new Vector3(0, 1, 0);   


            //int neighbor = RNG.Next(0, 4);
            //transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        void moveAnt()
        {
            Vector3 newPosition = transform.position;
            bool moveComplete = false;
            int neighbor = RNG.Next(0, 4);
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
            /*
            bool moveComplete = false;
            int neighbor = 0;
            while (!moveComplete && neighbor < 4)
            {
                int neighbor = RNG.Next(0, 4);

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
                neighbor++;
            }
            */
        }

        bool movePosition(Vector3 position)
        {
            AbstractBlock nextBlock = WorldManagerInstance.GetBlock((int)position.x, (int)position.y, (int)position.z);
            AbstractBlock nextBlockBelow = WorldManagerInstance.GetBlock((int)position.x, (int)position.y - 1, (int)position.z);

            // check is next block is air and there is a block to stand on
            if (!nextBlock.isVisible() && nextBlockBelow.isVisible())
            {
                //transform.position = new Vector3(nextBlock.worldXCoordinate, nextBlock.worldYCoordinate, nextBlock.worldZCoordinate);
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

            //AbstractBlock nextBlock = WorldManagerInstance.GetBlock((int)position.x, (int)position.y, (int)position.z);
            //AbstractBlock nextBlockBelow = WorldManagerInstance.GetBlock((int)position.x, (int)position.y - 1, (int)position.z);

            // check is next block is air and there is a block to stand on
            //if (!nextBlock.isVisible() && nextBlockBelow.isVisible())
            //{
            //    transform.position = position;
            //    return true;
            //}

            // if above was false, check if the is a block two units down for the ant to stand on
            //AbstractBlock nextBlockTwoBelow = WorldManagerInstance.GetBlock((int)position.x, (int)position.y - 2, (int)position.z);
            //if (!nextBlock.isVisible() && nextBlockTwoBelow.isVisible())
            //{
            //    transform.position = new Vector3(nextBlockTwoBelow.worldXCoordinate, nextBlockTwoBelow.worldYCoordinate+1, nextBlockTwoBelow.worldZCoordinate); //nextBlockTwoBelow.position; //position;
            //    return true;
            //}

            //AbstractBlock nextBlockAbove = WorldManagerInstance.GetBlock((int)position.x, (int)position.y + 1, (int)position.z);

            //return false;
        }

        bool moveRight(Vector3 position)
        {
            AbstractBlock nextBlock = WorldManagerInstance.GetBlock((int)position.x, (int)position.y, (int)position.z);
            AbstractBlock nextBlockBelow = WorldManagerInstance.GetBlock((int)position.x, (int)position.y - 1, (int)position.z);
            if(!nextBlock.isVisible() && nextBlockBelow.isVisible())
            {
                transform.position = position;
                return true;
            }
            AbstractBlock nextBlockTwoBelow = WorldManagerInstance.GetBlock((int)position.x, (int)position.y - 2, (int)position.z);
            if (!nextBlock.isVisible() && nextBlockTwoBelow.isVisible())
            {
                transform.position = position;
                return true;
            }
            return false;
        }

        bool moveLeft(Vector3 position)
        {
            AbstractBlock nextBlock = WorldManagerInstance.GetBlock((int)position.x, (int)position.y, (int)position.z);
            AbstractBlock nextBlockBelow = WorldManagerInstance.GetBlock((int)position.x, (int)position.y - 1, (int)position.z);
            return false;
        }

        bool moveForward(Vector3 position)
        {
            AbstractBlock nextBlock = WorldManagerInstance.GetBlock((int)position.x, (int)position.y, (int)position.z);
            AbstractBlock nextBlockBelow = WorldManagerInstance.GetBlock((int)position.x, (int)position.y - 1, (int)position.z);
            return false;
        }

        bool moveBackward(Vector3 position)
        {
            AbstractBlock nextBlock = WorldManagerInstance.GetBlock((int)position.x, (int)position.y, (int)position.z);
            AbstractBlock nextBlockBelow = WorldManagerInstance.GetBlock((int)position.x, (int)position.y-1, (int)position.z);
            return false;
        }
    }
}
