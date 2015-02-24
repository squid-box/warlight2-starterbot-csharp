namespace warlight2_starterbot_csharp.Bot
{
    using System;
    using System.Collections.Generic;
    
    using Map;
    using Move;

    public class BotState
    {
        /// <summary>
        /// My name.
        /// </summary>
        public string MyName { get; private set; }
	    
        /// <summary>
        /// Opponents name.
        /// </summary>
        public string OpponentName { get; private set; }
	    
        /// <summary>
        /// This map is known from the start, contains all the regions and how they are connected, doesn't change after initialization.
        /// </summary>
        public Map FullMap { get; private set; }

        /// <summary>
        /// This map represents everything the player can see, updated at the end of each round.
        /// </summary>
        public Map VisibleMap { get; private set; }
	
        /// <summary>
        /// List of regions the player can choose the start from
        /// </summary>
	    public List<Region> PickableStartingRegions;
	    
        /// <summary>
        /// Wastelands, i.e. neutral regions with a larger amount of armies on them. Given before the picking of starting regions
        /// </summary>
        public List<Region> Wastelands;
	
        /// <summary>
        /// List of all the opponent's moves, reset at the end of each round
        /// </summary>
	    public List<Move> OpponentMoves; 

        /// <summary>
        /// Number of armies the player can place on map
        /// </summary>
	    public int StartingArmies;
	    
        /// <summary>
        /// 
        /// </summary>
        public int MaxRounds;
	    
        /// <summary>
        /// 
        /// </summary>
        public int RoundNumber;

        /// <summary>
        /// Total time that can be in the timebank
        /// </summary>
	    public long TotalTimebank;
	    
        /// <summary>
        /// The amount of time that is added to the timebank per requested move.
        /// </summary>
        public long TimePerMove;

        /// <summary>
        /// Create a default BotState.
        /// </summary>
        public BotState()
        {
            OpponentMoves = new List<Move>();
            RoundNumber = 0;

            MyName = string.Empty;
            OpponentName = string.Empty;
            FullMap = new Map();

        }

        /// <summary>
        /// Update a given setting.
        /// </summary>
        /// <param name="key">Setting to change.</param>
        /// <param name="parts">Data to set.</param>
        public void UpdateSettings(string key, string[] parts)
        {
            if (key.Equals("starting_regions") && parts.Length > 3)
            {
                SetPickableStartingRegions(parts);
                return;
            }
            
            var value = parts[2];

            if (key.Equals("your_bot"))
            {
                MyName = value;
            }
            else if (key.Equals("opponent_bot"))
            {
                OpponentName = value;
            }
            else if (key.Equals("max_rounds"))
            {
                MaxRounds = int.Parse(value);
            }
            else if (key.Equals("timebank"))
            {
                TotalTimebank = long.Parse(value);
            }
            else if (key.Equals("time_per_move"))
            {
                TimePerMove = long.Parse(value);
            }
            else if (key.Equals("starting_armies"))
            {
                StartingArmies = int.Parse(value);
                RoundNumber++; //next round
            }
        }

        /// <summary>
        /// Initial map is given to the bot with all the information except for player and armies info.
        /// </summary>
        /// <param name="mapInput"></param>
        public void SetupMap(string[] mapInput)
        {
            int i, regionId, superRegionId, wastelandId, reward;

            if (mapInput[1].Equals("super_regions"))
            {
                for (i = 2; i < mapInput.Length; i++)
                {
                    try
                    {
                        superRegionId = int.Parse(mapInput[i]);
                        i++;
                        reward = int.Parse(mapInput[i]);
                        FullMap.Add(new SuperRegion(superRegionId, reward));
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine("Unable to parse SuperRegions " + e.Message);
                    }
                }
            }
            else if (mapInput[1].Equals("regions"))
            {
                for (i = 2; i < mapInput.Length; i++)
                {
                    try
                    {
                        regionId = int.Parse(mapInput[i]);
                        i++;
                        superRegionId = int.Parse(mapInput[i]);
                        SuperRegion superRegion = FullMap.GetSuperRegion(superRegionId);
                        FullMap.Add(new Region(regionId, superRegion));
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine("Unable to parse Regions " + e.Message);
                    }
                }
            }
            else if (mapInput[1].Equals("neighbors"))
            {
                for (i = 2; i < mapInput.Length; i++)
                {
                    try
                    {
                        Region region = FullMap.GetRegion(int.Parse(mapInput[i]));
                        i++;
                        string[] neighborIds = mapInput[i].Split(',');
                        for (int j = 0; j < neighborIds.Length; j++)
                        {
                            Region neighbor = FullMap.GetRegion(int.Parse(neighborIds[j]));
                            region.AddNeighbor(neighbor);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine("Unable to parse Neighbors " + e.Message);
                    }
                }
            }
            else if (mapInput[1].Equals("wastelands"))
            {
                Wastelands = new List<Region>();
                for (i = 2; i < mapInput.Length; i++)
                {
                    try
                    {
                        wastelandId = int.Parse(mapInput[i]);
                        Wastelands.Add(FullMap.GetRegion(wastelandId));
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine("Unable to parse wastelands " + e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Regions from wich a player is able to pick his preferred starting region.
        /// </summary>
        /// <param name="input"></param>
        public void SetPickableStartingRegions(string[] input)
        {
            PickableStartingRegions = new List<Region>();
            for (var i = 2; i < input.Length; i++)
            {
                try
                {
                    var regionId = int.Parse(input[i]);
                    var pickableRegion = FullMap.GetRegion(regionId);
                    PickableStartingRegions.Add(pickableRegion);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine("Unable to parse pickable regions " + e.Message);
                }
            }
        }

        /// <summary>
        /// Visible regions are given to the bot with player and armies info.
        /// </summary>
        /// <param name="mapInput"></param>
        public void UpdateMap(string[] mapInput)
	    {
		    VisibleMap = FullMap.GetMapCopy();
		    for(var i=1; i<mapInput.Length; i++)
		    {
			    try 
                {
				    var region = VisibleMap.GetRegion(int.Parse(mapInput[i]));
				    var playerName = mapInput[i+1];
				    var armies = int.Parse(mapInput[i+2]);
				
				    region.PlayerName = playerName;
				    region.Armies = armies;
				    i += 2;
			    }
			    catch(Exception e) 
                {
				    Console.Error.WriteLine("Unable to parse Map Update " + e.Message);
			    }
		    }

		    var unknownRegions = new List<Region>();
		
		    //remove regions which are unknown.
		    foreach (var region in VisibleMap.Regions)
	        {
	            if (region.PlayerName.Equals("unknown"))
	            {
	                unknownRegions.Add(region);
	            }
	        }
            foreach (var unknownRegion in unknownRegions)
	        {
	            VisibleMap.Regions.Remove(unknownRegion);
	        }
	    }

        /// <summary>
        /// Parses a list of the opponent's moves every round. 
        /// Clears it at the start, so only the moves of this round are stored.
        /// </summary>
        /// <param name="moveInput"></param>
        public void ReadOpponentMoves(string[] moveInput)
        {
            OpponentMoves.Clear();

            for (var i = 1; i < moveInput.Length; i++)
            {
                try
                {
                    Move move;
                    if (moveInput[i + 1].Equals("place_armies"))
                    {
                        var region = VisibleMap.GetRegion(int.Parse(moveInput[i + 2]));
                        var playerName = moveInput[i];
                        var armies = int.Parse(moveInput[i + 3]);
                        move = new PlaceArmiesMove(playerName, region, armies);
                        i += 3;
                    }
                    else if (moveInput[i + 1].Equals("attack/transfer"))
                    {
                        var fromRegion = VisibleMap.GetRegion(int.Parse(moveInput[i + 2]));
                        if (fromRegion == null) 
                        {
                            //might happen if the region isn't visible
                            fromRegion = FullMap.GetRegion(int.Parse(moveInput[i + 2]));
                        }

                        var toRegion = VisibleMap.GetRegion(int.Parse(moveInput[i + 3]));
                        if (toRegion == null) 
                        {
                            //might happen if the region isn't visible
                            toRegion = FullMap.GetRegion(int.Parse(moveInput[i + 3]));
                        }

                        var playerName = moveInput[i];
                        var armies = int.Parse(moveInput[i + 4]);
                        move = new AttackTransferMove(playerName, fromRegion, toRegion, armies);
                        i += 4;
                    }
                    else
                    { 
                        //never happens
                        continue;
                    }
                    OpponentMoves.Add(move);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine("Unable to parse Opponent moves " + e.Message);
                }
            }
        }
    }
}
