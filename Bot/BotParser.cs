namespace warlight2_starterbot_csharp.Bot
{
    using System;

    using Map;

    /// <summary>
    /// Used to read input for the bot.
    /// </summary>
    public class BotParser
    {
	    private readonly Bot _bot;

        private readonly BotState _currentState;
	
	    public BotParser(Bot bot)
	    {
		    _bot = bot;
		    _currentState = new BotState();
	    }
	
        /// <summary>
        /// Read expected incoming information.
        /// </summary>
	    public void Run()
	    {
	        string line;

            while ((line = Console.ReadLine()) != null)
		    {
		        if (line.Length == 0)
		        {
		            continue;
		        }

			    var parts = line.Split(' ');

			    if(parts[0].Equals("pick_starting_region")) //pick which regions you want to start with
			    {
				    _currentState.SetPickableStartingRegions(parts);
				    Region startingRegion = _bot.GetStartingRegion(_currentState, long.Parse(parts[1]));
				
				    Console.Out.WriteLine(startingRegion.Id);
			    }
			    else if(parts.Length == 3 && parts[0].Equals("go")) 
			    {
				    //we need to do a move
				    var output = string.Empty;

				    if(parts[1].Equals("place_armies")) 
				    {
					    //place armies
					    var placeArmiesMoves = _bot.GetPlaceArmiesMoves(_currentState, long.Parse(parts[2]));
					    
                        foreach (var move in placeArmiesMoves)
				        {
				            output += move.ToString() + ",";
				        }
				    } 
				    else if(parts[1].Equals("attack/transfer")) 
				    {
					    //attack/transfer
					    var attackTransferMoves = _bot.GetAttackTransferMoves(_currentState, long.Parse(parts[2]));
					    
                        foreach (var move in attackTransferMoves)
				        {
				            output += move.ToString() + ",";
				        }
				    }
			        if (output.Length > 0)
			        {
			            Console.Out.WriteLine(output);
			        }
			        else
			        {
			            Console.Out.WriteLine("No moves");
			        }
			    } 
                else if(parts[0].Equals("settings")) 
                {
				    //update settings
				    _currentState.UpdateSettings(parts[1], parts);
			    } 
                else if(parts[0].Equals("setup_map")) 
                {
				    //initial full map is given
				    _currentState.SetupMap(parts);
			    } 
                else if(parts[0].Equals("update_map")) 
                {
				    //all visible regions are given
				    _currentState.UpdateMap(parts);
			    } 
                else if(parts[0].Equals("opponent_moves")) 
                {
				    //all visible opponent moves are given
				    _currentState.ReadOpponentMoves(parts);
			    } 
                else 
                {
				    Console.Error.WriteLine("Unable to parse line \"{0}\"", line);
			    }
		    }
	    }
    }
}
