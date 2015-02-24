namespace warlight2_starterbot_csharp.Map
{
    using System.Collections.Generic;

    public class SuperRegion
    {
        public int Id { get; private set; }
        public int ArmiesReward;
        public LinkedList<Region> SubRegions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="armiesReward"></param>
        public SuperRegion(int id, int armiesReward)
        {
            Id = id;
            ArmiesReward = armiesReward;
            SubRegions = new LinkedList<Region>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subRegion"></param>
        public void AddSubRegion(Region subRegion)
        {
            if (!SubRegions.Contains(subRegion))
            {
                SubRegions.AddLast(subRegion);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A string with the name of the player that fully owns this SuperRegion.</returns>
        public string OwnedByPlayer()
	    {
            var playerName = SubRegions.First.Value.PlayerName;
		    foreach (var region in SubRegions)
		    {
		        if (!playerName.Equals(region.PlayerName))
		        {
		            return null;
		        }
		    }
		    return playerName;
	    }
    }
}
