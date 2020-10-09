using SRTPluginProviderRECVX.Enumerations;
using SRTPluginProviderRECVX.Models;
using System;
using System.ComponentModel;

namespace SRTPluginUIRECVXWPF.Models
{
    public class InventoryItem : BaseNotifyModel
    {
        public const int ImageWidth = 96;
        public const int ImageHeight = 96;

        public InventoryEntry Entry { get; set; }

        public ClippingModel Clipping { get; } = new ClippingModel(ImageWidth, ImageHeight);

        public InventoryItem(InventoryEntry entry)
        {
            Entry = entry;
            Entry.PropertyChanged += UpdatePropertyEvent;

            UpdateClipping();
        }

        private void UpdatePropertyEvent(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "UpdateEntry")
                UpdateClipping();
        }

        private void UpdateClipping()
        {
            switch (Entry.Type)
            {
                // Row 1
                case ItemEnumeration.RocketLauncher:
                    Clipping.Update(1, 0, Entry.SlotSize);
                    break;
                case ItemEnumeration.AssaultRifle:
                    Clipping.Update(3, 0, Entry.SlotSize);
                    break;
                case ItemEnumeration.SniperRifle:
                    Clipping.Update(5, 0, Entry.SlotSize);
                    break;

                // Row 2
                case ItemEnumeration.Shotgun:
                    Clipping.Update(1, 1, Entry.SlotSize);
                    break;
                case ItemEnumeration.HandgunGlock17:
                    Clipping.Update(2, 1, Entry.SlotSize);
                    break;
                case ItemEnumeration.GrenadeLauncher:
                    Clipping.Update(3, 1, Entry.SlotSize);
                    break;
                case ItemEnumeration.BowGun:
                    Clipping.Update(4, 1, Entry.SlotSize);
                    break;
                case ItemEnumeration.CombatKnife:
                    Clipping.Update(5, 1, Entry.SlotSize);
                    break;
                case ItemEnumeration.Handgun:
                    Clipping.Update(6, 1, Entry.SlotSize);
                    break;

                // Row 3
                case ItemEnumeration.CustomHandgun:
                    Clipping.Update(0, 2, Entry.SlotSize);
                    break;
                case ItemEnumeration.LinearLauncher:
                    Clipping.Update(1, 2, Entry.SlotSize);
                    break;
                case ItemEnumeration.HandgunBullets:
                    Clipping.Update(2, 2, Entry.SlotSize);
                    break;
                case ItemEnumeration.MagnumBullets:
                    Clipping.Update(3, 2, Entry.SlotSize);
                    break;
                case ItemEnumeration.MagnumBulletsInsideCase:
                    Clipping.Update(3, 2, Entry.SlotSize);
                    break;
                case ItemEnumeration.ShotgunShells:
                    Clipping.Update(4, 2, Entry.SlotSize);
                    break;
                case ItemEnumeration.GrenadeRounds:
                    Clipping.Update(5, 2, Entry.SlotSize);
                    break;
                case ItemEnumeration.AcidRounds:
                    Clipping.Update(6, 2, Entry.SlotSize);
                    break;

                // Row 4
                case ItemEnumeration.FlameRounds:
                    Clipping.Update(0, 3, Entry.SlotSize);
                    break;
                case ItemEnumeration.BowGunArrows:
                    Clipping.Update(1, 3, Entry.SlotSize);
                    break;
                case ItemEnumeration.M93RPart:
                    Clipping.Update(2, 3, Entry.SlotSize);
                    break;
                case ItemEnumeration.FAidSpray:
                    Clipping.Update(3, 3, Entry.SlotSize);
                    break;
                case ItemEnumeration.GreenHerb:
                    Clipping.Update(4, 3, Entry.SlotSize);
                    break;
                case ItemEnumeration.RedHerb:
                    Clipping.Update(5, 3, Entry.SlotSize);
                    break;
                case ItemEnumeration.BlueHerb:
                    Clipping.Update(6, 3, Entry.SlotSize);
                    break;

                // Row 5
                case ItemEnumeration.MixedHerb2Green:
                    Clipping.Update(0, 4, Entry.SlotSize);
                    break;
                case ItemEnumeration.MixedHerbRedGreen:
                    Clipping.Update(1, 4, Entry.SlotSize);
                    break;
                case ItemEnumeration.MixedHerbBlueGreen:
                    Clipping.Update(2, 4, Entry.SlotSize);
                    break;
                case ItemEnumeration.MixedHerb2GreenBlue:
                    Clipping.Update(3, 4, Entry.SlotSize);
                    break;
                case ItemEnumeration.MixedHerb3Green:
                    Clipping.Update(4, 4, Entry.SlotSize);
                    break;
                case ItemEnumeration.MixedHerbGreenBlueRed:
                    Clipping.Update(5, 4, Entry.SlotSize);
                    break;

                // Row 6
                case ItemEnumeration.InkRibbon:
                    Clipping.Update(0, 5, Entry.SlotSize);
                    break;
                case ItemEnumeration.Magnum:
                    Clipping.Update(1, 5, Entry.SlotSize);
                    break;
                case ItemEnumeration.GoldLugers:
                    Clipping.Update(2, 5, Entry.SlotSize);
                    break;
                case ItemEnumeration.SubMachineGun:
                    Clipping.Update(4, 5, Entry.SlotSize);
                    break;
                case ItemEnumeration.BowGunPowder:
                    Clipping.Update(6, 5, Entry.SlotSize);
                    break;

                // Row 7
                case ItemEnumeration.GunPowderArrow:
                    Clipping.Update(0, 6, Entry.SlotSize);
                    break;
                case ItemEnumeration.BOWGasRounds:
                    Clipping.Update(1, 6, Entry.SlotSize);
                    break;
                case ItemEnumeration.MGunBullets:
                    Clipping.Update(2, 6, Entry.SlotSize);
                    break;
                case ItemEnumeration.GasMask:
                    Clipping.Update(3, 6, Entry.SlotSize);
                    break;
                case ItemEnumeration.RifleBullets:
                    Clipping.Update(4, 6, Entry.SlotSize);
                    break;
                case ItemEnumeration.DuraluminCaseUnused:
                    Clipping.Update(5, 6, Entry.SlotSize);
                    break;
                case ItemEnumeration.ARifleBullets:
                    Clipping.Update(6, 6, Entry.SlotSize);
                    break;

                // Row 8
                case ItemEnumeration.AlexandersPierce:
                    Clipping.Update(0, 7, Entry.SlotSize);
                    break;
                case ItemEnumeration.AlexandersJewel:
                    Clipping.Update(1, 7, Entry.SlotSize);
                    break;
                case ItemEnumeration.AlfredsRing:
                    Clipping.Update(2, 7, Entry.SlotSize);
                    break;
                case ItemEnumeration.AlfredsJewel:
                    Clipping.Update(3, 7, Entry.SlotSize);
                    break;
                case ItemEnumeration.LugerReplica:
                    Clipping.Update(4, 7, Entry.SlotSize);
                    break;
                case ItemEnumeration.FamilyPicture:
                    Clipping.Update(5, 7, Entry.SlotSize);
                    break;
                case ItemEnumeration.CalicoBullets:
                    Clipping.Update(6, 7, Entry.SlotSize);
                    break;

                // Row 9
                case ItemEnumeration.Lockpick:
                    Clipping.Update(0, 8, Entry.SlotSize);
                    break;
                case ItemEnumeration.GlassEye:
                    Clipping.Update(1, 8, Entry.SlotSize);
                    break;
                case ItemEnumeration.PianoRoll:
                    Clipping.Update(2, 8, Entry.SlotSize);
                    break;
                case ItemEnumeration.SteeringWheel:
                    Clipping.Update(3, 8, Entry.SlotSize);
                    break;
                case ItemEnumeration.CraneKey:
                    Clipping.Update(4, 8, Entry.SlotSize);
                    break;
                case ItemEnumeration.Lighter:
                    Clipping.Update(5, 8, Entry.SlotSize);
                    break;
                case ItemEnumeration.EaglePlate:
                    Clipping.Update(6, 8, Entry.SlotSize);
                    break;

                // Row 10
                case ItemEnumeration.SidePack:
                    Clipping.Update(0, 9, Entry.SlotSize);
                    break;
                case ItemEnumeration.MapRoll:
                    Clipping.Update(1, 9, Entry.SlotSize);
                    break;
                case ItemEnumeration.HawkEmblem:
                    Clipping.Update(2, 9, Entry.SlotSize);
                    break;
                case ItemEnumeration.QueenAntObject:
                    Clipping.Update(3, 9, Entry.SlotSize);
                    break;
                case ItemEnumeration.KingAntObject:
                    Clipping.Update(4, 9, Entry.SlotSize);
                    break;
                case ItemEnumeration.BiohazardCard:
                    Clipping.Update(5, 9, Entry.SlotSize);
                    break;
                case ItemEnumeration.DuraluminCaseM93RParts:
                    Clipping.Update(6, 9, Entry.SlotSize);
                    break;
                case ItemEnumeration.DuraluminCaseBowGunPowder:
                    Clipping.Update(6, 9, Entry.SlotSize);
                    break;
                case ItemEnumeration.DuraluminCaseMagnumRounds:
                    Clipping.Update(6, 9, Entry.SlotSize);
                    break;

                // Row 11
                case ItemEnumeration.Detonator:
                    Clipping.Update(0, 10, Entry.SlotSize);
                    break;
                case ItemEnumeration.ControlLever:
                    Clipping.Update(1, 10, Entry.SlotSize);
                    break;
                case ItemEnumeration.GoldDragonfly:
                    Clipping.Update(2, 10, Entry.SlotSize);
                    break;
                case ItemEnumeration.SilverKey:
                    Clipping.Update(3, 10, Entry.SlotSize);
                    break;
                case ItemEnumeration.GoldKey:
                    Clipping.Update(4, 10, Entry.SlotSize);
                    break;
                case ItemEnumeration.ArmyProof:
                    Clipping.Update(5, 10, Entry.SlotSize);
                    break;
                case ItemEnumeration.NavyProof:
                    Clipping.Update(6, 10, Entry.SlotSize);
                    break;

                // Row 12
                case ItemEnumeration.AirForceProof:
                    Clipping.Update(0, 11, Entry.SlotSize);
                    break;
                case ItemEnumeration.KeyWithTag:
                    Clipping.Update(1, 11, Entry.SlotSize);
                    break;
                case ItemEnumeration.IDCard:
                    Clipping.Update(2, 11, Entry.SlotSize);
                    break;
                case ItemEnumeration.Map:
                    Clipping.Update(3, 11, Entry.SlotSize);
                    break;
                case ItemEnumeration.AirportKey:
                    Clipping.Update(4, 11, Entry.SlotSize);
                    break;
                case ItemEnumeration.EmblemCard:
                    Clipping.Update(5, 11, Entry.SlotSize);
                    break;
                case ItemEnumeration.SkeletonPicture:
                    Clipping.Update(6, 11, Entry.SlotSize);
                    break;

                // Row 13
                case ItemEnumeration.MusicBoxPlate:
                    Clipping.Update(0, 12, Entry.SlotSize);
                    break;
                case ItemEnumeration.GoldDragonflyNoWings:
                    Clipping.Update(1, 12, Entry.SlotSize);
                    break;
                case ItemEnumeration.Album:
                    Clipping.Update(2, 12, Entry.SlotSize);
                    break;
                case ItemEnumeration.Halberd:
                    Clipping.Update(3, 12, Entry.SlotSize);
                    break;
                case ItemEnumeration.Extinguisher:
                    Clipping.Update(4, 12, Entry.SlotSize);
                    break;
                case ItemEnumeration.Briefcase:
                    Clipping.Update(5, 12, Entry.SlotSize);
                    break;
                case ItemEnumeration.PadlockKey:
                    Clipping.Update(6, 12, Entry.SlotSize);
                    break;

                // Row 14
                case ItemEnumeration.TG01:
                    Clipping.Update(0, 13, Entry.SlotSize);
                    break;
                case ItemEnumeration.SpAlloyEmblem:
                    Clipping.Update(1, 13, Entry.SlotSize);
                    break;
                case ItemEnumeration.ValveHandle:
                    Clipping.Update(2, 13, Entry.SlotSize);
                    break;
                case ItemEnumeration.OctaValveHandle:
                    Clipping.Update(3, 13, Entry.SlotSize);
                    break;
                case ItemEnumeration.MachineRoomKey:
                    Clipping.Update(4, 13, Entry.SlotSize);
                    break;
                case ItemEnumeration.MiningRoomKey:
                    Clipping.Update(5, 13, Entry.SlotSize);
                    break;
                case ItemEnumeration.BarCodeSticker:
                    Clipping.Update(6, 13, Entry.SlotSize);
                    break;

                // Row 15
                case ItemEnumeration.SterileRoomKey:
                    Clipping.Update(0, 14, Entry.SlotSize);
                    break;
                case ItemEnumeration.DoorKnob:
                    Clipping.Update(1, 14, Entry.SlotSize);
                    break;
                case ItemEnumeration.BatteryPack:
                    Clipping.Update(2, 14, Entry.SlotSize);
                    break;
                case ItemEnumeration.HemostaticWire:
                    Clipping.Update(3, 14, Entry.SlotSize);
                    break;
                case ItemEnumeration.TurnTableKey:
                    Clipping.Update(4, 14, Entry.SlotSize);
                    break;
                case ItemEnumeration.ChemStorageKey:
                    Clipping.Update(5, 14, Entry.SlotSize);
                    break;
                case ItemEnumeration.ClementAlpha:
                    Clipping.Update(6, 14, Entry.SlotSize);
                    break;

                // Row 16
                case ItemEnumeration.ClementSigma:
                    Clipping.Update(0, 15, Entry.SlotSize);
                    break;
                case ItemEnumeration.TankObject:
                    Clipping.Update(1, 15, Entry.SlotSize);
                    break;
                case ItemEnumeration.SpAlloyEmblemUnused:
                    Clipping.Update(2, 15, Entry.SlotSize);
                    break;
                case ItemEnumeration.ClementMixture:
                    Clipping.Update(3, 15, Entry.SlotSize);
                    break;
                case ItemEnumeration.RustedSword:
                    Clipping.Update(4, 15, Entry.SlotSize);
                    break;
                case ItemEnumeration.Hemostatic:
                    Clipping.Update(5, 15, Entry.SlotSize);
                    break;
                case ItemEnumeration.SecurityCard:
                    Clipping.Update(6, 15, Entry.SlotSize);
                    break;

                // Row 17
                case ItemEnumeration.SecurityFile:
                    Clipping.Update(0, 16, Entry.SlotSize);
                    break;
                case ItemEnumeration.AlexiasChoker:
                    Clipping.Update(1, 16, Entry.SlotSize);
                    break;
                case ItemEnumeration.AlexiasJewel:
                    Clipping.Update(2, 16, Entry.SlotSize);
                    break;
                case ItemEnumeration.QueenAntRelief:
                    Clipping.Update(3, 16, Entry.SlotSize);
                    break;
                case ItemEnumeration.KingAntRelief:
                    Clipping.Update(4, 16, Entry.SlotSize);
                    break;
                case ItemEnumeration.RedJewel:
                    Clipping.Update(5, 16, Entry.SlotSize);
                    break;
                case ItemEnumeration.BlueJewel:
                    Clipping.Update(6, 16, Entry.SlotSize);
                    break;

                // Row 18
                case ItemEnumeration.Socket:
                    Clipping.Update(0, 17, Entry.SlotSize);
                    break;
                case ItemEnumeration.SqValveHandle:
                    Clipping.Update(1, 17, Entry.SlotSize);
                    break;
                case ItemEnumeration.Serum:
                    Clipping.Update(2, 17, Entry.SlotSize);
                    break;
                case ItemEnumeration.EarthenwareVase:
                    Clipping.Update(3, 17, Entry.SlotSize);
                    break;
                case ItemEnumeration.PaperWeight:
                    Clipping.Update(4, 17, Entry.SlotSize);
                    break;
                case ItemEnumeration.SilverDragonflyNoWings:
                    Clipping.Update(5, 17, Entry.SlotSize);
                    break;
                case ItemEnumeration.SilverDragonfly:
                    Clipping.Update(6, 17, Entry.SlotSize);
                    break;

                // Row 19
                case ItemEnumeration.WingObject:
                    Clipping.Update(0, 18, Entry.SlotSize);
                    break;
                case ItemEnumeration.Crystal:
                    Clipping.Update(1, 18, Entry.SlotSize);
                    break;
                case ItemEnumeration.GoldDragonfly1Wing:
                    Clipping.Update(2, 18, Entry.SlotSize);
                    break;
                case ItemEnumeration.GoldDragonfly2Wings:
                    Clipping.Update(3, 18, Entry.SlotSize);
                    break;
                case ItemEnumeration.GoldDragonfly3Wings:
                    Clipping.Update(4, 18, Entry.SlotSize);
                    break;
                case ItemEnumeration.File:
                    Clipping.Update(5, 18, Entry.SlotSize);
                    break;
                case ItemEnumeration.PlantPot:
                    Clipping.Update(6, 18, Entry.SlotSize);
                    break;

                // Row 20
                case ItemEnumeration.PictureB:
                    Clipping.Update(0, 19, Entry.SlotSize);
                    break;
                case ItemEnumeration.M1P:
                    Clipping.Update(1, 19, Entry.SlotSize);
                    break;
                case ItemEnumeration.BowGunPowderUnused:
                    Clipping.Update(3, 19, Entry.SlotSize);
                    break;
                case ItemEnumeration.EnhancedHandgun:
                    Clipping.Update(4, 19, Entry.SlotSize);
                    break;
                case ItemEnumeration.PlayingManual:
                    Clipping.Update(6, 19, Entry.SlotSize);
                    break;

                // Shares Icon (Unused Content)
                case ItemEnumeration.PrisonersDiary:
                    Clipping.Update(4, 7, Entry.SlotSize);
                    break;
                case ItemEnumeration.DirectorsMemo:
                    Clipping.Update(5, 7, Entry.SlotSize);
                    break;
                case ItemEnumeration.Instructions:
                    Clipping.Update(6, 7, Entry.SlotSize);
                    break;
                case ItemEnumeration.AlfredsMemo:
                    Clipping.Update(3, 15, Entry.SlotSize);
                    break;
                case ItemEnumeration.BoardClip:
                    Clipping.Update(6, 19, Entry.SlotSize);
                    break;

                // No Icon (Unused Content)
                case ItemEnumeration.None:
                default:
                    Clipping.Update(0, 0, Entry.SlotSize);
                    break;
            }
        }
    }
}