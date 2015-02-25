namespace warlight2_starterbot_csharp.Bot
{
    using System;
    using System.Collections.Generic;

    using Map;
    using Move;

    /// <summary>
    /// This is a simple bot that does random (but correct) moves.
    /// This class implements the Bot interface and overrides its Move methods.
    /// You can implement these methods yourself very easily now,
    /// since you can retrieve all information about the match from variable “state”.
    /// When the bot decided on the move to make, it returns an List of Moves. 
    /// The bot is started by creating a Parser to which you add
    /// a new instance of your bot, and then the parser is started.
    /// </summary>
    public class BotStarter : IBot
    {
        /// <summary>
        /// A method that returns which region the bot would like to start on, the pickable regions are stored in the BotState.
        /// The bots are asked in turn (ABBAABBAAB) where they would like to start and return a single region each time they are asked.
        /// </summary>
        /// <param name="state">Current BotState.</param>
        /// <param name="timeOut">Time limit for this operation, in milliseconds.</param>
        /// <returns>This method returns one random region from the given pickable regions.</returns>
	    public Region GetStartingRegion(BotState state, long timeOut)
	    {
		    var rand = new Random().NextDouble();
		    var r = (int) (rand*state.PickableStartingRegions.Count);
            var regionId = state.PickableStartingRegions[r].Id;
		    var startingRegion = state.FullMap.GetRegion(regionId);
		
		    return startingRegion;
	    }

        /// <summary>
        /// This method is called for at first part of each round. This example puts two armies on random regions
        /// until he has no more armies left to place.
        /// </summary>
        /// <param name="state">Current BotState.</param>
        /// <param name="timeOut">Time limit for this operation, in milliseconds.</param>
        /// <returns>The list of PlaceArmiesMoves for one round.</returns>
	    public List<PlaceArmiesMove> GetPlaceArmiesMoves(BotState state, long timeOut) 
	    {
		    var placeArmiesMoves = new List<PlaceArmiesMove>();
		    var myName = state.MyName;
		    var armies = 2;
		    var armiesLeft = state.StartingArmies;
		    var visibleRegions = state.VisibleMap.Regions;
		
		    while(armiesLeft > 0)
		    {
		        var rand = new Random().NextDouble();
			    var r = (int) (rand*visibleRegions.Count);
			    var region = visibleRegions[r];
			
			    if(region.OwnedByPlayer(myName))
			    {
				    placeArmiesMoves.Add(new PlaceArmiesMove(myName, region, armies));
				    armiesLeft -= armies;
			    }
		    }

		    return placeArmiesMoves;
	    }

        /// <summary>
        /// This method is called for at the second part of each round. This example attacks if a region has
        /// more than 6 armies on it, and transfers if it has less than 6 and a neighboring owned region.
        /// </summary>
        /// <param name="state">Current BotState.</param>
        /// <param name="timeOut">Time limit for this operation, in milliseconds.</param>
        /// <returns>The list of PlaceArmiesMoves for one round.</returns>
	    public List<AttackTransferMove> GetAttackTransferMoves(BotState state, long timeOut) 
	    {
		    var attackTransferMoves = new List<AttackTransferMove>();
		    var myName = state.MyName;
		    var armies = 5;
		    var maxTransfers = 10;
		    var transfers = 0;
		
		    foreach (var fromRegion in state.VisibleMap.Regions)
		    {
			    if(fromRegion.OwnedByPlayer(myName)) //do an attack
			    {
				    var possibleToRegions = new List<Region>();
				    possibleToRegions.AddRange(fromRegion.Neighbors);
				
				    while(possibleToRegions.Count != 0)
				    {
				        var rand = new Random().NextDouble();
					    var r = (int) (rand*possibleToRegions.Count);
					    var toRegion = possibleToRegions[r];
					
					    if (!toRegion.PlayerName.Equals(myName) && fromRegion.Armies > 6) //do an attack
					    {
						    attackTransferMoves.Add(new AttackTransferMove(myName, fromRegion, toRegion, armies));
						    break;
					    }
					    else if (toRegion.PlayerName.Equals(myName) && fromRegion.Armies > 1 && transfers < maxTransfers) //do a transfer
					    {
					        attackTransferMoves.Add(new AttackTransferMove(myName, fromRegion, toRegion, armies));
					        transfers++;
					        break;
					    }
					    else
					    {
					        possibleToRegions.Remove(toRegion);
					    }
				    }
			    }
		    }
		
		    return attackTransferMoves;
	    }

        /// <summary>
        /// Main entry point.
        /// </summary>
        /// <param name="args">Not used.</param>
	    public static void Main(string[] args)
	    {
		    var parser = new BotParser(new BotStarter());
		    parser.Run();
	    }
    }
}
