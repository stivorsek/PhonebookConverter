using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Translate.V3;
using Google.Api.Gax.ResourceNames;
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
