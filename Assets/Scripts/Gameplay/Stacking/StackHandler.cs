using System;
using System.Collections.Generic;
using UnityEngine;
using StackingSample.Events;
using StackingSample.ScriptableScripts.Stacking;
using StackingSample.Gameplay.Stacking.FollowType;

namespace StackingSample.Gameplay.Stacking
{
    public class StackHandler : MonoBehaviour
    {
        [field: SerializeField] private StackingObjectDataSO stackingObjectDataSO;
        private List<StackObject> stackedObjectList = new();
        private Transform firstStackPos;
        private Transform followingPos;
        private int followingIndex;
        private float followSpeed;

        private void Awake()
        {
            firstStackPos = this.transform;
            followSpeed = stackingObjectDataSO.TransformFollowWaySpeed;
        }

        private void OnEnable()
        {
            StackEvents.OnStackObjectColliderTriggered += OnStackObjectColliderTriggered;
            StackEvents.OnStackObjectThrow += OnStackObjectThrow;
            StackEvents.OnStackSineFollowWayTriggered += OnStackSineFollowWayTriggered;
        }

        private void OnDisable()
        {
            StackEvents.OnStackObjectColliderTriggered -= OnStackObjectColliderTriggered;
            StackEvents.OnStackObjectThrow -= OnStackObjectThrow;
            StackEvents.OnStackSineFollowWayTriggered -= OnStackSineFollowWayTriggered;
        }

        private void ReorderStackAfterThrow()
        {
            if (stackedObjectList.Count == 0) return;
            followingIndex--;

            for (int i = stackedObjectList.Count - 1; i >= 0; i--)
            {
                followingPos = (i > 0) ? stackedObjectList[i - 1].transform : firstStackPos;
                stackedObjectList[i].StackMovementType.SetFollowingObjectMovementSettings(followingPos, stackingObjectDataSO.FollowingThreshold, followSpeed);
            }
        }

        private void ResetStackOrder(Type stackMovementType)
        {
            for (int i = 0; i < stackedObjectList.Count; i++)
            {
                stackedObjectList[i].StackMovementType.StopCurrentMovementSequence();
                stackedObjectList[i].SetMovementType(stackMovementType);
                followingPos = (i > 0) ? stackedObjectList[i - 1].transform : firstStackPos;
                stackedObjectList[i].StackMovementType.SetFollowingObjectMovementSettings(followingPos, stackingObjectDataSO.FollowingThreshold, followSpeed);
                stackedObjectList[i].StackMovementType.StartMovementSequence();
            }
        }

        #region Events
        private void OnStackObjectColliderTriggered(StackObject stackedObj)
        {
            stackedObjectList.Add(stackedObj);
            stackedObj.SetObjectLayer(stackingObjectDataSO.InStackLayer);
            if (stackingObjectDataSO.IsFollowTypeSineWave)
                stackedObj.SetMovementType(typeof(SineFollowType));
            else
                stackedObj.SetMovementType(typeof(TransformFollowType));

            followingPos = (stackedObjectList.Count <= 1) ? firstStackPos : stackedObjectList[followingIndex].transform;
            if (followingPos != firstStackPos) followingIndex++;

            stackedObj.StackMovementType.SetFollowingObjectMovementSettings(followingPos, stackingObjectDataSO.FollowingThreshold, followSpeed);
            stackedObj.StackMovementType.StartMovementSequence();
        }

        private void OnStackObjectThrow()
        {
            if (stackedObjectList.Count == 0) return;
            var throwedObject = stackedObjectList[0];
            stackedObjectList.RemoveAt(0);
            throwedObject.StackMovementType.StopCurrentMovementSequence();
            throwedObject.ThrowController.ThrowObject(stackingObjectDataSO.ThrowForce);
            throwedObject.SetObjectLayer(stackingObjectDataSO.OutStackLayer);
            throwedObject.StartIgnoreStackableSequence(stackingObjectDataSO.IgnoredStackableLayer);
            ReorderStackAfterThrow();
        }

        private void OnStackSineFollowWayTriggered(bool sineFollowWayTriggered)
        {
            followSpeed = sineFollowWayTriggered ? stackingObjectDataSO.SineFollowWaySpeed : stackingObjectDataSO.TransformFollowWaySpeed;
            if (sineFollowWayTriggered)
                ResetStackOrder(typeof(SineFollowType));
            else
                ResetStackOrder(typeof(TransformFollowType));
        }

        #endregion
    }
}
