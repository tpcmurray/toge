using System;
using TOGE;

namespace OkayIAmABrush
{
    class Program
    {
        static void Main(string[] args)
        {
            var world = new World();

            #region Initial Setup
            //  ============== this is a typical command window line width (80) ================
            world.Introduction =

                "\r\n\r\n                 ========= Okay, I Am A Brush =========\r\n\r\n" +

                "You force yourself awake. The throbbing pain in your skull seems synced to \r\n" +
                " blaring horn and flashing light of the red alert. You remember an explosion \r\n" +
                " during the attack, being surrounded by medical staff, being anesthetized, and \r\n" +
                " just now waking up. An automated message instructs all personnel to abandon \r\n" +
                " ship.\r\n\r\n";

            world.Outro =
                "You use the captain's hand on the hand scanner and the pod begins to power up." +
                " You strap yourself into one of the 5 chairs as your attention is split " +
                " between the violent shaking of the ship and your high heartrate. After what " +
                " seems like much too long, the pod finally counts down from 5. On fire, the" +
                " G-forces make your head injury pound. Just as weightlessness begins you hear" +
                " a deafening pop, feel burning, and then ... \r\n\r\n";

            #endregion

            #region Item Setup

            var doctor = new Doctor();
            var medicalBayDoor = new MedicalBayDoor();
            var comBadge = new ComBadge();
            var arm = new Arm();
            var engineer = new Engineer();
            var coupler = new Coupler();
            var conduit = new Conduit();
            var podDoor = new PodDoor();
            var podConsole = new PodConsole();

            world.ItemStore.Add(comBadge);
            world.ItemStore.Add(coupler);

            #endregion

            #region Location setup

            //   [  Medical ]     [ Engineering ]
            //        |                 |
            //   [ Corridor ] --- [   Bridge    ]
            //                          |
            //                    [  Capt Qrts  ]
            //                          |
            //                    [   Esc Pod   ]

            world.Locations.Add("medicalBay", new Location
            {
                Name = "in the medical bay.",
                DistanceDescription = "the medical bay.",
                LookDescription = "You are in the medical bay. Under the sharp red light you see various medical\r\n" +
                                  " devices littering the floor and a strong chemical smell burns your nose.\r\n" +
                                  " Except for the body of a doctor, there are no people in the room. \r\n"
            });

            world.Locations.Add("corridor", new Location
            {
                Name = "in a corridor.",
                DistanceDescription = "a corridor.",
                LookDescription = "You are in a primary corridor on the main deck. The way west is blocked\r\n" +
                                  " by debris, which is bad as that's where the escape pods are."
            });

            world.Locations.Add("bridge", new Location
            {
                Name = "on the ship's bridge.",
                DistanceDescription = "a door to the ship's bridge.",
                LookDescription = "You are on the bridge. For the first time in your career, there are no people\r\n" +
                                  " here except ... the captain lies lifeless in the command chair, very badly\r\n" +
                                  " injured. A large chunk of his right shoulder is missing, and his right arm\r\n" +
                                  " is severed. Nothing here is lit up; maybe there's a power problem? Wait,\r\n" +
                                  " the captain has a personal escape pod off his quarters. Maybe it's\r\n" +
                                  " still there?"
            });

            world.Locations.Add("engineering", new Location
            {
                Name = "in the engineering room.",
                DistanceDescription = "the door to engineering.",
                LookDescription = "With the red glow and bodies strewn around, this place is makes you queasy.\r\n" +
                                  " You find a bulkhead with a power conduit inside that leads to the bridge,\r\n" +
                                  " but a primary power coupler is missing. A dead engineer lays on a\r\n" +
                                  " workstation below the bulkhead."
            });

            world.Locations.Add("captainsQuarters", new Location
            {
                Name = "in the captain's quarters.",
                DistanceDescription = "the door to the captain's quarters.",
                LookDescription = "You are standing in the captain's quarters. "
            });

            world.Locations.Add("pod", new Location
            {
                Name = "in the captain's escape pod.",
                DistanceDescription = "a hatch to the captains' person escape pod.",
                LookDescription = "Even though it's the captain's, it's not much bigger than normal pods. There\r\n" +
                                  " are 5 chairs, and the center chair is clearly the captain's. Each chair has\r\n" +
                                  " a console."
            });

            // setup room links
            world.Locations["medicalBay"].South = world.Locations["corridor"];
            world.Locations["medicalBay"].SouthIsBlockedText = "The door to leave the medical bay isn't opening.";

            world.Locations["corridor"].North = world.Locations["medicalBay"];
            world.Locations["corridor"].East = world.Locations["bridge"];

            //                                              ============== this is a typical command window line width (80) ================
            world.Locations["bridge"].West = world.Locations["corridor"];
            world.Locations["bridge"].North = world.Locations["engineering"];
            world.Locations["bridge"].South = world.Locations["captainsQuarters"];
            world.Locations["bridge"].SouthIsBlockedText = "The door to the captain's quarters doesn't open even though you have the\r\n" +
                                                           " doctors ComBadge. You are fairly certain that should give you access.\r\n" +
                                                           " Maybe the door, like the everything else on the bridge, isn't powered?";

            world.Locations["engineering"].South = world.Locations["bridge"];

            world.Locations["captainsQuarters"].North = world.Locations["bridge"];
            world.Locations["captainsQuarters"].South = world.Locations["pod"];

            world.Locations["pod"].North = world.Locations["captainsQuarters"];

            // setup room items
            world.Locations["medicalBay"].Items.Add(medicalBayDoor);
            world.Locations["medicalBay"].Items.Add(doctor);

            world.Locations["bridge"].Items.Add(arm);

            world.Locations["engineering"].Items.Add(engineer);
            world.Locations["engineering"].Items.Add(conduit);

            world.Locations["captainsQuarters"].Items.Add(podDoor);

            world.Locations["pod"].Items.Add(podConsole);




            #endregion

            // player starting room
            world.Player.Location = world.Locations["medicalBay"];

            var game = new Game(world);
            game.Start();
        }
    }

    #region Item creation
    public class Doctor : Item
    {
        public Doctor()
        {
            Name = "Doctor";
            CanBePickedUp = false;
            LookDescription = "The doctor is dead. She's wearing a typical white jacket with her ComBadge\r\n" +
                              " on the front.";
        }
        public override void Use(World world)
        {
            World.WriteLine("You tap the badge to communicate, but no one answers. The ship must be empty.");
        }
        public override void UseOn(Item item, World world)
        {
            World.WriteLine("Wait, you want to use the doctor's body on the " + item.Name + "...?\r\n" +
                            " That's sick dude.");
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
            Name = "Door";
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
            LookDescription = "It's a common communications badge the same as yours (if you had it, which\r\n" +
                              " you don't), only this one belongs to the good doctor.";
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
        }
    }
    public class Arm : Item
    {
        public Arm()
        {
            Name = "Arm";
            CanBePickedUp = true;
            LookDescription = "The captain's arm.";
        }

        public override void Use(World world)
        {
            World.WriteLine("You wave the arm. No one waves back. Isn't the ship going to self destruct\r\n" +
                            " soon? Maybe you have better things to do?");
        }

        public override void UseOn(Item item, World world)
        {
            if (item.Name.ToLower().Equals("console"))
            {
                world.IsEnded = true;
            }
            else
            {
                World.WriteLine("You try, but the arm doesn't do anything.");
            }
        }
        public override void Look(World world)
        {
            World.WriteLine(LookDescription);
        }
    }
    public class Engineer : Item
    {
        public Engineer()
        {
            Name = "Engineer";
            CanBePickedUp = false;
            LookDescription = "The engineer is dead. He has something in his hand ... a coupler?";
        }
        public override void Use(World world)
        {
            World.WriteLine("You tap the badge to communicate, but no one answers. The ship must be empty.");
        }
        public override void UseOn(Item item, World world)
        {
            World.WriteLine("Wait, you want to use the doctor's body on the " + item.Name + "...? \r\n" +
                            " That's sick dude.");
        }
        public override void Look(World world)
        {
            World.WriteLine(LookDescription);
            world.Player.Location.Items.Add(world.ItemStore.Get("Coupler"));
        }
    }
    public class Coupler : Item
    {
        public Coupler()
        {
            Name = "Coupler";
            CanBePickedUp = true;
            LookDescription = "A power coupler. This can probably fix the power conduit in engineering.";
        }

        public override void Use(World world)
        {
            World.WriteLine("Use it on what?");
        }

        public override void UseOn(Item item, World world)
        {
            if (item.Name.ToLower().Equals("conduit"))
            {
                World.WriteLine("The coupler snaps cleanly into place. Maybe the power line is fixed?");
                world.Locations["bridge"].SouthIsBlockedText = "";
                world.Locations["bridge"].LookDescription =
                    "You are on the bridge. For the first time in your career, there are no people\r\n" +
                    " here except ... the captain lies lifeless in the command chair, very badly\r\n" +
                    " injured. A large chunk of his right shoulder is missing, and his right arm\r\n" +
                    " is severed. The displays and consoles are now lit up.";
                world.Locations["captainsQuarters"].Items.Get("door").LookDescription = "It's a regular automatic door, and is now responsive.";
            }
            else
            {
                World.WriteLine("The power coupler doesn't seem to fit on that.");
            }
        }
        public override void Look(World world)
        {
            World.WriteLine(LookDescription);
        }
    }
    public class Conduit : Item
    {
        public Conduit()
        {
            Name = "Conduit";
            CanBePickedUp = false;
            LookDescription = "A disjointed power conduit. Best you can tell, this leads to the bridge.";
        }

        public override void Use(World world)
        {
            World.WriteLine("You want to use it, but it's disconnected.");
        }

        public override void UseOn(Item item, World world)
        {
            if (item.Name.ToLower().Equals("coupler"))
            {
                World.WriteLine("The coupler snaps cleanly into place. Maybe the power line is fixed?");
                world.Locations["bridge"].SouthIsBlockedText = "";
                world.Locations["bridge"].LookDescription =
                    "You are on the bridge. For the first time in your career, there are no people\r\n" +
                    " here except ... the captain lies lifeless in the command chair, very badly\r\n" +
                    " injured. A large chunk of his right shoulder is missing, and his right arm\r\n" +
                    " is severed. The consoles are now lit up.";
                world.Locations["captainsQuarters"].Items.Get("door").LookDescription = "It's a regular automatic door, and is now responsive.";
            }
            else
            {
                World.WriteLine("You can't move the conduit.");
            }
        }
        public override void Look(World world)
        {
            World.WriteLine(LookDescription);
        }
    }
    public class PodDoor : Item
    {
        public PodDoor()
        {
            Name = "Door";
            CanBePickedUp = false;
            LookDescription = "It's a regular automatic door, but it doesn't seem responsive.";
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
    public class PodConsole : Item
    {
        public PodConsole()
        {
            Name = "Console";
            CanBePickedUp = false;
            LookDescription = "It's a typical launch console.";
        }

        public override void Use(World world)
        {
                         // ============== this is a typical command window line width (80) ================
            World.WriteLine("The console is powered, but doesn't respond to you. It says 'pod ready' and\r\n" +
                            " has a hand image on the left sied.");
        }

        public override void UseOn(Item item, World world)
        {
            if (item.Name.ToLower().Equals("arm"))
            {
                world.IsEnded = true;
            }
            else
            {
                World.WriteLine("Maybe you should try using something with the console.");
            }
        }
        public override void Look(World world)
        {
            World.WriteLine(LookDescription);
        }
    }

    #endregion
}
