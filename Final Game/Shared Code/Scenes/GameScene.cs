using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.Map;

namespace HuntTheWumpus.SharedCode.Scenes
{
    class GameScene : Scene
    {
        // This can be fixed along with the map's namespace
        Map.Map Map;
        public override void LoadContent(ContentManager Content)
        {

        }

        public override void Initialize()
        {
            // Ideally, the Map would have a reset method, but this should work for now.
            Map = new Map.Map();
        }

        public override void Update(GameTime GameTime)
        {

        }

        public override void Draw(GameTime GameTime, SpriteBatch TargetBatch)
        {
        }

        public override void UnloadContent()
        {

        }
    }
}
