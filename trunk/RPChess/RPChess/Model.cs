using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace RPChess
{
    enum MoveDirection { Right, FowardRight, Forward, ForwardLeft, 
            Left, BackwardLeft, Backward, BackwardRight };
    
    struct BoardLocation 
    {
        public int X;
        public int Y;
    }

    class Model
    {
        private Board _board;
        public Model()
        {
            initialize();
        }

        protected void initialize()
        {
        }
    }

    class Board
    {
        public Piece[] TeamWhite
        {
            get
            {
                return TeamWhite;
            }
        }

        public Piece[] TeamBlack
        {
            get
            {
                return TeamBlack;
            }
        }
        public readonly int Width;
        public readonly int Length;

        public Board()
        {
            initialize();
        }

        protected void initialize()
        {
        }
    }

    class Piece
    {
        public readonly int MAX_HP;
        public readonly Ability[] abilityList;
        public readonly String name;
        public readonly Move[] moveSet;
        public int HP
        {
            get
            {
                return HP;
            }
        }

        public Piece ( String name, int HP, Ability[] aList,
            Move[] mSet )
        {
            this.name = name;
            MAX_HP = HP;
            abilityList = aList;
            moveSet = mSet;
        }

        public int takeDamage( uint damage )
        {
            _HP = _HP - damage;
            return _HP;
        }

        public int healHP(uint heal)
        {
            _HP = _HP + heal;
            return _HP;
        }
    }

    class Ability
    {
        public readonly int[][] AreaOfEffect;
        public readonly String Name;
        public readonly int MAX_POINTS;
        public int Points
        {
            get
            {
                return Points;
            }
        }

        /// <summary>
        /// Constructor for Ability, sets up the varibles and sets the
        /// useability points to MAX_POINTS.
        /// </summary>
        /// <param name="name" type="String">
        /// The name to give the ability.
        /// </param>
        /// <param name="AreaOfEffect" type="int[][]">
        /// An array of boundless size that is a value map of the 
        /// damage or healing the ability creates.
        /// </param>
        /// <param name="points" type="int">
        /// The amount of points.
        /// </param>
        public Ability(String name, int[][] areaOfEffect, int points)
        {
            Name = name;
            AreaOfEffect = areaOfEffect;
            MAX_POINTS = points;
            initialize();
        }

        /// <summary>
        /// Resets the points equal to MAX_POINTS.
        /// </summary>
        public void initialize()
        {
            points = MAX_POINTS;
        }

        /// <summary>
        /// Use the ability, decreases the Ability's points by 1.
        /// </summary>
        /// <returns>Returns the remaining points.</returns>
        public int use()
        {
            _points = _points - 1;
            return _points;
        }
    }


    /// <summary>
    /// Class level summary documentation goes here.</summary>
    /// <remarks>
    /// Longer comments can be associated with a type or member 
    /// through the remarks tag</remarks>
    class Move
    {
        private readonly MoveDirection Direction;
        private readonly int Length;
        public readonly int Forward;
        public readonly int Right;

        public Move(int forward, int right)
        {
            Forward = forward;
            Right = right;
            Direction = null;
            Length = null;
        }

        public Move(MoveDirection direction, int length)
        {
            Direction = direction;
            Length = lenght;
            int forward = new int(Math.Sin(new double(Direction / 4)));
            int right = new int(Math.Cos(new double(Direction / 4)));

            if (length = -1)
            {
                right *= Int32.MaxValue;
                forward *= Int32.MaxValue;
            }
            else
            {
                right *= Length;
                left *= Length;
            }

            Forward = forward;
            Right = right;
        }

        //public Move(XmlReader xml)
        //{
        //    if (xml.LocalName.ToLower().Equals("move"))
        //    {
        //        Direction = xml.GetAttribute("Direction");
        //        Length = xml.GetAttribute("Length");
        //        Forward = xml.GetAttribute("Forward");
        //        Right = xml.GetAttribute("Right");
        //    }
        //}

        public BoardLocation moveFrom(BoardLocation bLoc)
        {
            bLoc.X += Right;
            bLoc.Y += Forward;
            return bLoc;
        }
    }
}