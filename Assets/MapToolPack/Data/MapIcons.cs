using System;
using System.Collections.Generic;
using UnityEngine;

public static class MapIcons
{
    public static Dictionary<int, Sprite> sprites;
    public static Dictionary<int, string> SpriteText;
    public static void LoadIcons()
    {
        if (sprites == null)
        {
            MapIcons.sprites = new Dictionary<int, Sprite>();
            MapIcons.SpriteText = new Dictionary<int, string>();
            MapIcons.SpriteText.Add(56, "MapIcons/MapIconTownBaseTier1");
            MapIcons.SpriteText.Add(57, "MapIcons/MapIconTownBaseTier2");
            MapIcons.SpriteText.Add(58, "MapIcons/MapIconTownBaseTier3");

            MapIcons.SpriteText.Add(8, "MapIcons/MapIconStaticForwardBase1");
            MapIcons.SpriteText.Add(9, "MapIcons/MapIconStaticForwardBase2");
            MapIcons.SpriteText.Add(10, "MapIcons/MapIconStaticForwardBase3");

            MapIcons.SpriteText.Add(11, "MapIcons/MapIconMedical");
            MapIcons.SpriteText.Add(12, "MapIcons/MapIconVehicle");
            MapIcons.SpriteText.Add(13, "MapIcons/MapIconStaticArmory");

            MapIcons.SpriteText.Add(20, "MapIcons/MapIconSalvage");
            MapIcons.SpriteText.Add(21, "MapIcons/MapIconComponents");
            MapIcons.SpriteText.Add(22, "MapIcons/MapIconOilWell");
            MapIcons.SpriteText.Add(23, "MapIcons/MapIconSulfur");

            MapIcons.SpriteText.Add(28, "MapIcons/MapIconObservationTower");
            MapIcons.SpriteText.Add(32, "MapIcons/MapIconSulfurMine");
            MapIcons.SpriteText.Add(33, "MapIcons/MapIconStorageFacility");
            MapIcons.SpriteText.Add(34, "MapIcons/MapIconFactory");

            MapIcons.SpriteText.Add(37, "MapIcons/MapIconRocketSite");
            MapIcons.SpriteText.Add(38, "MapIcons/MapIconSalvageMine");
            MapIcons.SpriteText.Add(39, "MapIcons/MapIconConstructionYard");
            MapIcons.SpriteText.Add(40, "MapIcons/MapIconComponentMine");

            MapIcons.SpriteText.Add(41, "MapIcons/MapIconStaticOilWell");


            MapIcons.SpriteText.Add(45, "MapIcons/MapIconRelicBase");
            MapIcons.SpriteText.Add(46, "MapIcons/MapIconRelicBase");
            MapIcons.SpriteText.Add(47, "MapIcons/MapIconRelicBase");

            MapIcons.SpriteText.Add(48, "MapIcons/MapIconBunkerBaseTier1");
            MapIcons.SpriteText.Add(49, "MapIcons/MapIconBunkerBaseTier2");
            MapIcons.SpriteText.Add(50, "MapIcons/MapIconBunkerBaseTier3");

            MapIcons.SpriteText.Add(51, "MapIcons/MapIconMassProductionFactory");
            MapIcons.SpriteText.Add(52, "MapIcons/MapIconSeaport");

            MapIcons.SpriteText.Add(61, "MapIcons/MapIconCoal");
            MapIcons.SpriteText.Add(62, "MapIcons/MapIconOilWell");

            MapIcons.sprites = new Dictionary<int, Sprite>();


            foreach (var a in MapIcons.SpriteText)
            {
                try
                {
                    Sprite tempSprite = Resources.Load<Sprite>(a.Value);
                    MapIcons.sprites.Add(a.Key, tempSprite);

                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
            }
        }
    }
}
