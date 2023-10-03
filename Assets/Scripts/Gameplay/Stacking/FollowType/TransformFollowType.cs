using System.Collections;
using UnityEngine;

namespace StackingSample.Gameplay.Stacking.FollowType
{
    public class TransformFollowType : StackObjectFollowBase
    {
        public override void SetFollowingObjectMovementSettings(Transform followedObj, float followThreshold, float followSpeed)
        {
            base.SetFollowingObjectMovementSettings(followedObj, followThreshold, followSpeed);
        }

        protected override IEnumerator MovementSequence()
        {
            while (IsFollowing)
            {
                yield return new WaitForEndOfFrame();
                MovementDirection.Set(
                    Mathf.Lerp(transform.position.x, FollowedObjPos.transform.position.x, FollowSpeed * Time.deltaTime),
                    FollowedObjPos.transform.position.y + FollowThreshold);

                transform.position = MovementDirection;
            }
            yield break;
        }
    }
}
