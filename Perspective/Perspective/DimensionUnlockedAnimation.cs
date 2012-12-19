using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Perspective
{
    class DimensionUnlockedAnimation
    {
        string unlockText;
        SpriteFont unlockFont;

        const float startScale = 0.0f;
        const float endScale = 2.0f;
        const int duration = 2 * 1000 + 500;

        Vector2 position;

        int queuedUnlocks;

        int currentTime;

        public DimensionUnlockedAnimation(SpriteFont spriteFont, string unlockText = "Dimension Unlocked!")
        {
            this.unlockText = unlockText;
            unlockFont = spriteFont;

            Init();
        }

        public void Init()
        {
            queuedUnlocks = 0;
            currentTime = 0;

            position = new Vector2();
            
      
            position.Y = 500;
        }

        public void AddUnlock()
        {
            ++queuedUnlocks;
        }


        public void Update(GameTime gametime)
        {
            if (queuedUnlocks > 0)
            {
                currentTime += gametime.ElapsedGameTime.Milliseconds;
                if (currentTime > duration)
                {
                    --queuedUnlocks;
                    currentTime = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (queuedUnlocks > 0)
            {
                float scale = lerp((float)currentTime / (float)duration, startScale, endScale);
                float stringWidth = unlockFont.MeasureString(unlockText).X;
                position.X = 350 - ((stringWidth / 2.0f) * scale);
                spriteBatch.DrawString(unlockFont, unlockText, position, Color.Red, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 0.0f);
            }
        }

        public float lerp(float percentage, float start, float end)
        {
            return start + (percentage * (end-start));
        }

    }
}
