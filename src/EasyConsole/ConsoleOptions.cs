namespace EasyConsole
{
    public class ConsoleOptions
    {
        public ConsoleOptions()
        {
            ClearScreenBeforeRun = false;
            ClearScreenBeforeShowingMenu = true;
        }

        public bool ClearScreenBeforeRun { get; set; } 
        public bool ClearScreenBeforeShowingMenu { get; set; } 
    }
}