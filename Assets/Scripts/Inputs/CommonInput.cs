using UnityEngine;

namespace Inputs
{
    public abstract class CommonInput : MonoBehaviour
    {
        protected InputActions InputActions { get; private set; }

        protected virtual void Awake()
        {
            InputActions = new InputActions();
        }

        protected virtual void OnEnable()
        {
            InputActions.Enable();
        }

        protected virtual void OnDisable()
        {
            InputActions.Disable();
        }

        public interface ICallback
        {
            void Return();
        }
    }
}