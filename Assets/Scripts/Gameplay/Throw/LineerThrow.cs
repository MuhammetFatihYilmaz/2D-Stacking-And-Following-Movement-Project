using System;
using UnityEngine;

namespace StackingSample.Gameplay.Throw
{
    public class LineerThrow : ThrowControlBase
    {
        private Rigidbody2D rigidBody;

        private void Awake() => TryGetComponent(out rigidBody);

        public override void ThrowObject(float force)
        {
            var inputValueHorizontal = Input.GetAxis("Horizontal");

            (inputValueHorizontal >= 0 ? (Action)(() => rigidBody.AddForce(transform.right * force, ForceMode2D.Impulse))
                                       : () => rigidBody.AddForce(-transform.right * force, ForceMode2D.Impulse))();
        }
    }
}
