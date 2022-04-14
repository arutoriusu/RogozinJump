using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.RogozinGame
{
    public class Rogozin : MonoBehaviour
    {
        private Rigidbody2D rb;
        private float Movement_Speed = 10f;

        [SerializeField] GameObject backgroundLeftBorder;
        [SerializeField] GameObject backgroundRightBorder;
        [SerializeField] GameObject gameManagerObj;

        private float Movement = 0;
        private float timeDelay;

        // Start is called before the first frame update
        void Start()
        {
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(-4, 4));
            rb = GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.up * 400);
        }

        void Update()
        {
            // Set Movement value
            Movement = Input.GetAxis("Horizontal") * Movement_Speed;

            if (gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0f)
            {
                GetComponent<CapsuleCollider2D>().isTrigger = false;
            }
            else
            {
                GetComponent<CapsuleCollider2D>().isTrigger = true;
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // Calculate player velocity
            Vector2 Velocity = rb.velocity;
            Velocity.x = Movement;
            rb.velocity = Velocity;


            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

            if (screenPos.x < 0 && timeDelay + 0.3f < Time.time)
            {
                transform.position = new Vector3(backgroundRightBorder.transform.position.x, transform.position.y, transform.position.z);
                timeDelay = Time.time;
            }
            else if (screenPos.x > Screen.width && timeDelay + 0.3f < Time.time)
            {
                transform.position = new Vector3(backgroundLeftBorder.transform.position.x, transform.position.y, transform.position.z);
                timeDelay = Time.time;
            }
            if (screenPos.y < 0)
            {
                gameManagerObj.GetComponent<GameManager>().IsGameOver = true;
            }

            float pointer_x = Input.GetAxis("Mouse X");
            float pointer_y = Input.GetAxis("Mouse Y");

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                pointer_x = Input.touches[0].deltaPosition.x;
                transform.position = new Vector3(pointer_x * 0.3f * Time.deltaTime + transform.position.x, transform.position.y, transform.position.z);
            }

            /*if (Input.GetMouseButton(0))
            {
                float mouseX = Input.GetAxis("Mouse X");
                transform.position = new Vector3(transform.position.x + mouseX * .5f, transform.position.y, transform.position.z);
            }*/
        }
    }
}