//-----------------------------------------------------------------------------
// BackgroundScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GIJoeraffe
{
    /// <summary>
    /// The background screen sits behind all the other menu screens.
    /// It draws a background image that remains fixed in place regardless
    /// of whatever transitions the screens on top of it may be doing.
    /// 
    /// A bit boring right now, but needed something in place...
    /// </summary>
    class BackgroundScreen : GameScreen
    {
        Texture2D title;
        Texture2D background;

        /// <summary>
        /// Constructor.
        /// </summary>
        public BackgroundScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.0);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        /// <summary>
        /// Loads graphics content for this screen. The background texture is quite
        /// big, so we use our own local ContentManager to load it. This allows us
        /// to unload before going from the menus into the game itself, wheras if we
        /// used the shared ContentManager provided by the Game class, the content
        /// would remain loaded forever.
        /// </summary>
        public override void LoadContent()
        {
            //title = ScreenManager.Game.Content.Load<Texture2D>("GIJOERAFFE");
            background = ScreenManager.Game.Content.Load<Texture2D>("GIJOERAFFE");
        }

        /// <summary>
        /// Updates the background screen. Unlike most screens, this should not
        /// transition off even if it has been covered by another screen: it is
        /// supposed to be covered, after all! This overload forces the
        /// coveredByOtherScreen parameter to false in order to stop the base
        /// Update method wanting to transition off.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
        }

        /// <summary>
        /// Draws the background screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            // Make the menu slide into place during transitions, using a
            // power curve to make things look more interesting (this makes
            // the movement slow down as it nears the end).
            float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            spriteBatch.Begin();

            int colorMax = 255;

            // Background
            spriteBatch.Draw(background, new Vector2(0, 0), new Color(colorMax, colorMax, colorMax, TransitionAlpha));

            // Title
           // spriteBatch.Draw(title, new Vector2(34, 32), new Color(255, 255, 255, TransitionAlpha));

            spriteBatch.End();
        }
    }
}
