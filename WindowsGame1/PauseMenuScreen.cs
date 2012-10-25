//-----------------------------------------------------------------------------
// PauseMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using System;
using Microsoft.Xna.Framework;
#if ZUNE
using Microsoft.Xna.Framework.Media;
#endif

namespace Xbox360Game1
{
    /// <summary>
    /// Primarily for controlling what song is playing.  The UI is a bit clunky at the moment
    /// and will be improved.  Also, no Media support on Windows yet, so #ifdef'd sections out
    /// </summary>
    class PauseMenuScreen : MenuScreen
    {
        MenuEntry songMenuEntry;
        MenuEntry playMenuEntry;

#if ZUNE
        MediaLibrary library;
#endif

        static int selectedSongIndex = 0;
        static int playingSongIndex = -1;

        /// <summary>
        /// Constructor.
        /// </summary>
        public PauseMenuScreen()
            : base("Pause")
        {

            IsPopup = true;

            MenuEntry resumeGameMenuEntry = new MenuEntry("RESUME");
            MenuEntry quitGameMenuEntry = new MenuEntry("QUIT");
            resumeGameMenuEntry.Selected += OnCancel;
            quitGameMenuEntry.Selected += QuitGameMenuEntrySelected;

            // Create our menu entries.
            songMenuEntry = new MenuEntry("SONG:");
            playMenuEntry = new MenuEntry("PLAY");

#if ZUNE
            library = new MediaLibrary();
#endif

            MenuEntry backMenuEntry = new MenuEntry("BACK");

            // Hook up menu event handlers.
            songMenuEntry.Selected += SongMenuEntrySelected;
            playMenuEntry.Selected += PlayMenuEntrySelected;
            backMenuEntry.Selected += OnCancel;
            
            // Add entries to the menu.
            MenuEntries.Add(resumeGameMenuEntry);
            MenuEntries.Add(songMenuEntry);
            MenuEntries.Add(playMenuEntry);
            MenuEntries.Add(quitGameMenuEntry);

#if ZUNE
            if (playingSongIndex != -1)
            {
                selectedSongIndex = playingSongIndex;
            }

            UpdateMenuText();

#endif

            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        private void UpdateMenuText()
        {
#if ZUNE
            if (library.Songs.Count > 0)
            {
                songMenuEntry.Text = "SONG: " + library.Songs[selectedSongIndex] + (library.Songs[selectedSongIndex].IsProtected ? " (DRM)" : "");
                if (MediaPlayer.State == MediaState.Playing)
                    playMenuEntry.Text = "STOP";
                else
                {
                    if (library.Songs[selectedSongIndex].IsProtected == true)
                    {
                        playMenuEntry.Text = "-";
                    }
                    else
                    {
                        playMenuEntry.Text = "PLAY";
                    }
                }
            }
            else
            {
                songMenuEntry.Text = "(NO SONGS)";
                playMenuEntry.Text = "-";
            }
#endif
        }

        void AdvanceSong()
        {
#if ZUNE
            if (library.Songs.Count == 0)
                return;

            selectedSongIndex = (selectedSongIndex + 1) % library.Songs.Count;

            UpdateMenuText();
#endif
        }

        /// <summary>
        /// Event handler for when the Language menu entry is selected.
        /// </summary>
        void SongMenuEntrySelected(object sender, EventArgs e)
        {
            AdvanceSong();
        }

        void PlayMenuEntrySelected(object sender, EventArgs e)
        {
#if ZUNE
            if (library.Songs.Count == 0)
                return;

            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Stop();
                playingSongIndex = -1;
            }
            else
            {
                if (library.Songs[selectedSongIndex].IsProtected == false)
                {
                    MediaPlayer.Play(library.Songs[selectedSongIndex]);
                    playingSongIndex = selectedSongIndex;
                }
            }

            UpdateMenuText();
#endif
        }

        /// <summary>
        /// Event handler for when the Quit Game menu entry is selected.
        /// </summary>
        void QuitGameMenuEntrySelected(object sender, EventArgs e)
        {
            foreach (GameScreen screen in ScreenManager.GetScreens())
                screen.ExitScreen();

            ScreenManager.AddScreen(new BackgroundScreen());
            ScreenManager.AddScreen(new MainMenuScreen());
        }

        /// <summary>
        /// Draws the pause menu screen. This darkens down the gameplay screen
        /// that is underneath us, and then chains to the base MenuScreen.Draw.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2 / 3);

            base.Draw(gameTime);
        }
    }
}
