using System;
using System.Collections.Generic;
using System.Text;

namespace chesswar
{
    class Battle
    {
        private Model world;
        private MoveType phase;
        private int round;
        private int active_player;

        public Battle(Model World)
        {
            world = World;
            phase = MoveType.Movement;
            round = 0;
            active_player = 0;
        }

        public bool Action(int player, Piece piece, IMove move)
        {
            if (player != active_player)
                return false;
            if (move.Type != phase)
                return false;
            // Attempt move
            return false;
        }

        public MoveType Next()
        {
            if (phase == MoveType.Movement)
            {
                phase = MoveType.Melee;
            }
            else if (phase == MoveType.Melee)
            {
                phase = MoveType.Shooting;
            } 
            else
            {
                phase = MoveType.Movement;
                if (active_player == world.PlayerCount - 1)
                {
                    round++;
                }
                else
                {
                    active_player++;
                }
            }

            return phase;
        }

        public MoveType CurrentPhase
        {
            get
            {
                return phase;
            }
        }
    }
}
