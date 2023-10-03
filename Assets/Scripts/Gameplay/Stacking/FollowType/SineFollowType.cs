using System.Collections;
using System.Reflection;
using UnityEngine;
using StackingSample.Events;
using StackingSample.ScriptableScripts.Stacking;

namespace StackingSample.Gameplay.Stacking.FollowType
{
    public class SineFollowType : StackObjectFollowBase
    {
        private float followSpeed;
        private float amplitude;
        private bool isRandomizeSpeedOn;
        private float sineWaveValue;

        private void OnEnable()
        {
            StackEvents.OnStackSineRandomizeSpeedTriggered += OnStackSineRandomizeSpeedTriggered;
            StackEvents.OnStackSineAmplitudeValueChanged += OnStackSineAmplitudeValueChanged;
        }

        private void OnDisable()
        {
            StackEvents.OnStackSineRandomizeSpeedTriggered -= OnStackSineRandomizeSpeedTriggered;
            StackEvents.OnStackSineAmplitudeValueChanged -= OnStackSineAmplitudeValueChanged;
        }
        public override void SetFollowingObjectMovementSettings(Transform followedObj, float followThreshold, float followSpeed)
        {
            base.SetFollowingObjectMovementSettings(followedObj, followThreshold, followSpeed);
            ArrangeFollowSpeed();
        }

        protected override IEnumerator MovementSequence()
        {
            while (IsFollowing)
            {
                yield return new WaitForEndOfFrame();

                sineWaveValue = (Mathf.Sin(Time.time * followSpeed) * amplitude);

                MovementDirection.Set(
                FollowedObjPos.transform.position.x + sineWaveValue,
                FollowedObjPos.transform.position.y + FollowThreshold);

                transform.position = MovementDirection;
            }
            yield break;
        }

        private void ArrangeFollowSpeed()
        {
            if (isRandomizeSpeedOn)
            {
                var SineFollowWaySpeed = typeof(StackingObjectDataSO)
                .GetField(nameof(StackingObjectDataSO.SineFollowWaySpeed))
                .GetCustomAttribute<RangeAttribute>();

                followSpeed = (SineFollowWaySpeed != null) ? Random.Range(SineFollowWaySpeed.min, SineFollowWaySpeed.max) 
                                                           : Random.Range(2f, 5f);
            }
            else
                followSpeed = FollowSpeed;
        }

        #region Events
        private void OnStackSineRandomizeSpeedTriggered(bool isRandomizeSpeedOn)
        {
            this.isRandomizeSpeedOn = isRandomizeSpeedOn;
            ArrangeFollowSpeed();
        }

        private void OnStackSineAmplitudeValueChanged(float value)
        {
            amplitude = value;
        }
        #endregion
    }
}
