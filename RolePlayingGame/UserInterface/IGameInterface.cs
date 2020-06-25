using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace RolePlayingGame.UserInterface
{
    public interface IGameInterface
    {
        void DrawUserInterface(object sender, PaintEventArgs e);
    }
}
