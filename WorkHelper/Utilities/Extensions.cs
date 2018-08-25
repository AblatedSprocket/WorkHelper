using System.IO;
using System.Windows.Markup;
using System.Xml;
using WorkHelper.Controls;

namespace WorkHelper.Utilities
{
    public static class Extensions
    {
        public static WorkItemControl Clone(this WorkItemControl item)
        {
            string itemXaml = XamlWriter.Save(item);
            StringReader reader = new StringReader(itemXaml);
            XmlReader xmlReader = XmlReader.Create(reader);
            return (WorkItemControl)XamlReader.Load(xmlReader);
        }
    }
}
