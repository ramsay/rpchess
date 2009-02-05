namespace RPChess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;

    public class Army : IRPChessObject
    {
        private string name;
        private uint initiative;
        private Piece[] ranks;
        private uint wealth;
        private string description;

        public Army()
        {
            this.name = null;
            this.initiative = 0;
            this.ranks = null;
            this.wealth = 0;
            this.description = null;
        }

        public Army( 
            string name,
            uint initiative,
            Piece[] Ranks,
            uint wealth,
            string description)
        {
            this.name = name;
            this.initiative = initiative;
            this.ranks = Ranks;
            this.wealth = wealth;
            this.description = description;
        }

        public Army(XmlDocument xmldoc)
        {
            this.name = null;
            this.initiative = 0;
            this.ranks = null;
            this.wealth = 0;
            this.description = null;
        }

        public void Initialize()
        {
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public uint Initiative
        {
            get
            {
                return this.initiative;
            }
        }

        public Piece[] Ranks
        {
            get
            {
                return this.ranks;
            }
        }

        public uint Wealth
        {
            get
            {
                return this.wealth;
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
        }


        public uint SumMove()
        {
            uint movesum = 0;
            foreach (Piece p in Ranks)
            {
                movesum += p.Move;
            }
            return movesum;
        }

        public int SumSave()
        {
            int savesum = 0;
            foreach (Piece p in Ranks)
            {
                savesum += p.Save;
            }
            return savesum;
        }

        public int SumMelee()
        {
            int meleesum = 0;
            foreach (Piece p in Ranks)
            {
                meleesum += p.Melee;
            }
            return meleesum;
        }

        public XmlDocument ToXmlDocument()
        {
            XmlDocument xmldoc = new XmlDocument();
            StringBuilder repr = new StringBuilder();
            repr.AppendLine("<army name=\"" + name + "\">");
            repr.AppendLine("\t<initiative>" + this.initiative + "<\\initiative>");
            repr.AppendLine("\t<wealth>" + this.wealth + "<\\wealth>");
            repr.AppendLine("\t<description>" + this.description + "<\\description>");
            repr.AppendLine("\t<ranks>");
            foreach (Piece p in Ranks)
            {
                repr.AppendLine(p.ToXmlDocument().FirstChild.InnerXml);
            }
            repr.AppendLine("\t<\\ranks>");
            repr.Append("<\\army>");
            xmldoc.InnerXml = repr.ToString();
            return xmldoc;
        }

        public IRPChessObject FromXmlDocument(XmlDocument xmldoc)
        {
            return (IRPChessObject)new Army();
        }
    }
}
