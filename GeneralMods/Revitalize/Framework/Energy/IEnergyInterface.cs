using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revitalize.Framework.Illuminate;
using Revitalize.Framework.Managers;
using Revitalize.Framework.Utilities;
using StardewValley;
using StardustCore.Animations;

namespace Revitalize.Framework.Energy
{
    public interface IEnergyInterface
    {
        string text { get; set; }
        string text { get; set; }
        string Name { get; set; }
        string Name { get; set; }
        string DisplayName { get; set; }
        string DisplayName { get; set; }
        string id { get; }
        string id { get; }
        GameLocation location { get; set; }
        GameLocation location { get; set; }
        AnimationManager animationManager { get; }
        AnimationManager animationManager { get; }
        Texture2D displayTexture { get; }
        Texture2D displayTexture { get; }
        string ItemInfo { get; set; }
        string ItemInfo { get; set; }

        bool canBePlacedHere(GameLocation l, Vector2 tile);
        bool canBePlacedHere(GameLocation l, Vector2 tile);
        bool canStackWith(Item other);
        bool canStackWith(Item other);
        bool checkForAction(Farmer who, bool justCheckingForActivity = false);
        bool checkForAction(Farmer who, bool justCheckingForActivity = false);
        bool clicked(Farmer who);
        bool clicked(Farmer who);
        void draw(SpriteBatch spriteBatch, int x, int y, float alpha = 1);
        void draw(SpriteBatch spriteBatch, int xNonTile, int yNonTile, float layerDepth, float alpha = 1);
        void draw(SpriteBatch spriteBatch, int x, int y, float alpha = 1);
        void draw(SpriteBatch spriteBatch, int xNonTile, int yNonTile, float layerDepth, float alpha = 1);
        void drawFullyInMenu(SpriteBatch spriteBatch, Vector2 objectPosition, float Depth);
        void drawFullyInMenu(SpriteBatch spriteBatch, Vector2 objectPosition, float Depth);
        void drawInMenu(SpriteBatch spriteBatch, Vector2 location, float scaleSize, float transparency, float layerDepth, bool drawStackNumber, Color c, bool drawShadow);
        void drawInMenu(SpriteBatch spriteBatch, Vector2 location, float scaleSize, float transparency, float layerDepth, bool drawStackNumber, Color c, bool drawShadow);
        void drawInMenu(SpriteBatch spriteBatch, Vector2 location, float scaleSize, float transparency, float layerDepth, bool drawStackNumber, Color c, bool drawShadow);
        void drawPlacementBounds(SpriteBatch spriteBatch, GameLocation location);
        void drawPlacementBounds(SpriteBatch spriteBatch, GameLocation location);
        void drawWhenHeld(SpriteBatch spriteBatch, Vector2 objectPosition, Farmer f);
        void drawWhenHeld(SpriteBatch spriteBatch, Vector2 objectPosition, Farmer f);
        void dyeColor(NamedColor DyeColor);
        void dyeColor(NamedColor DyeColor);
        void eraseDye();
        void eraseDye();
        string generateDefaultRotationalAnimationKey();
        string generateDefaultRotationalAnimationKey();
        string generateRotationalAnimationKey();
        string generateRotationalAnimationKey();
        Dictionary<string, string> getAdditionalSaveData();
        Dictionary<string, string> getAdditionalSaveData();
        Rectangle getBoundingBox(Vector2 tileLocation);
        Rectangle getBoundingBox(Vector2 tileLocation);
        Color getCategoryColor();
        Color getCategoryColor();
        string getCategoryName();
        string getCategoryName();
        string getDescription();
        string getDescription();
        string getDisplayNameFromStringsFile(string objectID);
        string getDisplayNameFromStringsFile(string objectID);
        ref EnergyManager GetEnergyManager();
        ref EnergyManager GetEnergyManager();
        ref EnergyManager GetEnergyManager();
        ref FluidManagerV2 GetFluidManager();
        ref FluidManagerV2 GetFluidManager();
        ref InventoryManager GetInventoryManager();
        ref InventoryManager GetInventoryManager();
        Item getOne();
        Item getOne();
        void getUpdate();
        void getUpdate();
        void initializeBasics();
        void initializeBasics();
        void InitNetFields();
        void InitNetFields();
        bool isPassable();
        bool isPassable();
        bool performToolAction(Tool t, GameLocation location);
        bool performToolAction(Tool t, GameLocation location);
        bool placementAction(GameLocation location, int x, int y, Farmer who = null);
        bool placementAction(GameLocation location, int x, int y, Farmer who = null);
        void rebuild(Dictionary<string, string> additionalSaveData, object replacement);
        void rebuild(Dictionary<string, string> additionalSaveData, object replacement);
        bool removeAndAddToPlayersInventory();
        bool removeAndAddToPlayersInventory();
        void replaceAfterLoad();
        void replaceAfterLoad();
        bool requiresUpdate();
        bool requiresUpdate();
        bool rightClicked(Farmer who);
        bool rightClicked(Farmer who);
        void rotate();
        void rotate();
        int salePrice();
        int salePrice();
        int sellToStorePrice(long PlayerID = -1);
        int sellToStorePrice(long PlayerID = -1);
        void SetEnergyManager(ref EnergyManager Manager);
        void SetEnergyManager(ref EnergyManager Manager);
        void SetEnergyManager(ref EnergyManager Manager);
        void SetFluidManager(FluidManagerV2 FluidManager);
        void SetFluidManager(FluidManagerV2 FluidManager);
        void SetInventoryManager(InventoryManager Manager);
        void SetInventoryManager(InventoryManager Manager);
        bool shiftRightClicked(Farmer who);
        bool shiftRightClicked(Farmer who);
        void updateDrawPosition(int x, int y);
        void updateDrawPosition(int x, int y);
        void updateInfo();
        void updateInfo();
        void updateWhenCurrentLocation(GameTime time, GameLocation environment);
        void updateWhenCurrentLocation(GameTime time, GameLocation environment);
    }
}
