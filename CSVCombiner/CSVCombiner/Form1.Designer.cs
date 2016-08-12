namespace CSVCombiner
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
            this.btnSelectFiles = new System.Windows.Forms.Button();
            this.lblFilesSelected = new System.Windows.Forms.Label();
            this.lblTimeStamps = new System.Windows.Forms.Label();
            this.cbxRealOffset = new System.Windows.Forms.CheckBox();
            this.tbxTimeOffset = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCombine = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSelectFiles
            // 
            this.btnSelectFiles.Location = new System.Drawing.Point(12, 12);
            this.btnSelectFiles.Name = "btnSelectFiles";
            this.btnSelectFiles.Size = new System.Drawing.Size(135, 23);
            this.btnSelectFiles.TabIndex = 0;
            this.btnSelectFiles.Text = "Select files";
            this.btnSelectFiles.UseVisualStyleBackColor = true;
            this.btnSelectFiles.Click += new System.EventHandler(this.btnSelectFiles_Click);
            // 
            // lblFilesSelected
            // 
            this.lblFilesSelected.AutoSize = true;
            this.lblFilesSelected.Location = new System.Drawing.Point(12, 58);
            this.lblFilesSelected.Name = "lblFilesSelected";
            this.lblFilesSelected.Size = new System.Drawing.Size(0, 13);
            this.lblFilesSelected.TabIndex = 1;
            // 
            // lblTimeStamps
            // 
            this.lblTimeStamps.AutoSize = true;
            this.lblTimeStamps.Location = new System.Drawing.Point(12, 91);
            this.lblTimeStamps.Name = "lblTimeStamps";
            this.lblTimeStamps.Size = new System.Drawing.Size(0, 13);
            this.lblTimeStamps.TabIndex = 2;
            // 
            // cbxRealOffset
            // 
            this.cbxRealOffset.AutoSize = true;
            this.cbxRealOffset.Location = new System.Drawing.Point(12, 127);
            this.cbxRealOffset.Name = "cbxRealOffset";
            this.cbxRealOffset.Size = new System.Drawing.Size(113, 17);
            this.cbxRealOffset.TabIndex = 3;
            this.cbxRealOffset.Text = "Use real timeoffset";
            this.cbxRealOffset.UseVisualStyleBackColor = true;
            // 
            // tbxTimeOffset
            // 
            this.tbxTimeOffset.Location = new System.Drawing.Point(12, 164);
            this.tbxTimeOffset.Name = "tbxTimeOffset";
            this.tbxTimeOffset.Size = new System.Drawing.Size(35, 20);
            this.tbxTimeOffset.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Time offset [s]";
            // 
            // btnCombine
            // 
            this.btnCombine.Location = new System.Drawing.Point(12, 206);
            this.btnCombine.Name = "btnCombine";
            this.btnCombine.Size = new System.Drawing.Size(135, 23);
            this.btnCombine.TabIndex = 6;
            this.btnCombine.Text = "Combine";
            this.btnCombine.UseVisualStyleBackColor = true;
            this.btnCombine.Click += new System.EventHandler(this.btnCombine_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(165, 243);
            this.Controls.Add(this.btnCombine);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxTimeOffset);
            this.Controls.Add(this.cbxRealOffset);
            this.Controls.Add(this.lblTimeStamps);
            this.Controls.Add(this.lblFilesSelected);
            this.Controls.Add(this.btnSelectFiles);
            this.Name = "Form1";
            this.Text = "CSV Combiner";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectFiles;
        private System.Windows.Forms.Label lblFilesSelected;
        private System.Windows.Forms.Label lblTimeStamps;
        private System.Windows.Forms.CheckBox cbxRealOffset;
        private System.Windows.Forms.TextBox tbxTimeOffset;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCombine;
    }
}

