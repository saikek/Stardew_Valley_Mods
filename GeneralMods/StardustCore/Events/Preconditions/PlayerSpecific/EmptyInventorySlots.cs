using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewValley;

namespace StardustCore.Events.Preconditions.PlayerSpecific
{
    public class EmptyInventorySlots:EventPrecondition
    {

        public int amount;

        public EmptyInventorySlots()
        {

        }

        public EmptyInventorySlots(int Amount)
        {
            this.amount = Amount;
        }

        public override string ToString()
        {
            return this.precondition_playerHasInventorySlotsFree();
        }

        /// <summary>
        /// Creates the precondition that the player has atleast this many inventory slots free for the event.
        /// </summary>
        /// <returns></returns>
        public string precondition_playerHasInventorySlotsFree()
        {
            StringBuilder b = new StringBuilder();
            b.Append("c ");
            b.Append(this.amount.ToString());
            return b.ToString();
        }

        public override bool meetsCondition()
        {
            return Game1.player.freeSpotsInInventory() >= this.amount;
        }
    }
}
