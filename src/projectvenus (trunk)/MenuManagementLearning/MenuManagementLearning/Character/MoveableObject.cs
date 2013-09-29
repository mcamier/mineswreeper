using System;
using GameGenerics.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjektVenus
{
    public abstract class MoveableObject : Base2DGameComponent
    {
        #region Fields
        private Vector2 position;
        private Texture2D texture;
        #endregion

        #region Events
        public event EventMoveableObjectCollisionHandler CollisionDetected;
        #endregion

        #region Properties
        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }
        public float X
        {
            get { return this.position.X; }
            set { this.position.X = value; }
        }

        public float Y
        {
            get { return this.position.Y; }
            set { this.position.Y = value; }
        }

        public virtual int Width
        {
            get { return this.Texture.Width; }
        }

        public virtual int Height
        {
            get { return this.Texture.Height; }
        }

        public Texture2D Texture
        {
            get { return this.texture; }
            set { this.texture = value; }
        }
        #endregion

        
        #region Constructors
        public MoveableObject(Game game, Texture2D texture)
            : this(game, texture, Vector2.Zero)
        {
        }

        public MoveableObject(Game game)
            : base(game)
        {
            this.Position = position;
            this.Texture = null;
        }

        public MoveableObject(Game game, Texture2D texture, Vector2 position)
            : base(game)
        {
            this.Texture = texture;
            this.Position = position;
        }
        #endregion


        #region Methods
        public bool CollidesWith(MoveableObject o)
        {
            return this.CollidesWith(o, true);
        }
        
        public bool CollidesWith(MoveableObject o, bool pixelPerfect)
        {
            Rectangle object1 = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Width, this.Height);
            Rectangle object2 = new Rectangle((int)o.Position.X, (int)o.Position.Y, o.Width, o.Height);
            Rectangle intersect = Rectangle.Intersect(object1, object2);

            if (pixelPerfect)
            {
                if ((intersect.Width != 0 || intersect.Height != 0) && PixelPerfectCollision(this, o, intersect))
                {
                    this.OnCollisionDetected(this, o);
                    return true;
                }
                else return false;
            }

            if (intersect.Width != 0 || intersect.Height != 0)
            {
                this.OnCollisionDetected(this, o);
                return true;
            }
            else return false;
        }

        private static bool PixelPerfectCollision(MoveableObject a, MoveableObject b, Rectangle intersection) 
        {
            Color[] bitsA = new Color[intersection.Width * intersection.Height];
            Color[] bitsB = new Color[intersection.Width * intersection.Height];
            Rectangle rectA = new Rectangle( (int)(Math.Abs(intersection.X - a.X)), (int)(Math.Abs(intersection.Y - a.Y)), intersection.Width, intersection.Height );
            Rectangle rectB = new Rectangle( (int)(Math.Abs(intersection.X - b.X)), (int)(Math.Abs(intersection.Y - b.Y)), intersection.Width, intersection.Height);
            a.Texture.GetData<Color>(0, rectA, bitsA, 0, intersection.Width * intersection.Height);
            b.Texture.GetData<Color>(0, rectB, bitsB, 0, intersection.Width * intersection.Height);

            for (int y = 0 ; y < intersection.Height ; y++) {
                for (int x = 0 ; x < intersection.Width ; x++) {
                    Color cA = bitsA[y * intersection.Width + x];
                    Color cB = bitsB[y * intersection.Width + x];
                    // alpha treshold
                    if (cA.A > 100 && cB.A > 100) {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        protected virtual void OnCollisionDetected(MoveableObject a, MoveableObject b)
        {
            if (CollisionDetected != null)
            {
                this.CollisionDetected(a, b);
                if (b.CollisionDetected != null)
                {
                    b.CollisionDetected(b, a);
                }
            }
        }
    }
}
