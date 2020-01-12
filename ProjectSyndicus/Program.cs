using System;

namespace ProjectSyndicus
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new SyndicusGame())
                game.Run();
        }
    }
}
