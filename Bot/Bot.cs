namespace warlight2_starterbot_csharp.Bot
{
    using System.Collections.Generic;

    using Map;
    using Move;

    /// <summary>
    /// Interface for a bot.
    /// </summary>
    public interface Bot
    {
        /// <summary>
        /// A method that returns which region the bot would like to start on, the pickable regions are stored in the BotState.
        /// The bots are asked in turn (ABBAABBAAB) where they would like to start and return a single region each time they are asked.
        /// </summary>
        /// <param name="state">Current BotState.</param>
        /// <param name="timeOut">Time limit for this operation, in milliseconds.</param>
        /// <returns>This method returns one random region from the given pickable regions.</returns>
        Region GetStartingRegion(BotState state, long timeOut);

        /// <summary>
        /// This method is called for at first part of each round, where the bot places new armies.
        /// </summary>
        /// <param name="state">Current BotState.</param>
        /// <param name="timeOut">Time limit for this operation, in milliseconds.</param>
        /// <returns>The list of PlaceArmiesMoves for one round.</returns>
        List<PlaceArmiesMove> GetPlaceArmiesMoves(BotState state, long timeOut);

        /// <summary>
        /// This method is called for at the second part of each round, where the bot orders attacks or transfers.
        /// </summary>
        /// <param name="state">Current BotState.</param>
        /// <param name="timeOut">Time limit for this operation, in milliseconds.</param>
        /// <returns>The list of PlaceArmiesMoves for one round.</returns>
        List<AttackTransferMove> GetAttackTransferMoves(BotState state, long timeOut);
    }
}
