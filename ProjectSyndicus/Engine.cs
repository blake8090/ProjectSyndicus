using ProjectSyndicus.Screens;
using SFML.Graphics;
using SFML.Window;

namespace ProjectSyndicus
{
    class Engine
    {
        private bool running = false;
        private RenderWindow window;

        private Screen currentScreen;

        public Screen CurrentScreen
        {
            get => currentScreen;
            set
            {
                currentScreen.Stop();
                currentScreen = value;
            }
        }

        public void Start()
        {
            // TODO: load config

            // TODO: split window handling into separate class?
            var mode = new VideoMode(800, 600);
            window = new RenderWindow(mode, "Project Syndicus");

            window.KeyPressed += OnKeyPressed;
            window.Closed += (sender, e) => Stop();

            Run();
        }

        public void Stop()
        {
            running = false;
        }

        private void Run()
        {
            running = true;

            while (running)
            {
                window.DispatchEvents();

                if (currentScreen != null)
                {
                    currentScreen.Update();
                    currentScreen.Input();
                    currentScreen.Render();
                }

                window.Display();
            }

            window.Close();
        }

        private void OnKeyPressed(object sender, KeyEventArgs keyEvent)
        {
            if (keyEvent.Code == Keyboard.Key.Escape)
            {
                Stop();
            }
        }
    }
}
