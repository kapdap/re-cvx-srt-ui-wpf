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

        private InventoryEntry _entry;
        public InventoryEntry Entry
        {
            get => _entry;
            set
            {
                if (!value.Equals(_entry))
                {
                    _entry = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _clipX;
        public int ClipX
        {
            get => _clipX;
            set
            {
                if (_clipX != value)
                {
                    _clipX = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _clipY;
        public int ClipY
        {
            get => _clipY;
            set
            {
                if (_clipY != value)
                {
                    _clipY = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _clipWidth;
        public int ClipWidth
        {
            get => _clipWidth;
            set
            {
                if (_clipWidth != value)
                {
                    _clipWidth = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _clipHeight;
        public int ClipHeight
        {
            get => _clipHeight;
            set
            {
                if (_clipHeight != value)
                {
                    _clipHeight = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _clipping;
        public string Clipping
        {
            get => _clipping;
            set
            {
                if (_clipping != value)
                {
                    _clipping = value;
                    OnPropertyChanged();
                }
            }
        }

        public InventoryItem(InventoryEntry entry)
        {
            Entry = entry;
            Entry.PropertyChanged += UpdatePropertyEvent;

            UpdateClipping();
        }

        private void UpdatePropertyEvent(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "UpdateEntry")
                return;

            UpdateClipping();
        }

        private void UpdateClipping()
        {
            int[] clipping = GetClipping();

            ClipX = clipping[0];
            ClipY = clipping[1];
            ClipWidth = clipping[2];
            ClipHeight = clipping[3];

            Clipping = String.Format("{0},{1},{2},{3}", ClipX, ClipY, ClipWidth, ClipHeight);
        }

        private int[] GetClipping()
        {
            switch (Entry.Type)
            {
                // Row 1
                case ItemEnumeration.RocketLauncher:
                    return GenerateClipping(1, 0, Entry.SlotSize);
                case ItemEnumeration.AssaultRifle:
                    return GenerateClipping(3, 0, Entry.SlotSize);
                case ItemEnumeration.SniperRifle:
                    return GenerateClipping(5, 0, Entry.SlotSize);

                // Row 2
                case ItemEnumeration.Shotgun:
                    return GenerateClipping(1, 1, Entry.SlotSize);
                case ItemEnumeration.HandgunGlock17:
                    return GenerateClipping(2, 1, Entry.SlotSize);
                case ItemEnumeration.GrenadeLauncher:
                    return GenerateClipping(3, 1, Entry.SlotSize);
                case ItemEnumeration.BowGun:
                    return GenerateClipping(4, 1, Entry.SlotSize);
                case ItemEnumeration.CombatKnife:
                    return GenerateClipping(5, 1, Entry.SlotSize);
                case ItemEnumeration.Handgun:
                    return GenerateClipping(6, 1, Entry.SlotSize);

                // Row 3
                case ItemEnumeration.CustomHandgun:
                    return GenerateClipping(0, 2, Entry.SlotSize);
                case ItemEnumeration.LinearLauncher:
                    return GenerateClipping(1, 2, Entry.SlotSize);
                case ItemEnumeration.HandgunBullets:
                    return GenerateClipping(2, 2, Entry.SlotSize);
                case ItemEnumeration.MagnumBullets:
                    return GenerateClipping(3, 2, Entry.SlotSize);
                case ItemEnumeration.MagnumBulletsInsideCase:
                    return GenerateClipping(3, 2, Entry.SlotSize);
                case ItemEnumeration.ShotgunShells:
                    return GenerateClipping(4, 2, Entry.SlotSize);
                case ItemEnumeration.GrenadeRounds:
                    return GenerateClipping(5, 2, Entry.SlotSize);
                case ItemEnumeration.AcidRounds:
                    return GenerateClipping(6, 2, Entry.SlotSize);

                // Row 4
                case ItemEnumeration.FlameRounds:
                    return GenerateClipping(0, 3, Entry.SlotSize);
                case ItemEnumeration.BowGunArrows:
                    return GenerateClipping(1, 3, Entry.SlotSize);
                case ItemEnumeration.M93RPart:
                    return GenerateClipping(2, 3, Entry.SlotSize);
                case ItemEnumeration.FAidSpray:
                    return GenerateClipping(3, 3, Entry.SlotSize);
                case ItemEnumeration.GreenHerb:
                    return GenerateClipping(4, 3, Entry.SlotSize);
                case ItemEnumeration.RedHerb:
                    return GenerateClipping(5, 3, Entry.SlotSize);
                case ItemEnumeration.BlueHerb:
                    return GenerateClipping(6, 3, Entry.SlotSize);

                // Row 5
                case ItemEnumeration.MixedHerb2Green:
                    return GenerateClipping(0, 4, Entry.SlotSize);
                case ItemEnumeration.MixedHerbRedGreen:
                    return GenerateClipping(1, 4, Entry.SlotSize);
                case ItemEnumeration.MixedHerbBlueGreen:
                    return GenerateClipping(2, 4, Entry.SlotSize);
                case ItemEnumeration.MixedHerb2GreenBlue:
                    return GenerateClipping(3, 4, Entry.SlotSize);
                case ItemEnumeration.MixedHerb3Green:
                    return GenerateClipping(4, 4, Entry.SlotSize);
                case ItemEnumeration.MixedHerbGreenBlueRed:
                    return GenerateClipping(5, 4, Entry.SlotSize);

                // Row 6
                case ItemEnumeration.InkRibbon:
                    return GenerateClipping(0, 5, Entry.SlotSize);
                case ItemEnumeration.Magnum:
                    return GenerateClipping(1, 5, Entry.SlotSize);
                case ItemEnumeration.GoldLugers:
                    return GenerateClipping(2, 5, Entry.SlotSize);
                case ItemEnumeration.SubMachineGun:
                    return GenerateClipping(4, 5, Entry.SlotSize);
                case ItemEnumeration.BowGunPowder:
                    return GenerateClipping(6, 5, Entry.SlotSize);

                // Row 7
                case ItemEnumeration.GunPowderArrow:
                    return GenerateClipping(0, 6, Entry.SlotSize);
                case ItemEnumeration.BOWGasRounds:
                    return GenerateClipping(1, 6, Entry.SlotSize);
                case ItemEnumeration.MGunBullets:
                    return GenerateClipping(2, 6, Entry.SlotSize);
                case ItemEnumeration.GasMask:
                    return GenerateClipping(3, 6, Entry.SlotSize);
                case ItemEnumeration.RifleBullets:
                    return GenerateClipping(4, 6, Entry.SlotSize);
                case ItemEnumeration.DuraluminCaseUnused:
                    return GenerateClipping(5, 6, Entry.SlotSize);
                case ItemEnumeration.ARifleBullets:
                    return GenerateClipping(6, 6, Entry.SlotSize);

                // Row 8
                case ItemEnumeration.AlexandersPierce:
                    return GenerateClipping(0, 7, Entry.SlotSize);
                case ItemEnumeration.AlexandersJewel:
                    return GenerateClipping(1, 7, Entry.SlotSize);
                case ItemEnumeration.AlfredsRing:
                    return GenerateClipping(2, 7, Entry.SlotSize);
                case ItemEnumeration.AlfredsJewel:
                    return GenerateClipping(3, 7, Entry.SlotSize);
                case ItemEnumeration.LugerReplica:
                    return GenerateClipping(4, 7, Entry.SlotSize);
                case ItemEnumeration.FamilyPicture:
                    return GenerateClipping(5, 7, Entry.SlotSize);
                case ItemEnumeration.CalicoBullets:
                    return GenerateClipping(6, 7, Entry.SlotSize);

                // Row 9
                case ItemEnumeration.Lockpick:
                    return GenerateClipping(0, 8, Entry.SlotSize);
                case ItemEnumeration.GlassEye:
                    return GenerateClipping(1, 8, Entry.SlotSize);
                case ItemEnumeration.PianoRoll:
                    return GenerateClipping(2, 8, Entry.SlotSize);
                case ItemEnumeration.SteeringWheel:
                    return GenerateClipping(3, 8, Entry.SlotSize);
                case ItemEnumeration.CraneKey:
                    return GenerateClipping(4, 8, Entry.SlotSize);
                case ItemEnumeration.Lighter:
                    return GenerateClipping(5, 8, Entry.SlotSize);
                case ItemEnumeration.EaglePlate:
                    return GenerateClipping(6, 8, Entry.SlotSize);

                // Row 10
                case ItemEnumeration.SidePack:
                    return GenerateClipping(0, 9, Entry.SlotSize);
                case ItemEnumeration.MapRoll:
                    return GenerateClipping(1, 9, Entry.SlotSize);
                case ItemEnumeration.HawkEmblem:
                    return GenerateClipping(2, 9, Entry.SlotSize);
                case ItemEnumeration.QueenAntObject:
                    return GenerateClipping(3, 9, Entry.SlotSize);
                case ItemEnumeration.KingAntObject:
                    return GenerateClipping(4, 9, Entry.SlotSize);
                case ItemEnumeration.BiohazardCard:
                    return GenerateClipping(5, 9, Entry.SlotSize);
                case ItemEnumeration.DuraluminCaseM93RParts:
                    return GenerateClipping(6, 9, Entry.SlotSize);
                case ItemEnumeration.DuraluminCaseBowGunPowder:
                    return GenerateClipping(6, 9, Entry.SlotSize);
                case ItemEnumeration.DuraluminCaseMagnumRounds:
                    return GenerateClipping(6, 9, Entry.SlotSize);

                // Row 11
                case ItemEnumeration.Detonator:
                    return GenerateClipping(0, 10, Entry.SlotSize);
                case ItemEnumeration.ControlLever:
                    return GenerateClipping(1, 10, Entry.SlotSize);
                case ItemEnumeration.GoldDragonfly:
                    return GenerateClipping(2, 10, Entry.SlotSize);
                case ItemEnumeration.SilverKey:
                    return GenerateClipping(3, 10, Entry.SlotSize);
                case ItemEnumeration.GoldKey:
                    return GenerateClipping(4, 10, Entry.SlotSize);
                case ItemEnumeration.ArmyProof:
                    return GenerateClipping(5, 10, Entry.SlotSize);
                case ItemEnumeration.NavyProof:
                    return GenerateClipping(6, 10, Entry.SlotSize);

                // Row 12
                case ItemEnumeration.AirForceProof:
                    return GenerateClipping(0, 11, Entry.SlotSize);
                case ItemEnumeration.KeyWithTag:
                    return GenerateClipping(1, 11, Entry.SlotSize);
                case ItemEnumeration.IDCard:
                    return GenerateClipping(2, 11, Entry.SlotSize);
                case ItemEnumeration.Map:
                    return GenerateClipping(3, 11, Entry.SlotSize);
                case ItemEnumeration.AirportKey:
                    return GenerateClipping(4, 11, Entry.SlotSize);
                case ItemEnumeration.EmblemCard:
                    return GenerateClipping(5, 11, Entry.SlotSize);
                case ItemEnumeration.SkeletonPicture:
                    return GenerateClipping(6, 11, Entry.SlotSize);

                // Row 13
                case ItemEnumeration.MusicBoxPlate:
                    return GenerateClipping(0, 12, Entry.SlotSize);
                case ItemEnumeration.GoldDragonflyNoWings:
                    return GenerateClipping(1, 12, Entry.SlotSize);
                case ItemEnumeration.Album:
                    return GenerateClipping(2, 12, Entry.SlotSize);
                case ItemEnumeration.Halberd:
                    return GenerateClipping(3, 12, Entry.SlotSize);
                case ItemEnumeration.Extinguisher:
                    return GenerateClipping(4, 12, Entry.SlotSize);
                case ItemEnumeration.Briefcase:
                    return GenerateClipping(5, 12, Entry.SlotSize);
                case ItemEnumeration.PadlockKey:
                    return GenerateClipping(6, 12, Entry.SlotSize);

                // Row 14
                case ItemEnumeration.TG01:
                    return GenerateClipping(0, 13, Entry.SlotSize);
                case ItemEnumeration.SpAlloyEmblem:
                    return GenerateClipping(1, 13, Entry.SlotSize);
                case ItemEnumeration.ValveHandle:
                    return GenerateClipping(2, 13, Entry.SlotSize);
                case ItemEnumeration.OctaValveHandle:
                    return GenerateClipping(3, 13, Entry.SlotSize);
                case ItemEnumeration.MachineRoomKey:
                    return GenerateClipping(4, 13, Entry.SlotSize);
                case ItemEnumeration.MiningRoomKey:
                    return GenerateClipping(5, 13, Entry.SlotSize);
                case ItemEnumeration.BarCodeSticker:
                    return GenerateClipping(6, 13, Entry.SlotSize);

                // Row 15
                case ItemEnumeration.SterileRoomKey:
                    return GenerateClipping(0, 14, Entry.SlotSize);
                case ItemEnumeration.DoorKnob:
                    return GenerateClipping(1, 14, Entry.SlotSize);
                case ItemEnumeration.BatteryPack:
                    return GenerateClipping(2, 14, Entry.SlotSize);
                case ItemEnumeration.HemostaticWire:
                    return GenerateClipping(3, 14, Entry.SlotSize);
                case ItemEnumeration.TurnTableKey:
                    return GenerateClipping(4, 14, Entry.SlotSize);
                case ItemEnumeration.ChemStorageKey:
                    return GenerateClipping(5, 14, Entry.SlotSize);
                case ItemEnumeration.ClementAlpha:
                    return GenerateClipping(6, 14, Entry.SlotSize);

                // Row 16
                case ItemEnumeration.ClementSigma:
                    return GenerateClipping(0, 15, Entry.SlotSize);
                case ItemEnumeration.TankObject:
                    return GenerateClipping(1, 15, Entry.SlotSize);
                case ItemEnumeration.SpAlloyEmblemUnused:
                    return GenerateClipping(2, 15, Entry.SlotSize);
                case ItemEnumeration.ClementMixture:
                    return GenerateClipping(3, 15, Entry.SlotSize);
                case ItemEnumeration.RustedSword:
                    return GenerateClipping(4, 15, Entry.SlotSize);
                case ItemEnumeration.Hemostatic:
                    return GenerateClipping(5, 15, Entry.SlotSize);
                case ItemEnumeration.SecurityCard:
                    return GenerateClipping(6, 15, Entry.SlotSize);

                // Row 17
                case ItemEnumeration.SecurityFile:
                    return GenerateClipping(0, 16, Entry.SlotSize);
                case ItemEnumeration.AlexiasChoker:
                    return GenerateClipping(1, 16, Entry.SlotSize);
                case ItemEnumeration.AlexiasJewel:
                    return GenerateClipping(2, 16, Entry.SlotSize);
                case ItemEnumeration.QueenAntRelief:
                    return GenerateClipping(3, 16, Entry.SlotSize);
                case ItemEnumeration.KingAntRelief:
                    return GenerateClipping(4, 16, Entry.SlotSize);
                case ItemEnumeration.RedJewel:
                    return GenerateClipping(5, 16, Entry.SlotSize);
                case ItemEnumeration.BlueJewel:
                    return GenerateClipping(6, 16, Entry.SlotSize);

                // Row 18
                case ItemEnumeration.Socket:
                    return GenerateClipping(0, 17, Entry.SlotSize);
                case ItemEnumeration.SqValveHandle:
                    return GenerateClipping(1, 17, Entry.SlotSize);
                case ItemEnumeration.Serum:
                    return GenerateClipping(2, 17, Entry.SlotSize);
                case ItemEnumeration.EarthenwareVase:
                    return GenerateClipping(3, 17, Entry.SlotSize);
                case ItemEnumeration.PaperWeight:
                    return GenerateClipping(4, 17, Entry.SlotSize);
                case ItemEnumeration.SilverDragonflyNoWings:
                    return GenerateClipping(5, 17, Entry.SlotSize);
                case ItemEnumeration.SilverDragonfly:
                    return GenerateClipping(6, 17, Entry.SlotSize);

                // Row 19
                case ItemEnumeration.WingObject:
                    return GenerateClipping(0, 18, Entry.SlotSize);
                case ItemEnumeration.Crystal:
                    return GenerateClipping(1, 18, Entry.SlotSize);
                case ItemEnumeration.GoldDragonfly1Wing:
                    return GenerateClipping(2, 18, Entry.SlotSize);
                case ItemEnumeration.GoldDragonfly2Wings:
                    return GenerateClipping(3, 18, Entry.SlotSize);
                case ItemEnumeration.GoldDragonfly3Wings:
                    return GenerateClipping(4, 18, Entry.SlotSize);
                case ItemEnumeration.File:
                    return GenerateClipping(5, 18, Entry.SlotSize);
                case ItemEnumeration.PlantPot:
                    return GenerateClipping(6, 18, Entry.SlotSize);

                // Row 20
                case ItemEnumeration.PictureB:
                    return GenerateClipping(0, 19, Entry.SlotSize);
                case ItemEnumeration.M1P:
                    return GenerateClipping(1, 19, Entry.SlotSize);
                case ItemEnumeration.BowGunPowderUnused:
                    return GenerateClipping(3, 19, Entry.SlotSize);
                case ItemEnumeration.EnhancedHandgun:
                    return GenerateClipping(4, 19, Entry.SlotSize);
                case ItemEnumeration.PlayingManual:
                    return GenerateClipping(6, 19, Entry.SlotSize);

                // Shares Icon (Unused Content)
                case ItemEnumeration.PrisonersDiary:
                    return GenerateClipping(4, 7, Entry.SlotSize);
                case ItemEnumeration.DirectorsMemo:
                    return GenerateClipping(5, 7, Entry.SlotSize);
                case ItemEnumeration.Instructions:
                    return GenerateClipping(6, 7, Entry.SlotSize);
                case ItemEnumeration.AlfredsMemo:
                    return GenerateClipping(3, 15, Entry.SlotSize);
                case ItemEnumeration.BoardClip:
                    return GenerateClipping(6, 19, Entry.SlotSize);

                // No Icon (Unused Content)
                default:
                    return GenerateClipping(0, 0, Entry.SlotSize);
            }
        }

        private int[] GenerateClipping(int column, int row, int size = 1)
        {
            int[] clipping = new int[4];

            clipping[0] = ImageWidth * column;
            clipping[1] = ImageHeight * row;
            clipping[2] = ImageWidth * size;
            clipping[3] = ImageHeight;

            return clipping;
        }
    }
}