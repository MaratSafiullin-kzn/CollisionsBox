#region Namespaces
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Text;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Steel;
using Autodesk.Revit.DB.Structure;

using ComponentManager = Autodesk.Windows.ComponentManager;
using IWin32Window = System.Windows.Forms.IWin32Window;

using RVTDocument = Autodesk.Revit.DB.Document;
using RVTransaction = Autodesk.Revit.DB.Transaction;

using RVTExternalCommandData = Autodesk.Revit.UI.ExternalCommandData;

using VCRevitRibbonUtil;
#endregion

namespace СollisionsBox
{
    public class App : IExternalApplication
    {
        static String addinAssmeblyPath = typeof(App).Assembly.Location;
        public static App thisApp = null;
        private Form1 m_MyForm;


        public Result OnStartup(UIControlledApplication a)
        {
            createPanel(a);

            m_MyForm = null;
            thisApp = this;

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            if (m_MyForm != null && m_MyForm.Visible)
            {
                m_MyForm.Close();
            }

            return Result.Succeeded;
        }

        public void ShowForm(UIApplication uiapp)
        {
            IWin32Window revit_window = new JtWindowHandle(ComponentManager.ApplicationWindow);

            if (m_MyForm == null || m_MyForm.IsDisposed)
            {
                ExternalEventExample handler = new ExternalEventExample();
                ExternalEvent exEvent = ExternalEvent.Create(handler);

                m_MyForm = new Form1(exEvent, handler, uiapp);
                m_MyForm.Show(revit_window);
            }
        }

        void createPanel(UIControlledApplication application)
        {
            Ribbon ribbon = new Ribbon(application);
            ribbon
                .Tab("АО Казанский Гипронииавиапром")
                .Panel("Коллизии")
                .CreateButton<Command>("CollisionBox", "Коллизии \n Рамка выбора", btn => btn.SetLargeImage(СollisionsBox.Properties.Resources.imageСollisionsBox)
                                                                                             .SetLongDescription("Программа для просмотра коллизий экспортированых их Navisworks Manage"));
        }
    }
}
