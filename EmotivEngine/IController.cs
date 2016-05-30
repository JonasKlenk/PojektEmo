using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    interface IController
    {
        bool isReady();
        void setActive();
        void setDeactive();
        bool initialize();

        string Name { get; }
        string Type { get; }
        int ID { get; set; }
        string[] getCommands();

        event EventHandler<WarningEventArgs> Warning;
        event EventHandler<ErrorEventArgs> Error;      
    }
}
