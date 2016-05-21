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
        DeviceKategory getType();

        int getId();
        void setId(int id);
        //IControllableDevice getInstance();
        string[] getActions(); //gibt Liste mit den ausführbaren Aktionen zurück
        bool isReady(); //überprüft die Einsatzfähigkeit des Geräte (checkliste)
        bool setActive(); //aktiviert ein Gerät zum Fortbewegen
        bool setDeactive(); //deaktiviert ein Gerät zum Fortbewegen
    }
}
