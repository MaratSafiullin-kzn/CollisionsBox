using System.Xml.Serialization;

using Autodesk.Revit.DB;

namespace СollisionsBox
{
    [XmlRoot(ElementName = "Collision")]
    public class Collision
    {
        private string _name = null;
        private string _testName = null;
        private string _guid = null;
        private string _groupGuid = null;
        private string _type = null;
        private bool _isViewed = false;
        private string _projectName = null;

        private XYZ _xyz = null;

        [XmlElement(ElementName = "Name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [XmlElement(ElementName = "ProjectName")]
        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; }
        }

        [XmlElement(ElementName = "TestName")]
        public string TestName
        {
            get { return _testName; }
            set { _testName = value; }
        }

        [XmlElement(ElementName = "Guid")]
        public string Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }

        [XmlElement(ElementName = "GroupGuid")]
        public string GroupGuid
        {
            get { return _groupGuid; }
            set { _groupGuid = value; }
        }

        [XmlIgnore]
        public XYZ Сoordinates
        {
            get{ return _xyz; }
            set{ _xyz = value;}
        }

        [XmlElement(ElementName = "Type")]
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        [XmlElement(ElementName = "IsViewed")]
        public bool IsViewed
        {
            get { return _isViewed; }
            set { _isViewed = value; }
        }

        [XmlElement(ElementName = "X")]
        public double X { get; set; }

        [XmlElement(ElementName = "Y")]
        public double Y { get; set; }

        [XmlElement(ElementName = "Z")]
        public double Z { get; set; }
    }
}
