using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;

using Autodesk.Revit.DB;

namespace СollisionsBox
{
    public static class XmlReaderAndSaver
    {
        static XYZ _basePiont;

        //Переделать на xPath как будет время
        public static List<Collision> LoadElements(string path, Document doc)
        {
            List<Collision> _collisionList = new List<Collision>();
            _basePiont = BasePointInfo.GetBasePointCoordinate(doc);

            try
            {
                XmlDocument xDoc = new XmlDocument();

                xDoc.Load(path);

                XmlElement xRoot = xDoc.DocumentElement;

                foreach (XmlNode batchtest in xRoot)
                {
                    foreach (XmlNode clashtests in batchtest)
                    {
                        if (!(clashtests.Name == "clashtests")) continue;
                        foreach (XmlNode clashtest in clashtests)
                        {
                            XmlNode nameclashtest = clashtest.Attributes.GetNamedItem("name");

                            foreach (XmlNode clashresults in clashtest)
                            {
                                if (!(clashresults.Name == "clashresults")) continue;
                                foreach (XmlNode clash in clashresults)
                                {
                                    #region Если это единичная коллизия
                                    if (clash.Name == "clashresult")
                                    {
                                        XmlNode nameClashResult = clash.Attributes.GetNamedItem("name");
                                        XmlNode guidClashResult = clash.Attributes.GetNamedItem("guid");

                                        Collision col = new Collision();

                                        col.Name = nameClashResult.Value;
                                        col.TestName = nameclashtest.Value;
                                        col.Type = "alone";
                                        col.Guid = guidClashResult.Value;
    
                                        foreach (XmlNode childNode in clash.ChildNodes)
                                        {
                                            if (!(childNode.Name == "clashpoint")) continue;
                                            col.Сoordinates = XmlReaderAndSaver.XMLCoordinateToRVTCoordinate(childNode);
                                        }

                                        _collisionList.Add(col);
                                    }
                                    #endregion

                                    #region Если группа коллизий
                                    if (clash.Name == "clashgroup")
                                    {
                                        //MessageBox.Show("Группа");

                                        XmlNode nameClashGroup = clash.Attributes.GetNamedItem("name");
                                        XmlNode guidClashGroup = clash.Attributes.GetNamedItem("guid");

                                        Collision col = new Collision();

                                        col.Name = nameClashGroup.Value;
                                        col.TestName = nameclashtest.Value;
                                        col.Type = "group";
                                        col.Guid = guidClashGroup.Value;

                                        foreach (XmlNode childNode in clash.ChildNodes)
                                        {
                                            if (!(childNode.Name == "clashpoint")) continue;
                                            col.Сoordinates = XmlReaderAndSaver.XMLCoordinateToRVTCoordinate(childNode);
                                        }

                                        _collisionList.Add(col);

                                        foreach (XmlNode groupclashresults in clash)
                                        {
                                            if (!(groupclashresults.Name == "clashresults")) continue;

                                            foreach (XmlNode groupChildResult in groupclashresults)
                                            {
                                                if (groupChildResult.Name == "clashresult")
                                                {

                                                    XmlNode nameGroupChildClashResult = groupChildResult.Attributes.GetNamedItem("name");
                                                    XmlNode guidGroupChildClashResult = groupChildResult.Attributes.GetNamedItem("guid");

                                                    Collision ChildCol = new Collision();

                                                    ChildCol.Name = nameGroupChildClashResult.Value;
                                                    ChildCol.TestName = nameclashtest.Value;
                                                    ChildCol.Type = "child";
                                                    ChildCol.Guid = guidGroupChildClashResult.Value;
                                                    ChildCol.GroupGuid = guidClashGroup.Value;

                                                    foreach (XmlNode childNode in clash.ChildNodes)
                                                    {
                                                        if (!(childNode.Name == "clashpoint")) continue;
                                                        ChildCol.Сoordinates = XmlReaderAndSaver.XMLCoordinateToRVTCoordinate(childNode);
                                                    }
                                                    _collisionList.Add(ChildCol);
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                }
                            }
                        }
                    }
                }
                return _collisionList.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.Source + "\n" + ex.TargetSite.Name);
                return null;
            }
        }

        private static XYZ XMLCoordinateToRVTCoordinate(XmlNode xmlNode)
        {
            XmlNode pos3f = xmlNode.ChildNodes.Item(0);
            XmlNode _x = pos3f.Attributes.GetNamedItem("x");
            XmlNode _y = pos3f.Attributes.GetNamedItem("y");
            XmlNode _z = pos3f.Attributes.GetNamedItem("z");

            string dec_sep = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            Autodesk.Revit.DB.XYZ xyz = new Autodesk.Revit.DB.XYZ(
              (Convert.ToDouble(_x.Value.Replace(".", dec_sep).Replace(",", dec_sep)) / 0.3048) + _basePiont.X,
              (Convert.ToDouble(_y.Value.Replace(".", dec_sep).Replace(",", dec_sep)) / 0.3048) + _basePiont.Y,
              (Convert.ToDouble(_z.Value.Replace(".", dec_sep).Replace(",", dec_sep)) / 0.3048) + _basePiont.Z);

            return xyz;
        }
    }
}
