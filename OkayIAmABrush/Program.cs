using System;
using TOGE;

namespace OkayIAmABrush
{
    class Program
    {
        static void Main(string[] args)
        {
            var world = new World();

            //  ============== this is a typical command window line width (80) ================
            world.Introduction =

                "\r\n\r\n                 ========= Okay, I Am A Brush =========\r\n\r\n" +

                "You force yourself awake. The throbbing pain in your skull seems synced to \r\n" +
                "blaring horn and flashing light of the red alert. You remember an explosion \r\n" +
                "during the attack, being surrounded by medical staff, being anesthetized, and \r\n" +
                "just now waking up. An automated message instructs all personnel to abandon \r\n" +
                "ship.\r\n\r\n"
                ;

            //                                       [ Medical  ]     [ Engineering ]
            //                                             |                 |
            //  [ Esc Pod  ] --- [  Elevator   ] --- [ Corridor ] --- [   Bridge    ]
            //                                             |                 |
            //                                       [          ] --- [  Capt Qrts  ]

            #region Location setup

            // setup rooms
            world.Locations.Add("medicalBay", new Location
            {
                Name = "in the medical bay.",
                DistanceDescription = "the medical bay.",
                LookDescription = "You are in the medical bay. Under the sharp red light you see various medical\r\n" +
                                  "devices littering the floor and a strong chemical smell burns your nose.\r\n" +
                                  "Except for the body of a medic, there are no people in the room. \r\n"
            });

            world.Locations.Add("corridor", new Location
            {
                Name = "in a corridor.",
                DistanceDescription = "a corridor.",
                LookDescription = "You are in a generic corridor."
            });

            world.Locations.Add("bridge", new Location
            {
                Name = "on the ships bridge.",
                DistanceDescription = " a door to the ships bridge.",
                LookDescription = "You are on the bridge. For the first time in your career, there are no people here. (further description)"
            });

            world.Locations.Add("captainsQuarters", new Location
            {
                Name = "in the captain's quarters.",
                DistanceDescription = " the door to the captain's quarters.",
                LookDescription = "You are standing in the captain's quarters. (further description)"
            });

            // setup room links
            world.Locations["medicalBay"].South = world.Locations["corridor"];
            //world.Locations["medicalBay"].SouthIsBlockedText = "The door won't open.";

            world.Locations["corridor"].North = world.Locations["medicalBay"];
            world.Locations["corridor"].East = world.Locations["bridge"];

            world.Locations["bridge"].West = world.Locations["corridor"];
            world.Locations["bridge"].South = world.Locations["captainsQuarters"];
            world.Locations["bridge"].SouthIsBlockedText = "The door doesn't open for you.";

            world.Locations["captainsQuarters"].North = world.Locations["bridge"];
            #endregion

            world.Player.Location = world.Locations["medicalBay"];

            var game = new Game(world);
            game.Start();
        }
    }
}
