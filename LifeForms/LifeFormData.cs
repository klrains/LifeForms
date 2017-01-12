using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace LifeForms
{
    internal static class LifeFormData
    {
        private static XDocument DataDoc;
        internal static void LoadXMLDoc()
        {
            //Gets XML data into memory
            DataDoc = new XDocument();
            DataDoc = XDocument.Load(@"Data.xml");
        }

        internal static void AddLifeform(LifeForm lifeform)
        {
            //Writes new lifeform data to xml document
            XElement root = DataDoc.Element("lifeforms");
            XElement newlifeform = new XElement("lifeform");

            lifeform.Properties.ToList().ForEach(p => newlifeform.Add(new XElement(p.Key, p.Value)));
            root.Add(newlifeform);

            DataDoc.Save(@"Data.xml");
        }

        internal static List<LifeForm> SearchData(string searchValue)
        {
            //Search all lifeforms with properties that have a specific value and returns all of the results
            return (from lf in LoadLifeForms()
                    where lf.Properties.Values.Any(val => val.ToUpper().Contains(searchValue.ToUpper()))
                    select lf).ToList();
        }

        private static List<LifeForm> LoadLifeForms()
        {
            //Gets all lifeform data from xml and returns it in a list
            List<LifeForm> allLifeForms = new List<LifeForm>();
            var allLifeElements = DataDoc.Element("lifeforms").Elements("lifeform").ToList();

            foreach (var e in allLifeElements)
            {
                LifeForm lf = new LifeForm();

                foreach (var p in e.Elements())
                {
                    lf.Properties.Add(p.Name.ToString(), p.Value);  
                }

                allLifeForms.Add(lf);
            }

            return allLifeForms;
        }
    }
}
