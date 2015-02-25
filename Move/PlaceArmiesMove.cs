namespace warlight2_starterbot_csharp.Move
{
    using Map;

    /// <summary>
    /// This Move is used in the first part of each round. It represents
    /// what Region is increased with how many armies.
    /// </summary>
    public class PlaceArmiesMove : Move
    {
        #region Properties

        /// <summary>
        /// Region this Move targets.
        /// </summary>
        public Region Region { get; private set; }
        
        /// <summary>
        /// Number of armies to place to given Region.
        /// </summary>
        public int Armies { get; set; }

        #endregion

        /// <summary>
        /// Creates a PlaceArmiesMove.
        /// </summary>
        /// <param name="playerName">Name of player performing move.</param>
        /// <param name="region">Region to reinforce.</param>
        /// <param name="armies">Number of armies to place.</param>
        public PlaceArmiesMove(string playerName, Region region, int armies)
        {
            PlayerName = playerName;
            Region = region;
            Armies = armies;
        }

        /// <summary>
        /// A string representation of this Move.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (IllegalMove.Equals(""))
            {
                return PlayerName + " place_armies " + Region.Id + " " + Armies;
            }
            else
            {
                return PlayerName + " illegal_move " + IllegalMove;
            }
        }
    }
}
