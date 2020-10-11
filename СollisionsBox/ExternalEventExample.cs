#region Namespaces
using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Text;
using System.Diagnostics;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Steel;
using Autodesk.Revit.DB.Structure;
using ComponentManager = Autodesk.Windows.ComponentManager;
using IWin32Window = System.Windows.Forms.IWin32Window;
using DialogResult = System.Windows.Forms.DialogResult;

using RVTExternalCommandData = Autodesk.Revit.UI.ExternalCommandData;
using RVTDocument = Autodesk.Revit.DB.Document;
using RVTransaction = Autodesk.Revit.DB.Transaction;
#endregion

namespace СollisionsBox
{
    public class ExternalEventExample : IExternalEventHandler
    {
        private View3D _v3d;

        private Collision _collision;
       
        private UIDocument _uidoc;
        private Document _doc;
        private int _sectionBoxSize;

        public void Execute(UIApplication app)
        {
            _uidoc = app.ActiveUIDocument;
            _doc = app.ActiveUIDocument.Document;

            try
            {
                using (RVTransaction tr = new RVTransaction(_doc))
                {
                    tr.Start("Section Box");

                    BoundingBoxXYZ sectionBox = _v3d.GetSectionBox();

                    XYZ _minBB = new XYZ(
                        _collision.Сoordinates.X - (_sectionBoxSize / 2 / 304.8),
                        _collision.Сoordinates.Y - (_sectionBoxSize / 2 / 304.8),
                        _collision.Сoordinates.Z - (_sectionBoxSize / 2 / 304.8)
                        );

                    XYZ _maxBB = new XYZ(
                        _collision.Сoordinates.X + (_sectionBoxSize / 2 / 304.8),
                        _collision.Сoordinates.Y + (_sectionBoxSize / 2 / 304.8),
                        _collision.Сoordinates.Z + (_sectionBoxSize / 2 / 304.8)
                        );

                    Transform t = sectionBox.Transform;

                    XYZ globmax = t.OfPoint(sectionBox.Max);
                    XYZ deltaXYZ = globmax - sectionBox.Max;

                    sectionBox.Max = _maxBB - deltaXYZ;
                    sectionBox.Min = _minBB - deltaXYZ;

                    _v3d.SetSectionBox(sectionBox);

                    XYZ up = new XYZ(-1, 1, 2);
                    XYZ forward = new XYZ(-1, 1, -1);
                    XYZ eyePoint = new XYZ(
                        _collision.Сoordinates.X,
                        _collision.Сoordinates.Y,
                        _collision.Сoordinates.Z);
                    ViewOrientation3D neworient = new ViewOrientation3D(eyePoint, up, forward);
                    _v3d.SetOrientation(neworient);

                    if (_v3d.IsSectionBoxActive == false) _v3d.IsSectionBoxActive = true;

                    tr.Commit();

                    _uidoc.ActiveView = _v3d;
                    _uidoc.RefreshActiveView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.Source + "\n" + ex.TargetSite.Name);
            }

        }

        public string GetName()
        {
            return "External Event Example";
        }

        public View3D View3D
        {
            set
            {
                _v3d = value;
            }
        }

        public Collision Collision
        {
            set
            {
                _collision = value;
            }
        }

        public int SectionBoxSize
        {
            set
            {
                _sectionBoxSize = value;
            }
        }
    }
}
