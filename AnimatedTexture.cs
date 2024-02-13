using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

/// <summary>
/// From http://community.sgdotnet.org/blogs/zhongqiang/archive/2007/06/29/XNA-AnimatedTexture-Class.aspx
/// </summary>
///
namespace GIJoeraffe
{

    public class AnimatedTexture
    {

        private int frameCountVal;
        private Texture2D myTexture;
        private float TimePerFrame;
        private int Frame;
        private int topVal;
        private int leftVal;
        private int heightVal;
        private int widthVal;
        private float TotalElapsed;
        private bool Paused;
        public float Rotation, Scale, Depth;
        public Vector2 Origin;

        public AnimatedTexture(Vector2 Origin, float Rotation, float Scale, float Depth)
        {

            this.Origin = Origin;
            this.Rotation = Rotation;
            this.Scale = Scale;
            this.Depth = Depth;

        }

        public void Load(GraphicsDevice device, ContentManager content, string asset, int FrameCount, int FramesPerSec)
        {

            frameCount = FrameCount;
            myTexture = content.Load<Texture2D>(asset);
            TimePerFrame = (float)1 / FramesPerSec;
            Frame = 0;
            TotalElapsed = 0;
            Paused = false;
            left = 0;
            top = 0;

        }

        // class AnimatedTexture
        public void UpdateFrame(float elapsed)
        {

            if (Paused)
                return;

            TotalElapsed += elapsed;

            if (TotalElapsed > TimePerFrame)
            {

                Frame++;

                // Keep the Frame between 0 and the total frames, minus one.
                Frame = Frame % frameCount;
                TotalElapsed -= TimePerFrame;

            }

        }

        // class AnimatedTexture
        public void DrawFrame(SpriteBatch Batch, Vector2 screenpos)
        {

            DrawFrame(Batch, Frame, screenpos);

        }

        public void DrawFrame(SpriteBatch Batch, int Frame, Vector2 screenpos)
        {

            //int FrameWidth = myTexture.Width / framecount;
            Rectangle sourcerect = new Rectangle(left + Frame * width, top,
            width, height);
            Batch.Draw(myTexture, screenpos, sourcerect, Color.White,
            Rotation, Origin, Scale, SpriteEffects.None, Depth);

        }

        public void setRect(int left, int top, int width, int height)
        {
            this.left = left;
            this.top = top;
            this.width = width;
            this.height = height;
        }

        public bool IsPaused
        {

            get { return Paused; }

        }

        public void Reset()
        {

            Frame = 0;
            TotalElapsed = 0f;

        }

        public void Stop()
        {

            Pause();
            Reset();

        }

        public void Play()
        {

            Paused = false;

        }

        public void Pause()
        {

            Paused = true;

        }

        public int top
        {
            get
            {
                return topVal;
            }
            set
            {
                topVal = value;
            }
        }

        public int left
        {
            get
            {
                return leftVal;
            }
            set
            {
                leftVal = value;
            }
        }

        public int height
        {
            get
            {
                return heightVal;
            }
            set
            {
                heightVal = value;
            }
        }

        public int width
        {
            get
            {
                return widthVal;
            }
            set
            {
                widthVal = value;
            }
        }

        public int frameCount
        {
            get
            {
                return frameCountVal;
            }
            set
            {
                frameCountVal = value;
            }
        }

        public void setFramesPerSecond(int fps)
        {
            TimePerFrame = (float)1 / fps;
        }
    }
}