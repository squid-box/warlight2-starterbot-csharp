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
        public Region fromRegion { get; private set; }
        public Region toRegion { get; private set; }
        public int Armies { get; set; }

        public AttackTransferMove(string playerName, Region fromRegion, Region toRegion, int armies)
        {
            base.PlayerName = playerName;
            this.fromRegion = fromRegion;
            this.toRegion = toRegion;
            this.Armies = armies;
        }

        /// <summary>
        /// A string representation of this Move.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (IllegalMove.Equals(""))
            {
                return PlayerName + " attack/transfer " + fromRegion.Id + " " + toRegion.Id + " " + Armies;
            }
            else
            {
                return PlayerName + " illegal_move " + IllegalMove;
            }
        }
    }
}
