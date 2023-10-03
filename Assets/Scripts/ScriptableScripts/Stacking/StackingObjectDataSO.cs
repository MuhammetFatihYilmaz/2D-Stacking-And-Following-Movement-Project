using UnityEngine;

namespace StackingSample.ScriptableScripts.Stacking
{
    [CreateAssetMenu(fileName = nameof(StackingObjectDataSO), menuName = "StackingSample/StackingData/" + nameof(StackingObjectDataSO))]
    public class StackingObjectDataSO : ScriptableObject
    {
        public float FollowingThreshold;
        public bool IsFollowTypeSineWave;
        public float TransformFollowWaySpeed;
        [Range(2f, 5f)] public float SineFollowWaySpeed;
        [Range(0.05f, 0.15f)] public float SineAmplitude;
        [Range(20f, 50f)] public float ThrowForce;
        public LayerMask InStackLayer;
        public LayerMask OutStackLayer;
        public LayerMask IgnoredStackableLayer;
    }
}
