using UnityEngine;

namespace StackingSample.Gameplay.PhysicsInteraction
{
    public class PhysicsInteractionHandler : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ITriggerInteract triggerInteract))
            {
                triggerInteract.OnTriggerInteraction();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent(out ICollisionInteract collisionInteract))
            {
                collisionInteract.OnCollisionInteraction();
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent(out ICollisionInteract collisionInteract))
            {
                collisionInteract.OnCollisionInteraction();
            }
        }
    }
}
