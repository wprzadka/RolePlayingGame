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
                button.Click += new EventHandler((sender, e) => ChoseClass());
            }
        }

        private void ChoseClass()
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
                new Warrior("player", new Equipment()),
                new Archer("player", new Equipment()),
                new Mage("player", new Equipment())
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
            _controls.Clear();
            foreach (var button in _buttons)
            {
                _controls.Add(button);
            }
        }
    }
}
