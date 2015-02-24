namespace warlight2_starterbot_csharp.Bot
{
    using System.Collections.Generic;

    using Map;
    using Move;

    /// <summary>
    /// 
    /// </summary>
    public interface Bot
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        Region GetStartingRegion(BotState state, long timeOut);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        List<PlaceArmiesMove> GetPlaceArmiesMoves(BotState state, long timeOut);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        List<AttackTransferMove> GetAttackTransferMoves(BotState state, long timeOut);
    }
}
