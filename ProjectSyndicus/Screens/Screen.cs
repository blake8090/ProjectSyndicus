using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectSyndicus.Screens
{
    abstract class Screen
    {
        public abstract void Start();
        public abstract void Stop();
        public abstract void Update();
        public abstract void Input();
        public abstract void Render();
    }
}
