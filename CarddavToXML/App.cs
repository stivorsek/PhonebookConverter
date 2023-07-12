using CarddavToXML.UI;

namespace CarddavToXML
{
    internal class App : IApp
    {
        private readonly IChoise _firstUI;

        public  App(IChoise firstUI)
        {
            _firstUI = firstUI;
        }
        public void Run()
        {
            _firstUI.FirstUIChoise();
        }
    }
}
