using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using System.Collections.Generic;

namespace GameGenerics.Input
{
    class Mapping
    {
        #region Fields
        private InputActions action;
        private Devices device;
        private int key;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public Mapping(InputActions action, Devices device, int key) 
        {
            this.device = device;
            this.action = action;
            this.key = key;
        }

        public Mapping(InputActions action, Keys k)
            : this(action, Devices.Keyboard, (int)k)
        {
        }

        public Mapping(InputActions action, Buttons k)
            : this(action, Devices.Gamepad, (int)k)
        {
        }
        #endregion

        #region Methods
        public bool Pressed
        {
            get
            {
                switch (device)
                {
                    case Devices.Keyboard:
                        return InputState.IsPressedKey((Keys)key);
                    case Devices.Gamepad:
                        return InputState.IsPressedButton((Buttons)key);
                }
                return false;
            }
        }

        public bool PressedOnce
        {
            get
            {
                switch (device)
                {
                    case Devices.Keyboard:
                        return InputState.IsPressedKeyOnce(((Keys)key));
                    case Devices.Gamepad:
                        return InputState.IsPressedButtonOnce(((Buttons)key));
                }
                return false;
            }
        }

        public static IEnumerable<Mapping> Make(InputActions action, params KeyValuePair<Devices,int>[] keys)
        {
            var list = new List<Mapping>();
            for(int i = 0 ; i < keys.Length ; i++)
            {
                list.Add(new Mapping(action, keys[i].Key, keys[i].Value));
            }
            return list;
        }
        #endregion
    }
}
