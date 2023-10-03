using StackingSample.Events;
using UnityEngine;

namespace StackingSample.Gameplay.Throw
{
    public class ThrowHandler : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
                StackEvents.OnStackObjectThrow?.Invoke();
        }
    }
}
