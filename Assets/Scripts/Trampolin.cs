using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Assets.Scripts.RogozinGame
{
    public class Trampolin : TrampolinParent
    {
        private float Jump_Force = 10f;
        private float moveDistance = 5f;

        private Vector3 sourcePosition;
        private Vector3 destinationPosition;
        private bool movingSwitch = false;
        private int horizontalOrVerticalMove;

        [SerializeField] GameObject leftBorder;
        [SerializeField] GameObject rightBorder;

        public GameObject LeftBorder { get => leftBorder; set => leftBorder = value; }
        public GameObject RightBorder { get => rightBorder; set => rightBorder = value; }

        private void Start()
        {
            MovableOrNot();
            
            ArrayList tramplines = PrepareArea();
            foreach (GameObject t in tramplines)
            {
                t.GetComponent<Trampolin>().LeftBorder = LeftBorder;
                t.GetComponent<Trampolin>().RightBorder = RightBorder;
            }
        }

        private void MovableOrNot()
        {
            int movableChance = UnityEngine.Random.Range(0, 3);
            if (movableChance == 0)
            {
                TrampolinType = "movable";
                sourcePosition = transform.position;
                horizontalOrVerticalMove = UnityEngine.Random.Range(0, 2);
                if (horizontalOrVerticalMove == 0)
                {
                    if (sourcePosition.x + 10 >= RightBorder.transform.position.x)
                    {
                        destinationPosition = new Vector3(sourcePosition.x - moveDistance, sourcePosition.y, sourcePosition.z);
                    }
                    else
                    {
                        destinationPosition = new Vector3(sourcePosition.x + moveDistance, sourcePosition.y, sourcePosition.z);
                    }
                }
                else
                {
                    if (sourcePosition.y + 10 >= RightBorder.transform.position.y)
                    {
                        destinationPosition = new Vector3(sourcePosition.x, sourcePosition.y - moveDistance, sourcePosition.z);
                    }
                    else
                    {
                        destinationPosition = new Vector3(sourcePosition.x, sourcePosition.y + moveDistance, sourcePosition.z);
                    }
                }
            }
            else
            {
                TrampolinType = "regular";
            }
        }

        private void FixedUpdate()
        {
            if (TrampolinType == "movable")
            {
                MoveTrampolin();
            }
        }

        private void MoveTrampolin()
        {
            if (horizontalOrVerticalMove == 0)
            {
                if (sourcePosition.x - destinationPosition.x > 0)
                {
                    var step = CheckPlatformTrajectory(true);
                    transform.position = new Vector3(transform.position.x + step, transform.position.y, transform.position.z);
                    if (transform.position.x <= destinationPosition.x)
                    {
                        movingSwitch = true;
                    }
                    else if (transform.position.x >= sourcePosition.x)
                    {
                        movingSwitch = false;
                    }
                }
                else
                {
                    var step = CheckPlatformTrajectory(false);
                    transform.position = new Vector3(transform.position.x + step, transform.position.y, transform.position.z);
                    if (transform.position.x >= destinationPosition.x)
                    {
                        movingSwitch = true;
                    }
                    else if (transform.position.x <= sourcePosition.x)
                    {
                        movingSwitch = false;
                    }
                }  
            }
            else
            {
                if (sourcePosition.y - destinationPosition.y > 0)
                {
                    var step = CheckPlatformTrajectory(true);
                    transform.position = new Vector3(transform.position.x, transform.position.y + step, transform.position.z);
                    if (transform.position.y <= destinationPosition.y)
                    {
                        movingSwitch = true;
                    }
                    else if (transform.position.y >= sourcePosition.y)
                    {
                        movingSwitch = false;
                    }
                }
                else
                {
                    var step = CheckPlatformTrajectory(false);
                    transform.position = new Vector3(transform.position.x, transform.position.y + step, transform.position.z);
                    if (transform.position.y >= destinationPosition.y)
                    {
                        movingSwitch = true;
                    }
                    else if (transform.position.y <= sourcePosition.y)
                    {
                        movingSwitch = false;
                    }
                }
            }
        }

        private float CheckPlatformTrajectory(bool rightToLeft)
        {
            float move = 0;
            if (rightToLeft)
            {
                if (!movingSwitch)
                {
                    move = -0.1f;
                }
                else
                {
                    move = 0.1f;
                }
            }
            else
            {
                if (!movingSwitch)
                {
                    move = 0.1f;
                }
                else
                {
                    move = -0.1f;
                }
            }
            return move;
        }

        void OnCollisionEnter2D(Collision2D Other)
        {
            if (Other.relativeVelocity.y <= 0f)
            {
                Rigidbody2D rb = Other.collider.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 Force = rb.velocity;
                    Force.y = Jump_Force;
                    rb.velocity = Force;

                    GameManagerObj.GetComponent<GameManager>().countPlatformsUsed += 1;
                }
            }
        }
    }
}