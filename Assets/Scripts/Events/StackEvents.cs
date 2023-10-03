using System;
using StackingSample.Gameplay.Stacking;

namespace StackingSample.Events
{
    public static class StackEvents
    {
        public static Action<StackObject> OnStackObjectColliderTriggered;
        public static Action OnStackObjectThrow;
        public static Action<bool> OnStackSineFollowWayTriggered;
        public static Action<bool> OnStackSineRandomizeSpeedTriggered;
        public static Action<float> OnStackSineAmplitudeValueChanged;
    }
}
