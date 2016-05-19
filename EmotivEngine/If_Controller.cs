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
        bool setActive();
        bool setDeactive();
        bool initialize();
        IController getInstance(CentralControlEngine cce);
        string[] getCommands();
        event EventHandler Warning;
        event EventHandler Error;        
    }
}
