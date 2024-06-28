using UnityEngine;

namespace Player
{
    public class PlayerMovements : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private Rigidbody2D playerRigidBody;
        private Vector2 movement;

        private void Update() => PlayerMovementUpAndDownInputs();

        private void PlayerMovementUpAndDownInputs()
        {
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");
        }

        private void FixedUpdate() => MovePlayer();

        private void MovePlayer() => playerRigidBody.MovePosition(playerRigidBody.position + movement* speed * Time.deltaTime);
    }
}