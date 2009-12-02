
using System;

namespace chesswartest
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;
    using NUnit.Framework;
	
	[TestFixture]
	public class BattleTest
	{
		
		public BattleTest()
		{
		}
		
        [TestFixtureSetUp]
        public void BattleTestSetup()
        {
        }

        [TestFixtureTearDown]
        public void BattleTestTeardown()
        {
        }

		/*
		 * Test a battle between two players with only a King and a pawn per side.
		 */
        [Test]
        public void MeleeOnlyNoAIBattleTest()
        {
			//Choose race
				//Player 1 Humans
				//Player 2 Orcs
			//Choose army
				//Player 1
					//General
					//1 Infantry
				//Player 2
					//Warlord
					//1 Orc
			//Deploy army
				//Player 1
					//General to a8
					//Infantry:1 to a7
				//Player 2
					//Warlord to h1
					//Orc:1 to h2
			//Get initial player
				//Assert (active_player == Player 1)
			//Play game until a player wins.
				//Player 1 - Turn
					//Movement Phase
						//Move Infantry:1 to g1
							//Infantry:1 CHARGE h1
						//Move General to g2
							//General CHARGE h1
					//Shooting Phase (do nothing)
					//Melee Phase
						//Inftantry:1 D6.Roll()+0-1:
							// >5 Warlord Destoyed
							// 5 Warlord D6.Roll()>2
							// 4 Warlord D6.Roll()>2
							// 2,3 Nothing
							// <2 Infantry:1 D6.Roll()>4
						//General D6.Roll()+1-1:
							// >5 Warlord Destoyed
							// 5 Warlord D6.Roll()>2
							// 4 Warlord D6.Roll()>2
							// 2,3 Nothing
							// <2 Infantry:1 D6.Roll()>2
				//Player 2 - Turn
					//Movement Phase (do nothing)
					//Shooting Phase (do nothing)
					//Melee Phase	 (do nothing)
        }
	}
}
