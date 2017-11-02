/**********************************************************
 * Model ID Changer                                       *
 *                                                        *
 * By:Tycone                                              *
 *                                                        *
 * ********************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using DOL.GS.PacketHandler;

namespace DOL.GS.Scripts
{
	
	public class Morpher: GameNPC
    {

        #region Settings

        //The required Mininum level of the player to use this NPC.
        int PlayerMinLevel = 1; 


        //Mininum ModelID allowed for players model.
        int MinModel = 5; 
      
        
        //Maxinum ModelID allowed for players model.
        int MaxModel = 5000;


        //Controls wiether or not players can have their morphs restored
        bool AllowMorphRestore = true;


        /* Model ID's that will be blocked from use. When you add a new ID the new int [#] right before
           the {models} must match match the ammount of models in the array. */
        int[] BlockedModels =  new int[833] {9, 11, 666, 12, 13, 15, 21, 29, 30, 31, 47, 118, 119, 120, 121, 122, 253, 408,
            409, 410, 411, 413, 414, 447, 448, 449, 450, 463, 464, 603, 607, 608, 612, 
            613, 614, 630, 631, 632, 633, 634, 659, 660, 665, 667, 678, 679, 765, 818, 823,
            824, 828, 830, 843, 858, 862, 863, 891, 904, 905, 907, 908, 909, 910, 911, 912,
            913, 918, 928, 930, 931, 932, 933, 934, 935, 936, 937, 940, 941, 942, 943, 944,
            945, 966, 967, 968, 969, 970, 971, 972, 973, 974, 999, 1000, 1001, 1002, 1003,
            1004, 1005, 1007, 1049, 1050, 1180, 1200, 1201, 1202, 1208, 1212, 1219, 1235,
            1236, 1237, 1238, 1239, 1240, 1241, 1242, 1243, 1244, 1245, 1246, 1247, 1248,
            2149, 1250, 1251, 1252, 1253, 1254, 1258, 1259, 1260, 1261, 1262, 1263, 1266,
            1269, 1275, 1389, 1390, 1391, 1392, 1393, 1394, 1431, 1432, 1433, 1434, 1435, 
            1436, 1437, 1438, 1441, 1442, 1443, 1444, 1445, 1446, 1447, 1448, 1449, 1450, 
            1451, 1452, 1453, 1454, 1455, 1456, 1457, 1458, 1459, 1450, 1451, 1452, 1453,
            1454, 1455, 1456, 1457, 1458, 1459, 1460, 1461, 1462, 1463, 1464, 1465, 1466,
            1467, 1468, 1469, 1470, 1472, 1473, 1474, 1475, 1476, 1477, 1478, 1479, 1480,
            1481, 1482, 1483, 1484, 1485, 1486, 1487, 1488, 1489, 1490, 1491, 1492, 1493,
            1494, 1495, 1496, 1497, 1498, 1499, 1500, 1501, 1502, 1503, 1504, 1505, 1506,
            1507, 1508, 1509, 1510, 1511, 1512, 1513, 1514, 1515, 1516, 1517, 1518, 1519,
            1520, 1521, 1522, 1523, 1524, 1525, 1526, 1527, 1528, 1529, 1530, 1531, 1532,
            1533, 1534, 1535, 1536, 1537, 1538, 1539, 1540, 1541, 1542, 1543, 1544, 1545, 
            1546, 1547, 1548, 1549, 1550, 1551, 1552, 1553, 1554, 1555, 1556, 1557, 1558, 
            1559, 1560, 1561, 1562, 1563, 1564, 1565, 1566, 1567, 1568, 1569, 1570, 1571, 
            1572, 1573, 1583, 1588, 1589, 1590, 1591, 1592, 1593, 1594, 1595, 1598, 1599, 
            1600, 1601, 1602, 1603, 1604, 1605, 1608, 1609, 1610, 1611, 1612, 1613, 1614,
            1615, 1616, 1617, 1618, 1619, 1620, 1621, 1622, 1623, 1624, 1626, 1627, 1628,
            1629, 1630, 1631, 1632, 1633, 1634, 1635, 1636, 1637, 1638, 1639, 1640, 1641,
            1642, 1643, 1644, 1645, 1646, 1648, 1671, 1677, 1678, 1679, 1680, 1684, 1685,
            1686, 1687, 1698, 1700, 1701, 1737, 1748, 1739, 1748, 1761, 1762, 1763, 1764, 1765, 
            1766, 1767, 1768, 1769, 1770, 1822, 1823, 1870, 1871, 1872, 1873, 1874, 1875, 1876, 1877,
            1878, 1879, 1880, 1881, 1882, 1886, 1887, 1903, 1905, 1906, 1907, 1908, 1923, 1925, 1926, 1927,
            1928, 1929, 1934, 1936, 1937, 1939, 1940, 1941, 1942, 1943, 1956, 1957, 1958, 1959, 2000, 2004, 2005,
            2006, 2007, 2008, 2009, 2010, 2011, 2012, 2019, 2020, 2021, 2027, 2028, 2029, 2030, 2031, 2033, 2034, 2035,
            2036, 2037, 2038, 2039, 2042, 2044, 2046, 2047, 2049, 2050, 2051, 2052, 2059, 2060, 2061, 2069, 2077, 2078, 2079,
            2095, 2153, 2167, 2168, 2169, 2170, 2301, 2446, 2447, 2448, 2456, 2457, 2458, 2459, 2461, 2462, 2495, 2496, 2497,
            2498, 2511, 2512, 2651, 2655, 2656, 2660, 2661, 2662, 2678, 2679, 2680, 2681, 2682, 2707, 2708, 2713, 2714, 2715, 2726,
            2727, 2813, 2866, 2871, 2872, 2878, 2891, 2906, 2910, 2911, 2939, 2952, 2953, 2955, 2956, 2957, 2958, 2959, 2960, 2961, 
            2966,  2976, 2978, 2979, 2980, 2981, 2982, 2983, 2984, 2985, 2988, 2989, 2990, 2991, 2992, 3014, 3015, 3016, 3017, 3018,
            3019, 3020, 3021, 3022, 3031, 3048, 3049, 3050, 3051, 3052, 3053, 3054, 3098, 3099, 3260, 3267, 3306, 3307, 3308, 3309, 
            3310, 3311, 3313, 3314, 3316, 3317, 3318, 3319, 3320, 3321, 3322, 3323, 3437, 3438, 3439, 3440, 3441, 3486, 3490, 3491, 
            3492, 3493, 3494, 3495, 3496, 3497, 3498, 3499, 3500, 3501, 3502, 3503, 3504, 3505, 3506, 3507, 3508, 3509, 3510, 3511,
            3512, 3513, 3514, 3515, 3516, 3517, 3518, 3519, 3520, 3521, 3522, 3523, 3524, 3525, 3526, 3527, 3528, 3529, 3530, 3531, 
            3532, 3533, 3534, 3535, 3536, 3537, 3538, 3539, 3540, 3541, 3542, 3543, 3545, 3546, 3547, 3548, 3549, 3550, 3551, 3552, 
            3553, 3554, 3555, 3556, 3557, 3558, 3559, 3560, 3561, 3562, 3563, 3564, 3565, 3566, 3567, 3568, 3569, 3570, 3571, 3572,
            3573, 3574, 3575, 3576, 3577, 3578, 3579, 3580, 3581, 3582, 3583, 3584, 3585, 3586, 3587, 3588, 3589, 3590, 3591, 3592,
            3593, 3594, 3595, 3596, 3597, 3598, 3599, 3600, 3601, 3602, 3603, 3604, 3605, 3606, 3607, 3608, 3609, 3610, 3611, 3612,
            3613, 3614, 3615, 3631, 3719, 3720, 3732, 3733, 3734, 3735, 3748, 3749, 3750, 3818, 3870, 3911, 3926, 3927, 3928, 3929,
            3930, 3934, 3935, 3951, 3953, 3954, 3955, 3956, 3957, 3971, 3972, 3973, 3974, 3975, 3976, 3985, 3986, 3987, 3988, 3989,
            3990, 3991, 4048, 4046, 4047, 4048, 4049, 4075, 4076, 4077, 4078, 4079, 4080, 4081, 4082, 4083, 4084, 4085, 4086, 4087,
            4092, 4095, 4096, 4097, 4098, 4099, 4100, 4101, 4125, 4126, 4127, 4201, 4215, 4216, 4217, 4218, 4349, 4494, 4495, 4496,
            4497, 4498, 4499, 4500, 4501, 4502, 4503, 4504, 4505, 4506, 4507, 4508, 4509, 4510, 4543, 4544, 4545, 4546, 4559, 4560,
            4564, 4565, 4671, 4672, 4673, 4699, 4703, 4704, 4705, 4706, 4707, 4709, 4710, 4726, 4728, 4729, 4730, 4751, 4752, 4753,
            4755, 4756, 4761, 4762, 4763, 4914, 4918, 4919, 4920, 4924, 4926, 4938, 4939, 4954, 4955, 4958, 4959, 4987, 5000};
        

        //Controls wiether or not the npc charges bps for morphs.
        bool ChargeBountyPoints = true;


        //How many BPs are charged per morph.
        long BountyPointPrice = 5;


        //Controls wiether or not bps are charged for restoring a players original morph.
        bool ChargeMorphRestore = true;

        
        //Ammount of bps charged for a morph restore.
        long MorphRestorePrice = 1;

        //Controls wiether or not the player can view a list of the blocked models.
        bool CanseeBlockedModels = true;

        //Controls wiether or not the user can view the settings.
        bool CanViewSettings = true;

        #endregion



        #region Code

        public override bool Interact(GamePlayer player)
        {
            string settings = "";

            if (CanViewSettings == true)
            {
                settings = " \n Or do you want to see the current [Settings] of this NPC. ";
            }

            string restore = "";
            if (AllowMorphRestore == true)
                restore = " \n Or do you want to be [restored] to your original morph? ";
            SayTo(player, "Hello " + player.Name + ", your current model is " + player.Model + ", whsiper to me a model ID. " +  settings + restore);
            return base.Interact(player);
        }

        public override bool WhisperReceive(GameLiving source, string str)
        {
       
            bool iscase = false;
            string morphRtext = "No Morph Restores!";
            string MorphSet = "Free Morphs!";
            GamePlayer Dude = source as GamePlayer;
            long rprice = 0;
            string rcostmsg = "";
            string blkmdsmsg = "";

            if (AllowMorphRestore == true)
            {
                morphRtext = "Free Morph Restores";
                if (MorphRestorePrice > 0)
                {
                    morphRtext = "Morph Restores Cost:" + MorphRestorePrice + " bps."; 
                }
            }
            if (ChargeBountyPoints == true)
            {
                if (BountyPointPrice > 0)
                {
                    MorphSet = "Morph Cost:" + BountyPointPrice + " bps.";
                }
            }

            if (CanseeBlockedModels == true)
            {
                blkmdsmsg = "\n Would you like to see the [blocked] models?";
            }
            

            if ((str == "Settings")) 
            {
                if (CanViewSettings == true)
                {
                    SayTo(Dude, "The current settings for this NPC are as follows\n"
                    + "\n Mininum Player Level:" + PlayerMinLevel + "."
                    + "\n Mininum Model ID:" + MinModel + "."
                    + "\n Maxinum Model ID:" + MaxModel + "."
                    + "\n " + morphRtext
                    + "\n" + MorphSet
                    + "\n" + blkmdsmsg);
                }
                iscase = true;
     
               
            }

            if ((str == "restored"))
            {
                iscase = true;
                if (Dude.BountyPoints < MorphRestorePrice)
                {
                    SayTo(Dude, "Sorry " + Dude.Name + ", you do not have enough BPs.");
                    return false;
                }
             
                if (AllowMorphRestore == true)
                {
                    if (ChargeMorphRestore == true)
                    {
                        rprice = MorphRestorePrice;
                    }
                    if (rprice > 0)
                    {
                      
                        Dude.BountyPoints += -rprice;
                        rcostmsg = "It has cost you " + rprice + " bounty points";
                        UpdateShit(Dude);
                    }
               
                                                  
                    Dude.Model = (ushort)Dude.PlayerCharacter.OriginalCreationModel;
                    Dude.PlayerCharacter.CreationModel = Dude.PlayerCharacter.OriginalCreationModel;
               
                    SayTo(Dude, "Your morph has been restored too " + Dude.Model + ". " + rcostmsg + "\n");
                }
            
                return false;

            }


            if (str == "blocked")
            {
                iscase = true;
                if (CanseeBlockedModels = true)
                {

                    int blockedcount = 0;
                    foreach (int models in BlockedModels)
                    {
                        blockedcount += 1;
                        SayTo(Dude, "Their are " + Convert.ToString(blockedcount) + " Blocked Models and they are as follows");



                    }

                    foreach (int models in BlockedModels)
                    {
                        SayTo(Dude, Convert.ToString(models));
                    }


                    blockedcount = 0;
                    return false;
                }

                return true;
            }

     
            if (iscase == false)
            {
                Console.WriteLine("iscase check = " + Convert.ToString(iscase));

                int model = Convert.ToUInt16(str);


                if (Dude.Level < PlayerMinLevel)
                {
                    SayTo(Dude, "Come back when you are level " + PlayerMinLevel + ".\n");
                }
                if (model < MinModel || model > MaxModel)
                {
                    SayTo(Dude, "Sorry the model " + Convert.ToString(model) + " was not between " + Convert.ToString(MinModel) +
                    " and " + Convert.ToString(MaxModel) + ". \n");
                    return false;
                }


                foreach (int BM in BlockedModels)
                {

                    if (model == Convert.ToInt16(BM))
                    {
                        SayTo(Dude, "Sorry the model " + Convert.ToString(model) + " Is among the blocked models");
                        return false;

                    }

                }

                string Morphmsg = "Your morph is now " + model + ".";

                if (ChargeBountyPoints == true)
                {
                    if (BountyPointPrice > 0)
                    {
                        if (Dude.BountyPoints < BountyPointPrice)
                        {
                            SayTo(Dude, "Sorry " + Dude.Name + ", you do not have enough BPs.");
                            return false;
                        }
                        Morphmsg = "Your morph is now " + model + ", it cost you " + BountyPointPrice + " bps.";
                        Dude.BountyPoints += -BountyPointPrice;
                        UpdateShit(Dude);
                        
                    }

                }
                if (Dude.PlayerCharacter.OriginalCreationModel == 0)
                {
                    Dude.PlayerCharacter.OriginalCreationModel = Dude.PlayerCharacter.CreationModel;
                }
                Dude.Model = Convert.ToUInt16(str);
                Dude.PlayerCharacter.CreationModel = Convert.ToInt16(str);


            }
           return true;
          return base.WhisperReceive(source, str);
        }

        public void UpdateShit(GamePlayer player)
        {
            player.Out.SendUpdatePoints();
            player.Out.SendUpdatePlayerSkills();
            player.Out.SendUpdatePlayer();
            player.Out.SendUpdateMoney();
        

        }
     

        #endregion
    }
}
