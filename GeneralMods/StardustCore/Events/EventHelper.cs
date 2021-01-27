using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using StardustCore.Events.Preconditions;
using StardustCore.Events.Preconditions.TimeSpecific;
using StardewValley;

namespace StardustCore.Events
{
    /// <summary>
    ///TODO: Figure out forked dialogue
    /// 
    /// Helps in creating events based in code for stardew valley.
    /// https://stardewvalleywiki.com/Modding:Event_data
    ///
    /// A lot of the function comments were taken from the SDV wiki.
    /// </summary>
    public class EventHelper
    {

        /// <summary>
        /// Nexus user id for Omegasis (aka me).
        /// </summary>
        protected int nexusUserId = 32171640;

        /// <summary>
        /// Wraps SDV facing direction.
        /// </summary>
        public enum FacingDirection
        {
            Up,
            Right,
            Down,
            Left
        }

        public enum Layers
        {
            Back,
            Paths,
            Buildings,
            Front,
            AlwaysFront
        }

        public enum FestivalItemType
        {
            Pan,
            Sculpture,
            Rod,
            Sword,
            Hero,
            Joja,
            SlimeEgg
        }


        protected StringBuilder eventData;
        protected StringBuilder eventPreconditionData;
        protected List<EventPrecondition> eventPreconditions;
        protected int eventID;

        public string eventName;

        public EventHelper()
        {
            this.eventData = new StringBuilder();
            this.eventPreconditionData = new StringBuilder();
            this.eventPreconditions = new List<EventPrecondition>();
        }

        public EventHelper(string EventName,int ID, LocationPrecondition Location, TimePrecondition Time, EventDayExclusionPrecondition NotTheseDays, EventStartData StartData)
        {
            this.eventName = EventName;
            this.eventData = new StringBuilder();
            this.eventPreconditionData = new StringBuilder();
            this.eventPreconditions = new List<EventPrecondition>();
            this.eventID = ID;
            this.Add(Location);
            this.Add(Time);
            this.Add(NotTheseDays);
            this.Add(StartData.ToString());
        }

        public EventHelper(string EventName,int ID,List<EventPrecondition> Conditions, EventStartData StartData)
        {
            this.eventName = EventName;
            this.eventID = ID;
            this.eventData = new StringBuilder();
            this.eventPreconditions = new List<EventPrecondition>();
            this.eventPreconditionData = new StringBuilder();
            foreach (var v in Conditions)
            {
                this.Add(v);
            }
            this.Add(StartData.ToString());
        }

        /// <summary>
        /// Adds in the event precondition data to the string builder and appends seperators as necessary.
        /// </summary>
        /// <param name="Data"></param>
        public virtual void Add(EventPrecondition Data)
        {
            this.eventPreconditions.Add(Data);
            if (this.eventPreconditionData.Length > 0)
            {
                this.eventPreconditionData.Append(this.GetSeperator());
            }
            this.eventPreconditionData.Append(Data.ToString());
        }

        /// <summary>
        /// Adds in the data to the event data.Aka what happens during the event.
        /// </summary>
        /// <param name="Data"></param>
        public virtual void Add(string Data)
        {

            if (this.eventData.Length > 0)
            {
                this.eventData.Append(this.GetSeperator());
            }
            this.eventData.Append(Data);
        }

        /// <summary>
        /// Adds in the data to the event data. Aka what happens during the event.
        /// </summary>
        /// <param name="Builder"></param>
        public virtual void Add(StringBuilder Builder)
        {
            this.Add(Builder.ToString());
        }


        /// <summary>
        /// Converts the direction to enum.
        /// </summary>
        /// <param name="Dir"></param>
        /// <returns></returns>
        public virtual int GetFacingDirectionNumber(FacingDirection Dir)
        {
            return (int)Dir;
        }

        /// <summary>
        /// Gets the layer string from the Layer enum. Has weird values???/
        /// </summary>
        /// <param name="Layer"></param>
        /// <returns></returns>
        public virtual int GetLayerName(Layers Layer)
        {
            return 724;
            //if (Layer == Layers.AlwaysFront) return "AlwaysFront";
            //if (Layer == Layers.Back) return "Back";
            //if (Layer == Layers.Buildings) return "Buildings";
            //if (Layer == Layers.Front) return "Front";
            //if (Layer == Layers.Paths) return "Paths";
            //return "";
        }

        /// <summary>
        /// Gets the even parsing seperator.
        /// </summary>
        /// <returns></returns>
        public virtual string GetSeperator()
        {
            return "/";
        }

        /// <summary>
        /// Gets the starting event numbers based off of my nexus user id.
        /// </summary>
        /// <returns></returns>
        public virtual string GetUniqueEventStartID()
        {
            string s = this.nexusUserId.ToString();
            return s.Substring(0, 4);
        }
        /// <summary>
        /// Gets the id for the event.
        /// </summary>
        /// <returns></returns>
        public virtual int GetEventID()
        {
            return Convert.ToInt32(this.GetUniqueEventStartID() + this.eventID.ToString());
        }

        /// <summary>
        /// Checks to ensure I don't create a id value that is too big for nexus.
        /// </summary>
        /// <param name="IDToCheck"></param>
        /// <returns></returns>
        public virtual bool IsIdValid(int IDToCheck)
        {
            if (IDToCheck > 2147483647 || IDToCheck < 0) return false;
            else return true;
        }

        /// <summary>
        /// Checks to ensure I don't create a id value that is too big for nexus.
        /// </summary>
        /// <param name="IDToCheck"></param>
        /// <returns></returns>
        public virtual bool IsIdValid(string IDToCheck)
        {
            if (Convert.ToInt32(IDToCheck) > 2147483647 || Convert.ToInt32(IDToCheck) < 0) return false;
            else return true;
        }
        /// <summary>
        /// Gets the string representation for the event's data.
        /// </summary>
        /// <returns></returns>
        public virtual string GetEventString()
        {
            return this.eventData.ToString();
        }
        /// <summary>
        /// Creates the Stardew Valley event from the given event data.
        /// </summary>
        /// <param name="PlayerActor"></param>
        /// <returns></returns>
        public virtual StardewValley.Event GetEvent(Farmer PlayerActor = null)
        {
            return new StardewValley.Event(this.GetEventString(), Convert.ToInt32(this.GetEventID()), PlayerActor);
        }

        /// <summary>
        /// Checks to see if all of the event preconditions have been met and starts the event if so.
        /// </summary>
        public virtual bool StartEventAtLocationifPossible()
        {
            if (this.CanEventOccur())
            {
                //Game1.player.currentLocation.currentEvent = this.getEvent();
                Game1.player.currentLocation.startEvent(this.GetEvent());
                return true;
            }
            return false;
        }
        

        //~~~~~~~~~~~~~~~~//
        //   Validation   //
        //~~~~~~~~~~~~~~~~//

        /// <summary>
        /// Checks to see if the event can occur.
        /// </summary>
        /// <returns></returns>
        public virtual bool CanEventOccur()
        {
            foreach (EventPrecondition eve in this.eventPreconditions)
            {
                if (eve.meetsCondition() == false) return false;
            }

            return true;
        }

        //~~~~~~~~~~~~~~~~//
        //      Actions   //
        //~~~~~~~~~~~~~~~~//

        /// <summary>
        /// Adds an object at the specified tile from the TileSheets\Craftables.png sprite sheet
        /// </summary>
        /// <param name="xTile"></param>
        /// <param name="yTile"></param>
        /// <param name="ID"></param>
        public virtual void AddBigProp(int xTile, int yTile, int ID)
        {
            StringBuilder b = new StringBuilder();
            b.Append("addBigProp ");
            b.Append(xTile.ToString());
            b.Append(" ");
            b.Append(yTile.ToString());
            b.Append(" ");
            b.Append(ID.ToString());
            this.Add(b);
        }

        /// <summary>
        /// Starts an active dialogue event with the given ID and a length of 4 days.
        /// </summary>
        /// <param name="ID"></param>
        public virtual void AddConversationTopic(string ID)
        {
            StringBuilder b = new StringBuilder();
            b.Append("addBigProp ");
            b.Append(ID);
            this.Add(b);
        }

        /// <summary>
        /// Adds the specified cooking recipe to the player.
        /// </summary>
        /// <param name="Recipe"></param>
        public virtual void AddCookingRecipe(string Recipe)
        {
            StringBuilder b = new StringBuilder();
            b.Append("addCookingRecipe ");
            b.Append(Recipe);
            this.Add(b);
        }

        /// <summary>
        /// Adds the specified crafting recipe to the player.
        /// </summary>
        /// <param name="Recipe"></param>
        public virtual void AddCraftingRecipe(string Recipe)
        {
            StringBuilder b = new StringBuilder();
            b.Append("addCraftingRecipe ");
            b.Append(Recipe);
            this.Add(b);
        }

        /// <summary>
        /// Add a non-solid prop from the current festival texture. Default solid width/height is 1. Default display height is solid height.
        /// </summary>
        public virtual void AddFloorProp(int PropIndex, int XTile, int YTile, int SolidWidth, int SolidHeight, int DisplayHeight)
        {
            StringBuilder b = new StringBuilder();
            b.Append("addFloorProp ");
            b.Append(PropIndex.ToString());
            b.Append(" ");
            b.Append(XTile.ToString());
            b.Append(" ");
            b.Append(YTile.ToString());
            b.Append(" ");
            b.Append(SolidWidth.ToString());
            b.Append(" ");
            b.Append(SolidHeight.ToString());
            b.Append(" ");
            b.Append(DisplayHeight.ToString());
            this.Add(b);
        }

        /// <summary>
        /// Adds a glowing temporary sprite at the specified tile from the Maps\springobjects.png sprite sheet. A light radius of 0 just places the sprite.
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="XPosition"></param>
        /// <param name="YPosition"></param>
        /// <param name="LightRadius"></param>
        public virtual void AddLantern(int ItemID, int XPosition, int YPosition, float LightRadius)
        {
            StringBuilder b = new StringBuilder();
            b.Append("addLantern ");
            b.Append(ItemID.ToString());
            b.Append(" ");
            b.Append(XPosition.ToString());
            b.Append(" ");
            b.Append(YPosition.ToString());
            b.Append(" ");
            b.Append(LightRadius.ToString());
            this.Add(b);
        }

        /// <summary>
        /// 	Set a letter as received.
        /// </summary>
        /// <param name="ID"></param>
        public virtual void AddMailReceived(string ID)
        {
            StringBuilder b = new StringBuilder();
            b.Append("addMailReceived ");
            b.Append(ID);
            this.Add(b);
        }

        /// <summary>
        /// Adds a temporary sprite at the specified tile from the Maps\springobjects.png sprite sheet.
        /// </summary>
        /// <param name="XTile"></param>
        /// <param name="YTile"></param>
        /// <param name="ParentSheetIndex"></param>
        /// <param name="Layer"></param>
        public virtual void AddObject(int XTile, int YTile, int ParentSheetIndex)
        {
            StringBuilder b = new StringBuilder();
            b.Append("addObject ");
            b.Append(XTile.ToString());
            b.Append(" ");
            b.Append(YTile.ToString());
            b.Append(" ");
            b.Append(ParentSheetIndex);
            this.Add(b);
        }

        /// <summary>
        /// Add a solid prop from the current festival texture. Default solid width/height is 1. Default display height is solid height.
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="XTile"></param>
        /// <param name="YTile"></param>
        /// <param name="SolidWidth"></param>
        /// <param name="SolidHeight"></param>
        /// <param name="DisplayHeight"></param>
        public virtual void AddProp(int Index, int XTile, int YTile, int SolidWidth, int SolidHeight, int DisplayHeight)
        {
            StringBuilder b = new StringBuilder();
            b.Append("addProp ");
            b.Append(Index.ToString());
            b.Append(" ");
            b.Append(XTile.ToString());
            b.Append(" ");
            b.Append(YTile.ToString());
            b.Append(" ");
            b.Append(SolidWidth.ToString());
            b.Append(" ");
            b.Append(SolidHeight.ToString());
            b.Append(" ");
            b.Append(DisplayHeight.ToString());
            this.Add(b);
        }

        /// <summary>
        /// Add the specified quest to the quest log.
        /// </summary>
        /// <param name="QuestID"></param>
        public virtual void AddQuest(int QuestID)
        {
            StringBuilder b = new StringBuilder();
            b.Append("addQuest ");
            b.Append(QuestID.ToString());
            this.Add(b);
        }

        /// <summary>
        /// Add a temporary actor. 'breather' is boolean. The category determines where the texture will be loaded from, default is Character. Animal name only applies to animal.
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="SpriteWidth"></param>
        /// <param name="SpriteHeight"></param>
        /// <param name="TileX"></param>
        /// <param name="TileY"></param>
        /// <param name="Direction"></param>
        /// <param name="Breather"></param>
        public virtual void AddTemporaryActor_NPC(NPC npc, int SpriteWidth, int SpriteHeight, int TileX, int TileY, FacingDirection Direction, bool Breather)
        {
            StringBuilder b = new StringBuilder();
            b.Append("addTemporaryActor ");
            b.Append(npc.Name);
            b.Append(" ");
            b.Append(SpriteWidth.ToString());
            b.Append(" ");
            b.Append(SpriteHeight.ToString());
            b.Append(" ");
            b.Append(TileX.ToString());
            b.Append(" ");
            b.Append(TileY.ToString());
            b.Append(" ");
            b.Append(this.GetFacingDirectionNumber(Direction));
            b.Append(" ");
            b.Append(Breather);
            b.Append(" ");
            b.Append("Character");
            this.Add(b);
        }


        public virtual void AddTemporaryActor_NPC(string npc, int SpriteWidth, int SpriteHeight, int TileX, int TileY, FacingDirection Direction, bool Breather)
        {
            StringBuilder b = new StringBuilder();
            b.Append("addTemporaryActor ");
            b.Append(npc);
            b.Append(" ");
            b.Append(SpriteWidth.ToString());
            b.Append(" ");
            b.Append(SpriteHeight.ToString());
            b.Append(" ");
            b.Append(TileX.ToString());
            b.Append(" ");
            b.Append(TileY.ToString());
            b.Append(" ");
            b.Append(this.GetFacingDirectionNumber(Direction));
            b.Append(" ");
            b.Append(Breather);
            b.Append(" ");
            b.Append("Character");
            this.Add(b);
        }
        /// <summary>
        /// Add a temporary actor. 'breather' is boolean. The category determines where the texture will be loaded from, default is Character. Animal name only applies to animal.
        /// </summary>
        /// <param name="character"></param>
        /// <param name="SpriteWidth"></param>
        /// <param name="SpriteHeight"></param>
        /// <param name="TileX"></param>
        /// <param name="TileY"></param>
        /// <param name="Direction"></param>
        /// <param name="Breather"></param>
        /// <param name="AnimalName"></param>
        public virtual void AddTemporaryActor_Animal(string character, int SpriteWidth, int SpriteHeight, int TileX, int TileY, FacingDirection Direction, bool Breather, string AnimalName)
        {
            StringBuilder b = new StringBuilder();
            b.Append("addTemporaryActor ");
            b.Append(character);
            b.Append(" ");
            b.Append(SpriteWidth.ToString());
            b.Append(" ");
            b.Append(SpriteHeight.ToString());
            b.Append(" ");
            b.Append(TileX.ToString());
            b.Append(" ");
            b.Append(TileY.ToString());
            b.Append(" ");
            b.Append(Direction);
            b.Append(" ");
            b.Append(Breather);
            b.Append(" ");
            b.Append("Animal");
            b.Append(" ");
            b.Append(AnimalName);
            this.Add(b);
        }

        /// <summary>
        /// Add a temporary actor. 'breather' is boolean. The category determines where the texture will be loaded from, default is Character. Animal name only applies to animal.
        /// </summary>
        /// <param name="character"></param>
        /// <param name="SpriteWidth"></param>
        /// <param name="SpriteHeight"></param>
        /// <param name="TileX"></param>
        /// <param name="TileY"></param>
        /// <param name="Direction"></param>
        /// <param name="Breather"></param>
        public virtual void AddTemporaryActor_Monster(string character, int SpriteWidth, int SpriteHeight, int TileX, int TileY, FacingDirection Direction, bool Breather)
        {
            StringBuilder b = new StringBuilder();
            b.Append("addTemporaryActor ");
            b.Append(character);
            b.Append(" ");
            b.Append(SpriteWidth.ToString());
            b.Append(" ");
            b.Append(SpriteHeight.ToString());
            b.Append(" ");
            b.Append(TileX.ToString());
            b.Append(" ");
            b.Append(TileY.ToString());
            b.Append(" ");
            b.Append(Direction);
            b.Append(" ");
            b.Append(Breather);
            b.Append(" ");
            b.Append("Monster");
            this.Add(b);
        }

        /// <summary>
        /// Places on object on the furniture at a position. If the location is FarmHouse, then it will always be placed on the initial table.
        /// </summary>
        /// <param name="XPosition"></param>
        /// <param name="YPosition"></param>
        /// <param name="ObjectParentSheetIndex"></param>
        public virtual void AddToTable(int XPosition, int YPosition, int ObjectParentSheetIndex)
        {
            StringBuilder b = new StringBuilder();
            b.Append("addToTable ");
            b.Append(XPosition);
            b.Append(" ");
            b.Append(YPosition);
            b.Append(" ");
            b.Append(ObjectParentSheetIndex);
            this.Add(b);
        }

        /// <summary>
        /// Adds the Return Scepter to the player's inventory.
        /// </summary>
        public virtual void AddTool_ReturnScepter()
        {
            StringBuilder b = new StringBuilder();
            b.Append("addTool Wand");
            this.Add(b);
        }

        /// <summary>
        /// 	Set multiple movements for an NPC. You can set True to have NPC walk the path continuously. Example: /advancedMove Robin false 0 3 2 0 0 2 -2 0 0 -2 2 0/
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="Loop"></param>
        /// <param name="TilePoints"></param>
        public virtual void AdvanceMove(NPC npc, bool Loop, List<Point> TilePoints)
        {
            StringBuilder b = new StringBuilder();
            b.Append("advancedMove ");
            b.Append(npc.Name);
            b.Append(" ");
            b.Append(Loop.ToString());
            b.Append(" ");
            for (int i = 0; i < TilePoints.Count; i++)
            {
                b.Append(TilePoints[i].X);
                b.Append(" ");
                b.Append(TilePoints[i].Y);
                if (i != TilePoints.Count - 1)
                {
                    b.Append(" ");
                }
            }
            this.Add(b);
        }

        public virtual void AdvanceMove(string Actor, bool Loop, List<Point> TilePoints)
        {
            StringBuilder b = new StringBuilder();
            b.Append("advancedMove ");
            b.Append(Actor);
            b.Append(" ");
            b.Append(Loop.ToString());
            b.Append(" ");
            for (int i = 0; i < TilePoints.Count; i++)
            {
                b.Append(TilePoints[i].X);
                b.Append(" ");
                b.Append(TilePoints[i].Y);
                if (i != TilePoints.Count - 1)
                {
                    b.Append(" ");
                }
            }

            ModCore.ModMonitor.Log(b.ToString(), StardewModdingAPI.LogLevel.Info);

            this.Add(b);
        }

        /// <summary>
        /// Modifies the ambient light level, with RGB values from 0 to 255. Note that it works by removing colors from the existing light ambience, so ambientLight 1 80 80 would reduce green and blue and leave the light with a reddish hue.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public virtual void SetAmbientLight(int r, int g, int b)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("ambientLight ");
            builder.Append(r);
            builder.Append(" ");
            builder.Append(g);
            builder.Append(" ");
            builder.Append(b);
            this.Add(builder);
        }

        /// <summary>
        /// Modifies the ambient light level, with RGB values from 0 to 255. Note that it works by removing colors from the existing light ambience, so ambientLight 1 80 80 would reduce green and blue and leave the light with a reddish hue.
        /// </summary>
        /// <param name="color"></param>
        public virtual void SetAmbientLight(Color color)
        {
            this.SetAmbientLight(color.R, color.G, color.B);
        }

        //animalNaming

        public virtual void AnimalNaming()
        {
            StringBuilder b = new StringBuilder();
            b.Append("animalNaming");
            this.Add(b);
        }

        /// <summary>
        /// Animate a named actor, using one or more <frames> from their sprite sheet, for <frame duration> milliseconds per frame. <flip> indicates whether to flip the sprites along the Y axis; <loop> indicates whether to repeat the animation until stopAnimation is used.
        /// </summary>
        /// <param name="ActorName"></param>
        /// <param name="Flip"></param>
        /// <param name="Loop"></param>
        /// <param name="FrameDuration">In milliseconds</param>
        /// <param name="Frames"></param>
        public virtual void Animate(string ActorName, bool Flip, bool Loop, int FrameDuration, List<int> Frames)
        {
            StringBuilder b = new StringBuilder();
            b.Append("animate ");
            b.Append(ActorName);
            b.Append(" ");
            b.Append(Flip.ToString().ToLowerInvariant());
            b.Append(" ");
            b.Append(Loop.ToString().ToLowerInvariant());
            b.Append(" ");
            b.Append(FrameDuration);
            b.Append(" ");
            for (int i = 0; i < Frames.Count; i++)
            {
                b.Append(Frames[i]);
                if (i != Frames.Count - 1)
                {
                    b.Append(" ");
                }
            }
            this.Add(b);
        }

        /// <summary>
        /// Attach an actor to the most recent temporary sprite.
        /// </summary>
        /// <param name="Actor"></param>
        public virtual void AttachCharacterToTempSprite(string Actor)
        {
            StringBuilder b = new StringBuilder();
            b.Append("attachCharacterToTempSprite ");
            b.Append(Actor);
            this.Add(b);
        }

        /// <summary>
        /// Awards the festival prize to the winner for the easter egg hunt and ice fishing contest.
        /// </summary>
        public virtual void AwardFestivalPrize()
        {
            StringBuilder b = new StringBuilder();
            b.Append("awardFestivalPrize");
            this.Add(b);
        }

        /// <summary>
        /// Awards the specified item to the player. Possible item types are "pan", "sculpture", "rod", "sword", "hero", "joja", and "slimeegg".
        /// </summary>
        /// <param name="ItemType"></param>
        public virtual void AwardFestivalPrize(FestivalItemType ItemType)
        {
            StringBuilder b = new StringBuilder();
            b.Append("awardFestivalPrize ");

            if (ItemType == FestivalItemType.Hero)
            {
                b.Append("hero");
            }
            else if (ItemType == FestivalItemType.Joja)
            {
                b.Append("joja");
            }
            else if (ItemType == FestivalItemType.Pan)
            {
                b.Append("pan");
            }
            else if (ItemType == FestivalItemType.Rod)
            {
                b.Append("rod");
            }
            else if (ItemType == FestivalItemType.Sculpture)
            {
                b.Append("sculpture");
            }
            else if (ItemType == FestivalItemType.SlimeEgg)
            {
                b.Append("slimeegg");
            }
            else if (ItemType == FestivalItemType.Sword)
            {
                b.Append("sword");
            }

            this.Add(b);
        }

        /// <summary>
        /// Causes the event to be seen by all players when triggered.
        /// </summary>
        public virtual void BroadcastEvent()
        {
            StringBuilder b = new StringBuilder();
            b.Append("broadcastEvent");
            this.Add(b);
        }

        /// <summary>
        /// Trigger question about adopting your pet.
        /// </summary>
        public virtual void PetAdoptionQuestion()
        {
            StringBuilder b = new StringBuilder();
            b.Append("catQuestion");
            this.Add(b);
        }

        /// <summary>
        /// Trigger the question for the farm cave type. This will work again later, however changing from bats to mushrooms will not Remove the mushroom spawning objects.
        /// </summary>
        public virtual void FarmCaveTypeQuestion()
        {
            StringBuilder b = new StringBuilder();
            b.Append("cave");
            this.Add(b);
        }

        /// <summary>
        /// Change to another location and run the remaining event script there.
        /// </summary>
        /// <param name="location"></param>
        public virtual void ChangeLocation(GameLocation location)
        {
            StringBuilder b = new StringBuilder();
            b.Append("changeLocation ");
            b.Append(location.NameOrUniqueName);
            this.Add(b);
        }


        /// <summary>
        /// Change to another location and run the remaining event script there.
        /// </summary>
        /// <param name="location"></param>
        public virtual void ChangeLocation(string location)
        {
            StringBuilder b = new StringBuilder();
            b.Append("changeLocation ");
            b.Append(location);
            this.Add(b);
        }

        /// <summary>
        /// Change the specified tile to a particular value.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="TileIndex"></param>
        public virtual void ChangeMapTile(Layers l, int X, int Y, int TileIndex)
        {
            StringBuilder b = new StringBuilder();
            b.Append("changeMapTile ");
            b.Append(this.GetLayerName(l));
            b.Append(" ");
            b.Append(X);
            b.Append(" ");
            b.Append(Y);
            b.Append(" ");
            b.Append(TileIndex);
            this.Add(b);
        }

        /// <summary>
        /// Change the NPC's portrait to be from "Portraits/<actor>_<Portrait>".
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="Portrait"></param>
        public virtual void ChangePortrait(string npc, string Portrait)
        {
            StringBuilder b = new StringBuilder();
            b.Append("changePortrait ");
            b.Append(npc);
            b.Append(" ");
            b.Append(Portrait);
            this.Add(b);
        }

        /// <summary>
        /// Change the actor's sprite to be from "Characters/<actor>_<sprite>".
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="Sprite"></param>
        public virtual void ChangeSprite(string npc, string Sprite)
        {
            StringBuilder b = new StringBuilder();
            b.Append("changeSprite ");
            b.Append(npc);
            b.Append(" ");
            b.Append(Sprite);
            this.Add(b);
        }

        /// <summary>
        /// 	Change the location to a temporary one loaded from the map file specified by <map>. The [pan] argument indicates the tile coordinates to pan to (defaults to 0, 0).
        /// </summary>
        /// <param name="Map"></param>
        public virtual void ChangeToTemporaryMap(string Map)
        {
            StringBuilder b = new StringBuilder();
            b.Append("changeToTemporaryMap ");
            b.Append(Map);
            this.Add(b);
        }
        /// <summary>
        /// 	Change the location to a temporary one loaded from the map file specified by <map>. The [pan] argument indicates the tile coordinates to pan to (defaults to 0, 0).
        /// </summary>
        /// <param name="Map"></param>
        /// <param name="PanPosition"></param>
        public virtual void ChangeToTemporaryMap(string Map, Point PanPosition)
        {
            StringBuilder b = new StringBuilder();
            b.Append("changeToTemporaryMap ");
            b.Append(Map);
            b.Append(" ");
            b.Append(PanPosition.X);
            b.Append(" ");
            b.Append(PanPosition.Y);
            this.Add(b);
        }

        /// <summary>
        /// Changes the NPC's vertical texture offset. Example: ChangeYSourceRectOffset Abigail 96 will offset her sprite sheet, showing her looking left instead of down. This persists for the rest of the event. This is only used in Emily's Clothing Therapy event to display the various outfits properly.
        /// </summary>
        /// <param name="NPC"></param>
        /// <param name="YOffset"></param>
        public virtual void ChangeYSourceRectOffset(string NPC, int YOffset)
        {
            StringBuilder b = new StringBuilder();
            b.Append("changeYSourceRectOffset ");
            b.Append(NPC);
            b.Append(" ");
            b.Append(YOffset);
            this.Add(b);
        }

        /// <summary>
        /// Acts as if the player had clicked the specified x/y coordinate and triggers any relevant action. It is commonly used to open doors from inside events, but it can be used for other purposes. If you use it on an NPC you will talk to them, and if the player is holding an item they will give that item as a gift. doAction activates objects in the main game world (their actual location outside of the event), so activating NPCs like this is very tricky, and their reaction varies depending on what the player is holding.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public virtual void DoAction(int x, int y)
        {
            StringBuilder b = new StringBuilder();
            b.Append("doAction ");
            b.Append(x);
            b.Append(" ");
            b.Append(y);
            this.Add(b);
        }

        /// <summary>
        /// Make the given NPC name perform an emote, which is a little icon shown above the NPC's head. Emotes are stored in Content\TileSheets\emotes.xnb (see list of emotes).
        /// </summary>
        /// <param name="Actor"></param>
        /// <param name="EmoteID"></param>
        public virtual void Emote(string Actor, int EmoteID)
        {
            StringBuilder b = new StringBuilder();
            b.Append("emote ");
            b.Append(Actor);
            b.Append(" ");
            b.Append(EmoteID*4);
            this.Add(b);
        }

        public virtual void EmoteFarmer(int EmoteID)
        {
            this.Emote("farmer", EmoteID);
        }

        public virtual void Emote_Empty(string Actor)
        {
            this.Emote(Actor, 0);
        }

        public virtual void Emote_NoWater(string Actor)
        {
            this.Emote(Actor, 1);
        }

        public virtual void Emote_QuestionMark(string Actor)
        {
            this.Emote(Actor, 2);
        }

        public virtual void Emote_Angry(string Actor)
        {
            this.Emote(Actor, 3);
        }

        public virtual void Emote_ExclamationMark(string Actor)
        {
            this.Emote(Actor, 4);
        }
        public virtual void Emote_Heart(string Actor)
        {
            this.Emote(Actor, 5);
        }
        public virtual void Emote_Sleeping(string Actor)
        {
            this.Emote(Actor, 6);
        }
        public virtual void Emote_Sad(string Actor)
        {
            this.Emote(Actor, 7);
        }
        public virtual void Emote_Happy(string Actor)
        {
            this.Emote(Actor, 8);
        }
        public virtual void Emote_No(string Actor)
        {
            this.Emote(Actor, 9);
        }
        public virtual void Emote_Pause(string Actor)
        {
            this.Emote(Actor,10);
        }
        public virtual void Emote_Thinking(string Actor)
        {
            this.Emote(Actor, 10);
        }
        public virtual void Emote_Fishing(string Actor)
        {
            this.Emote(Actor, 11);
        }

        public virtual void Emote_CommunityCenterTablet(string Actor)
        {
            this.Emote(Actor, 12);
        }

        public virtual void Emote_Gaming(string Actor)
        {
            this.Emote(Actor, 13);
        }
        public virtual void Emote_MusicNote(string Actor)
        {
            this.Emote(Actor, 14);
        }
        public virtual void Emote_Blushing(string Actor)
        {
            this.Emote(Actor, 15);
        }
        public virtual void Emote_Embarrased(string Actor)
        {
            this.Emote_Blushing(Actor);
        }

        public virtual void EmoteFarmer_Empty()
        {
            this.EmoteFarmer(0);
        }

        public virtual void EmoteFarmer_NoWater()
        {
            this.EmoteFarmer(1);
        }

        public virtual void EmoteFarmer_QuestionMark()
        {
            this.EmoteFarmer(2);
        }

        public virtual void EmoteFarmer_Angry()
        {
            this.EmoteFarmer(3);
        }

        public virtual void EmoteFarmer_ExclamationMark()
        {
            this.EmoteFarmer(4);
        }
        public virtual void EmoteFarmer_Heart()
        {
            this.EmoteFarmer(5);
        }
        public virtual void EmoteFarmer_Sleeping()
        {
            this.EmoteFarmer(6);
        }
        public virtual void EmoteFarmer_Sad()
        {
            this.EmoteFarmer(7);
        }
        public virtual void EmoteFarmer_Happy()
        {
            this.EmoteFarmer(8);
        }
        public virtual void EmoteFarmer_No()
        {
            this.EmoteFarmer(9);
        }
        public virtual void EmoteFarmer_Pause()
        {
            this.EmoteFarmer(10);
        }
        public virtual void EmoteFarmer_Thinking()
        {
            this.EmoteFarmer_Pause();
        }
        public virtual void EmoteFarmer_Fishing()
        {
            this.EmoteFarmer(11);
        }

        public virtual void EmoteFarmer_CommunityCenterTablet()
        {
            this.EmoteFarmer(12);
        }

        public virtual void EmoteFarmer_Gaming()
        {
            this.EmoteFarmer(13);
        }
        public virtual void EmoteFarmer_MusicNote()
        {
            this.EmoteFarmer(14);
        }
        public virtual void EmoteFarmer_Blushing()
        {
            this.EmoteFarmer(15);
        }
        public virtual void Emote_Embarrased()
        {
            this.EmoteFarmer_Blushing();
        }


        /// <summary>
        /// Ends the current event by fading out, then resumes the game world and places the player on the square where they entered the zone. All end parameters do this by default unless otherwise stated.
        /// </summary>
        public virtual void End()
        {
            StringBuilder b = new StringBuilder();
            b.Append("end");
            this.Add(b);
        }

        /// <summary>
        /// Same as end, and Additionally clears the existing NPC dialogue for the day and replaces it with the line(s) specified at the end of the command. Example usage: end dialogue Abigail "It was fun talking to you today.$h"
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="Dialogue"></param>
        public virtual void EndDialogue(NPC npc, string Dialogue)
        {
            StringBuilder b = new StringBuilder();
            b.Append("end dialogue ");
            b.Append(npc.Name);
            b.Append(" ");
            b.Append(Dialogue);
            this.Add(b);
        }

        /// <summary>
        /// Ends the event, sets npc dialogue, and warps the player out of the location.
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="Dialogue"></param>
        public virtual void EndDialogueWarpOut(NPC npc, string Dialogue)
        {
            StringBuilder b = new StringBuilder();
            b.Append("end dialogueWarpOut ");
            b.Append(npc.Name);
            b.Append(" ");
            b.Append(Dialogue);
            this.Add(b);
        }

        /// <summary>
        /// Same as end, and Additionally turns the specified NPC invisible (cannot be interacted with until the next day).
        /// </summary>
        /// <param name="npc"></param>

        public virtual void EndInvisible(NPC npc)
        {
            StringBuilder b = new StringBuilder();
            b.Append("end invisible ");
            b.Append(npc.Name);
            b.Append(" ");
            this.Add(b);
        }

        public virtual void EndInvisibleWarpOut(NPC npc)
        {
            StringBuilder b = new StringBuilder();
            b.Append("end invisibleWarpOut ");
            b.Append(npc.Name);
            b.Append(" ");
            this.Add(b);
        }

        /// <summary>
        /// Ends both the event and the day (warping player to their bed, saving the game, selling everything in the shipping box, etc).
        /// </summary>

        public virtual void EndNewDay()
        {
            StringBuilder b = new StringBuilder();
            b.Append("end newDay");
            this.Add(b);
        }

        /// <summary>
        /// Same as end, and Additionally warps the player to the map coordinates specified in x y.
        /// </summary>
        /// <param name="xTile"></param>
        /// <param name="yTile"></param>
        public virtual void EndPosition(int xTile, int yTile)
        {
            StringBuilder b = new StringBuilder();
            b.Append("end position");
            b.Append(xTile);
            b.Append(" ");
            b.Append(yTile);
            this.Add(b);
        }

        public virtual void EndWarpOut()
        {
            StringBuilder b = new StringBuilder();
            b.Append("end warpOut");
            this.Add(b);
        }

        public virtual void PlayerFaceDirection(FacingDirection Dir)
        {
            this.ActorFaceDirection("farmer",Dir);
        }

        public virtual void NpcFaceDirection(NPC NPC, FacingDirection Dir)
        {
            this.ActorFaceDirection(NPC.Name, Dir);
        }

        public virtual void ActorFaceDirection(string Actor, FacingDirection Dir)
        {
            StringBuilder b = new StringBuilder();
            b.Append("faceDirection ");
            b.Append(Actor);
            b.Append(" ");
            b.Append(this.GetFacingDirectionNumber(Dir).ToString());
            b.Append(" ");
            b.Append(true);
            this.Add(b);
        }


        /// <summary>
        /// Special code to make junimos face a direction because it doesn't work the same as npcs.
        /// </summary>
        /// <param name="Actor">The name of the junimo actor.</param>
        /// <param name="Dir">The direction for the junimo to face.</param>
        public virtual void JunimoFaceDirection(string Actor,FacingDirection Dir)
        {
            this.ActorFaceDirection(Actor, Dir);
            int frame = 0;
            bool flip = false;
            if(Dir.Equals(FacingDirection.Down))
            {
                frame = 0;
            }
            else if(Dir.Equals(FacingDirection.Left))
            {
                frame = 16;
                flip = true;
            }
            else if (Dir.Equals(FacingDirection.Right))
            {
                frame = 16;
            }
            else if(Dir.Equals(FacingDirection.Up))
            {
                frame = 32;
            }
            this.Animate(Actor, flip, true, 250, new List<int>() { frame, frame });
        }

        /// <summary>
        /// Fades out the screen.
        /// </summary>
        public virtual void FadeOut()
        {
            StringBuilder b = new StringBuilder();
            b.Append("fade true");
            this.Add(b);
        }

        public virtual void FadeIn()
        {
            StringBuilder b = new StringBuilder();
            b.Append("fade");
            this.Add(b);
        }

        /// <summary>
        /// Makes the farmer eat an object.
        /// </summary>
        /// <param name="ID"></param>
        public virtual void FarmerEatObject(int ID)
        {
            StringBuilder b = new StringBuilder();
            b.Append("farmerEat ");
            b.Append(ID);
            this.Add(b);
        }

        //TODO: Support event forking.

        /// <summary>
        /// Add the given number of friendship points with the named NPC. (There are 250 points per heart.)
        /// </summary>
        /// <param name="NPC"></param>
        /// <param name="Amount"></param>
        public virtual void AddFriendship(NPC NPC, int Amount)
        {
            StringBuilder b = new StringBuilder();
            b.Append("friendship ");
            b.Append(NPC.Name);
            b.Append(" ");
            b.Append(Amount);
            this.Add(b);
        }

        /// <summary>
        /// Fade to black at a particular speed (default 0.007). If no speed is specified, the event will continue immediately; otherwise, it will continue after the fade is finished. The fade effect disappears when this command is done; to avoid that, use the viewport command to move the camera off-screen.
        /// </summary>
        /// <param name="speed"></param>
        public virtual void GlobalFadeOut(double speed=0.007)
        {
            StringBuilder b = new StringBuilder();
            b.Append("globalFade ");
            b.Append(speed);
            this.Add(b);
        }

        public virtual void GlobalFadeIn(double speed=0.007)
        {
            StringBuilder b = new StringBuilder();
            b.Append("globalFadeToClear ");
            b.Append(speed);
            this.Add(b);
        }
        /// <summary>
        /// Fade to clear (unfade?) at a particular speed (default 0.007). If no speed is specified, the event will continue immediately; otherwise, it will continue after the fade is finished.
        /// </summary>
        /// <param name="speed"></param>
        public virtual void GlobalFadeToClear(double speed)
        {
            StringBuilder b = new StringBuilder();
            b.Append("globalFadeToClear ");
            b.Append(speed);
            this.Add(b);
        }

        /// <summary>
        /// Make the screen glow once, fading into and out of the <r> <g> <b> values over the course of a second. If <hold> is true it will fade to and hold that color until stopGlowing is used.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="Hold"></param>
        public virtual void Glow(Color color, bool Hold)
        {
            StringBuilder b = new StringBuilder();
            b.Append("glow ");
            b.Append(color.R);
            b.Append(" ");
            b.Append(color.G);
            b.Append(" ");
            b.Append(color.B);
            b.Append(" ");
            b.Append(Hold);
            this.Add(b);
        }

        /// <summary>
        /// Stops npc movement
        /// </summary>
        public virtual void StopNPCMovement()
        {
            StringBuilder b = new StringBuilder();
            b.Append("halt");
            this.Add(b);
        }
        /// <summary>
        /// Stops npc movement.
        /// </summary>
        public virtual void Halt()
        {
            this.StopNPCMovement();
        }

        /// <summary>
        /// Make a the named NPC jump. The default intensity is 8.
        /// </summary>
        /// <param name="ActorName"></param>
        /// <param name="Intensity"></param>
        public virtual void ActorJump(string ActorName, int Intensity=8)
        {
            StringBuilder b = new StringBuilder();
            b.Append("jump ");
            b.Append(Intensity);
            this.Add(b);
        }

        /// <summary>
        /// 	Queue a letter to be received tomorrow (see Content\Data\mail.xnb for available mail).
        /// </summary>
        /// <param name="LetterID"></param>
        public virtual void AddMailForTomorrow(string LetterID)
        {
            StringBuilder b = new StringBuilder();
            b.Append("mail ");
            b.Append(LetterID);
            this.Add(b);
        }

        /// <summary>
        /// Show a dialogue box (no speaker). See dialogue format for the <text> format.
        /// </summary>
        /// <param name="Message"></param>
        public virtual void ShowMessage(string Message)
        {
            StringBuilder b = new StringBuilder();
            b.Append("message ");
            b.Append("\\\""); 
            b.Append(Message);
            b.Append("\"");
            this.Add(b);
        }

        /// <summary>
        /// Move the given actor a certain amount of tiles.
        /// </summary>
        /// <param name="Actor"></param>
        /// <param name="xOffset"></param>
        /// <param name="yOffset"></param>
        /// <param name="Dir"></param>
        /// <param name="Continue"></param>
        public virtual void MoveActor(string Actor, int xOffset, int yOffset, FacingDirection Dir, bool Continue)
        {
            StringBuilder b = new StringBuilder();
            b.Append("move ");
            b.Append(Actor);
            b.Append(" ");
            b.Append(xOffset);
            b.Append(" ");
            b.Append(yOffset);
            b.Append(" ");
            b.Append(this.GetFacingDirectionNumber(Dir));
            b.Append(" ");
            b.Append(Continue);
            this.Add(b);
        }

        public virtual void MoveActorUp(string Actor, int TileAmount, FacingDirection FinishingFacingDirection, bool EventDoesntPause)
        {
            this.MoveActor(Actor, 0, -TileAmount, FinishingFacingDirection, EventDoesntPause);
        }

        public virtual void MoveActorDown(string Actor, int TileAmount, FacingDirection FinishingFacingDirection, bool EventDoesntPause)
        {
            this.MoveActor(Actor, 0, TileAmount, FinishingFacingDirection, EventDoesntPause);
        }

        public virtual void MoveActorLeft(string Actor, int TileAmount, FacingDirection FinishingFacingDirection, bool EventDoesntPause)
        {
            this.MoveActor(Actor, -TileAmount, 0, FinishingFacingDirection, EventDoesntPause);
        }

        public virtual void MoveActorRight(string Actor, int TileAmount, FacingDirection FinishingFacingDirection, bool EventDoesntPause)
        {
            this.MoveActor(Actor, TileAmount, 0, FinishingFacingDirection, EventDoesntPause);
        }

        /// <summary>
        /// Make a named NPC move by the given tile offset from their current position (along one axis only), and face the given direction when they're done. To move along multiple axes, you must specify multiple move commands. By default the event pauses while a move command is occurring, but if <continue> is set to true the movement is asynchronous and will run simultaneously with other event commands.
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="xOffset"></param>
        /// <param name="yOffset"></param>
        /// <param name="Dir"></param>
        /// <param name="Continue"></param>
        public virtual void MoveNPC(NPC npc, int xOffset, int yOffset, FacingDirection Dir, bool Continue)
        {
            StringBuilder b = new StringBuilder();
            b.Append("move ");
            b.Append(npc.Name);
            b.Append(" ");
            b.Append(xOffset);
            b.Append(" ");
            b.Append(yOffset);
            b.Append(" ");
            b.Append(this.GetFacingDirectionNumber(Dir));
            b.Append(" ");
            b.Append(Continue);
            this.Add(b);
        }

        public virtual void MoveNPCUp(NPC npc, int TileAmount, FacingDirection FinishingFacingDirection,bool EventDoesntPause)
        {
            this.MoveNPC(npc, 0, -TileAmount, FinishingFacingDirection, EventDoesntPause);
        }

        public virtual void MoveNPCDown(NPC npc, int TileAmount, FacingDirection FinishingFacingDirection, bool EventDoesntPause)
        {
            this.MoveNPC(npc, 0, TileAmount, FinishingFacingDirection, EventDoesntPause);
        }

        public virtual void MoveNPCLeft(NPC npc, int TileAmount, FacingDirection FinishingFacingDirection, bool EventDoesntPause)
        {
            this.MoveNPC(npc, -TileAmount,0,FinishingFacingDirection, EventDoesntPause);
        }

        public virtual void MoveNPCRight(NPC npc, int TileAmount, FacingDirection FinishingFacingDirection, bool EventDoesntPause)
        {
            this.MoveNPC(npc,TileAmount,0,FinishingFacingDirection, EventDoesntPause);
        }

        public virtual void MoveFarmer(int xOffset, int yOffset, FacingDirection Dir, bool Continue)
        {
            StringBuilder b = new StringBuilder();
            b.Append("move ");
            b.Append("farmer");
            b.Append(" ");
            b.Append(xOffset);
            b.Append(" ");
            b.Append(yOffset);
            b.Append(" ");
            b.Append(this.GetFacingDirectionNumber(Dir));
            b.Append(" ");
            b.Append(Continue);
            this.Add(b);
        }

        public virtual void MoveFarmerUp(int TileAmount, FacingDirection FinishingFacingDirection, bool EventDoesntPause)
        {
            this.MoveFarmer(0, -TileAmount, FinishingFacingDirection, EventDoesntPause);
        }
        public virtual void MoveFarmerDown(int TileAmount, FacingDirection FinishingFacingDirection, bool EventDoesntPause)
        {
            this.MoveFarmer(0, TileAmount, FinishingFacingDirection, EventDoesntPause);
        }

        public virtual void MoveFarmerLeft(int TileAmount, FacingDirection FinishingFacingDirection, bool EventDoesntPause)
        {
            this.MoveFarmer(-TileAmount,0,FinishingFacingDirection, EventDoesntPause);
        }

        public virtual void MoveFarmerRight(int TileAmount, FacingDirection FinishingFacingDirection, bool EventDoesntPause)
        {
            this.MoveFarmer(TileAmount, 0,FinishingFacingDirection, EventDoesntPause);
        }



        /// <summary>
        /// Pause the game for the given number of milliseconds.
        /// </summary>
        /// <param name="Milliseconds"></param>
        public virtual void PauseGame(int Milliseconds)
        {
            StringBuilder b = new StringBuilder();
            b.Append("pause ");
            b.Append(Milliseconds);
            this.Add(b);
        }

        /// <summary>
        /// 	Play the specified music track ID. If the track is 'samBand', the track played will Change depend on certain dialogue answers (76-79).
        /// </summary>
        /// <param name="id"></param>
        public virtual void PlayMusic(string id)
        {
            StringBuilder b = new StringBuilder();
            b.Append("playMusic ");
            b.Append(id);
            this.Add(b);
        }

        /// <summary>
        /// Play a given sound ID from the game's sound bank.
        /// </summary>
        /// <param name="id"></param>
        public virtual void PlaySound(string id)
        {
            StringBuilder b = new StringBuilder();
            b.Append("playSound ");
            b.Append(id);
            this.Add(b);
        }

        /// <summary>
        /// Give the player control back.
        /// </summary>
        public virtual void GivePlayerControl()
        {
            StringBuilder b = new StringBuilder();
            b.Append("playSound ");
            this.Add(b);
        }

        /// <summary>
        /// Offset the position of the named NPC by the given number of pixels. This happens instantly, with no walking animation.
        /// </summary>
        /// <param name="ActorName"></param>
        /// <param name="PixelsX"></param>
        /// <param name="PixelsY"></param>
        public virtual void PositionOffset(string ActorName,int PixelsX, int PixelsY)
        {
            StringBuilder b = new StringBuilder();
            b.Append("positionOffset ");
            b.Append(PixelsX);
            b.Append(" ");
            b.Append(PixelsY);
            this.Add(b);
        }

        /// <summary>
        /// Show a dialogue box with some answers and an optional question. When the player chooses an answer, the event script continues with no other effect.
        /// </summary>
        /// <param name="Question"></param>
        /// <param name="Answer1"></param>
        /// <param name="Answer2"></param>
        public virtual void QuestionNoFork(string Question, string Answer1, string Answer2)
        {
            StringBuilder b = new StringBuilder();
            b.Append("question null ");
            b.Append(Question);
            b.Append("#");
            b.Append(Answer1);
            b.Append("#");
            b.Append(Answer2);
            this.Add(b);
        }

        /// <summary>
        /// Remove the first of an object from a player's inventory.
        /// </summary>
        /// <param name="ParentSheetIndex"></param>
        public virtual void RemoveItem(int ParentSheetIndex) {

            StringBuilder b = new StringBuilder();
            b.Append("removeItem ");
            b.Append(ParentSheetIndex);
            this.Add(b);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TileX"></param>
        /// <param name="TileY"></param>
        public virtual void RemoveObject(int TileX, int TileY)
        {
            StringBuilder b = new StringBuilder();
            b.Append("removeObject ");
            b.Append(TileX);
            b.Append(" ");
            b.Append(TileY);
            this.Add(b);
        }

        /// <summary>
        /// Remove the specified quest from the quest log.
        /// </summary>
        /// <param name="ID"></param>
        public virtual void RemoveQuest(int ID)
        {
            StringBuilder b = new StringBuilder();
            b.Append("removeQuest ");
            b.Append(ID);
            this.Add(b);
        }

        /// <summary>
        /// Remove the temporary sprite at a position.
        /// </summary>
        /// <param name="XTile"></param>
        /// <param name="YTile"></param>
        public virtual void RemoveSprite(int XTile, int YTile)
        {
            StringBuilder b = new StringBuilder();
            b.Append("removeSprite ");
            b.Append(" ");
            b.Append(XTile);
            b.Append(" ");
            b.Append(YTile);
            this.Add(b);
        }

        /// <summary>
        /// Remove all temporary sprites.
        /// </summary>
        public virtual void RemoveTemporarySprites()
        {
            StringBuilder b = new StringBuilder();
            b.Append("removeTemporarySprites");
            this.Add(b);
        }

        /// <summary>
        /// Flashes the screen white for an instant. An alpha value from 0 to 1 adjusts the brightness, and values from 1 and out flashes pure white for x seconds.
        /// </summary>
        /// <param name="Alpha"></param>
        public virtual void ScreenFlash(float Alpha)
        {
            StringBuilder b = new StringBuilder();
            b.Append("screenFlash ");
            b.Append(Alpha);
            this.Add(b);
        }

        public virtual void SetPlayerRunning()
        {
            StringBuilder b = new StringBuilder();
            b.Append("setRunning");
            this.Add(b);
        }

        public virtual void ShakeNPC(NPC npc,int Milliseconds)
        {
            StringBuilder b = new StringBuilder();
            b.Append("shake ");
            b.Append(npc.Name);
            b.Append(" ");
            b.Append(Milliseconds);
            this.Add(b);
        }

        public virtual void ShakeFarmer(int Milliseconds)
        {
            StringBuilder b = new StringBuilder();
            b.Append("shake ");
            b.Append("farmer");
            b.Append(" ");
            b.Append(Milliseconds);
            this.Add(b);
        }

        /// <summary>
        /// Set the named NPC's current frame in their Content\Characters\*.xnb spritesheet. Note that setting the farmer's sprite only Changes parts of the sprite (some times arms, some times arms and legs and torso but not the head, etc). To rotate the whole sprite, use faceDirection farmer <0/1/2/3> first before modifying the sprite with showFrame. Frame ID starts from 0. If farmer is the one whose frame is being set, "farmer" can be eliminated, i.e. both showFrame farmer <frame ID> and showFrame <frame ID> would work.
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="FrameIndex"></param>
        public virtual void ShowNPCFrame(NPC npc, int FrameIndex)
        {
            StringBuilder b = new StringBuilder();
            b.Append("showFrame ");
            b.Append(npc.Name);
            b.Append(" ");
            b.Append(FrameIndex);
            this.Add(b);
        }

        /// <summary>
        /// Shows the given frame for the farmer. Forces the farmer to rotate first as specified by the wiki,
        /// </summary>
        /// <param name="Direction"></param>
        /// <param name="FrameIndex"></param>
        public virtual void ShowFrameFarmer(FacingDirection Direction,int FrameIndex)
        {
            this.PlayerFaceDirection(Direction);
            StringBuilder b = new StringBuilder();
            b.Append("showFrame ");
            b.Append(FrameIndex);
            this.Add(b);
        }

        //Skippable is enabled by default.


        /// <summary>
        /// Show dialogue text from a named NPC; see dialogue format.
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="Message"></param>
        public virtual void Speak(NPC npc, string Message)
        {
            StringBuilder b = new StringBuilder();
            b.Append("speak ");
            b.Append(npc.Name);
            b.Append(" ");
            b.Append('"');
            b.Append(Message);
            b.Append('"');
            this.Add(b);
        }

        /// <summary>
        /// 	Make the player start jittering.
        /// </summary>
        public virtual void PlayerJitter()
        {
            StringBuilder b = new StringBuilder();
            b.Append("startJittering");
            this.Add(b);
        }

        /// <summary>
        /// 	Stop movement from advancedMove.
        /// </summary>
        public virtual void StopAdvancedMoves()
        {
            StringBuilder b = new StringBuilder();
            b.Append("stopAdvancedMoves");
            this.Add(b);
        }

        /// <summary>
        /// 	Stop the farmer's current animation.
        /// </summary>
        public virtual void StopFarmerAnimation()
        {
            StringBuilder b = new StringBuilder();
            b.Append("stopAnimation farmer");
            this.Add(b);
        }

        /// <summary>
        /// Stop the named NPC's current animation. Not applicable to the farmer.
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="EndFrame"></param>
        public virtual void StopNPCAnimation(NPC npc, int EndFrame)
        {
            StringBuilder b = new StringBuilder();
            b.Append("stopAnimation ");
            b.Append(npc.Name);
            b.Append(" ");
            b.Append(EndFrame);
            this.Add(b);
        }

        /// <summary>
        /// 	Make the screen stop glowing.
        /// </summary>
        public virtual void StopGlowing()
        {
            StringBuilder b = new StringBuilder();
            b.Append("stopGlowing");
            this.Add(b);
        }

        /// <summary>
        /// 	Make the player stop jittering.
        /// </summary>
        public virtual void StopJittering()
        {
            StringBuilder b = new StringBuilder();
            b.Append("stopJittering");
            this.Add(b);
        }

        /// <summary>
        /// 	Stop any currently playing music.
        /// </summary>
        public virtual void StopMusic()
        {
            StringBuilder b = new StringBuilder();
            b.Append("stopMusic");
            this.Add(b);
        }

        /// <summary>
        /// 	Make the farmer stop running.
        /// </summary>
        public virtual void StopRunning()
        {
            StringBuilder b = new StringBuilder();
            b.Append("stopRunning");
            this.Add(b);
        }

        /// <summary>
        /// 	Make the farmer stop swimming.
        /// </summary>
        public virtual void StopFarmerSwimming()
        {
            StringBuilder b = new StringBuilder();
            b.Append("stopSwimming farmer");
            this.Add(b);
        }

        /// <summary>
        /// 	Make the npc stop swimming.
        /// </summary>
        public virtual void StopNPCSwimming(NPC npc)
        {
            StringBuilder b = new StringBuilder();
            b.Append("stopSwimming ");
            b.Append(npc.Name);
            this.Add(b);
        }

        /// <summary>
        /// 	Make the farmer start swimming.
        /// </summary>
        public virtual void StartFarmerSwimming()
        {
            StringBuilder b = new StringBuilder();
            b.Append("swimming farmer");
            this.Add(b);
        }


        /// <summary>
        /// Make the npc start swimming.
        /// </summary>
        /// <param name="npc"></param>
        public virtual void StartNPCSwimming(NPC npc)
        {
            StringBuilder b = new StringBuilder();
            b.Append("swimming ");
            b.Append(npc.Name);
            this.Add(b);
        }

        /// <summary>
        /// 	Changes the current event (ie. event commands) to another event in the same location.
        /// </summary>
        /// <param name="ID"></param>
        public virtual void SwitchEvent(int ID)
        {
            StringBuilder b = new StringBuilder();
            b.Append("switchEvent ");
            b.Append(ID);
            this.Add(b);
        }


        /// <summary>
        /// Create a temporary sprite with the given parameters.
        /// </summary>
        /// <param name="XPos"></param>
        /// <param name="YPos"></param>
        /// <param name="Index"></param>
        /// <param name="AnimationLength"></param>
        /// <param name="AnimationInterval"></param>
        /// <param name="Looped"></param>
        /// <param name="LoopCount"></param>
        public virtual void TemporarySprite(int XPos, int YPos, int Index, int AnimationLength, int AnimationInterval, bool Looped, int LoopCount)
        {
            StringBuilder b = new StringBuilder();
            b.Append("temporarySprite ");
            b.Append(XPos);
            b.Append(" ");
            b.Append(YPos);
            b.Append(" ");
            b.Append(Index);
            b.Append(" ");
            b.Append(AnimationLength);
            b.Append(" ");
            b.Append(AnimationInterval);
            b.Append(" ");
            b.Append(Looped);
            b.Append(" ");
            b.Append(LoopCount);
            this.Add(b);
        }

        /// <summary>
        /// 	Show a small text bubble over the named NPC's head with the given text; see dialogue format.
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="Message"></param>
        public virtual void ShowSpeechBubble(NPC npc, string Message)
        {
            StringBuilder b = new StringBuilder();
            b.Append("textAboveHead ");
            b.Append(npc.Name);
            b.Append(" ");
            b.Append(Message);
            this.Add(b);
        }

        /// <summary>
        /// Pan the the camera in the direction (and with the velocity) defined by x/y for the given duration in milliseconds. Example: "viewport move 2 -1 5000" moves the camera 2 pixels right and 1 pixel up for 5 seconds.
        /// </summary>
        /// <param name="xPixelAmount"></param>
        /// <param name="yPixelAmount"></param>
        /// <param name="MillisecondDuration"></param>
        public virtual void PanViewport(int xPixelAmount, int yPixelAmount, int MillisecondDuration)
        {
            StringBuilder b = new StringBuilder();
            b.Append("viewport move ");
            b.Append(xPixelAmount);
            b.Append(" ");
            b.Append(yPixelAmount);
            b.Append(" ");
            b.Append(MillisecondDuration);
            this.Add(b);
        }

        /// <summary>
        /// Instantly reposition the camera to center on the given X, Y tile position. TODO: explain other parameters.
        /// </summary>
        /// <param name="XPosition"></param>
        /// <param name="YPosition"></param>
        public virtual void SetViewportPosition(int XPosition, int YPosition)
        {
            StringBuilder b = new StringBuilder();
            b.Append("viewport ");
            b.Append(XPosition);
            b.Append(" ");
            b.Append(YPosition);
            b.Append(" ");
            b.Append("true");
            this.Add(b);
        }

        /// <summary>
        /// Wait for other players (vanilla MP).
        /// </summary>
        public virtual void WaitForAllPlayers()
        {
            StringBuilder b = new StringBuilder();
            b.Append("waitForOtherPlayers");
            this.Add(b);
        }

        /// <summary>
        /// Warp the named NPC to a position to the given X, Y tile coordinate. This can be used to warp characters off-screen.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public virtual void WarpPlayer(int x, int y)
        {
            StringBuilder b = new StringBuilder();
            b.Append("warp farmer ");
            b.Append(x);
            b.Append(" ");
            b.Append(y);
            this.Add(b);
        }

        /// <summary>
        /// Warp the named NPC to a position to the given X, Y tile coordinate. This can be used to warp characters off-screen.
        /// </summary>
        /// <param name="ActorName"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public virtual void WarpActor(string ActorName,int x, int y)
        {
            StringBuilder b = new StringBuilder();
            b.Append("warp ");
            b.Append(ActorName);
            b.Append(" ");
            b.Append(x);
            b.Append(" ");
            b.Append(y);
            this.Add(b);
        }

        public virtual void WarpNPC(NPC NPC, int x, int y)
        {
            StringBuilder b = new StringBuilder();
            b.Append("warp ");
            b.Append(NPC.Name);
            b.Append(" ");
            b.Append(x);
            b.Append(" ");
            b.Append(y);
            this.Add(b);
        }
    }
}
