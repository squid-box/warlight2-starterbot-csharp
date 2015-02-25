namespace warlight2_starterbot_csharp.Move
{
    /// <summary>
    /// Base class for a Move.
    /// </summary>
    public abstract class Move
    {
        #region Properties

        /// <summary>
        /// Name of the player that did this move.
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// Gets the value of the error message if move is illegal, else remains empty.
        /// </summary>
        public string IllegalMove { get; set; }

        #endregion

        /// <summary>
        /// Only called from subclasses.
        /// </summary>
        protected Move()
        {
            IllegalMove = string.Empty;
        }
    }
}
