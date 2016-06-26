using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotivEngine
{
    interface IController
    {
        /// <summary>
        /// Determines whether this instance is ready. And goes throught a "Checklist"
        /// </summary>
        /// <returns></returns>
        bool isReady();
        /// <summary>
        /// Sets the active. And "Starts the engines".
        /// </summary>
        void setActive();
        /// <summary>
        /// Sets the deactive. And "Stops the engines".
        /// </summary>
        void setDeactive();
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns></returns>
        bool initialize();

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }
        /// <summary>
        /// Gets the type of the Controller.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        string Type { get; }
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        int ID { get; set; }
        /// <summary>
        /// Gets the commands, provided and supported of the Controller.
        /// </summary>
        /// <returns></returns>
        string[] getCommands();

        /// <summary>
        /// Occurs when [warning]. Minor Problems.
        /// </summary>
        event EventHandler<WarningEventArgs> Warning;
        /// <summary>
        /// Occurs when [error]. Seriouse Problems.
        /// </summary>
        event EventHandler<ErrorEventArgs> Error;      
    }
}
