using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;

namespace СollisionsBox
{
    public static class BasePointInfo
    {
        public static XYZ GetBasePointCoordinate(Document doc)
        {
            XYZ _xyz = null;

            BasePoint bp = new FilteredElementCollector(doc)
                    .WhereElementIsNotElementType()
                    .OfCategory(BuiltInCategory.OST_ProjectBasePoint)
                    .First() as BasePoint;

            _xyz = bp.Position;

            return _xyz;
        }
    }
}
