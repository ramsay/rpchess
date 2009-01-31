///<summary>
/// Handles the controller interfaces.
///</summary>
namespace RPChess
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;
    /// <summary>
    /// Interface to keep all future controllers interchangeable.
    /// </summary>
    interface Controller : IObject
    {
    }

    class TextController : Controller
    {
        private uint _count;
        public int Count
        {
            get
            {
                return (int)_count;
            }
        }
        public TextController()
        {
        }
        public TextController(bool init)
        {
            if ( init )
            {
                Initialize();
            }
        }
        public void Initialize()
        {
			_count = 0;
        }
        public XmlDocument ToXmlDocument()
        {
            return new XmlDocument();
        }
        public IObject FromXmlDocument(XmlDocument xml)
        {
            return (IObject)new TextController();
        }
    }
}
