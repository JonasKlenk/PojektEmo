using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    interface IControllableDevice
    {
        int Id { get; set; }
        string Name { get; set; }
        void performAction(Command action); // Führt Command entsprechend aus
        void enterFallbackMode(); //Notfallmethode: Bringt Gerät in sicheren Zustand

        //Administrativ:
        DeviceCategory getType();
        
        //IControllableDevice getInstance();
        string[] getActions(); //gibt Liste mit den ausführbaren Aktionen zurück
        bool isReady(); //überprüft die Einsatzfähigkeit des Geräte (checkliste)
        void setActive(); //aktiviert ein Gerät zum Fortbewegen
        void setDeactive(); //deaktiviert ein Gerät zum Fortbewegen
    }
}
