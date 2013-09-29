using System.Collections.Generic;
using GameGenerics.Path.Curves;

namespace GameGenerics.Path
{
    public abstract class PathBase
    {
        List<BezierCurve> nodes = new List<BezierCurve>();
        int currentNode;
        float ration;
        bool cyclic;
        bool loop;

        public bool IsCyclic
        {
            get { return this.cyclic; }
            set { this.cyclic = value; }
        }

        public bool IsLoop
        {
            get { return this.loop; }
            set { this.loop = value; }
        }

        public PathBase() 
        {
            this.currentNode = 0;
            this.ration = 0;
            this.cyclic = false;
            this.loop = false;
        }
    }
}
