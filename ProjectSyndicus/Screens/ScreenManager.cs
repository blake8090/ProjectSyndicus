namespace ProjectSyndicus.Screens
{
    class ScreenManager
    {
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
    }
}
