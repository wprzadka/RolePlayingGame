using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using RolePlayingGame.Engine;

namespace RolePlayingGame.UserInterface
{
    public class GameInterface : IGameInterface
    {
        private readonly IGameState _gameState;

        private readonly int _windowWidth;

        private readonly int _windowHeight;

        private readonly Font _font;
        public GameInterface(IGameState state, int width, int height, Font font)
        {
            _gameState = state;
            _windowWidth = width;
            _windowHeight = height;
            _font = font;
        }
        public void DrawUserInterface(object sender, PaintEventArgs e)
        {
            TextRenderer.DrawText(e.Graphics, _gameState.Message, _font,
                new Rectangle(60, _windowHeight / 2, 400, _windowHeight / 2 - 80), Color.White, TextFormatFlags.Top | TextFormatFlags.EndEllipsis);

            // hp line params
            var hpBar = new Rectangle(_windowWidth * 7 / 10, _windowHeight / 20, 160, 40);
            var healthState = _gameState.PlayerCharacter.Health / (float)_gameState.PlayerCharacter.MaxHealth;

            // draw hp line
            e.Graphics.FillRectangle(new SolidBrush(Color.LimeGreen), new Rectangle(_windowWidth * 7 / 10, _windowHeight / 20, (int)(160 * healthState), 40));
            e.Graphics.DrawRectangle(new Pen(Color.Black, 6), hpBar);
            TextRenderer.DrawText(e.Graphics, ((int)(healthState * 100)).ToString() + "%", _font, 
                hpBar, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
           
            // draw player level
            var level = _gameState.PlayerCharacter.Level;
            TextRenderer.DrawText(e.Graphics, "level " + level.ToString(), _font,
                new Rectangle(_windowWidth * 6 / 10, _windowHeight / 20, 60, 40),
                Color.Gold, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            // draw current experience
            var exp = _gameState.PlayerCharacter.Experience;
            TextRenderer.DrawText(e.Graphics, exp.ToString() + " exp", _font,
                new Rectangle(_windowWidth * 8 / 10, _windowHeight / 20 + 60, 100, 30), 
                Color.Gold, TextFormatFlags.Left | TextFormatFlags.Top);

            // draw player stats
            var stats = "Hp: " + _gameState.PlayerCharacter.Health.ToString() + 
                        "/" + _gameState.PlayerCharacter.MaxHealth.ToString() +
                        "\nArmor: " + _gameState.PlayerCharacter.Armor.ToString() + 
                        "\nDamage: " + _gameState.PlayerCharacter.Damage.ToString();
            TextRenderer.DrawText(e.Graphics, stats, _font,
                new Rectangle(_windowWidth * 8 / 10, _windowHeight / 20 + 90, 100, 300),
                Color.White, TextFormatFlags.Left | TextFormatFlags.Top);


        }
    }
}
