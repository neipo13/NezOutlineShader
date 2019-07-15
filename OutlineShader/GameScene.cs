using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Textures;
using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace OutlineShader
{
    public class GameScene : Scene
    {
        public enum anims
        {
            Idle,
            Run
        }

        OutlineMaterial mat;
        Sprite<anims> sprite;
        public override void initialize()
        {
            base.initialize();
            addRenderer(new DefaultRenderer());

            var texture = content.Load<Texture2D>("mario-sprites");
            var subtextures = Subtexture.subtexturesFromAtlas(texture, 16, 16);

            var effect = content.Load<Effect>("effects/PixelOutline");

            var entity = new Entity();
            sprite = new Sprite<anims>();
            sprite.addAnimation(anims.Idle, new SpriteAnimation(subtextures.GetRange(0, 4)));
            sprite.addAnimation(anims.Run, new SpriteAnimation(subtextures.GetRange(8, 7)));
            sprite.play(anims.Idle);
            mat = new OutlineMaterial(effect, sprite);
            mat.Enabled = true;
            mat.TexelSize = 1f / texture.Width; // A texel is a pixel within the size of the texture (not subtexture) as related to a 0-1 scale so for a 32x32 texture a texel size is 1/32
            sprite.material = mat;
            entity.addComponent(sprite);

            entity.position = new Vector2(100, 100);
            addEntity(entity);

            var e2 = new Entity();
            var sprite2 = new Sprite<anims>();
            sprite2.addAnimation(anims.Idle, new SpriteAnimation(subtextures.GetRange(0, 4)));
            sprite2.addAnimation(anims.Run, new SpriteAnimation(subtextures.GetRange(8, 7)));
            sprite2.play(anims.Idle);
            var mat2 = new OutlineMaterial(effect, sprite2);
            mat2.Enabled = true;
            mat2.TexelSize = 1f / texture.Width; // A texel is a pixel within the size of the texture (not subtexture) as related to a 0-1 scale so for a 32x32 texture a texel size is 1/32
            sprite2.material = mat2;
            e2.addComponent(sprite2);

            e2.position = new Vector2(150, 50);
            addEntity(e2);

        }

        public override void update()
        {
            base.update();

            if (Nez.Input.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                if(sprite.currentAnimation == anims.Idle)
                {
                    sprite.play(anims.Run);
                }
                else
                {
                    sprite.play(anims.Idle);
                }
            }
            if (Nez.Input.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Enter))
            {
                mat.Enabled = !mat.Enabled;
            }
            if (Nez.Input.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.C))
            {
                mat.OutlineColor = Nez.Random.nextColor();
            }
        }
    }
}
