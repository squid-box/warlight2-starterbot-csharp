namespace warlight2_starterbot_csharp.Map
{
    using System.Collections.Generic;

    public class Region
    {
        /// <summary>
        /// ID of this Region.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// List of neighbours to this Region.
        /// </summary>
        public LinkedList<Region> Neighbors { get; private set; }

        /// <summary>
        /// SuperRegion of this Region.
        /// </summary>
        public SuperRegion SuperRegion { get; private set; }

        /// <summary>
        /// Number of armies currently in this Region.
        /// </summary>
        public int Armies { get; set; }

        /// <summary>
        /// Name of the owner of this Region.
        /// </summary>
        public string PlayerName { get; set; }

        public Region(int id, SuperRegion superRegion)
        {
            Id = id;
            SuperRegion = superRegion;
            Neighbors = new LinkedList<Region>();
            PlayerName = "unknown";
            Armies = 0;

            superRegion.AddSubRegion(this);
        }

        public Region(int id, SuperRegion superRegion, string playerName, int armies)
        {
            Id = id;
            SuperRegion = superRegion;
            Neighbors = new LinkedList<Region>();
            PlayerName = playerName;
            Armies = armies;

            superRegion.AddSubRegion(this);
        }

        public void AddNeighbour(Region neighbour)
        {
            if (!Neighbors.Contains(neighbour))
            {
                Neighbors.AddLast(neighbour);
                neighbour.AddNeighbour(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="region"></param>
        /// <returns>True if this Region is a neighbour of given Region, false otherwise.</returns>
        public bool IsNeighbour(Region region)
        {
            return Neighbors.Contains(region);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerName"></param>
        /// <returns>True if this region is owned by given playerName, false otherwise.</returns>
        public bool OwnedByPlayer(string playerName)
        {
            return playerName.Equals(PlayerName);
        }
    }
}
