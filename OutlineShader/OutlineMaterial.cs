using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlineShader
{
    public class OutlineMaterial : Material
    {
        public bool Enabled
        {
            get
            {
                return sprite.color.R > 0.1f;
            }
            set
            {
                if (value)
                {
                    sprite?.setColor(Color.Red);
                }
                else
                {
                    sprite?.setColor(Color.Black);
                }
            }
        }

        public float TexelSize
        {
            get { return texelSize; }
            set
            {
                texelSize = value;
                texelSizeParam?.SetValue(value);
            }
        }

        public Color OutlineColor
        {
            get { return outlineColor; }
            set
            {
                outlineColor = value;
                outlineColorParam?.SetValue(value.ToVector4());
            }
        }

        public Vector2 FrameSize
        {
            get { return frameSize; }
            set
            {
                frameSize = value;
                frameSizeParam?.SetValue(value);
            }
        }

        Sprite sprite;
        float texelSize = 1f;
        Color outlineColor = Color.White;
        Vector2 frameSize = new Vector2(16f, 16f);

        private EffectParameter texelSizeParam;
        private EffectParameter outlineColorParam;
        private EffectParameter frameSizeParam;

        public OutlineMaterial(Effect effect, Sprite sprite) : base(effect)
        {
            //set the sprite
            this.sprite = sprite;

            //set the values in the shader
            texelSizeParam = effect.Parameters["_texelSize"];
            outlineColorParam = effect.Parameters["_color"];
            frameSizeParam = effect.Parameters["_frameSize"];


            //set the starting values there
            texelSizeParam.SetValue(texelSize);
            outlineColorParam.SetValue(outlineColor.ToVector4());
            frameSizeParam.SetValue(frameSize);

        }
    }
}
