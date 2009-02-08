//-----------------------------------------------------------------------
// <copyright file="Special.cs" company="BENTwerx">
//     GPLv3 Copyright 2008 Robert Ramsay
// </copyright>
// <author>Robert Ramsay</author>
//-----------------------------------------------------------------------

namespace RPChess
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Xml;

    public class Special
    {
    }

    /// <summary>
    /// A base class for attacks.
    /// <Implements>Move</Implements>
    /// </summary>
    public class Attack : IMove
    {
        /// <summary>
        /// The name of the attack, user customizable.
        /// Inheritable, protected, string.
        /// </summary>
        private string name;

        /// <summary>
        /// The internal storage of MaxPoints.
        /// Inheritable, protected, int.
        /// </summary>
        private int pointsMax;

        /// <summary>
        /// Internal representation for Points.
        /// Inheritable, protected, int.
        /// </summary>
        private int points;

        /// <summary>
        /// Initializes a new instance of the Attack class.
        /// Default constructor, Initializes members to zero.
        /// </summary>
        public Attack()
        {
            this.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the Attack class.
        /// This is probably not very useful besides testing the members in a 
        /// unit outside all the derived classes.
        /// </summary>
        /// <param name="name">Aesthetic Identifier</param>
        /// <param name="points">Number of times Attack may be "used"</param>
        public Attack(string name, int points)
        {
            this.name = name;
            this.pointsMax = points;
            this.Reset();
        }

        /// <summary>
        /// Gets the Name of the attack for aesthetic purposes.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets the Maximum amount of ability points this attack will be 
        /// Initialized to.
        /// </summary>
        public int MaxPoints
        {
            get
            {
                return this.pointsMax;
            }
        }

        /// <summary>
        /// Gets the current amount of ability points that the Attack has left.
        /// </summary>
        public int Points
        {
            get
            {
                return this.points;
            }
        }

        /// <summary>
        /// Gets the MoveType,
        /// Identifies this as a Move of type Attack.
        /// </summary>
        public MoveType Type
        {
            get
            {
                return MoveType.Attack;
            }
        }

        /// <summary>
        /// Use the ability, decreases the Ability's points by 1.
        /// </summary>
        /// <returns>Returns the remaining points.</returns>
        public int Use()
        {
            this.points--;
            if (this.points < 0)
            {
                this.points = 0;
            }

            return this.points;
        }

        /// <summary>
        /// Use the ability, decreases the Ability's points by 1.
        /// </summary>
        /// <param name="uses">The number of simultaneous uses.</param>
        /// <returns>Returns the remaining points.</returns>
        public int Use(int uses)
        {
            this.points -= uses;
            if (this.points < 0)
            {
                this.points = 0;
            }

            return this.points;
        }

        /// <summary>
        /// Resets the points to MaxPoints.
        /// </summary>
        /// <returns>Returns the remaining points.</returns>
        public int Reset()
        {
            this.points = this.pointsMax;
            return this.points;
        }

        /// <summary>
        /// Resets member data to zero state, usually 
        /// brings Point back to MaxPoints, etc.
        /// </summary>
        public virtual void Initialize()
        {
            this.name = string.Empty;
            this.pointsMax = 0;
        }

        /// <summary>
        /// Formats member data into an XML document.
        /// Required by all inheriting Classes. Allows for a simple
        /// and loopable Constructor method.  Also eases loading/saving
        /// from file.
        /// </summary>
        /// <returns>
        /// An xml document containing all of the member data of an attack.
        /// </returns>
        public virtual XmlDocument ToXmlDocument()
        {
            XmlDocument xml = new XmlDocument();
            return xml;
        }

        /// <summary>
        /// Loads member data from an xml document.
        /// Required by all inheriting Classes. Allows for a simple
        /// and loopable Constructor method.  Also eases loading/saving
        /// from file.
        /// </summary>
        /// <param name="xml">
        /// An xml document containing all of the member data of an attack.
        /// </param>
        /// <returns>An Attack Type-Casted as an IRPChessObject.</returns>
        public virtual IRPChessObject FromXmlDocument(XmlDocument xml)
        {
            this.Initialize();
            if (xml.FirstChild.Name == "Attack")
            {
                // TODO
            }
            else
            {
                Console.Error.WriteLine("xmlDocument is not an Attack");
            }

            return (IRPChessObject)new Attack();
        }
    }

    /// <summary>
    /// An area of effect attack for wide spread multispace attacks.
    /// <Implements>Move</Implements>
    /// <Implements>Attack</Implements>
    /// </summary>
    public class AreaOfEffectAbility : Attack
    {
        /// <summary>
        /// The 2d integer array that represents the AreaOfEffect Shape.
        /// </summary>
        private int[,] areaOfEffect;
        public ReadOnlyCollection<int> AreaOfEffect;

        /// <summary>
        /// Initializes a new instance of the AreaOfEffectAbility class.
        /// </summary>
        /// <param name="name">Aesthetic identifier.</param>
        /// <param name="points">
        /// How many times the ability may be used.
        /// </param>
        /// <param name="areaOfEffect">
        /// An array of integers expressing the size and shape of the ability.
        /// </param>
        public AreaOfEffectAbility(string name, int points, int[,] areaOfEffect)
            : base(name, points)
        {
            this.areaOfEffect = areaOfEffect;
            this.AreaOfEffect = new ReadOnlyCollection<int>(this.AreaOfEffect);
        }

        /// <summary>
        /// Resets memeber data to un-used state, affects points.
        /// </summary>
        public override void Initialize()
        {
            Reset();
        }

        /// <summary>
        /// Useful for saving data to file.
        /// </summary>
        /// <returns>
        /// An xml document containing AreaOfEffectAbility memeber data.
        /// </returns>
        public override XmlDocument ToXmlDocument()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(string.Empty); // TODO
            return xml;
        }

        /// <summary>
        /// Loads the AreaOfEffectAbility from an XML document
        /// </summary>
        /// <param name="xml">
        /// An XML document containing AreaOfEffectAbility member data.
        /// </param>
        /// <returns>An AreaOfEffectAbility Type-Casted as an IRPChessObject</returns>
        public override IRPChessObject FromXmlDocument(XmlDocument xml)
        {
            this.Initialize();
            if (xml.FirstChild.Name == "AreaOfEffectAbility")
            {
                // TODO
            }
            else
            {
                Console.Error.WriteLine("xmlDocument is not an AreaOfEffectAbility");
            }

            return (IRPChessObject)new Attack();
        }
    }

    /// <summary>
    /// Directional attack for ranged attacks.
    /// <Implements>Move</Implements>
    /// <Implements>Attack</Implements>
    /// </summary>
    public class DirectionalAbility : Attack
    {
        /// <summary>
        /// The direction of the attack.
        /// </summary>
        private BoardVector vector;

        /// <summary>
        /// The Damage factor of the ability.
        /// </summary>
        private int damage;

        ////private bool _ranged;

        ////private bool _stopable;

        /// <summary>
        /// Initializes a new instance of the DirectionalAbility class.
        /// </summary>
        /// <param name="name">Fancy name of the DirectionalAbility.</param>
        /// <param name="points">The Ability Poins the ability costs.</param>
        /// <param name="vector">The direction of the DirectionalAbility</param>
        /// <param name="damage">The damage factor of the attack.</param>
        public DirectionalAbility(
            string name,
            int points,
            BoardVector vector,
            int damage)
            : base(name, points)
        {
            this.vector = vector;
            this.damage = damage;
        }

        /// <summary>
        /// Initializes a new instance of the DirectionalAbility class from an
        /// xml document.
        /// </summary>
        /// <param name="xml">
        /// An XmlDocument containing DirectionAbility member data.
        /// </param>
        public DirectionalAbility(XmlDocument xml)
        {
            this.FromXmlDocument(xml);
        }

        /// <summary>
        /// Gets the Direction in which the Ability acts.
        /// </summary>
        public BoardVector Vector
        {
            get
            {
                return this.vector;
            }
        }

        /// <summary>
        /// Gets the amount of damage this Ability gives to the target.
        /// </summary>
        public int Damage
        {
            get
            {
                return this.damage;
            }
        }

        /// <summary>
        /// Re-Initializes the DirectionalAbility to a fresh
        /// un-used state.
        /// </summary>
        public override void Initialize()
        {
            Reset();
        }

        /// <summary>
        /// Calls ToXMLstring() inorder to form a more perfect Union.
        /// </summary>
        /// <returns>
        /// An XmlDocument containing DirectionAbility member data.
        /// </returns>
        public override XmlDocument ToXmlDocument()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(this.ToXMLstring());
            return xml;
        }

        /// <summary>
        /// Loads the member data from an xml document.
        /// </summary>
        /// <param name="xml">
        /// Xml document containing correctly formatted Directional Ability
        ///  member data.
        /// </param>
        /// <returns>A Directional Ability Type-Casted as an IRPChessObject</returns>
        public override IRPChessObject FromXmlDocument(XmlDocument xml)
        {
            return (IRPChessObject)new Attack();
        }

        /// <summary>
        /// Forms an Xml Snippet representing this attack.
        /// </summary>
        /// <returns>string that uses xml syntax.</returns>
        public string ToXMLstring()
        {
            string repr = "<attack name=\"" + Name +
            "\" direction=\"" + this.vector.Direction +
            "\" length=\"" + this.vector.Length +
            "\" damage=\"" + this.damage +
            "\"/>";
            return repr;
        }
    }

}
