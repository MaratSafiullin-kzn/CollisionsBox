using System;
using System.IO;
using System.Collections.Generic;

using System.Drawing;
using System.Linq;

using System.Windows.Forms;
using System.Xml.Serialization;

using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

using Microsoft.VisualBasic;

using RVTExternalCommandData = Autodesk.Revit.UI.ExternalCommandData;
using RVTDocument = Autodesk.Revit.DB.Document;
using RVTransaction = Autodesk.Revit.DB.Transaction;


namespace СollisionsBox
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        private Document _doc;
        private UIDocument _uidoc;
        private UIApplication _uiapp;

        private ExternalEvent m_ExEvent;
        private ExternalEventExample m_Handler;

        private List<View3D> listView3D;
        private List<Collision> collisionList = new List<Collision>();

        //Для сохранения:
        static string appDataRoaming = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        static string myFolder = appDataRoaming + "\\CollisionBox";
        static string filePath = myFolder + "\\collisions.xml";

        public Form1(ExternalEvent exEvent, ExternalEventExample handler, UIApplication uiapp)
        {
            InitializeComponent();

            m_ExEvent = exEvent;
            m_Handler = handler;

            _uiapp = uiapp;
            _uidoc = _uiapp.ActiveUIDocument;
            _doc = _uidoc.Document;

            listView3D = View3DReader.Get3DviewList(_doc);

            foreach (var i in listView3D)
            {
                comboBoxView3D.Items.Add(i.Name);
            }

            comboBoxView3D.SelectedIndex = 0;

            openFileDialog1.Filter = "Text files(*.xml)|*.xml";
            openFileDialog1.InitialDirectory = GetOpenFileDialogInitialDirectory(_doc);

            try
            {
                if (File.Exists(filePath))
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(List<Collision>));

                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {
                        if(fs.Length != 0)
                        {
                            List<Collision> desCol = (List<Collision>)formatter.Deserialize(fs);
                            collisionList.AddRange(desCol);

                            XYZ _basePiont = BasePointInfo.GetBasePointCoordinate(_doc);
                            foreach (Collision col in collisionList)
                            {
                                Autodesk.Revit.DB.XYZ xyz = new Autodesk.Revit.DB.XYZ(
                                       col.X,
                                       col.Y,
                                       col.Z
                                       );
                                col.Сoordinates = xyz;
                            }

                            CreateTreeViewNodes(collisionList);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка открытия файлов сохранения: " + ex.Message);
            }
        }

        private void buttonLoadFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;

                collisionList.AddRange(XmlReaderAndSaver.LoadElements(openFileDialog1.FileName, _doc));
                //string projectName = Interaction.InputBox("Название объекта: ");
                CreateTreeViewNodes(collisionList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.Source + "\n" + ex.TargetSite.Name);
            }
        }

        private void buttonOpenCollision_Click(object sender, EventArgs e)
        {
            if(treeView1.SelectedNode.Level != 0)
            {
                treeView1.SelectedNode.NodeFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
                collisionList.Find(x => x.Guid == ((Collision)treeView1.SelectedNode.Tag).Guid).IsViewed = true;

                Collision c = treeView1.SelectedNode.Tag as Collision;

                m_Handler.Collision = c;
                m_Handler.View3D = listView3D.First(x => x.Name == comboBoxView3D.Text);

                if (int.Parse(textBoxSize.Text) > 100)
                {
                    m_Handler.SectionBoxSize = int.Parse(textBoxSize.Text);
                }
                else
                {
                    m_Handler.SectionBoxSize = 100;
                    textBoxSize.Text = "100";
                }

                m_ExEvent.Raise();
            }  
        }

        private string GetOpenFileDialogInitialDirectory(Document doc)
        {
            string _path = "";
            try
            {
                BasicFileInfo bfi = BasicFileInfo.Extract(doc.PathName);
                string centralPath = bfi.CentralPath;
                _path = String.Join(@"\", centralPath.Split('\\').TakeWhile(x => x != "00 3D модель"));
            }
            catch{}
            return _path;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            m_ExEvent.Dispose();
            m_ExEvent = null;
            m_Handler = null;

            foreach (Collision col in collisionList)
            {
                col.X = col.Сoordinates.X;
                col.Y = col.Сoordinates.Y;
                col.Z = col.Сoordinates.Z;
            }

            if(treeView1.Nodes.Count != 0)
            {
                try
                {
                    if (!Directory.Exists(myFolder))
                    {
                        Directory.CreateDirectory(myFolder);
                    }

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    XmlSerializer formatter = new XmlSerializer(typeof(List<Collision>));
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        formatter.Serialize(fs, collisionList);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка сохранения: " + ex.Message);
                }
            }
            else
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            
            collisionList.Clear();

            base.OnFormClosed(e);
        }

        private void textBoxSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                DeleteColl(treeView1.SelectedNode);

                collisionList.Remove((Collision)treeView1.SelectedNode.Tag);
                treeView1.SelectedNode.Remove();
            }
        }

        private void DeleteColl(TreeNode oParentNode)
        {
            collisionList.Remove((Collision)oParentNode.Tag);

            foreach (TreeNode oSubNode in oParentNode.Nodes)
            {
                DeleteColl(oSubNode);
            }
        }

        private void buttonNoView_Click(object sender, EventArgs e)
        {
            /* Debug
            MessageBox.Show("Список коллизий: " + collisionList.Count.ToString());

            List<string> ParName = new List<string>();				
            foreach (Collision col in collisionList)
            {
                ParName.Add(col.TestName + " " + col.Name + " " + col.Type);
            }
            MessageBox.Show(string.Join(Environment.NewLine, ParName));
            */

            //treeView1.SelectedNode.IsSelected & treeView1.SelectedNode.Level != 0
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Tag != null)
            {
                if (((Collision)treeView1.SelectedNode.Tag).IsViewed == false)
                {
                    if (((Collision)treeView1.SelectedNode.Tag).Type == "group")
                    {
                        collisionList.Find(x => x.Guid == ((Collision)treeView1.SelectedNode.Tag).Guid).IsViewed = true;
                        collisionList.FindAll(x => x.GroupGuid == ((Collision)treeView1.SelectedNode.Tag).Guid).ForEach(x => x.IsViewed = true);

                        treeView1.SelectedNode.NodeFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
                        foreach (TreeNode tn in treeView1.SelectedNode.Nodes)
                        {
                            tn.NodeFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
                        }
                    }
                    else
                    {
                        collisionList.Find(x => x.Guid == ((Collision)treeView1.SelectedNode.Tag).Guid).IsViewed = true;
                        treeView1.SelectedNode.NodeFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
                    }
                }
                else
                {
                    if (((Collision)treeView1.SelectedNode.Tag).Type == "group")
                    {
                        collisionList.Find(x => x.Guid == ((Collision)treeView1.SelectedNode.Tag).Guid).IsViewed = false;
                        collisionList.FindAll(x => x.GroupGuid == ((Collision)treeView1.SelectedNode.Tag).Guid).ForEach(x => x.IsViewed = false);

                        treeView1.SelectedNode.NodeFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
                        foreach (TreeNode tn in treeView1.SelectedNode.Nodes)
                        {
                            tn.NodeFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
                        }
                    }
                    else
                    {
                        collisionList.Find(x => x.Guid == ((Collision)treeView1.SelectedNode.Tag).Guid).IsViewed = false;
                        treeView1.SelectedNode.NodeFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
                    }
                }
            }
        }

        private void CreateTreeViewNodes(List<Collision> collisionList)
        {
            var tests = collisionList.GroupBy(x => x.TestName).ToList();

            foreach (var test in tests)
            {
                IEnumerable<Collision> alones = test.Where(x => x.Type == "alone").ToList();
                IEnumerable<Collision> groups = test.Where(x => x.Type == "group").ToList();

                TreeNode nodeTest = new TreeNode(test.Key);

                foreach (Collision alone in alones)
                {
                    TreeNode nodeAlone = new TreeNode(alone.Name);
                    nodeAlone.Tag = alone;

                    if(alone.IsViewed == true)
                         {  nodeAlone.NodeFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular); }
                    else {  nodeAlone.NodeFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold); }

                    nodeTest.Nodes.Add(nodeAlone);
                }

                foreach (Collision group in groups)
                {
                    TreeNode nodeGroup = new TreeNode(group.Name);
                    nodeGroup.Tag = group;

                    if (group.IsViewed == true)
                    { nodeGroup.NodeFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular); }
                    else { nodeGroup.NodeFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold); }

                    IEnumerable<Collision> groupChilds = test.Where(x => x.GroupGuid == group.Guid).ToList();

                    foreach (Collision child in groupChilds)
                    {
                        TreeNode nodeChild = new TreeNode(child.Name);
                        nodeChild.Tag = child;

                        if (child.IsViewed == true)
                        { nodeChild.NodeFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular); }
                        else { nodeChild.NodeFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold); }

                        nodeGroup.Nodes.Add(nodeChild);
                    }

                    nodeTest.Nodes.Add(nodeGroup);
                }

                treeView1.Nodes.Add(nodeTest);
            }
        }
    }
}
