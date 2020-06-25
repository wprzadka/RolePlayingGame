using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Windows.Forms;
using RolePlayingGame.Engine.Characters.Player;
using RolePlayingGame.Engine.Characters.Player.Classes;
using RolePlayingGame.Engine.Items;

namespace RolePlayingGame.UserInterface
{
    class MainMenu : IMainMenu
    {
        private readonly int _windowWith;
        
        private readonly int _windowHeight;

        private readonly Action<IPlayerCharacter> _startGameFunction; 
        
        private IList<Button> _buttons;

        private Control.ControlCollection _controls;

        public MainMenu(Control.ControlCollection controls, int width, int height, Action<IPlayerCharacter> startGameFunc)
        {
            _controls = controls;
            _windowWith = width;
            _windowHeight = height;
            _startGameFunction = startGameFunc;
        }

        private void ChoseName()
        {
            var textBox = new TextBox
            {
                Width = _windowWith / 2,
                Height = 180,
                Location = new Point(_windowWith / 4, _windowHeight / 4),
                PlaceholderText = @"Name"
            };
            var acceptButton = new Button
            {
                Width = 80,
                Height = 60,
                Location = new Point(_windowWith * 3 / 4, _windowHeight / 4 + 200),
                Text = @"Ok",
                Name = "Ok",
                BackColor = Color.DarkSlateGray,
                ForeColor = Color.AliceBlue
            };
            acceptButton.Click += new EventHandler((e, sender) => ChoseClass(textBox.Text));

            _controls.Clear();
            _controls.Add(textBox);
            _controls.Add(acceptButton);
        }

        private void ChoseClass(string playerName)
        {
            var classNames = new List<string>
            {
                "Warrior", "Archer", "Mage"
            };
            var charactersTiles = new List<Button>();
            var shiftPos = 1;
            foreach (var name in classNames)
            {
                var newTile = new Button
                {
                    Width = _windowWith / 3 - _windowWith / 8,
                    Height = _windowHeight - _windowHeight / 4,
                    Location = new Point(_windowWith / 4 * shiftPos - _windowWith / 8, _windowHeight / 20),
                    Text = name,
                    Name = name,
                    BackColor = Color.AliceBlue,
                    ForeColor = Color.DarkSlateGray
                };
                ++shiftPos;
                charactersTiles.Add(newTile);
            }
            var characters = new List<PlayerCharacter>
            {
                new Warrior(playerName, new Equipment()),
                new Archer(playerName, new Equipment()),
                new Mage(playerName, new Equipment())
            };

            foreach (var (character, button) in characters.Zip(charactersTiles))
            {
                button.Click += new EventHandler((e, sender) => _startGameFunction(character));
            }
            _controls.Clear();
            foreach (var button in charactersTiles)
            {
                _controls.Add(button);
            }
        }

        public void loadScreen()
        {
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
                button.Click += new EventHandler((sender, e) => ChoseName());
            }
            _controls.Clear();
            foreach (var button in _buttons)
            {
                _controls.Add(button);
            }
        }
    }
}
