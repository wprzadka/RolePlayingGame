using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Text;
using System.Windows.Forms;

namespace RolePlayingGame.UserInterface
{
    class MainMenu : IMainMenu
    {
        private int _windowWith;
        
        private int _windowHeight;

        private IList<Button> _buttons;

        public MainMenu(Control.ControlCollection controls, int width, int height, Action startGameFunc)
        {
            Controls = controls;
            _windowWith = width;
            _windowHeight = height;

            var buttonsNames = new List<string>
            {
                "New game", "-", "-", "Exit"
            };
            _buttons = new List<Button>();
            var shiftPos = 1;
            foreach (var name in buttonsNames)
            {
                _buttons.Add(new Button
                {
                    Width = _windowWith / 2,
                    Height = _windowHeight / 7,
                    Location = new System.Drawing.Point(_windowWith / 4, _windowHeight / 6 * shiftPos),
                    Text = name,
                    Name = name,
                    BackColor = System.Drawing.Color.DarkSlateGray,
                    ForeColor = System.Drawing.Color.AliceBlue
                });
                ++shiftPos;
            }

            foreach (var button in _buttons)
            {
                button.Click += new EventHandler((sender, e) => startGameFunc());
            }
        }

        private bool startGame()
        {
            return false;
        }

        public Control.ControlCollection Controls { get; }

        public PaintEventHandler Paint { get; }

        public void loadScreen()
        {
            Controls.Clear();
            foreach (var button in _buttons)
            {
                Controls.Add(button);
            }
        }
    }
}
