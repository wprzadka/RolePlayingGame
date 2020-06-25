using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using RolePlayingGame.Engine;

namespace RolePlayingGame.UserInterface
{
    class GameInterface : IGameInterface
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
                new Rectangle(60, _windowHeight / 2, 400, 400), Color.White, TextFormatFlags.Top | TextFormatFlags.EndEllipsis);

            var hpBar = new Rectangle(_windowWidth * 7 / 10, _windowHeight / 20, 160, 40);
            float healthState = _gameState.PlayerCharacter.Health / (float)_gameState.PlayerCharacter.MaxHealth;
            e.Graphics.FillRectangle(new SolidBrush(Color.LimeGreen), new Rectangle(_windowWidth * 7 / 10, _windowHeight / 20, (int)(160 * healthState), 40));
            e.Graphics.DrawRectangle(new Pen(Color.Black, 6), hpBar);
            TextRenderer.DrawText(e.Graphics, ((int)(healthState * 100)).ToString() + "%", _font, hpBar, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }
}
