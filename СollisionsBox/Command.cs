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
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                if(View3DReader.Get3DviewList(commandData.Application.ActiveUIDocument.Document).Count != 0)
                {
                    ExternalEventExample handler = new ExternalEventExample();
                    ExternalEvent exEvent = ExternalEvent.Create(handler);
                    App.thisApp.ShowForm(commandData.Application);
                    return Result.Succeeded;
                }
                else
                {
                    TaskDialog.Show("Ошибка", "Создайте 3D вид!");
                    return Result.Failed;
                }  
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }

        }
    }
}
