namespace warlight2_starterbot_csharp.Move
{
    using Map;

    /// <summary>
    /// This Move is used in the first part of each round. It represents
    /// what Region is increased with how many armies.
    /// </summary>
    public class PlaceArmiesMove : Move
    {
        public Region Region { get; private set; }
        public int Armies { get; set; }

        public PlaceArmiesMove(string playerName, Region region, int armies)
        {
            base.PlayerName = playerName;
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
