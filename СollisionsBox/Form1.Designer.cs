namespace СollisionsBox
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonLoadFile = new System.Windows.Forms.Button();
            this.buttonOpenCollision = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxView3D = new System.Windows.Forms.ComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBoxSize = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonNoView = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonLoadFile
            // 
            this.buttonLoadFile.Location = new System.Drawing.Point(12, 12);
            this.buttonLoadFile.Name = "buttonLoadFile";
            this.buttonLoadFile.Size = new System.Drawing.Size(326, 23);
            this.buttonLoadFile.TabIndex = 0;
            this.buttonLoadFile.Text = "Добавить файл проверки";
            this.buttonLoadFile.UseVisualStyleBackColor = true;
            this.buttonLoadFile.Click += new System.EventHandler(this.buttonLoadFile_Click);
            // 
            // buttonOpenCollision
            // 
            this.buttonOpenCollision.Location = new System.Drawing.Point(12, 524);
            this.buttonOpenCollision.Name = "buttonOpenCollision";
            this.buttonOpenCollision.Size = new System.Drawing.Size(326, 23);
            this.buttonOpenCollision.TabIndex = 1;
            this.buttonOpenCollision.Text = "Сформировать рамку выбора";
            this.buttonOpenCollision.UseVisualStyleBackColor = true;
            this.buttonOpenCollision.Click += new System.EventHandler(this.buttonOpenCollision_Click);
            // 
            // treeView1
            // 
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(12, 41);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(326, 381);
            this.treeView1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 474);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Выбрать 3D вид:";
            // 
            // comboBoxView3D
            // 
            this.comboBoxView3D.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxView3D.FormattingEnabled = true;
            this.comboBoxView3D.Location = new System.Drawing.Point(166, 471);
            this.comboBoxView3D.Name = "comboBoxView3D";
            this.comboBoxView3D.Size = new System.Drawing.Size(172, 21);
            this.comboBoxView3D.TabIndex = 4;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // textBoxSize
            // 
            this.textBoxSize.Location = new System.Drawing.Point(166, 498);
            this.textBoxSize.Name = "textBoxSize";
            this.textBoxSize.Size = new System.Drawing.Size(172, 20);
            this.textBoxSize.TabIndex = 5;
            this.textBoxSize.Text = "1000";
            this.textBoxSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSize_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 501);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Размер куба, мм:";
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(263, 428);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 7;
            this.buttonClear.Text = "Очистить";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonNoView
            // 
            this.buttonNoView.Location = new System.Drawing.Point(12, 428);
            this.buttonNoView.Name = "buttonNoView";
            this.buttonNoView.Size = new System.Drawing.Size(245, 23);
            this.buttonNoView.TabIndex = 8;
            this.buttonNoView.Text = "Просмотрено    |    Не просмотрено";
            this.buttonNoView.UseVisualStyleBackColor = true;
            this.buttonNoView.Click += new System.EventHandler(this.buttonNoView_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 559);
            this.Controls.Add(this.buttonNoView);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxSize);
            this.Controls.Add(this.comboBoxView3D);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.buttonOpenCollision);
            this.Controls.Add(this.buttonLoadFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonLoadFile;
        private System.Windows.Forms.Button buttonOpenCollision;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxView3D;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox textBoxSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonNoView;
    }
}