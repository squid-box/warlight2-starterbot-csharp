namespace warlight2_starterbot_csharp.Map
{
    using System.Collections.Generic;

    public class SuperRegion
    {
        #region Properties

        /// <summary>
        /// ID of this SuperRegion.
        /// </summary>
        public int Id { get; private set; }
        
        /// <summary>
        /// Army bonus awarded if entire SuperRegion is controlled.
        /// </summary>
        public int ArmiesReward;
        
        /// <summary>
        /// List of regions in this SuperRegion.
        /// </summary>
        public List<Region> SubRegions;

        #endregion

        /// <summary>
        /// Create an empty SuperRegion.
        /// </summary>
        /// <param name="id">ID of this SuperRegion.</param>
        /// <param name="armiesReward">Army reward for this SuperRegion.</param>
        public SuperRegion(int id, int armiesReward)
        {
            Id = id;
            ArmiesReward = armiesReward;
            SubRegions = new List<Region>();
        }

        /// <summary>
        /// Add Region to this SuperRegion.
        /// </summary>
        /// <param name="subRegion">Region to add.</param>
        public void AddSubRegion(Region subRegion)
        {
            if (!SubRegions.Contains(subRegion))
            {
                SubRegions.Add(subRegion);
            }
        }

        /// <summary>
        /// Checks if this SuperRegion is completely controlled by one player.
        /// </summary>
        /// <returns>A string with the name of the player that fully owns this SuperRegion.</returns>
        public string OwnedByPlayer()
	    {
            var playerName = SubRegions[0].PlayerName;

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
