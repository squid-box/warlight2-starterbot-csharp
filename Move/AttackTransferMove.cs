namespace warlight2_starterbot_csharp.Move
{
    using Map;

    /// <summary>
    /// This Move is used in the second part of each round. It represents
    /// the attack or transfer of armies from fromRegion to toRegion. 
    /// If toRegion is owned by the player himself, it's a transfer. 
    /// If toRegion is owned by the opponent, this Move is an attack. 
    /// </summary>
    public class AttackTransferMove : Move
    {
        #region Properties

        /// <summary>
        /// Region to perform Move from.
        /// </summary>
        public Region FromRegion { get; private set; }
        
        /// <summary>
        /// Region to perform Move to.
        /// </summary>
        public Region ToRegion { get; private set; }
        
        /// <summary>
        /// Number of armies to be moved in this Move.
        /// </summary>
        public int Armies { get; set; }

        #endregion

        /// <summary>
        /// Create an attack/transfer Move.
        /// </summary>
        /// <param name="playerName">Player performing this move.</param>
        /// <param name="fromRegion">Region to move armies from.</param>
        /// <param name="toRegion">Region to move armies to.</param>
        /// <param name="armies">Number of armies to move.</param>
        public AttackTransferMove(string playerName, Region fromRegion, Region toRegion, int armies)
        {
            PlayerName = playerName;
            FromRegion = fromRegion;
            ToRegion = toRegion;
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
                return PlayerName + " attack/transfer " + FromRegion.Id + " " + ToRegion.Id + " " + Armies;
            }
            else
            {
                return PlayerName + " illegal_move " + IllegalMove;
            }
        }
    }
}
