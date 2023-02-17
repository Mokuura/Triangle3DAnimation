using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmEssentials;

namespace Triangle3DAnimation.Animation.Transformations
{
    public abstract class InitTransformation : Transformation
    {
        protected InitTransformation(TimeSingle start, TimeSingle end) : base(start, end)
        {
        }
    }
}
