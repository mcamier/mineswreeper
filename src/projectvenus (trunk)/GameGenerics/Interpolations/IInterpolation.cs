using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameGenerics.Interpolation
{
    public interface IInterpolation<T>
    {
        #region Properties
        T Min { get; set; }
        T Max { get; set; }
        #endregion

        #region Methods
        T Interpolate(float value);
        #endregion
    }
}
