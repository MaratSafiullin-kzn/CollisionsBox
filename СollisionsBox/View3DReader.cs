using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace СollisionsBox
{
    public static class View3DReader
    {
        public static List<View3D> Get3DviewList(Document doc)
        {
            List<View3D> _viewList = null;
            try
            {
                _viewList = new FilteredElementCollector(doc)
                    .WhereElementIsNotElementType()
                    .OfClass(typeof(View3D))
                    .Cast<View3D>()
                    .Where(x => x.IsTemplate == false)
                    .ToList();

                return _viewList;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.Source + "\n" + ex.TargetSite.Name);
                return _viewList;
            }
            
        }
    }
}
