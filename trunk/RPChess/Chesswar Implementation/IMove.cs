//-----------------------------------------------------------------------
// <copyright file="IMove.cs" company="BENTwerx">
//     GPLv3 Copyright 2008 Robert Ramsay
// </copyright>
// <author>Robert Ramsay</author>
//-----------------------------------------------------------------------

namespace RPChess
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// This enum matches words to cardinal directions (Forward, ForwardRight, 
    /// BackwardLeft, ...). This adheres to the standards of trigonometry:
    /// Each direction is the radian angle * 4/PI
    /// </summary>
    public enum MoveDirection
    {
        Right,
        FowardRight,
        Forward,
        ForwardLeft,
        Left,
        BackwardLeft,
        Backward,
        BackwardRight
    }

    /// <summary>
    /// Enum for determining which type of move the class is implementing.
    /// </summary>
    public enum MoveType
    {
        /// <summary>
        /// Capture is the standard chess move.
        /// </summary>
        Capture,

        /// <summary>
        /// Movement handles a pieces movement across the board.
        /// </summary>
        Movement,

        /// <summary>
        /// Attack has no movement only deals/heals damage to pieces.
        /// </summary>
        Attack
    }
    
    /// <summary>
    /// An interface for the different actions a Piece can do.
    /// </summary>
    public interface IMove : IRPChessObject
    {
        /// <summary>
        /// Gets the Type property of a Move
        /// </summary>
        /// <value>The type of move this IRPChessObject is an instance of.</value>
        MoveType Type
        {
            get;
        }
    }
}
