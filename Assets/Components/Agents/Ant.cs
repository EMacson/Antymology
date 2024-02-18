using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Antymology.Agents
{
    public class Ant : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// The X Coordinate of this ant.
        /// </summary>
        public int x { get; set; }

        /// <summary>
        /// The y Coordinate of this ant.
        /// </summary>
        public int y { get; set; }

        /// <summary>
        /// The z coordinate of this ant.
        /// </summary>
        public int z { get; set; }

        /// <summary>
        /// Whether or not this ant needs to have its mesh updated
        /// </summary>
        public bool updateNeeded { get; set; }

        /// <summary>
        /// The Unity mesh component of this ant object.
        /// </summary>
        Mesh mesh;

        /// <summary>
        /// The Unity mesh renderer component of this ant object.
        /// </summary>
        MeshRenderer renderer;

        /// <summary>
        /// The collider of this mesh
        /// </summary>
        MeshCollider collider;

        public GameObject antModelPrefab;

        #endregion

        #region Methods

        /// <summary>
        /// initialize the gameobject to have a mesh and mesh renderer, and set the references internally.
        /// </summary>
        public void Init(Material mat)
        {
            // Add MeshFilter component and get the mesh
            MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
            mesh = meshFilter.mesh;

            // Define cube vertices
            Vector3[] vertices = new Vector3[]
            {
            // Front face
            new Vector3(-0.5f, -0.5f, 0.5f),
            new Vector3(0.5f, -0.5f, 0.5f),
            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(-0.5f, 0.5f, 0.5f),

            // Back face
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, 0.5f, -0.5f)
            };

            // Define cube triangles
            int[] triangles = new int[]
            {
            // Front face
            0, 1, 2,
            2, 3, 0,

            // Back face
            5, 4, 7,
            7, 6, 5,

            // Top face
            3, 2, 6,
            6, 7, 3,

            // Bottom face
            1, 0, 4,
            4, 5, 1,

            // Left face
            4, 0, 3,
            3, 7, 4,

            // Right face
            1, 5, 6,
            6, 2, 1
            };

            // Set vertices and triangles to mesh
            mesh.vertices = vertices;
            mesh.triangles = triangles;

            // Add MeshRenderer component and set the material
            renderer = gameObject.AddComponent<MeshRenderer>();
            renderer.material = mat;

            // Add MeshCollider component
            collider = gameObject.AddComponent<MeshCollider>();
        }

        /// <summary>
        /// Generates a mesh object which is then passed to the Mesh component of this monobehaviour.
        /// </summary>
        public void GenerateMesh()
        {
            
        }
        public float speed = 5f; // Speed at which the object moves
        

        #endregion
    }
}
