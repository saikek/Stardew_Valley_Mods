using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Omegasis.HappyBirthday.Framework.EventPreconditions;
using StardustCore.Events;
using StardustCore.Events.Preconditions;
using StardustCore.Events.Preconditions.TimeSpecific;
using StardewValley;
using Microsoft.Xna.Framework;
using StardustCore.Events.Preconditions.PlayerSpecific;

namespace Omegasis.HappyBirthday.Framework
{
    public class BirthdayEvents
    {

        /// <summary>
        /// Creates the junimo birthday party event.
        /// </summary>
        /// <returns></returns>
        public static EventHelper CommunityCenterJunimoBirthday()
        {
            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new LocationPrecondition(Game1.getLocationFromName("CommunityCenter")));
            conditions.Add(new TimePrecondition(600, 2600));
            conditions.Add(new CanReadJunimo());
            conditions.Add(new StardustCore.Events.Preconditions.PlayerSpecific.JojaMember(false));
            conditions.Add(new CommunityCenterCompleted(false));
            //conditions.Add(new HasUnlockedCommunityCenter()); //Infered by the fact that you must enter the community center to trigger this event anyways.
            EventHelper e = new EventHelper("CommunityCenterBirthday", 19950, conditions, new EventStartData("playful", 32, 12, new EventStartData.FarmerData(32, 22, EventHelper.FacingDirection.Up), new List<EventStartData.NPCData>()));

            e.AddInJunimoActor("Juni", new Microsoft.Xna.Framework.Vector2(32, 10), StardustCore.IlluminateFramework.Colors.getRandomJunimoColor());
            e.AddInJunimoActor("Juni2", new Microsoft.Xna.Framework.Vector2(30, 11), StardustCore.IlluminateFramework.Colors.getRandomJunimoColor());
            e.AddInJunimoActor("Juni3", new Microsoft.Xna.Framework.Vector2(34, 11), StardustCore.IlluminateFramework.Colors.getRandomJunimoColor());
            e.AddInJunimoActor("Juni4", new Microsoft.Xna.Framework.Vector2(26, 11), StardustCore.IlluminateFramework.Colors.getRandomJunimoColor());
            e.AddInJunimoActor("Juni5", new Microsoft.Xna.Framework.Vector2(28, 11), StardustCore.IlluminateFramework.Colors.getRandomJunimoColor());
            e.AddInJunimoActor("Juni6Tank", new Vector2(38, 10), StardustCore.IlluminateFramework.Colors.getRandomJunimoColor());
            e.AddInJunimoActor("Juni7", new Vector2(27, 16), StardustCore.IlluminateFramework.Colors.getRandomJunimoColor());
            e.AddInJunimoActor("Juni8", new Vector2(40, 15), StardustCore.IlluminateFramework.Colors.getRandomJunimoColor());
            e.AddJunimoAdvanceMoveTiles(new StardustCore.Utilities.JunimoAdvanceMoveData("Juni6Tank", new List<Point>()
            {
                new Point(38,10),
                new Point(38,11),
                new Point(39,11),
                new Point(40,11),
                new Point(41,11),
                new Point(42,11),
                new Point(42,10),
                new Point(41,10),
                new Point(40,10),
                new Point(39,10),

            }, 60, 1, true)); ;

            e.FlipJunimoActor("Juni5", true);
            e.JunimoFaceDirection("Juni4", EventHelper.FacingDirection.Right); //Make a junimo face right.
            e.JunimoFaceDirection("Juni5", EventHelper.FacingDirection.Left);
            e.JunimoFaceDirection("Juni7", EventHelper.FacingDirection.Down);
            e.Animate("Juni", true, true, 250, new List<int>()
            {
                28,
                29,
                30,
                31
            });
            e.Animate("Juni7", false, true, 250, new List<int>()
            {
                44,45,46,47
            });
            e.Animate("Juni8", false, true, 250, new List<int>()
            {
                12,13,14,15
            });

            e.GlobalFadeIn();

            e.MoveFarmerUp(10, EventHelper.FacingDirection.Up, true);

            e.JunimoFaceDirection("Juni4", EventHelper.FacingDirection.Down);
            e.JunimoFaceDirection("Juni5", EventHelper.FacingDirection.Down);
            e.RemoveJunimoAdvanceMove("Juni6Tank");
            e.JunimoFaceDirection("Juni6Tank", EventHelper.FacingDirection.Down);
            e.JunimoFaceDirection("Juni7", EventHelper.FacingDirection.Right);
            e.FlipJunimoActor("Juni8", true);
            e.JunimoFaceDirection("Juni8", EventHelper.FacingDirection.Left);

            e.PlaySound("junimoMeep1");

            e.EmoteFarmer_ExclamationMark();
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:JunimoBirthdayParty_0"));
            e.EmoteFarmer_Heart();
            e.GlobalFadeOut(0.010);
            e.SetViewportPosition(-100, -100);
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:JunimoBirthdayParty_1"));
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:PartyOver"));
            e.addObjectToPlayersInventory(220, 1, false);

            e.End();

            return e;
        }


        /// <summary>
        /// Birthday event for when the player is dating Penny.
        /// Status: Completed.
        /// </summary>
        /// <returns></returns>
        public static EventHelper DatingBirthday_Penny()
        {

            NPC penny = Game1.getCharacterFromName("Penny");
            NPC pam = Game1.getCharacterFromName("Pam");

            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new LocationPrecondition(Game1.getLocationFromName("Trailer")));
            conditions.Add(new TimePrecondition(600, 2600));
            conditions.Add(new StardustCore.Events.Preconditions.NPCSpecific.DatingNPC(penny));

            //conditions.Add(new StardustCore.Events.Preconditions.NPCSpecific.DatingNPC(Game1.getCharacterFromName("Penny"));
            EventHelper e = new EventHelper("BirthdayDating:Penny", 19951, conditions, new EventStartData("playful", 12, 8, new EventStartData.FarmerData(12, 9, EventHelper.FacingDirection.Up), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(penny,12,7, EventHelper.FacingDirection.Up),
                new EventStartData.NPCData(pam,15,4, EventHelper.FacingDirection.Down)
            }));

            e.GlobalFadeIn();

            e.MoveFarmerUp(1, EventHelper.FacingDirection.Up, false);

            e.ActorFaceDirection("Penny", EventHelper.FacingDirection.Down);
            //starting = starting.Replace("@", Game1.player.Name);
            e.Speak(penny, HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingPennyBirthday_Penny:0"));
            e.Speak(pam, HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingPennyBirthday_Pam:0"));
            e.Speak(penny, HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingPennyBirthday_Penny:1"));
            e.Speak(pam, HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingPennyBirthday_Pam:1"));
            e.Emote_Angry("Penny");
            e.Speak(penny, HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingPennyBirthday_Penny:2")); //penny2
            e.Speak(penny, HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingPennyBirthday_Penny:3")); //penny3

            e.MoveActorLeft("Penny", 3, EventHelper.FacingDirection.Up, true);
            e.MoveFarmerRight(2, EventHelper.FacingDirection.Up, false);
            e.MoveFarmerUp(3, EventHelper.FacingDirection.Down, false);
            e.MoveActorRight("Penny", 5, EventHelper.FacingDirection.Up, true);
            e.MoveActorUp("Penny", 1, EventHelper.FacingDirection.Up, true);
            e.Speak(pam, HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingPennyBirthday_Pam:2")); //pam2
            e.Speak(penny, HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingPennyBirthday_Penny:4"));//penny4

            e.EmoteFarmer_Heart();
            e.Emote_Heart("Penny");
            e.GlobalFadeOut(0.010);
            e.SetViewportPosition(-100, -100);
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingPennyBirthday_Finish:0")); //penny party finish 0
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingPennyBirthday_Finish:1"));// penny party finish 1
            e.addObjectToPlayersInventory(220, 1, false);
            e.addObjectToPlayersInventory(346, 1, false);

            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:PartyOver"));

            e.End();

            return e;
        }

        public static EventHelper DatingBirthday_Penny_BigHome()
        {

            NPC penny = Game1.getCharacterFromName("Penny");
            NPC pam = Game1.getCharacterFromName("Pam");

            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new LocationPrecondition(Game1.getLocationFromName("Trailer_Big")));
            conditions.Add(new TimePrecondition(600, 2600));
            conditions.Add(new StardustCore.Events.Preconditions.NPCSpecific.DatingNPC(penny));

            //conditions.Add(new StardustCore.Events.Preconditions.NPCSpecific.DatingNPC(Game1.getCharacterFromName("Penny"));
            EventHelper e = new EventHelper("BirthdayDating:Penny_BigHome", 19951, conditions, new EventStartData("playful", 14, 8, new EventStartData.FarmerData(12, 11, EventHelper.FacingDirection.Up), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(penny,12,7, EventHelper.FacingDirection.Up),
                new EventStartData.NPCData(pam,15,4, EventHelper.FacingDirection.Down)
            }));

            e.GlobalFadeIn();

            e.MoveFarmerUp(3, EventHelper.FacingDirection.Up, false);

            e.ActorFaceDirection("Penny", EventHelper.FacingDirection.Down);
            //starting = starting.Replace("@", Game1.player.Name);
            e.Speak(penny, HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingPennyBirthday_Penny:0"));
            e.Speak(pam, HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingPennyBirthday_Pam:0"));
            e.Speak(penny, HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingPennyBirthday_Penny:1"));
            e.Speak(pam, HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingPennyBirthday_Pam:1"));
            e.Emote_Angry("Penny");
            e.Speak(penny, HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingPennyBirthday_Penny:2")); //penny2
            e.Speak(penny, HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingPennyBirthday_Penny:3")); //penny3

            e.MoveActorLeft("Penny", 3, EventHelper.FacingDirection.Up, true);
            e.MoveFarmerRight(2, EventHelper.FacingDirection.Up, false);
            e.MoveFarmerUp(3, EventHelper.FacingDirection.Down, false);
            e.MoveActorRight("Penny", 5, EventHelper.FacingDirection.Up, true);
            e.MoveActorUp("Penny", 1, EventHelper.FacingDirection.Up, true);
            e.Speak(pam, HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingPennyBirthday_Pam:2")); //pam2
            e.Speak(penny, HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingPennyBirthday_Penny:4"));//penny4

            e.EmoteFarmer_Heart();
            e.Emote_Heart("Penny");
            e.GlobalFadeOut(0.010);
            e.SetViewportPosition(-100, -100);
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingPennyBirthday_Finish:0")); //penny party finish 0
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingPennyBirthday_Finish:1"));// penny party finish 1
            e.addObjectToPlayersInventory(220, 1, false);
            e.addObjectToPlayersInventory(346, 1, false);

            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:PartyOver"));

            e.End();

            return e;
        }

        /// <summary>
        /// Birthday event for when the player is dating Maru.
        /// Finished.
        /// </summary>
        /// <returns></returns>
        public static EventHelper DatingBirthday_Maru()
        {
            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new LocationPrecondition(Game1.getLocationFromName("ScienceHouse")));
            conditions.Add(new TimePrecondition(600, 2600));

            NPC maru = Game1.getCharacterFromName("Maru");
            NPC sebastian = Game1.getCharacterFromName("Sebastian");
            NPC robin = Game1.getCharacterFromName("Robin");
            NPC demetrius = Game1.getCharacterFromName("Demetrius");

            conditions.Add(new StardustCore.Events.Preconditions.NPCSpecific.DatingNPC(maru));

            EventHelper e = new EventHelper("BirthdayDating:Maru", 19952, conditions, new EventStartData("playful", 28, 12, new EventStartData.FarmerData(23, 12, EventHelper.FacingDirection.Right), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(maru,27,11, EventHelper.FacingDirection.Down),
                new EventStartData.NPCData(sebastian,26,13, EventHelper.FacingDirection.Up),
                new EventStartData.NPCData(robin,28,9, EventHelper.FacingDirection.Up),
                new EventStartData.NPCData(demetrius,30,11, EventHelper.FacingDirection.Left)
            }));
            e.GlobalFadeIn();

            e.MoveFarmerRight(3, EventHelper.FacingDirection.Right, true);
            e.NpcFaceDirection(maru, EventHelper.FacingDirection.Left);
            e.NpcFaceDirection(demetrius, EventHelper.FacingDirection.Left);
            //Seb is already facing up.
            e.NpcFaceDirection(robin, EventHelper.FacingDirection.Down);

            //Dialogue goes here.
            //Seriously improve dialogue lines. Maru is probably the NPC I know the least about.
            e.Speak(maru, GetTranslatedString("Event:DatingMaruBirthday_Maru:0")); //maru 0
            e.Speak(demetrius, GetTranslatedString("Event:DatingMaruBirthday_Demetrius:0")); //demetrius 0
            e.Speak(maru, GetTranslatedString("Event:DatingMaruBirthday_Maru:1"));//Maru 1 //Spoiler she doesn't.
            e.Speak(sebastian, GetTranslatedString("Event:DatingMaruBirthday_Sebastian:0")); //sebastian 0
            e.Speak(robin, GetTranslatedString("Event:DatingMaruBirthday_Robin:0")); //robin 0
            e.Speak(demetrius, GetTranslatedString("Event:DatingMaruBirthday_Demetrius:1")); //demetrius 1
            e.Emote_ExclamationMark("Robin");
            e.NpcFaceDirection(robin, EventHelper.FacingDirection.Up);
            e.Speak(robin, GetTranslatedString("Event:DatingMaruBirthday_Robin:1")); //robin 1
            e.NpcFaceDirection(robin, EventHelper.FacingDirection.Down);
            e.MoveActorDown("Robin", 1, EventHelper.FacingDirection.Down, false);
            e.AddObject(27, 12, 220);

            e.Speak(maru, GetTranslatedString("Event:DatingMaruBirthday_Maru:2")); //maru 2
            e.EmoteFarmer_Thinking();
            e.Speak(sebastian, GetTranslatedString("Event:DatingMaruBirthday_Sebastian:1")); //Sebastian 1
            e.Speak(maru, GetTranslatedString("Event:DatingMaruBirthday_Maru:3")); //maru 3

            //Event finish commands.
            e.EmoteFarmer_Heart();
            e.Emote_Heart("Maru");
            e.GlobalFadeOut(0.010);
            e.SetViewportPosition(-100, -100);
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingMaruBirthday_Finish:0")); //maru party finish 0
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingMaruBirthday_Finish:1")); //maru party finish 0
            e.addObjectToPlayersInventory(220, 1, false);

            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:PartyOver"));
            e.End();
            return e;
        }

        /// <summary>
        /// Birthday event for when the player is dating Leah.
        /// Finished.
        /// </summary>
        /// <returns></returns>
        public static EventHelper DatingBirthday_Leah()
        {
            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new LocationPrecondition(Game1.getLocationFromName("LeahHouse")));
            conditions.Add(new TimePrecondition(600, 2600));

            NPC leah = Game1.getCharacterFromName("Leah");

            conditions.Add(new StardustCore.Events.Preconditions.NPCSpecific.DatingNPC(leah));

            EventHelper e = new EventHelper("BirthdayDating:Leah", 19954, conditions, new EventStartData("playful", 12, 7, new EventStartData.FarmerData(7, 9, EventHelper.FacingDirection.Up), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(leah,14,11, EventHelper.FacingDirection.Left),
            }));
            e.AddObject(11, 11, 220);
            e.GlobalFadeIn();
            e.MoveFarmerUp(2, EventHelper.FacingDirection.Up, false);
            e.MoveFarmerRight(5, EventHelper.FacingDirection.Down, false);
            e.NpcFaceDirection(leah, EventHelper.FacingDirection.Up);
            e.Speak(leah, GetTranslatedString("Event:DatingLeahBirthday_Leah:0")); //0
            e.MoveFarmerDown(2, EventHelper.FacingDirection.Down, false);
            e.MoveFarmerRight(1, EventHelper.FacingDirection.Down, false);
            e.MoveFarmerDown(1, EventHelper.FacingDirection.Down, false);
            e.Speak(leah, GetTranslatedString("Event:DatingLeahBirthday_Leah:1")); //1
            e.EmoteFarmer_Happy();
            e.Speak(leah, GetTranslatedString("Event:DatingLeahBirthday_Leah:2"));//2
            e.Speak(leah, GetTranslatedString("Event:DatingLeahBirthday_Leah:3"));//3
            e.Speak(leah, GetTranslatedString("Event:DatingLeahBirthday_Leah:4"));//4


            e.EmoteFarmer_Heart();
            e.Emote_Heart("Leah");
            e.GlobalFadeOut(0.010);
            e.SetViewportPosition(-100, -100);
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingLeahBirthday_Finish:0")); //maru party finish 0
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingLeahBirthday_Finish:1")); //maru party finish 0
            e.addObjectToPlayersInventory(220, 1, false);
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:PartyOver"));
            e.End();
            return e;
        }

        /// <summary>
        /// Birthday event for when the player is dating Abigail.
        /// Finished.
        /// </summary>
        /// <returns></returns>
        public static EventHelper DatingBirthday_Abigail_Seedshop()
        {
            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new LocationPrecondition(Game1.getLocationFromName("SeedShop")));
            conditions.Add(new TimePrecondition(600, 2600));

            if (Game1.player.hasCompletedCommunityCenter() == false)
            {
                conditions.Add(new StardustCore.Events.Preconditions.TimeSpecific.EventDayExclusionPrecondition(false, false, false, true, false, false, false));
            }

            NPC abigail = Game1.getCharacterFromName("Abigail");
            NPC pierre = Game1.getCharacterFromName("Pierre");
            NPC caroline = Game1.getCharacterFromName("Caroline");

            conditions.Add(new StardustCore.Events.Preconditions.NPCSpecific.DatingNPC(abigail));

            EventHelper e = new EventHelper("BirthdayDating:Abigail", 19955, conditions, new EventStartData("playful", 35, 7, new EventStartData.FarmerData(31, 11, EventHelper.FacingDirection.Up), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(abigail,36,9, EventHelper.FacingDirection.Left),
                new EventStartData.NPCData(pierre,33,6, EventHelper.FacingDirection.Down),
                new EventStartData.NPCData(caroline,35,5, EventHelper.FacingDirection.Up),
            }));
            e.GlobalFadeIn();

            //Dialogue here.
            e.MoveFarmerUp(2, EventHelper.FacingDirection.Right, false);
            e.MoveFarmerRight(4, EventHelper.FacingDirection.Right, false);

            e.Speak(abigail, GetTranslatedString("Event:DatingAbigailBirthday_Abigail:0")); //abi 0

            e.NpcFaceDirection(caroline, EventHelper.FacingDirection.Down);

            e.Speak(pierre, GetTranslatedString("Event:DatingAbigailBirthday_Pierre:0")); //pie 0
            e.Speak(caroline, GetTranslatedString("Event:DatingAbigailBirthday_Caroline:0")); //car 0
            e.AddObject(35, 5, 220);
            e.Speak(abigail, GetTranslatedString("Event:DatingAbigailBirthday_Abigail:1")); //abi 1
            e.Speak(pierre, GetTranslatedString("Event:DatingAbigailBirthday_Pierre:1")); //pie 1
            e.Speak(caroline, GetTranslatedString("Event:DatingAbigailBirthday_Caroline:1")); //car 1
            e.Speak(caroline, GetTranslatedString("Event:DatingAbigailBirthday_Caroline:2")); //car 2
            e.Speak(abigail, GetTranslatedString("Event:DatingAbigailBirthday_Abigail:2")); //abi 2
            e.EmoteFarmer_Thinking();
            e.Speak(abigail, GetTranslatedString("Event:DatingAbigailBirthday_Abigail:3"));//abi 3
            e.Speak(abigail, GetTranslatedString("Event:DatingAbigailBirthday_Abigail:4"));///abi 4

            e.EmoteFarmer_Heart();
            e.Emote_Heart("Abigail");
            e.GlobalFadeOut(0.010);
            e.SetViewportPosition(-100, -100);
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingAbigailBirthday_Finish:0")); //abi party finish 0
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingAbigailBirthday_Finish:1")); //abi party finish 0
            e.addObjectToPlayersInventory(220, 1, false);
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:PartyOver"));
            e.End();
            return e;

        }


        public static EventHelper DatingBirthday_Abigail_Mine()
        {
            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new LocationPrecondition(Game1.getLocationFromName("Mine")));
            conditions.Add(new TimePrecondition(600, 2600));

            var v=new StardustCore.Events.Preconditions.PlayerSpecific.JojaMember(true);
            if (v.meetsCondition())
            {
                conditions.Add(new StardustCore.Events.Preconditions.TimeSpecific.EventDayExclusionPrecondition(true, true, true, false, true, true, true));
            }
            else
            {
                if (Game1.player.hasCompletedCommunityCenter() == false)
                {
                    conditions.Add(new StardustCore.Events.Preconditions.TimeSpecific.EventDayExclusionPrecondition(true, true, true, false, true, true, true));
                }
            }

            NPC abigail = Game1.getCharacterFromName("Abigail");

            conditions.Add(new StardustCore.Events.Preconditions.NPCSpecific.DatingNPC(abigail));

            EventHelper e = new EventHelper("BirthdayDating:Abigail_Mine", 19955, conditions, new EventStartData("playful", 18, 8, new EventStartData.FarmerData(18, 12, EventHelper.FacingDirection.Up), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(abigail,18,4, EventHelper.FacingDirection.Down),
            }));
            e.GlobalFadeIn();

            //Dialogue here.
            e.MoveFarmerUp(7, EventHelper.FacingDirection.Up, false);

            e.Speak(abigail, GetTranslatedString("Event:DatingAbigailBirthday_Mine_Abigail:0")); //abi 0

            e.Speak(abigail, GetTranslatedString("Event:DatingAbigailBirthday_Mine_Abigail:1")); //abi 1
            e.EmoteFarmer_QuestionMark();
            e.Speak(abigail, GetTranslatedString("Event:DatingAbigailBirthday_Mine_Abigail:2")); //abi 2
            e.Speak(abigail, GetTranslatedString("Event:DatingAbigailBirthday_Mine_Abigail:3"));//abi 3
            e.EmoteFarmer_Thinking();
            e.Speak(abigail, GetTranslatedString("Event:DatingAbigailBirthday_Mine_Abigail:4"));///abi 4

            e.EmoteFarmer_Heart();
            e.Emote_Heart("Abigail");
            e.GlobalFadeOut(0.010);
            e.SetViewportPosition(-100, -100);
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingAbigailBirthday_Mine_Finish:0")); //abi party finish 0
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingAbigailBirthday_Mine_Finish:1")); //abi party finish 0
            e.addObjectToPlayersInventory(220, 1, false);
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:PartyOver"));
            e.End();
            return e;

        }

        public static EventHelper DatingBirthday_Emily()
        {
            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new LocationPrecondition(Game1.getLocationFromName("HaleyHouse")));
            conditions.Add(new TimePrecondition(600, 2600));

            NPC emily = Game1.getCharacterFromName("Emily");

            conditions.Add(new StardustCore.Events.Preconditions.NPCSpecific.DatingNPC(emily));

            EventHelper e = new EventHelper("BirthdayDating:Emily", 19956, conditions, new EventStartData("playful", 20, 18, new EventStartData.FarmerData(11, 20, EventHelper.FacingDirection.Right), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(emily,20,17, EventHelper.FacingDirection.Down),
            }));
            e.GlobalFadeIn();

            //Dialogue here.
            e.MoveFarmerRight(9, EventHelper.FacingDirection.Up, false);

            e.Speak(emily, GetTranslatedString("Event:DatingEmilyBirthday_Emily:0")); //emi 0
            e.Speak(emily, GetTranslatedString("Event:DatingEmilyBirthday_Emily:1")); //emi 0
            e.EmoteFarmer_Happy();
            e.Speak(emily, GetTranslatedString("Event:DatingEmilyBirthday_Emily:2")); //emi 0
            e.Speak(emily, GetTranslatedString("Event:DatingEmilyBirthday_Emily:3")); //emi 0
            e.Speak(emily, GetTranslatedString("Event:DatingEmilyBirthday_Emily:4")); //emi 0
            e.EmoteFarmer_Thinking();
            e.Speak(emily, GetTranslatedString("Event:DatingEmilyBirthday_Emily:5")); //emi 0


            e.EmoteFarmer_Heart();
            e.Emote_Heart("Emily");
            e.GlobalFadeOut(0.010);
            e.SetViewportPosition(-100, -100);
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingEmilyBirthday_Finish:0")); //abi party finish 0
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingEmilyBirthday_Finish:1")); //abi party finish 0
            e.addObjectToPlayersInventory(220, 1, false);
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:PartyOver"));
            e.End();
            return e;
        }


        public static EventHelper DatingBirthday_Haley()
        {

            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new LocationPrecondition(Game1.getLocationFromName("HaleyHouse")));
            conditions.Add(new TimePrecondition(600, 2600));

            NPC haley = Game1.getCharacterFromName("Haley");

            conditions.Add(new StardustCore.Events.Preconditions.NPCSpecific.DatingNPC(haley));

            EventHelper e = new EventHelper("BirthdayDating:Haley", 19957, conditions, new EventStartData("playful", 20, 18, new EventStartData.FarmerData(11, 20, EventHelper.FacingDirection.Right), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(haley,20,17, EventHelper.FacingDirection.Down),
            }));
            e.GlobalFadeIn();

            //Dialogue here.
            e.MoveFarmerRight(9, EventHelper.FacingDirection.Up, false);

            e.Speak(haley, GetTranslatedString("Event:DatingHaleyBirthday_Haley:0"));
            e.Speak(haley, GetTranslatedString("Event:DatingHaleyBirthday_Haley:1"));
            e.EmoteFarmer_Happy();
            e.Speak(haley, GetTranslatedString("Event:DatingHaleyBirthday_Haley:2"));
            e.Speak(haley, GetTranslatedString("Event:DatingHaleyBirthday_Haley:3"));
            e.EmoteFarmer_Thinking();
            e.Speak(haley, GetTranslatedString("Event:DatingHaleyBirthday_Haley:4"));


            e.EmoteFarmer_Heart();
            e.Emote_Heart("Haley");
            e.GlobalFadeOut(0.010);
            e.SetViewportPosition(-100, -100);
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingHaleyBirthday_Finish:0")); //abi party finish 0
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingHaleyBirthday_Finish:1")); //abi party finish 0
            e.addObjectToPlayersInventory(221, 1, false);
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:PartyOver"));
            e.End();
            return e;

        }

        public static EventHelper DatingBirthday_Sam()
        {
            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new LocationPrecondition(Game1.getLocationFromName("SamHouse")));
            conditions.Add(new TimePrecondition(600, 2600));

            NPC sam = Game1.getCharacterFromName("Sam");

            conditions.Add(new StardustCore.Events.Preconditions.NPCSpecific.DatingNPC(sam));

            EventHelper e = new EventHelper("BirthdayDating:Sam", 19959, conditions, new EventStartData("playful", 3, 6, new EventStartData.FarmerData(7, 9, EventHelper.FacingDirection.Up), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(sam,3,5, EventHelper.FacingDirection.Down),
            }));
            e.GlobalFadeIn();

            //Dialogue here.
            e.MoveFarmerUp(4, EventHelper.FacingDirection.Up, false);
            e.MoveFarmerLeft(3, EventHelper.FacingDirection.Left, false);
            e.NpcFaceDirection(sam, EventHelper.FacingDirection.Right);

            e.Speak(sam, GetTranslatedString("Event:DatingSamBirthday_Sam:0"));
            e.Speak(sam, GetTranslatedString("Event:DatingSamBirthday_Sam:1"));
            e.Speak(sam, GetTranslatedString("Event:DatingSamBirthday_Sam:2"));
            e.Speak(sam, GetTranslatedString("Event:DatingSamBirthday_Sam:3"));
            e.EmoteFarmer_Heart();
            e.Emote_Heart("Sam");
            e.GlobalFadeOut(0.010);
            e.SetViewportPosition(-100, -100);
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingSamBirthday_Finish:0")); //sam party finish 0
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingSamBirthday_Finish:1")); //sam party finish 0
            e.addObjectToPlayersInventory(206, 1, false);
            e.addObjectToPlayersInventory(167, 1, false);
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:PartyOver"));
            e.End();
            return e;
        }

        /// <summary>
        /// Event that occurs when the player is dating Sebastian.
        /// Status: Finished.
        /// </summary>
        /// <returns></returns>
        public static EventHelper DatingBirthday_Sebastian()
        {
            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new LocationPrecondition(Game1.getLocationFromName("ScienceHouse")));
            conditions.Add(new TimePrecondition(600, 2600));

            NPC maru = Game1.getCharacterFromName("Maru");
            NPC sebastian = Game1.getCharacterFromName("Sebastian");
            NPC robin = Game1.getCharacterFromName("Robin");
            NPC demetrius = Game1.getCharacterFromName("Demetrius");

            conditions.Add(new StardustCore.Events.Preconditions.NPCSpecific.DatingNPC(sebastian));

            EventHelper e = new EventHelper("BirthdayDating:Sebastian", 19952, conditions, new EventStartData("playful", 28, 12, new EventStartData.FarmerData(23, 12, EventHelper.FacingDirection.Right), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(maru,27,11, EventHelper.FacingDirection.Down),
                new EventStartData.NPCData(sebastian,26,13, EventHelper.FacingDirection.Up),
                new EventStartData.NPCData(robin,28,9, EventHelper.FacingDirection.Up),
                new EventStartData.NPCData(demetrius,30,11, EventHelper.FacingDirection.Left)
            }));
            e.GlobalFadeIn();

            e.MoveFarmerRight(3, EventHelper.FacingDirection.Right, true);
            e.NpcFaceDirection(maru, EventHelper.FacingDirection.Left);
            e.NpcFaceDirection(demetrius, EventHelper.FacingDirection.Left);
            //Seb is already facing up.
            e.NpcFaceDirection(robin, EventHelper.FacingDirection.Down);

            //Dialogue goes here.
            //Seriously improve dialogue lines. Maru is probably the NPC I know the least about.
            e.Speak(sebastian, GetTranslatedString("Event:DatingSebastianBirthday_Sebastian:0")); //sebastian 0
            e.Speak(robin, GetTranslatedString("Event:DatingSebastianBirthday_Robin:0")); //maru 0
            e.Speak(maru, GetTranslatedString("Event:DatingSebastianBirthday_Maru:0"));//Maru 0
            e.Speak(robin, GetTranslatedString("Event:DatingSebastianBirthday_Robin:1")); //robin 0
            e.Speak(demetrius, GetTranslatedString("Event:DatingSebastianBirthday_Demetrius:0")); //demetrius 0
            e.Speak(sebastian, GetTranslatedString("Event:DatingSebastianBirthday_Sebastian:1")); //Sebastian 1
            e.Emote_ExclamationMark("Robin");
            e.NpcFaceDirection(robin, EventHelper.FacingDirection.Up);
            e.Speak(robin, GetTranslatedString("Event:DatingSebastianBirthday_Robin:2")); //robin 1
            e.NpcFaceDirection(robin, EventHelper.FacingDirection.Down);
            e.MoveActorDown("Robin", 1, EventHelper.FacingDirection.Down, false);
            e.AddObject(27, 12, 220);
            e.Speak(demetrius, GetTranslatedString("Event:DatingSebastianBirthday_Demetrius:1")); //maru 2
            e.EmoteFarmer_Thinking();
            e.Speak(maru, GetTranslatedString("Event:DatingSebastianBirthday_Maru:1")); //maru 3
            e.Speak(sebastian, GetTranslatedString("Event:DatingSebastianBirthday_Sebastian:2")); //Sebastian 1

            //Event finish commands.
            e.EmoteFarmer_Heart();
            e.Emote_Heart("Sebastian");
            e.GlobalFadeOut(0.010);
            e.SetViewportPosition(-100, -100);
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingSebastianBirthday_Finish:0")); //maru party finish 0
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingSebastianBirthday_Finish:1")); //maru party finish 0
            e.addObjectToPlayersInventory(220, 1, false);

            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:PartyOver"));
            e.End();
            return e;
        }



        public static EventHelper DatingBirthday_Elliott()
        {
            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new LocationPrecondition(Game1.getLocationFromName("ElliottHouse")));
            conditions.Add(new TimePrecondition(600, 2600));

            NPC elliott = Game1.getCharacterFromName("Elliott");

            conditions.Add(new StardustCore.Events.Preconditions.NPCSpecific.DatingNPC(elliott));

            EventHelper e = new EventHelper("BirthdayDating:Elliott", 19958, conditions, new EventStartData("playful", 3, 5, new EventStartData.FarmerData(3, 8, EventHelper.FacingDirection.Up), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(elliott,3,5, EventHelper.FacingDirection.Down),
            }));
            e.GlobalFadeIn();

            //Dialogue here.
            e.MoveFarmerUp(2, EventHelper.FacingDirection.Up, false);
            e.Speak(elliott, GetTranslatedString("Event:DatingElliottBirthday_Elliott:0"));
            e.Speak(elliott, GetTranslatedString("Event:DatingElliottBirthday_Elliott:1"));
            e.Speak(elliott, GetTranslatedString("Event:DatingElliottBirthday_Elliott:2"));
            e.Speak(elliott, GetTranslatedString("Event:DatingElliottBirthday_Elliott:3"));
            e.Speak(elliott, GetTranslatedString("Event:DatingElliottBirthday_Elliott:4"));
            e.EmoteFarmer_Thinking();
            e.Speak(elliott, GetTranslatedString("Event:DatingElliottBirthday_Elliott:5"));
            e.EmoteFarmer_Heart();
            e.Emote_Heart("Elliott");
            e.GlobalFadeOut(0.010);
            e.SetViewportPosition(-100, -100);
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingElliottBirthday_Finish:0")); //abi party finish 0
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingElliottBirthday_Finish:1")); //abi party finish 0
            e.addObjectToPlayersInventory(220, 1, false);
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:PartyOver"));
            e.End();
            return e;
        }


        public static EventHelper DatingBirthday_Shane()
        {

            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new LocationPrecondition(Game1.getLocationFromName("AnimalShop")));
            conditions.Add(new TimePrecondition(600, 2600));

            NPC shane = Game1.getCharacterFromName("Shane");

            conditions.Add(new StardustCore.Events.Preconditions.NPCSpecific.DatingNPC(shane));

            EventHelper e = new EventHelper("BirthdayDating:Shane", 19960, conditions, new EventStartData("playful", 26, 15, new EventStartData.FarmerData(19, 18, EventHelper.FacingDirection.Left), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(shane,25,16, EventHelper.FacingDirection.Down),
            }));
            e.GlobalFadeIn();

            //Dialogue here.
            e.MoveFarmerRight(3, EventHelper.FacingDirection.Right, false);
            e.MoveFarmerUp(2, EventHelper.FacingDirection.Up, false);
            e.MoveFarmerRight(2, EventHelper.FacingDirection.Right, false);
            e.NpcFaceDirection(shane, EventHelper.FacingDirection.Left);

            e.Speak(shane, GetTranslatedString("Event:DatingShaneBirthday_Shane:0"));
            e.Speak(shane, GetTranslatedString("Event:DatingShaneBirthday_Shane:1"));
            e.Speak(shane, GetTranslatedString("Event:DatingShaneBirthday_Shane:2"));
            e.Speak(shane, GetTranslatedString("Event:DatingShaneBirthday_Shane:3"));
            e.EmoteFarmer_Heart();
            e.Emote_Heart("Shane");
            e.GlobalFadeOut(0.010);
            e.SetViewportPosition(-100, -100);
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingShaneBirthday_Finish:0")); //sam party finish 0
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingShaneBirthday_Finish:1")); //sam party finish 0
            e.addObjectToPlayersInventory(206, 1, false);
            e.addObjectToPlayersInventory(167, 1, false);
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:PartyOver"));
            e.End();
            return e;
        }

        public static EventHelper DatingBirthday_Harvey()
        {
            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new LocationPrecondition(Game1.getLocationFromName("HarveyRoom")));
            conditions.Add(new TimePrecondition(600, 2600));

            NPC harvey = Game1.getCharacterFromName("Harvey");

            conditions.Add(new StardustCore.Events.Preconditions.NPCSpecific.DatingNPC(harvey));

            EventHelper e = new EventHelper("BirthdayDating:Harvey", 19957, conditions, new EventStartData("playful", 6, 6, new EventStartData.FarmerData(6, 11, EventHelper.FacingDirection.Up), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(harvey,3,6, EventHelper.FacingDirection.Down),
            }));
            e.GlobalFadeIn();

            //Dialogue here.
            e.MoveFarmerUp(5, EventHelper.FacingDirection.Up, false);
            e.MoveFarmerLeft(2, EventHelper.FacingDirection.Left, false);
            e.NpcFaceDirection(harvey, EventHelper.FacingDirection.Right);
            e.Speak(harvey, GetTranslatedString("Event:DatingHarveyBirthday_Harvey:0"));
            e.Speak(harvey, GetTranslatedString("Event:DatingHarveyBirthday_Harvey:1"));
            e.EmoteFarmer_QuestionMark();
            e.Speak(harvey, GetTranslatedString("Event:DatingHarveyBirthday_Harvey:2"));
            e.Speak(harvey, GetTranslatedString("Event:DatingHarveyBirthday_Harvey:3"));


            e.EmoteFarmer_Heart();
            e.Emote_Heart("Harvey");
            e.GlobalFadeOut(0.010);
            e.SetViewportPosition(-100, -100);
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingHarveyBirthday_Finish:0")); //abi party finish 0
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingHarveyBirthday_Finish:1")); //abi party finish 0
            e.addObjectToPlayersInventory(237, 1, false);
            e.addObjectToPlayersInventory(348, 1, false);
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:PartyOver"));
            e.End();
            return e;
        }


        public static EventHelper DatingBirthday_Alex()
        {
            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new LocationPrecondition(Game1.getLocationFromName("JoshHouse")));
            conditions.Add(new TimePrecondition(600, 2600));

            NPC alex = Game1.getCharacterFromName("Alex");

            conditions.Add(new StardustCore.Events.Preconditions.NPCSpecific.DatingNPC(alex));

            EventHelper e = new EventHelper("BirthdayDating:Alex", 19959, conditions, new EventStartData("playful", 3, 20, new EventStartData.FarmerData(7, 19, EventHelper.FacingDirection.Left), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(alex,3,19, EventHelper.FacingDirection.Down),
            }));
            e.GlobalFadeIn();

            //Dialogue here.
            e.MoveFarmerLeft(3, EventHelper.FacingDirection.Left, false);
            e.NpcFaceDirection(alex, EventHelper.FacingDirection.Right);

            e.Speak(alex, GetTranslatedString("Event:DatingAlexBirthday_Alex:0"));
            e.Speak(alex, GetTranslatedString("Event:DatingAlexBirthday_Alex:1"));
            e.Speak(alex, GetTranslatedString("Event:DatingAlexBirthday_Alex:2"));
            e.Speak(alex, GetTranslatedString("Event:DatingAlexBirthday_Alex:3"));
            e.EmoteFarmer_Heart();
            e.Emote_Heart("Alex");
            e.GlobalFadeOut(0.010);
            e.SetViewportPosition(-100, -100);
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingAlexBirthday_Finish:0")); //sam party finish 0
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:DatingAlexBirthday_Finish:1")); //sam party finish 0
            e.addObjectToPlayersInventory(206, 1, false);
            e.addObjectToPlayersInventory(167, 1, false);
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:PartyOver"));
            e.End();
            return e;
        }


        /// <summary>
        /// Todo: Finish this.
        /// </summary>
        /// <returns></returns>
        public static EventHelper CommunityBirthday()
        {
            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new LocationPrecondition(Game1.getLocationFromName("CommunityCenter")));
            conditions.Add(new TimePrecondition(600, 2600));
            conditions.Add(new StardustCore.Events.Preconditions.PlayerSpecific.JojaMember(false));
            conditions.Add(new CommunityCenterCompleted(true));
            //conditions.Add(new HasUnlockedCommunityCenter()); //Infered by the fact that you must enter the community center to trigger this event anyways.
            EventHelper e = new EventHelper("CommunityCenterBirthday_All", 19961, conditions, new EventStartData("playful", -100, -100, new EventStartData.FarmerData(32, 22, EventHelper.FacingDirection.Up), new List<EventStartData.NPCData>()
            {
                new EventStartData.NPCData(Game1.getCharacterFromName("Lewis"),32,12, EventHelper.FacingDirection.Down),
                

            }));

            e.GlobalFadeIn();

            e.MoveFarmerUp(10, EventHelper.FacingDirection.Up, true);

            e.ShowMessage("Shhh. I think they are here.");
            e.ShowMessage("Somebody turn on the lights.");
            e.SetViewportPosition(32, 12);


            e.EmoteFarmer_ExclamationMark();
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:CommunityBirthdayParty_0"));
            e.EmoteFarmer_Heart();
            e.GlobalFadeOut(0.010);
            e.SetViewportPosition(-100, -100);
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:CommunityBirthdayParty_1"));
            e.ShowMessage(HappyBirthday.Config.translationInfo.getTranslatedString("Event:PartyOver"));
            e.addObjectToPlayersInventory(220, 1, false);

            e.End();

            return e;
        }

        /*

        public static EventHelper MarriedBirthday()
        {

        }




        public static EventHelper JojaBirthday()
        {

        }
        */


        public static string GetTranslatedString(string Key)
        {
            return HappyBirthday.Config.translationInfo.getTranslatedString(Key);
        }

    }
}
