using System.Collections;
using UnityEngine;

namespace StackingSample.Gameplay.Stacking.FollowType
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class StackObjectFollowBase : MonoBehaviour
    {
        private IEnumerator currentMovementSequence;
        private Rigidbody2D rigidBody;
        protected Vector2 MovementDirection;
        protected float FollowThreshold;
        protected Transform FollowedObjPos;
        protected float FollowSpeed;
        protected bool IsFollowing;

        private void Awake() => TryGetComponent(out rigidBody);

        protected abstract IEnumerator MovementSequence();

        public virtual void SetFollowingObjectMovementSettings(Transform followedObjPos, float followThreshold, float followSpeed)
        {
            rigidBody.velocity = Vector2.zero;
            FollowedObjPos = followedObjPos;
            FollowThreshold = followThreshold;
            FollowSpeed = followSpeed;
        }

        public void StartMovementSequence()
        {
            IsFollowing = true;
            currentMovementSequence = MovementSequence();
            StartCoroutine(currentMovementSequence);
        }

        public void StopCurrentMovementSequence()
        {
            if(currentMovementSequence != null)
                StopCoroutine(currentMovementSequence);

            IsFollowing = false;
        }
    }
}
