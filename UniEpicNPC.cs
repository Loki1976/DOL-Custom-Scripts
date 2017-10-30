/*
 * DAWN OF LIGHT - The first free open source DAoC server emulator
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 * 
 * --------------------------------------------------------------------------
 *  UniEpic Armor Merchant - By Deathwish
 *  
 *  Epic Armor merchant that selects the clients
 *  Class and sends out the classes Epic Armor in 
 *  a trade window.
 *  
 *  NPC needs the EpicArmor MerchantItems installed in
 *  Database to work - Items are already installed in dolpubdbV3.0.
 */


using System;
using System.Collections.Generic;
using System.Text;
using DOL.Database;
using DOL.GS.PacketHandler;

namespace DOL.GS
{
    public class UniEpicNPC : GameBountyMerchant
    {
        public override bool Interact(GamePlayer player)
        {
            if (player.CharacterClass.Name == "Enchantress")
            {
               TradeItems = new MerchantTradeItems("" + "Enchanter" + "Epic");
            }
            else if (player.CharacterClass.Name == "Sorceress")
            {
               TradeItems = new MerchantTradeItems("" + "Sorcerer" + "Epic");
            }
            else if (player.CharacterClass.Name == "Armswoman")
            {
               TradeItems = new MerchantTradeItems("" + "Armsman" + "Epic");
            }
            else if (player.CharacterClass.Name == "Blademistress")
            {
               TradeItems = new MerchantTradeItems("" + "Blademaster" + "Epic");
            }
            else if (player.CharacterClass.Name == "Heroine")
            {
               TradeItems = new MerchantTradeItems("" + "Hero" + "Epic");
            }
            else if (player.CharacterClass.Name == "Huntress")
            {
               TradeItems = new MerchantTradeItems("" + "Hunter" + "Epic");
            }
            else
            {	
				TradeItems = new MerchantTradeItems("" + player.CharacterClass.Name + "Epic");
            }
            player.Out.SendMerchantWindow(TradeItems, eMerchantWindowType.Bp);
            return true;
        }
	}
}
