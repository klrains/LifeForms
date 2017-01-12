using System.Collections.Generic;

namespace LifeForms
{
    internal class LifeForm
    {
        internal Dictionary<string, string> Properties { get; set; }

        internal LifeForm()
        {
            //Base LifeForm class
            Properties = new Dictionary<string, string>();
        }
    }
}
