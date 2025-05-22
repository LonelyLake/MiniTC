namespace MiniTC.View
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
            this.buttonСopy = new System.Windows.Forms.Button();
            this.panelRight = new MiniTC.PanelTC();
            this.panelLeft = new MiniTC.PanelTC();
            this.SuspendLayout();
            // 
            // buttonСopy
            // 
            this.buttonСopy.Location = new System.Drawing.Point(364, 645);
            this.buttonСopy.Name = "buttonСopy";
            this.buttonСopy.Size = new System.Drawing.Size(100, 50);
            this.buttonСopy.TabIndex = 2;
            this.buttonСopy.Text = "Copy >>>";
            this.buttonСopy.UseVisualStyleBackColor = true;
            // 
            // panelRight
            // 
            this.panelRight.CurrentPath = null;
            this.panelRight.Drives = null;
            this.panelRight.Items = null;
            this.panelRight.Location = new System.Drawing.Point(417, 12);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(405, 627);
            this.panelRight.TabIndex = 1;
            // 
            // panelLeft
            // 
            this.panelLeft.CurrentPath = null;
            this.panelLeft.Drives = null;
            this.panelLeft.Items = null;
            this.panelLeft.Location = new System.Drawing.Point(12, 12);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(399, 627);
            this.panelLeft.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 730);
            this.Controls.Add(this.buttonСopy);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private PanelTC panelLeft;
        private PanelTC panelRight;
        private System.Windows.Forms.Button buttonСopy;
    }
}

