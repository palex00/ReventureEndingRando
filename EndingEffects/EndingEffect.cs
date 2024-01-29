﻿using Atto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace ReventureEndingRando.EndingEffects
{
    abstract class EndingEffect
    {
        public abstract void ActivateEffect(int effectsReceived);

        public static EndingEffect InitFromEnum(EndingEffectsEnum e)
        {
            switch (e)
            {
                case EndingEffectsEnum.SpawnSwordPedestalItem:
                    return new SpawnSwordPedestal();
                case EndingEffectsEnum.SpawnSwordChest:
                    return new SpawnSwordChest();
                case EndingEffectsEnum.SpawnShovelChest:
                    return new SpawnItemSimple("World/Items/TreasureChest_Shovel");
                case EndingEffectsEnum.SpawnBoomerang:
                    return new SpawnItemSimple("World/Items/Item Boomerang");
                case EndingEffectsEnum.SpawnMapChest:
                    return new SpawnItemSimple("World/Items/TreasureChest_Map");
                case EndingEffectsEnum.SpawnCompassChest:
                    return new SpawnItemSimple("World/Items/TreasureChest_Compass");
                case EndingEffectsEnum.SpawnWhistleChest:
                    return new SpawnItemSimple("World/Items/TreasureChest_WhistleOfTime");
                case EndingEffectsEnum.SpawnBurgerChest:
                    return new SpawnItemSimple("World/Items/TreasureChest_Pizza");
                case EndingEffectsEnum.SpawnDarkstoneChest:
                    return new SpawnItemSimple("World/Interactables/Levers/RightLeversPlatform/TreasureChest_DarkStone");
                case EndingEffectsEnum.SpawnHookChest:
                    return new SpawnItemSimple("World/Items/TreasureChest_Hook");
                case EndingEffectsEnum.SpawnFishingRodChest:
                    return new SpawnItemSimple("World/Items/TreasureChest_FishingRod");
                case EndingEffectsEnum.SpawnLavaTrinketChest:
                    return new SpawnItemSimple("World/Items/TreasureChest_Trinket");
                case EndingEffectsEnum.SpawnMrHugsChest:
                    return new SpawnItemSimple("World/Items/TreasureChest_MrHugs");
                case EndingEffectsEnum.SpawnBombsChest:
                    return new SpawnItemSimple("World/Items/TreasureChest_Bomb");
                case EndingEffectsEnum.SpawnShieldChest:
                    return new SpawnItemSimple("World/Items/TreasureChest_Shield");
                case EndingEffectsEnum.SpawnNukeItem:
                    return new SpawnItemSimple("World/Items/TreasureChest_Cannonball");
                case EndingEffectsEnum.SpawnPrincessItem:
                    return new SpawnPrincess();
                case EndingEffectsEnum.SpawnAnvilItem:
                    return new SpawnItemSimple("World/Items/AnvilRope/SwingRope/Item Anvil");
                case EndingEffectsEnum.SpawnStrawberry:
                    return new SpawnItemSimple("World/Items/29_ClimbMountain_End");
                case EndingEffectsEnum.UnlockShopCannon:
                    return new SpawnItemSimple("World/Interactables/Cannons/ShopToFortressCannon");
                case EndingEffectsEnum.UnlockCastleToShopCannon:
                    return new SpawnItemSimple("World/Interactables/Cannons/TownToShopCannon");
                case EndingEffectsEnum.UnlockDarkCastleCannon:
                    return new SpawnItemSimple("World/Interactables/Cannons/FortressToTownCannon");
                case EndingEffectsEnum.UnlockCastleToDarkCastleCannon:
                    return new SpawnItemSimple("World/Interactables/Cannons/CastleToFortressCannon");
                case EndingEffectsEnum.UnlockGeyserVolcanoe:
                    return new SpawnItemSimple("World/Interactables/Cannons/CraterCannons/ShootFromCraterCannon");
                case EndingEffectsEnum.UnlockGeyserWaterfall:
                    return new SpawnItemSimple("World/Interactables/Cannons/CraterCannons/ShootFromCraterCannon (1)");
                case EndingEffectsEnum.UnlockGeyserDesert1:
                    return new SpawnItemSimple("World/Interactables/TrollCannons/CannonToDesertStart");
                case EndingEffectsEnum.UnlockGeyserDesert2:
                    return new SpawnItemSimple("World/Interactables/TrollCannons/CannonToTop");
                case EndingEffectsEnum.UnlockElevatorButton:
                    return new SpawnItemSimple("World/Interactables/Elevator Zone/CastleElevator/ElevatorButton");
                case EndingEffectsEnum.UnlockCallElevatorButtons:
                    return new SpawnItemSimple("World/Interactables/Elevator Zone/CallElevatorButtons");
                case EndingEffectsEnum.UnlockMirrorPortal:
                    return new SpawnItemSimple("World/PersistentElements/PrincessWardrove/PrincessPortal");
                case EndingEffectsEnum.UnlockFairyPortal:
                    return new SpawnItemSimple("World/PersistentElements/TownPortals");
                case EndingEffectsEnum.GrowVine:
                    return new SpawnVine();
                case EndingEffectsEnum.OpenCastleFloor:
                    return new OpenCastleHole();
                case EndingEffectsEnum.SpawnDragon:
                    return new SpawnDragon();
                case EndingEffectsEnum.SpawnShopkeeper:
                    return new SpawnItemSimple("World/NPCs/Shopkeepers");
                case EndingEffectsEnum.SpawnMimic:
                    return new SpawnMimic();
                case EndingEffectsEnum.SpawnKing:
                    return new SpawnKing();
                case EndingEffectsEnum.GrowChicken:
                    return new GrowChicken();
                case EndingEffectsEnum.EnableCloset:
                    return new SpawnItemSimple("World/PersistentElements/Wardrove");
                case EndingEffectsEnum.BuildStatue:
                    return new SpawnItemSimple("World/PersistentElements/PrincessStatue");
                case EndingEffectsEnum.AddPC:
                    return new AddPC();
                case EndingEffectsEnum.SpawnDolphins:
                    return new SpawnItemSimple("World/PersistentElements/Dolphins");
                case EndingEffectsEnum.SpawnMimicPet:
                    return new SpawnItemSimple("World/PersistentElements/MimicKennel");
                case EndingEffectsEnum.UnlockFacePlantStone:
                    return new UnlockFacePlantStone();
                case EndingEffectsEnum.EarthGem:
                    return new UnlockMilestone(MilestoneTypes.GotEarthGem);
                case EndingEffectsEnum.FireGem:
                    return new UnlockMilestone(MilestoneTypes.GotFireGem);
                case EndingEffectsEnum.WaterGem:
                    return new UnlockMilestone(MilestoneTypes.GotWaterGem);
                case EndingEffectsEnum.WindGem:
                    return new UnlockMilestone(MilestoneTypes.GotWindGem);
                default:
                    return null;
            }
        }
    }

    class SpawnItemSimple : EndingEffect
    {
        string name;

        public SpawnItemSimple(string _name)
        {
            name = _name;
        }

        public override void ActivateEffect(int effectsReceived)
        {
            GameObject simpleItem = GameObject.Find(name);
            AlterWithRestrictions awRestrictions = simpleItem.GetComponent<AlterWithRestrictions>();
            if (awRestrictions != null)
            {
                GameObject.Destroy(awRestrictions);
            }
            simpleItem.SetActive(effectsReceived > 0);
        }
    }

    class SpawnKing : EndingEffect
    {
        public override void ActivateEffect(int effectsReceived)
        {
            GameObject king = GameObject.Find("World/NPCs/KindomNPCs/TheKing");
            GameObject feedEnding = GameObject.Find("World/Interactables/98_FeedTheKing_End");
            king.SetActive(effectsReceived > 0);
            feedEnding.SetActive(effectsReceived > 0);
        }
    }
    class SpawnDragon : EndingEffect
    {
        public override void ActivateEffect(int effectsReceived)
        {
            GameObject dragon = GameObject.Find("World/NPCs/Dragon");
            GameObject roastedEnding = GameObject.Find("World/EndTriggers/13_RoastedByDragon_End");
            GameObject fireTrinketEnding = GameObject.Find("World/EndTriggers/47_DragonWithFireTrinket_End");
            GameObject dateEnding = GameObject.Find("World/EndTriggers/49_DatePrincessAndDragon_End");
            GameObject shieldEnding= GameObject.Find("World/EndTriggers/57_DragonWithShield_End");
            GameObject shieldTrinketEnding = GameObject.Find("World/EndTriggers/58_DragonWithShieldAndFireTrinket_End");
            dragon.SetActive(effectsReceived > 0);
            roastedEnding.SetActive(effectsReceived > 0);
            fireTrinketEnding.SetActive(effectsReceived > 0);
            dateEnding.SetActive(effectsReceived > 0);
            shieldEnding.SetActive(effectsReceived > 0);
            shieldTrinketEnding.SetActive(effectsReceived > 0);
        }
    }

    class SpawnMimic : EndingEffect
    {
        public override void ActivateEffect(int effectsReceived)
        {
            GameObject mimic = GameObject.Find("World/NPCs/FakePrincess");
            GameObject feedEnding = GameObject.Find("World/EndTriggers/84_FeedTheMimic_End");
            mimic.SetActive(effectsReceived > 0);
            feedEnding.SetActive(effectsReceived > 0);
        }
    }

    class SpawnPrincess : EndingEffect
    {
        public override void ActivateEffect(int effectsReceived)
        {
            GameObject princess = GameObject.Find("World/NPCs/Item Princess");
            GameObject ventEnding = GameObject.Find("World/EndTriggers/24_AirDuctsAccident_End");
            princess.SetActive(effectsReceived > 0);
            ventEnding.SetActive(effectsReceived > 0);
        }
    }

    class SpawnSwordPedestal: EndingEffect
    {
        public override void ActivateEffect(int effectsReceived)
        {
            GameObject itemSword = GameObject.Find("World/Items/Sword Item Pedestal/Item Sword");
            GameObject pedestal = GameObject.Find("World/Items/Sword Item Pedestal");
            itemSword.SetActive(effectsReceived > 0);
            pedestal.SetActive(effectsReceived > 0);
        }
    }

    class SpawnSwordChest : EndingEffect
    {
        public override void ActivateEffect(int effectsReceived)
        {
            //TODO remove chest completely
            GameObject swordChest = GameObject.Find("World/Items/SwordAtHome/TreasureChest_Sword");
            GameObject openChest = GameObject.Find("World/Items/SwordAtHome/OpenChest");
            GameObject swordAtHome = GameObject.Find("World/Items/SwordAtHome");
            swordAtHome.SetActive(effectsReceived > 0);
            openChest.SetActive(effectsReceived == 0);
            swordChest.SetActive(effectsReceived > 0);
        }
    }

    class SpawnVine : EndingEffect
    {
        public override void ActivateEffect(int effectsReceived)
        {
            GameObject badCrops = GameObject.Find("World/BackgroundElements/BadCrops");
            GameObject goodCrops = GameObject.Find("World/PersistentElements/GoodCrops");
            GameObject cropsCloud = GameObject.Find("World/PersistentElements/CropsClouds");
            badCrops.SetActive(effectsReceived == 0);
            goodCrops.SetActive(effectsReceived > 0);
            cropsCloud.SetActive(effectsReceived == 0);
        }
    }

    class OpenCastleHole : EndingEffect
    {
        public override void ActivateEffect(int effectsReceived)
        {
            GameObject castleHole = GameObject.Find("World/PersistentElements/Castlehole");
            castleHole.SetActive(effectsReceived == 0);
            // Keep this enabled, by vanilla settings. The boulder has no collision, but the ending is still possible
            //GameObject boulderUnderCastle = GameObject.Find("World/Boulders/BoulderUnderCastle");
            //boulderUnderCastle.SetActive(effectsReceived == 0);
        }
    }
    class UnlockFacePlantStone : EndingEffect
    {
        public override void ActivateEffect(int effectsReceived)
        {
            GameObject ending = GameObject.Find("Cinematics/75_LonkFaceplant_End");
            EndTrigger trigger = ending.GetComponent<EndTrigger>();
            EndingCountRequirement endingReq = (EndingCountRequirement) trigger.triggerRequirements[2];
            endingReq.endingsUnlockedCount = 0;
            ending.SetActive(effectsReceived > 0);
        }
    }


    class AddPC : EndingEffect
    {
        public override void ActivateEffect(int effectsReceived)
        {
            GameObject pc = GameObject.Find("World/PersistentElements/Lonk's PC");
            GameObject pcalt = GameObject.Find("World/PersistentElements/Lonk's PC Alt");
            pc.SetActive(effectsReceived > 0);
            pcalt.SetActive(effectsReceived > 0);
        }
    }

    class GrowChicken : EndingEffect
    {
        public override void ActivateEffect(int effectsReceived)
        {
            GameObject phase0 = GameObject.Find("World/PersistentElements/ChickenNest/Phase0");
            GameObject phase1 = GameObject.Find("World/PersistentElements/ChickenNest/Phase1");
            GameObject phase2 = GameObject.Find("World/PersistentElements/ChickenNest/Phase2");
            GameObject phase3 = GameObject.Find("World/PersistentElements/ChickenNest/Phase3");
            GameObject.Destroy(phase0.GetComponent<AlterWithRestrictions>());
            GameObject.Destroy(phase1.GetComponent<AlterWithRestrictions>());
            GameObject.Destroy(phase2.GetComponent<AlterWithRestrictions>());
            GameObject.Destroy(phase3.GetComponent<AlterWithRestrictions>());

            phase0.SetActive(effectsReceived == 1);
            phase1.SetActive(effectsReceived == 2);
            phase2.SetActive(effectsReceived == 3);
            phase3.SetActive(effectsReceived == 4);
        }
    }

    class UnlockMilestone : EndingEffect
    {
        MilestoneTypes milestone;

        public UnlockMilestone(MilestoneTypes _milestone)
        {
            milestone = _milestone;
        }

        public override void ActivateEffect(int effectsReceived)
        {
            IProgressionService progression = Core.Get<IProgressionService>();
            progression.UnlockMilestone(milestone);
        }
    }

    public enum EndingEffectsEnum
    {
        Nothing,
        //Item Locations
        SpawnSwordPedestalItem,
        SpawnSwordChest,
        SpawnShovelChest,
        SpawnBoomerang,
        SpawnMapChest,
        SpawnCompassChest,
        SpawnWhistleChest,
        SpawnBurgerChest,
        SpawnDarkstoneChest,
        SpawnHookChest,
        SpawnFishingRodChest,
        SpawnLavaTrinketChest,
        SpawnMrHugsChest,
        SpawnBombsChest,
        SpawnShieldChest,
        SpawnNukeItem,
        SpawnPrincessItem,
        SpawnAnvilItem,
        SpawnStrawberry,
        //Transportation/Paths
        UnlockShopCannon,
        UnlockCastleToShopCannon,
        UnlockDarkCastleCannon,
        UnlockCastleToDarkCastleCannon,
        UnlockGeyserDesert1,
        UnlockGeyserDesert2,
        UnlockGeyserVolcanoe,
        UnlockGeyserWaterfall,
        UnlockElevatorButton,
        UnlockCallElevatorButtons,
        UnlockMirrorPortal,
        UnlockFairyPortal,
        GrowVine,
        OpenCastleFloor,
        UnlockFacePlantStone,
        //NPCs
        SpawnDragon,
        SpawnShopkeeper,
        SpawnMimic,
        SpawnKing,
        GrowChicken,
        //Cosmetic
        EnableCloset,
        BuildStatue,
        AddPC,
        SpawnDolphins,
        SpawnMimicPet,
        //Milestones
        EarthGem,
        FireGem,
        WaterGem,
        WindGem
    }

    //Effect ideas
    //EnableNamechange,
    //EnablePrincessNamechange
    //EnableDarkLordNamechange
    //schornstein
    //desert druckplatte
    //mimic kiste
    //princess bed
    //own bed
    //lever1 darkstone room
    //lever2 darkstone room
    //lever3 darkstone room
    //minion next to house
    //Chicken1 progressive
    //Chicken2 progressive
    //Chicken3 progressive
    //Chicken4 progressive
    //Elder NPC
    //Sewer pipe
    //red orb
    //green orb
    //blue orb
    //yellow orb
    //Altar
    //Boulder NPC
    //River Grate Button
    //Selfdestruct Button
    //Princessgate Elevator
    //Xray Goggles
}
