using Inputs;
using Koyou.Commons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scenes.Games
{
    public class GameInput : CommonInput
    {
        #region MonoBehaviour

        protected override void Awake()
        {
            base.Awake();
            _gameActions = InputActions.Game;
            _gameActions.Disable();

            _gameActions.Move.AddObserver(Move);
        }

        #endregion

        #region GameInput

        private InputActions.GameActions _gameActions;

        public ICallback callback;

        public void ActiveInput()
        {
            _gameActions.Enable();
        }

        public void InactiveInput()
        {
            _gameActions.Disable();
        }

        private void Move(InputAction.CallbackContext ctx)
        {
            if (ctx.phase == InputActionPhase.Performed)
            {
                var value = ctx.ReadValue<Vector2>();
                // value to direction
                var direction = value switch
                {
                    var v when v.x >= 1f => Vector2Int.right,
                    var v when v.x <= -1f => Vector2Int.left,
                    var v when v.y >= 1f => Vector2Int.up,
                    var v when v.y <= -1f => Vector2Int.down,
                    _ => Vector2Int.zero
                };
                if (direction == Vector2Int.zero)
                {
                    return;
                }

                callback?.Move(direction);
            }

            // todo 后续处理持续移动 的逻辑
        }

        public new interface ICallback : CommonInput.ICallback
        {
            void Move(Vector2Int direction);
        }

        #endregion
    }
}