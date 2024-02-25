using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DashManager
{
    public class DashManager : MonoBehaviour
    {
        [SerializeField]
        private float dashDistance = 2.5f; // Distance covered by the player character during a dash

        [SerializeField]
        private float dashDuration = 0.1f; // Duration of the dash animation in seconds

        private float elapsedTime; // Time elapsed since the start of the dash

        private Animator visualAnimator;

        private Rigidbody2D rigidbody2D;

        private MovementManager movementManager;

        

        void Awake()
        {
            elapsedTime = 0f;
            visualAnimator = GetComponentInChildren<Animator>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            movementManager = GetComponent<MovementManager>();
        }


        private bool CanDash(Vector2 direction)
        {
            Vector2 finalPosition = (Vector2)transform.position + direction * dashDistance;
            RaycastHit2D hit = Physics2D.Raycast(finalPosition, direction, 0.1f);
            return hit.collider == null;
        }
        
        

        
        private void Dash() {
            Vector2 direction = Vector2.zero;
            switch (movementManager.LastDirection)
            {
                case 'N':
                    direction = Vector2.up;
                    break;
                case 'S':
                    direction = Vector2.down;
                    break;
                case 'E':
                    direction = Vector2.right;
                    break;
                case 'W':
                    direction = Vector2.left;
                    break;
                default:
                    break;
            }

            if (!CanDash(direction))
            {
                return;
            }

            movementManager.IsDashing = true;
            rigidbody2D.bodyType = RigidbodyType2D.Kinematic;

            if (direction != Vector2.zero)
            {
                rigidbody2D.velocity = direction * dashDistance / dashDuration;
                elapsedTime = dashDuration;
            }
        }
        


        // Update is called once per frame
        void Update()
        {
            MovementManager movement = GetComponent<MovementManager>();
            if (Input.GetKeyDown(KeyCode.LeftControl) && !movement.IsOnAnimation())
            {
                Dash();
            }
            
            if (elapsedTime > 0)
            {
                elapsedTime -= Time.deltaTime;
                if (elapsedTime <= 0)
                {
                    rigidbody2D.velocity = Vector2.zero;
                    rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                    movementManager.IsDashing = false;
                    elapsedTime = 0f;
                }
            }
        }
    }
}
