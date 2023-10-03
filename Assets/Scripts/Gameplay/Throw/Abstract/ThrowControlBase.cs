using UnityEngine;

namespace StackingSample.Gameplay.Throw
{
    public abstract class ThrowControlBase : MonoBehaviour
    {
        public abstract void ThrowObject(float force);
    }
}
