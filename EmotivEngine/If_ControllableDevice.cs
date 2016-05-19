using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    interface IControllableDevice
    {
        void performAction(Command action); // Führt Command entsprechend aus
        void enterFallbackMode(); //Notfallmethode: Bringt Gerät in sicheren Zustand



        //Administrativ:
        void initialize();
        string getType();
        IControllableDevice getInstance();
        string[] getActionTypes(); //gibt Liste mit den ausführbaren Aktionen zurück
        bool isReady(); //überprüft die Einsatzfähigkeit des Geräte (checkliste)
        bool setactive(); //aktiviert ein Gerät zum Fortbewegen
        bool setdeactive(); //deaktiviert ein Gerät zum Fortbewegen
    }
}
