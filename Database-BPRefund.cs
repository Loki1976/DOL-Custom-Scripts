/*
 * Author:		Sumy <sumy@instant50rvr.de>
 * Written for:	http://www.instant50rvr.de (C) 2008
 *
 * This Class should be used to refund your Item into Bountypoints
 * it will get all Informations out of the Database for Items/Procs
 * atleast it will hand out the player the amount which is set in
 * the Database.
 */

using System;
using System.Collections;
using System.Timers;
using System.IO;
using DOL;
using DOL.GS;
using DOL.GS.Commands;
using DOL.Database;
using DOL.Database.Attributes;
using DOL.GS.Scripts;
using DOL.Events;
using DOL.GS.GameEvents;
using DOL.GS.PacketHandler;
using DOL.GS.Quests;

#region Databasetable
namespace DOL.Database
{
    [DataTable(TableName = "RefundProcs")]
    public class RefundProcs : DataObject
    {
        private int m_procID;
        private bool m_autoSave;
        private int m_bpamout;

        public RefundProcs()
        {
            AutoSave = false;
        }

        public override bool AutoSave
        {
            get { return m_autoSave; }
            set { m_autoSave = value; }
        }

        [PrimaryKey]
        public int ProcID
        {
            get { return m_procID; }
            set { m_procID = value; }
        }

        [DataElement]
        public int BPAmount
        {
            get { return m_bpamout; }
            set { m_bpamout = value; }
        }
    }
    [DataTable(TableName = "RefundItem")]
    public class RefundItem : DataObject
    {
        private string m_itemname;
        private string m_id_nb;
        private int m_bpamount;

        private static bool m_autoSave;

        public RefundItem()
        {
            m_autoSave = false;
        }

        public override bool AutoSave
        {
            get { return m_autoSave; }
            set { m_autoSave = value; }
        }

        [PrimaryKey]
        public string ID_NB
        {
            get { return m_id_nb; }
            set { m_id_nb = value; }
        }

        [DataElement]
        public string ItemName
        {
            get { return m_itemname; }
            set
            {
                Dirty = true;
                m_itemname = value;
            }
        }

        [DataElement(AllowDbNull = false)]
        public int BPAmount
        {
            get { return m_bpamount; }
            set
            {
                Dirty = true;
                m_bpamount = value;
            }
        }
    }
}
#endregion

namespace DOL.GS.Scripts
{
    class BPMgr
    {
        #region Configure your Refund Mgr here
        /// <summary>
        /// the amount we should take for Procs
        /// </summary>
        protected const int PROC_BP_AMOUNT = 0;
        /// <summary>
        /// true if we should use the example methods
        /// </summary>
        protected const bool USE_EXAMPLES = false;
        #endregion

        [ScriptLoadedEvent]
        public static void OnScriptCompiled(DOLEvent e, object sender, EventArgs args)
        {
            GameServer.Database.RegisterDataObject(typeof(RefundItem));
            GameServer.Database.LoadDatabaseTable(typeof(RefundItem));
            GameServer.Database.RegisterDataObject(typeof(RefundProcs));
            GameServer.Database.LoadDatabaseTable(typeof(RefundProcs));
            //If we should use examples start the converting now
            if (USE_EXAMPLES)
            {
                AddProcs();
                ItemTemplatetoRefundItem();
            }
            //now get the items out of the DB
            CacheItems();
            //now get the procs out of the DB
            CacheProcs();
        }

        #region Example
        /// <summary>
        /// the proc ids we have used
        /// </summary>
        private static int[] m_IDS =
        {
            32124,
            32127, 
            32125 
        };

        /// <summary>
        /// called to add the procs to the database
        /// </summary>
        private static void AddProcs()
        {
            for (int i = 0; i < m_IDS.Length; i++)
            {
                RefundProcs proc = new RefundProcs();

                proc.ProcID = m_IDS[i];
                proc.BPAmount = PROC_BP_AMOUNT;

                GameServer.Database.AddNewObject(proc);
            }
        }

        /// <summary>
        /// called to convert every itemtemplate with a price to a refundable item
        /// </summary>
        private static void ItemTemplatetoRefundItem()
        {
            Hashtable list = new Hashtable();
            ItemTemplate[] items = (ItemTemplate[])GameServer.Database.SelectAllObjects(typeof(ItemTemplate));

            foreach (ItemTemplate item in items)
            {
                if (item.Copper > 0 || item.Silver > 0 || item.Gold > 0)
                {
                    RefundItem ritem = new RefundItem();

                    ritem.ItemName = item.Name;
                    ritem.ID_NB = item.Id_nb;

                    int amount = 0;
                    amount += item.Copper;
                    amount += item.Silver * 100;
                    amount += item.Gold * 10000;

                    ritem.BPAmount = amount;

                    GameServer.Database.AddNewObject(ritem);
                }
            }
        }
        #endregion

        #region Cache Items/Procs in Hashtables
        /// <summary>
        /// the cached items
        /// </summary>
        public static Hashtable cacheditems = new Hashtable();
        /// <summary>
        /// the cached procs
        /// </summary>
        public static Hashtable cachedprocs = new Hashtable();

        private static void CacheProcs()
        {
            RefundProcs[] procs = (RefundProcs[])GameServer.Database.SelectAllObjects(typeof(RefundProcs));
            foreach (RefundProcs proc in procs)
            {
                if (!cachedprocs.ContainsKey(proc.ProcID))
                    cachedprocs.Add(proc.ProcID, proc);
            }
        }

        private static void CacheItems()
        {
            RefundItem[] items = (RefundItem[])GameServer.Database.SelectAllObjects(typeof(RefundItem));
            foreach (RefundItem item in items)
            {
                if (!cacheditems.ContainsKey(item.ItemName))
                    cacheditems.Add(item.ItemName, item);
            }
        }
        #endregion
    }

    #region Command
    [CmdAttribute("&refund",
    ePrivLevel.GM,
    "Allows GMs to add new refundable items.",
    "/refund item <itemname> <id_nb> <amount>",
    "/refund proc <procid> <amount>")]
    public class RefundCommandHandler : AbstractCommandHandler
    {
        public int OnCommand(GameClient client, string[] args)
        {
            if (args.Length == 1)
            {
                DisplaySyntax(client);
                return 0;
            }

            switch (args[1])
            {
                case "item":
                    {
                        if (args.Length < 5)
                        {
                            DisplaySyntax(client);
                            return 0;
                        }

                        ItemTemplate temp = (ItemTemplate)(GameServer.Database.SelectObject(typeof(ItemTemplate), "Id_Nb = '" + args[3] + "'"));

                        if (temp == null)
                        {
                            DisplayMessage(client, "Itemtemplate not found");
                            return 0;
                        }
                        if (temp.Name != args[2])
                        {
                            DisplayMessage(client, "Itemname does not match Templatename");
                            return 0;
                        }

                        RefundItem item = new RefundItem();

                        item.ItemName = args[2];
                        item.ID_NB = args[3];
                        try
                        {
                            item.BPAmount = Convert.ToInt32(args[4]);
                        }
                        catch (Exception)
                        {
                            DisplayMessage(client, "Please enter a valid number");
                            return 0;
                        }

                        DisplayMessage(client, "Refunditem added to database");
                        GameServer.Database.SaveObject(item);
                    }
                    break;
                case "proc":
                    {
                        if (args.Length < 4)
                        {
                            DisplaySyntax(client);
                            return 0;
                        }

                        RefundProcs proc = new RefundProcs();

                        try
                        {
                            proc.ProcID = Convert.ToInt32(args[2]);
                            proc.BPAmount = Convert.ToInt32(args[3]);
                        }
                        catch (Exception)
                        {
                            DisplayMessage(client, "Please enter a valid number");
                            return 0;
                        }

                        DisplayMessage(client, "Refundproc added to database");
                        GameServer.Database.SaveObject(proc);
                    }
                    break;
            }

            return 0;
        }
    }
    #endregion

    public class DBBPRefund : GameNPC
    {
        /// <summary>
        /// true if we should log every refund
        /// </summary>
        protected const bool LOGGING = true;

        /// <summary>
        /// the hashtable which holds the players and the items
        /// </summary>
        private Hashtable m_items = new Hashtable();

        #region Helpers and Methods
        /// <summary>
        /// Return true if the Item is refundable
        /// </summary>
        /// <param name="item">the item which should be checked</param>
        private bool IsItemRefundable(InventoryItem item)
        {
            return BPMgr.cacheditems.ContainsKey(item.Name);
        }

        /// <summary>
        /// Returns the BP-Amount for the Item
        /// </summary>
        /// <param name="item">the item which should be refunded</param>
        /// <param name="player">the player which is refunding</param>
        private long ReturnAmount(InventoryItem item, GamePlayer player)
        {
            long amount = 0;

            if (BPMgr.cacheditems.ContainsKey(item.Name))
                amount += (BPMgr.cacheditems[item.Name] as RefundItem).BPAmount;

            amount += ReturnProcAmount(item.ProcSpellID);
            amount += ReturnProcAmount(item.ProcSpellID1);

            if (player.Guild != null && player.Guild.theGuildDB.BuffType == 2)
                amount -= (amount * 5) / 100;

            return amount;
        }

        /// <summary>
        /// Returns the BP-Amount for one Proc
        /// </summary>
        /// <param name="ID">the proc ID</param>
        public long ReturnProcAmount(int ID)
        {
            if (BPMgr.cachedprocs.ContainsKey(ID))
                return (BPMgr.cachedprocs[ID] as RefundProcs).BPAmount;

            return 0;
        }

        /// <summary>
        /// returns true if the player already has given a item
        /// </summary>
        /// <param name="player">the player which should be checked</param>
        protected bool AlreadyReceivedItem(GamePlayer player)
        {
            if (player == null)
                return false;

            if (m_items.ContainsKey(player))
            {
                SayTo(player, eChatLoc.CL_PopupWindow, "You have already handed me an item!");
                SendDialog(player);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// For logging every refund
        /// </summary>
        /// <param name="player">the player which is refunding</param>
        /// <param name="item">the item which is refunded</param>
        /// <param name="amount">the amount which is given to the player</param>
        private void WriteLogs(GamePlayer player, InventoryItem item, long amount)
        {
            string directory = "logs" + Path.DirectorySeparatorChar;
            string fileName = directory + "refund.txt";
            string text = string.Format("{0} ({1}) has refunded \"{2} ({3})\" for {4} Bountypoints", player.Name, player.Client.Account.Name, item.Name, item.Id_nb, amount);

            if (File.Exists(fileName))
            {
                StreamWriter SW = File.AppendText(fileName);
                SW.WriteLine(text);
                SW.Close();
            }
            else
            {
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                StreamWriter SW = File.CreateText(fileName);
                SW.WriteLine(text);
                SW.Close();
            }
        }
        #endregion

        #region ReciveItem and AddToWorld
        public override bool AddToWorld()
        {
            GameNpcInventoryTemplate template = new GameNpcInventoryTemplate();
            switch (Realm)
            {
                case eRealm.Albion:
                    template.AddNPCEquipment(eInventorySlot.TorsoArmor, 2230); break;
                case eRealm.Midgard:
                    template.AddNPCEquipment(eInventorySlot.TorsoArmor, 2232);
                    template.AddNPCEquipment(eInventorySlot.ArmsArmor, 2233);
                    template.AddNPCEquipment(eInventorySlot.LegsArmor, 2234);
                    template.AddNPCEquipment(eInventorySlot.HandsArmor, 2235);
                    template.AddNPCEquipment(eInventorySlot.FeetArmor, 2236);
                    break;
                case eRealm.Hibernia:
                    template.AddNPCEquipment(eInventorySlot.TorsoArmor, 2231); ; break;
            }

            Inventory = template.CloseTemplate();
            Flags = 16;	// Peace flag.
            return base.AddToWorld();
        }

        /// <summary>
        /// Called when the object is about to get an item from someone
        /// </summary>
        /// <param name="source">Source from where to get the item</param>
        /// <param name="templateID">templateID for item to add</param>
        /// <returns>true if the item was successfully received</returns>
        public override bool ReceiveItem(GameLiving source, InventoryItem item)
        {
            GamePlayer player = source as GamePlayer;
            if (player == null) return false;

            if (WorldMgr.GetDistance(this, source) > WorldMgr.INTERACT_DISTANCE)
            {
                player.Out.SendMessage("You are too far away to give anything to " + GetName(0, false) + ".", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                return false;
            }

            if (AlreadyReceivedItem(player))
                return false;

            if (player != null && item != null)
            {
                long amount = ReturnAmount(item, player);

                if (amount > 0 && !m_items.Contains(player) && IsItemRefundable(item))
                {
                    m_items.Add(player, item);
                    player.Inventory.RemoveItem(item);
                    SendDialog(player);
                }
                else if (!IsItemRefundable(item))
                    SayTo(player, eChatLoc.CL_PopupWindow, "This Item isn't refundable!");
                else
                    SayTo(player, eChatLoc.CL_PopupWindow, "Sorry I can't refund that Item!");
            }
            return base.ReceiveItem(source, item);
        }
        #endregion

        #region Dialog sending
        /// <summary>
        /// Called when the step is reached where the player has to accept the refund
        /// </summary>
        /// <param name="player">the player which have to accept</param>
        protected void SendDialog(GamePlayer player)
        {
            if (player == null) return;

            InventoryItem item = (InventoryItem)m_items[player];
            if (item == null) return;

            long amount = ReturnAmount(item, player);

            SayTo(player, eChatLoc.CL_PopupWindow, "Okay, I will give you " + amount + " Bounty Points for that!");
            player.Client.Out.SendCustomDialog("Do you want to sell this item for " + amount + " Bounty Points?", new CustomDialogResponse(RefundDialogResponse));
        }

        /// <summary>
        /// The Dialog for the Refunding
        /// </summary>
        protected void RefundDialogResponse(GamePlayer player, byte response)
        {
            if (player == null) return;

            InventoryItem item = (InventoryItem)m_items[player];
            if (item == null) return;

            long amount = ReturnAmount(item, player);

            if (response == 0x01 && amount > 0)
            {
                if (LOGGING)
                    WriteLogs(player, item, amount);

                amount /= (long)ServerProperties.Properties.BP_RATE;
                player.GainBountyPoints(amount);
            }
            else
            {
                player.Out.SendMessage("You decline to have your " + item.Name + " refunded.", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                player.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, item);
            }

            if (m_items.ContainsKey(player))
                m_items.Remove(player);
        }
        #endregion
    }
}