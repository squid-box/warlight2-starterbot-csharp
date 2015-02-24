namespace warlight2_starterbot_csharp.Move
{
    /// <summary>
    /// 
    /// </summary>
    public class Move
    {
        /// <summary>
        /// Name of the player that did this move.
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// gets the value of the error message if move is illegal, else remains empty
        /// </summary>
        public string IllegalMove { get; set; }

        public Move()
        {
            IllegalMove = string.Empty;
        }
    }
}
