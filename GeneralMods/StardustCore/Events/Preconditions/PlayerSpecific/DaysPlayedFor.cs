using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewValley;

namespace StardustCore.Events.Preconditions.PlayerSpecific
{
    public class DaysPlayedFor:EventPrecondition
    {
        /// <summary>
        /// The minimum amount of days that must be played for this event to occur.
        /// </summary>
        public int amount;

        public DaysPlayedFor()
        {

        }


        public DaysPlayedFor(int Amount)
        {
            this.amount = Amount;
        }

        public override string ToString()
        {
            return this.precondition_playerHasPlayedForXDays();
        }

        /// <summary>
        /// Player has played for atleast this many days.
        /// </summary>
        /// <param name="Amount"></param>
        /// <returns></returns>
        public string precondition_playerHasPlayedForXDays()
        {
            StringBuilder b = new StringBuilder();
            b.Append("j ");
            b.Append(this.amount.ToString());
            return b.ToString();
        }

        public override bool meetsCondition()
        {
            return Game1.player.stats.DaysPlayed >= this.amount;
        }
    }
}
