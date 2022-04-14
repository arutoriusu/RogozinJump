using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.RogozinGame
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform Rogozin;

        //private GameObject Game_Controller;
        private bool Game_Over = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        void FixedUpdate()
        {
            // Move camera to down if game over
            if (Game_Over)
            {
                // Delete player and all objects
                GameObject Player = GameObject.FindGameObjectWithTag("Player");
                GameObject[] Objects = GameObject.FindGameObjectsWithTag("Object");

                Destroy(Player);
                foreach (GameObject Obj in Objects)
                    Destroy(Obj);
            }
        }

        void LateUpdate()
        {
            if (!Game_Over)
            {
                // if target.y > camera.y + 2
                if (Rogozin.position.y > transform.position.y + 2)
                {
                    transform.position = new Vector3(transform.position.x, Rogozin.position.y - 2, transform.position.z);
                }
            }
        }
    }
}