using Nett;
using Serilog;

namespace ProjectSyndicus
{
    public class Config
    {
        public int ScreenWidth { get; private set; } = 800;
        public int ScreenHeight { get; private set; } = 600;
        public bool Fullscreen { get; private set; } = false;
    }
}
