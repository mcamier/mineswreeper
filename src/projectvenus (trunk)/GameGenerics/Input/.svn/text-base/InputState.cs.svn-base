using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace GameGenerics.Input
{
    /// <summary>
    /// Classe d'entrée utilisateur minimaliste
    /// </summary>
    public class InputState : GameComponent
    {
        #region Fields

        private static KeyboardState _currentKeyboardState;
        private static GamePadState _currentGamePadState;
        private static KeyboardState _lastKeyboardState;
        private static GamePadState _lastGamePadState;
        private static bool GamePadWasConnected;
        private static Dictionary<InputActions, IEnumerable<Mapping>> mappings = new Dictionary<InputActions,IEnumerable<Mapping>>();

        #endregion

        #region Properties

        public static KeyboardState CurrentKeyboardState
        {
            get { return _currentKeyboardState; }
        }

        public static GamePadState CurrentGamePadState
        {
            get { return _currentGamePadState; }
        }

        #endregion

        #region Constructors
        public InputState(Game game)
            : base(game)
        {
            _currentKeyboardState = new KeyboardState();
            _currentGamePadState = new GamePadState();
            _lastKeyboardState = new KeyboardState();
            _lastGamePadState = new GamePadState();
            InitializeMapping();
        }
        #endregion

        #region Public Methods

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _lastKeyboardState = _currentKeyboardState;
            _lastGamePadState = _currentGamePadState;
            _currentKeyboardState = Keyboard.GetState();
            _currentGamePadState = GamePad.GetState(PlayerIndex.One);
            GamePadWasConnected = _currentGamePadState.IsConnected;
        }

        public static bool IsPressedKey(Keys key)
        {
            return _currentKeyboardState.IsKeyDown(key);
        }

        public static bool IsPressedButton(Buttons button)
        {
            return _currentGamePadState.IsButtonDown(button);
        }

        public static bool IsPressedKeyOnce(Keys key)
        {
            return (_currentKeyboardState.IsKeyDown(key) &&
                    _lastKeyboardState.IsKeyUp(key));
        }

        public static bool IsPressedButtonOnce(Buttons button)
        {
            return (_currentGamePadState.IsButtonDown(button) &&
                    _lastGamePadState.IsButtonUp(button));
        }

        public static bool IsKeyHeld(Keys key)
        {
            return (_currentKeyboardState.IsKeyDown(key) &&
                    _lastKeyboardState.IsKeyDown(key));
        }

        public static bool IsButtonHeld(Buttons button)
        {
            return (_currentGamePadState.IsButtonDown(button) &&
                    _lastGamePadState.IsButtonDown(button));
        }

        public static bool IsReleasedKey(Keys key)
        {
            return (_currentKeyboardState.IsKeyUp(key) &&
                    _lastKeyboardState.IsKeyDown(key));
        }

        public static bool IsReleasedButton(Buttons button)
        {
            return (_currentGamePadState.IsButtonUp(button) &&
                    _lastGamePadState.IsButtonDown(button));
        }

        public static bool IsMenuSelect()
        {
            return IsPressedKeyOnce(Keys.Space) ||
                   IsPressedKeyOnce(Keys.Enter) ||
                   IsPressedButtonOnce(Buttons.A) ||
                   IsPressedButtonOnce(Buttons.Start);
        }

        public static bool IsMenuCancel()
        {
            return IsPressedKeyOnce(Keys.Escape) ||
                   IsPressedButtonOnce(Buttons.B) ||
                   IsPressedButtonOnce(Buttons.Back);
        }

        public static bool IsMenuUp()
        {
            return IsPressedKeyOnce(Keys.Up) ||
                   IsPressedButtonOnce(Buttons.DPadUp) ||
                   IsPressedButtonOnce(Buttons.LeftThumbstickUp);
        }

        public static bool IsMenuDown()
        {
            return IsPressedKeyOnce(Keys.Down) ||
                   IsPressedButtonOnce(Buttons.DPadDown) ||
                   IsPressedButtonOnce(Buttons.LeftThumbstickDown);
        }

        public static bool IsPauseGame()
        {
            return IsPressedKeyOnce(Keys.Escape) ||
                   //IsNewButtonPress(Buttons.Back) ||
                   IsPressedButtonOnce(Buttons.Start);
        }

        private static void AddMapping(InputActions action, params KeyValuePair<Devices, int>[] keys)
        {
            mappings.Add(action, Mapping.Make(action, keys));
        }

        private static void InitializeMapping()
        {
            AddMapping(InputActions.Accept, 
                new KeyValuePair<Devices, int>(Devices.Keyboard, (int)Keys.Enter), 
                new KeyValuePair<Devices, int>(Devices.Gamepad, (int)Buttons.A));

            AddMapping(InputActions.Cancel, 
                new KeyValuePair<Devices, int>(Devices.Keyboard, (int)Keys.Escape), 
                new KeyValuePair<Devices, int>(Devices.Gamepad, (int)Buttons.B));


            AddMapping(InputActions.MoveUp, 
                new KeyValuePair<Devices, int>(Devices.Keyboard, (int)Keys.W), 
                new KeyValuePair<Devices, int>(Devices.Gamepad, (int)Buttons.LeftThumbstickUp));

            AddMapping(InputActions.MoveDown, 
                new KeyValuePair<Devices, int>(Devices.Keyboard, (int)Keys.S), 
                new KeyValuePair<Devices, int>(Devices.Gamepad, (int)Buttons.LeftThumbstickDown));

            AddMapping(InputActions.MoveLeft, 
                new KeyValuePair<Devices, int>(Devices.Keyboard, (int)Keys.A), 
                new KeyValuePair<Devices, int>(Devices.Gamepad, (int)Buttons.LeftThumbstickLeft));

            AddMapping(InputActions.MoveRight,
                new KeyValuePair<Devices, int>(Devices.Keyboard, (int)Keys.D),
                new KeyValuePair<Devices, int>(Devices.Gamepad, (int)Buttons.LeftThumbstickRight));


            AddMapping(InputActions.ActiveShield,
                new KeyValuePair<Devices, int>(Devices.Keyboard, (int)Keys.Space),
                new KeyValuePair<Devices, int>(Devices.Gamepad, (int)Buttons.LeftTrigger));

            AddMapping(InputActions.Fire,
                new KeyValuePair<Devices, int>(Devices.Keyboard, (int)Keys.Enter),
                new KeyValuePair<Devices, int>(Devices.Gamepad, (int)Buttons.A));

            AddMapping(InputActions.AltFire,
                new KeyValuePair<Devices, int>(Devices.Keyboard, (int)Keys.RightShift),
                new KeyValuePair<Devices, int>(Devices.Gamepad, (int)Buttons.B));

            AddMapping(InputActions.SecondaryFire,
                new KeyValuePair<Devices, int>(Devices.Keyboard, (int)Keys.LeftControl),
                new KeyValuePair<Devices, int>(Devices.Gamepad, (int)Buttons.RightTrigger));



            AddMapping(InputActions.NextSpeaker, 
                new KeyValuePair<Devices, int>(Devices.Keyboard, (int)Keys.Enter), 
                new KeyValuePair<Devices, int>(Devices.Gamepad, (int)Buttons.A));

        }

        public static bool IsPressed(InputActions action)
        {
            IEnumerable<Mapping> keys;
            return mappings.TryGetValue(action, out keys) && keys.Any(k => k.Pressed);
        }
 
        public static bool IsPressedOnce(InputActions action)
        {
            IEnumerable<Mapping> keys;
            return mappings.TryGetValue(action, out keys) && keys.Any(k => k.PressedOnce);
        }
        #endregion
    }
}
