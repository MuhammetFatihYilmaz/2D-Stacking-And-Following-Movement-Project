using System;
using System.Collections;
using UnityEngine;
using StackingSample.Events;
using StackingSample.Gameplay.PhysicsInteraction;
using StackingSample.Gameplay.Stacking.FollowType;
using StackingSample.Gameplay.Throw;

namespace StackingSample.Gameplay.Stacking
{
    [RequireComponent(typeof(TransformFollowType))]
    [RequireComponent(typeof(SineFollowType))]
    [RequireComponent(typeof(LineerThrow))]
    public class StackObject : MonoBehaviour, ICollisionInteract
    {
        [HideInInspector] public StackObjectFollowBase StackMovementType;
        [HideInInspector] public TransformFollowType TransformFollowWay;
        [HideInInspector] public SineFollowType SineFollowWay;
        [HideInInspector] public ThrowControlBase ThrowController;
        private bool isStackableSequenceOver = true;

        private void Awake()
        {
            TryGetComponent(out TransformFollowWay);
            TryGetComponent(out SineFollowWay);
            TryGetComponent(out ThrowController);
        }

        public void OnCollisionInteraction()
        {
            StackEvents.OnStackObjectColliderTriggered?.Invoke(this);
        }

        public void SetMovementType(Type stackMovementType)
        {
            if(stackMovementType == typeof(TransformFollowType))
                this.StackMovementType = TransformFollowWay;
            else if(stackMovementType == typeof(SineFollowType))
                this.StackMovementType = SineFollowWay;
        }

        public void SetObjectLayer(LayerMask layer)
        {
            gameObject.layer = (int)Mathf.Log(layer.value, 2);
        }

        public void StartIgnoreStackableSequence(LayerMask ignoredLayer)
        {
            if (!isStackableSequenceOver) return;
                StartCoroutine(ObjectStackableSequence(ignoredLayer));
        }

        private IEnumerator ObjectStackableSequence(LayerMask ignoredLayer)
        {
            isStackableSequenceOver = false;
            int ignoredLayerValue = (int)Mathf.Log(ignoredLayer.value, 2);

            Physics2D.IgnoreLayerCollision(gameObject.layer, ignoredLayerValue, true);
            yield return new WaitForSeconds(0.5f);
            Physics2D.IgnoreLayerCollision(gameObject.layer, ignoredLayerValue, false);
            isStackableSequenceOver = true;
        }
    }
}
