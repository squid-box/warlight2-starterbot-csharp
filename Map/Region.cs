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
        /// List of neighbors to this Region.
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

        /// <summary>
        /// Create an empty region.
        /// </summary>
        /// <param name="id">Unique ID of this Region.</param>
        /// <param name="superRegion">SuperRegion this Region belongs to.</param>
        public Region(int id, SuperRegion superRegion)
        {
            Id = id;
            SuperRegion = superRegion;
            Neighbors = new LinkedList<Region>();
            PlayerName = "unknown";
            Armies = 0;

            superRegion.AddSubRegion(this);
        }

        /// <summary>
        /// Create an inhabited region.
        /// </summary>
        /// <param name="id">Unique ID of this Region.</param>
        /// <param name="superRegion">SuperRegion this Region belongs to.</param>
        /// <param name="playerName">Owner of this Region.</param>
        /// <param name="armies">Number of armies in this region.</param>
        public Region(int id, SuperRegion superRegion, string playerName, int armies)
        {
            Id = id;
            SuperRegion = superRegion;
            Neighbors = new LinkedList<Region>();
            PlayerName = playerName;
            Armies = armies;

            superRegion.AddSubRegion(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="neighbor"></param>
        public void AddNeighbor(Region neighbor)
        {
            if (!Neighbors.Contains(neighbor))
            {
                Neighbors.AddLast(neighbor);
                neighbor.AddNeighbor(this);
            }
        }

        /// <summary>
        /// Checks to see if given Region is a neighbor to this region.
        /// </summary>
        /// <param name="region"></param>
        /// <returns>True if this Region is a neighbor of given Region, false otherwise.</returns>
        public bool IsNeighbor(Region region)
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
