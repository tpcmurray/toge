using System;
using TOGE;

namespace OkayIAmABrush
{
    class Program
    {
        static void Main(string[] args)
        {
            var world = new World();

            #region Introduction
            //  ============== this is a typical command window line width (80) ================
            world.Introduction =

                "\r\n\r\n                 ========= Okay, I Am A Brush =========\r\n\r\n" +

                "You force yourself awake. The throbbing pain in your skull seems synced to \r\n" +
                "blaring horn and flashing light of the red alert. You remember an explosion \r\n" +
                "during the attack, being surrounded by medical staff, being anesthetized, and \r\n" +
                "just now waking up. An automated message instructs all personnel to abandon \r\n" +
                "ship.\r\n\r\n"
                ;
            #endregion

            #region Item Setup

            var doctor = new Doctor();
            var medicalBayDoor = new MedicalBayDoor();
            var comBadge = new ComBadge();
            world.ItemStore.Add(comBadge);

            #endregion

            #region Location setup

            //                                   [  Medical ]     [ Engineering ]
            //                                         |                 |
            //  [ Esc Pod ] --- [ Elevator ] --- [ Corridor ] --- [   Bridge    ]
            //                                         |                 |
            //                                   [  Lounge  ] --- [  Capt Qrts  ]

            world.Locations.Add("medicalBay", new Location
            {
                Name = "in the medical bay.",
                DistanceDescription = "the medical bay.",
                LookDescription = "You are in the medical bay. Under the sharp red light you see various medical\r\n" +
                                  "devices littering the floor and a strong chemical smell burns your nose.\r\n" +
                                  "Except for the body of a doctor, there are no people in the room. \r\n"
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
                DistanceDescription = " a door to the ship's bridge.",
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
            world.Locations["medicalBay"].SouthIsBlockedText = "The door to leave the medical bay isn't opening.";

            world.Locations["corridor"].North = world.Locations["medicalBay"];
            world.Locations["corridor"].East = world.Locations["bridge"];

            world.Locations["bridge"].West = world.Locations["corridor"];
            world.Locations["bridge"].South = world.Locations["captainsQuarters"];
            world.Locations["bridge"].SouthIsBlockedText = "The door doesn't open for you.";

            world.Locations["captainsQuarters"].North = world.Locations["bridge"];

            // setup room items
            world.Locations["medicalBay"].Items.Add(medicalBayDoor);
            world.Locations["medicalBay"].Items.Add(doctor);

            #endregion

            // player starting room
            world.Player.Location = world.Locations["medicalBay"];

            var game = new Game(world);
            game.Start();
        }
    }

    // ============== this is a typical command window line width (80) ================
    #region Item creation
    public class Doctor : Item
    {
        public Doctor()
        {
            Name = "Doctor";
            CanBePickedUp = false;
            LookDescription = "The doctor is dead. She's wearing a typical white jacket with her ComBadge " +
                              "on the front.";
        }
        public override void Use(World world)
        {
            World.WriteLine("You tap the badge to communicate, but no one answers. The ship must be empty.");
        }
        public override void UseOn(Item item, World world)
        {
            World.WriteLine("Wait, you want to use the doctor's body on the " + item.Name + "... " +
                            "That's sick dude.");
        }
        public override void Look(World world)
        {
            World.WriteLine(LookDescription);
            world.Player.Location.Items.Add(world.ItemStore.Get("ComBadge"));
        }
    }

    public class MedicalBayDoor : Item
    {
        public MedicalBayDoor()
        {
            Name = "door";
            CanBePickedUp = false;
            LookDescription = "It's a regular automatic door, it just won't open for you.";
        }
        public override void Use(World world)
        {
            World.WriteLine("You try various ways to open the door, but to no avail.");
        }
        public override void UseOn(Item item, World world)
        {
            World.WriteLine("You can't use a door on something. Seriously?");
        }
        public override void Look(World world)
        {
            World.WriteLine(LookDescription);
        }
    }

    public class ComBadge : Item
    {
        public ComBadge()
        {
            Name = "ComBadge";
            CanBePickedUp = true;
            LookDescription = "It's a common communications badge the same as yours (if you had it, which " +
                              "you don't), only this one belongs to the good doctor.";
        }

        public override void Use(World world)
        {
            World.WriteLine("You tap the badge to communicate, but no one answers. The ship must be empty.");
        }

        public override void UseOn(Item item, World world)
        {
            if (item.Name.ToLower().Equals("door"))
            {
                World.WriteLine("The door opens at last, be still my claustrophobia!");
                world.Locations["medicalBay"].SouthIsBlockedText = "";
            }
            else
            {
                World.WriteLine("You try, but that doesn't do anything.");
            }
        }
        public override void Look(World world)
        {
            World.WriteLine(LookDescription);
            world.Player.Location.Items.Add(world.ItemStore.Get("ComBadge"));
        }
    }
    #endregion
}
