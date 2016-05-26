using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    interface IController
    {
        string getType();
        int getId();
        bool isReady();
        void setActive();
        void setDeactive();
        bool initialize();
        void setId(int id);
        string[] getCommands();
        event EventHandler<WarningEventArgs> Warning;
        event EventHandler<ErrorEventArgs> Error;
        string Name { get; }
    }
}
