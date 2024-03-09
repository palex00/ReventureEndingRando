﻿using Archipelago.MultiClient.Net.Models;
using Atto;
using Newtonsoft.Json;
using ReventureEndingRando.EndingEffects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TMPro;
using UnityEngine;

namespace ReventureEndingRando
{
    public class EndingRandomizer
    {
        public Dictionary<EndingTypes, EndingEffectsEnum> randomization;

        public EndingRandomizer()
        {
            //randomization = new Dictionary<EndingTypes, EndingEffectsEnum>();
            //if (true)
            //if (!LoadState())
            //{
            //    Randomize();
            //    StoreState();
            //}
        }

        public void Randomize()
        {
            Seed seed = new Seed();
            randomization = seed.randomization;
            Plugin.PatchLogger.LogInfo("Finished generating Seed");
        }

        //public List<EndingEffectsEnum> UpdateWorld(IProgressionService progression)
        //{
        //    List<EndingEffectsEnum> enabledEffect = new List<EndingEffectsEnum>();
        //    foreach (KeyValuePair<EndingTypes, EndingEffectsEnum> entry in randomization)
        //    {
        //        EndingEffect ee = EndingEffect.InitFromEnum(entry.Value);
        //        bool endingAchieved = progression.IsEndingUnlocked(entry.Key);
        //        if (ee != null) {
        //            ee.ActivateEffect(endingAchieved);
        //            if (endingAchieved)
        //            {
        //                enabledEffect.Add(entry.Value);
        //            }
        //        }
        //    }

        //    //Update UI
        //    GameObject versionTextObj = GameObject.Find("Canvasses/OverlayCanvas/GamePanel/ZonePanel/zoneText/versionText");
        //    TextMeshProUGUI versionText = versionTextObj.GetComponent<TextMeshProUGUI>();
        //    versionText.SetText($"{versionText.text}; Rando: {MyPluginInfo.PLUGIN_VERSION}");

        //    //Show Last Unlocks


        //    return enabledEffect;
        //}

        public static List<EndingEffectsEnum> UpdateWorldArchipelago()
        {
            List<EndingEffectsEnum> enabledEffect = new List<EndingEffectsEnum>();

            Dictionary<long, int> receivedIDs = new Dictionary<long, int>();
            foreach (NetworkItem item in ArchipelagoConnection.session.Items.AllItemsReceived)
            {
                //Plugin.PatchLogger.LogInfo($"{item.Item}: ");
                if (receivedIDs.ContainsKey(item.Item))
                {
                    receivedIDs[item.Item] += 1;
                } else
                {
                    receivedIDs.Add(item.Item, 1);
                }
            }

            foreach (EndingEffectsEnum effect in Enum.GetValues(typeof(EndingTypes)).Cast<EndingTypes>().ToList())
            {
                EndingEffect ee = EndingEffect.InitFromEnum(effect);
                int effectReceived = receivedIDs.ContainsKey(Plugin.reventureItemOffset + (long) effect) ? receivedIDs[Plugin.reventureItemOffset + (long) effect] : 0;
                //Plugin.PatchLogger.LogInfo($"{effect}: {effectReceived}");
                if (ee != null)
                {
                    ee.ActivateEffect(effectReceived);
                    if (effectReceived != 0)
                    {
                        enabledEffect.Add(effect);
                    }
                }
            }

            // Handle Gems
            if (ArchipelagoConnection.gemsRandomized == 1)
            {
                //Randomized Gems, Disable Vanilla Locations
                GameObject.Find("World/Items/TetraGems/EarthGem").SetActive(false);
                GameObject.Find("World/Items/TetraGems/WindGem").SetActive(false);
                GameObject.Find("World/Items/TetraGems/WaterGem").SetActive(false);
                GameObject.Find("World/Items/TetraGems/FireGem").SetActive(false);
            }

            //Update UI
            GameObject versionTextObj = GameObject.Find("Canvasses/OverlayCanvas/GamePanel/ZonePanel/zoneText/versionText");
            TextMeshProUGUI versionText = versionTextObj.GetComponent<TextMeshProUGUI>();
            versionText.SetText($"{versionText.text}; Rando: {MyPluginInfo.PLUGIN_VERSION}");

            //Permanent changes
            //Disable cannon ending requirement and Add the missing ones to castle cannon
            Cannon townToShopCannon = GameObject.Find("World/Interactables/Cannons/TownToShopCannon/33_ShootCannonballToShop_End").GetComponent<Cannon>();
            ((EndingCountRequirement) townToShopCannon.requirementsToFail[0]).endingsUnlockedCount = 0;
            Cannon fortressToTownCannon = GameObject.Find("World/Interactables/Cannons/FortressToTownCannon/34_ShootCannonballToTown_End").GetComponent<Cannon>();
            ((EndingCountRequirement) fortressToTownCannon.requirementsToFail[0]).endingsUnlockedCount = 0;
            Cannon shopToFortress = GameObject.Find("World/Interactables/Cannons/ShopToFortressCannon/32_ShootCannonballToCastle_End").GetComponent<Cannon>();
            ((EndingCountRequirement) shopToFortress.requirementsToFail[2]).endingsUnlockedCount = 0;
            Cannon castleToDarkCastle = GameObject.Find("World/Interactables/Cannons/CastleToFortressCannon/CannonContents").GetComponent<Cannon>();
            castleToDarkCastle.requirementsToFail = shopToFortress.requirementsToFail;

            // Change Ultimate Door signs texts
            DisplayChat[] signLeft = Resources.FindObjectsOfTypeAll<DisplayChat>().Where(obj => obj.transform.position.x == 194.0 && obj.transform.position.y == -24.0).ToArray();
            DisplayChat[] signRight= Resources.FindObjectsOfTypeAll<DisplayChat>().Where(obj => obj.transform.position.x == 201.0 && obj.transform.position.y == -24.0).ToArray();
            //Perseverance and Patience yield the ultimate reward.
            if (ArchipelagoConnection.requiredEndings == 0)
            {
                signLeft[0].textToDisplay = "Perseverance and Patience yield... Wait ZERO? REALLY?";
            } else if (ArchipelagoConnection.requiredEndings == 1)
            {
                signLeft[0].textToDisplay = "Perseverance and Patience yield the ultimate reward. Or 1 Ending will do to";
            } else
            {
                signLeft[0].textToDisplay = "Perseverance and Patience yield the ultimate reward. Or " + ArchipelagoConnection.requiredEndings +" Endings will do to";
            }
            signLeft[0].useString = true;
            //This temple is guarded by the 4 gems that keep nature in balance: Earth, Water, Wind and Fire
            int requiredGems = (ArchipelagoConnection.gemsAmount * ArchipelagoConnection.gemsRequired) / 100;
            string gemText = "";
            if (requiredGems == 0)
            {
                gemText = "This temple is not guarded.";

            } else if (requiredGems == 1)
            {
                gemText = "This temple is guarded by the 1 gem that keeps nature in balance: Earth";
            } else if (requiredGems == 2)
            {
                gemText = "This temple is guarded by the 2 gems that keep nature in balance: Earth and Water";
            } else if (requiredGems == 3)
            {
                gemText = "This temple is guarded by the 3 gems that keep nature in balance: Earth, Water and Wind";
            } else if (requiredGems == 4)
            {
                gemText = "This temple is guarded by the 4 gems that keep nature in balance: Earth, Water, Wind and Fire";
            } else
            {
                gemText = "This temple is guarded by the " + requiredGems + " gems that keep nature in balance: Earth, Water, Wind, Fire and more";
            }
            signRight[0].textToDisplay = gemText;
            signRight[0].useString = true;
            return enabledEffect;
        }

        public bool LoadState()
        {
            try
            {
                randomization = JsonConvert.DeserializeObject<Dictionary<EndingTypes, EndingEffectsEnum>>(File.ReadAllText("randomizer.txt"));
                return true;
            } catch (Exception e)
            {
                Plugin.PatchLogger.LogInfo($"{e}");
                return false;
            }
        }

        public void StoreState()
        {
            string json = JsonConvert.SerializeObject(randomization);
            File.WriteAllText("randomizer.txt", json);
        }

        public override string ToString()
        {
            string o = "";
            foreach (KeyValuePair<EndingTypes, EndingEffectsEnum> entry in randomization)
            {
                o += $"{entry.Key}: {entry.Value}\n";
            }
            return o;
        }
    }
}
