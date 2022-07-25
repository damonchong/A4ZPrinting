namespace A4ZPrinting
{
  partial class frmPrint
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
            this.btnStandard = new System.Windows.Forms.Button();
            this.btnLeftJustified = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStandard
            // 
            this.btnStandard.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStandard.Location = new System.Drawing.Point(324, 144);
            this.btnStandard.Name = "btnStandard";
            this.btnStandard.Size = new System.Drawing.Size(104, 42);
            this.btnStandard.TabIndex = 0;
            this.btnStandard.Text = "Standard";
            this.btnStandard.UseVisualStyleBackColor = true;
            this.btnStandard.Click += new System.EventHandler(this.btnStandard_Click);
            // 
            // btnLeftJustified
            // 
            this.btnLeftJustified.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLeftJustified.Location = new System.Drawing.Point(314, 235);
            this.btnLeftJustified.Name = "btnLeftJustified";
            this.btnLeftJustified.Size = new System.Drawing.Size(124, 40);
            this.btnLeftJustified.TabIndex = 1;
            this.btnLeftJustified.Text = "Left Justified";
            this.btnLeftJustified.UseVisualStyleBackColor = true;
            this.btnLeftJustified.Click += new System.EventHandler(this.btnLeftJustified_Click);
            // 
            // frmPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLeftJustified);
            this.Controls.Add(this.btnStandard);
            this.Name = "frmPrint";
            this.Text = "Label Printing";
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnStandard;
        private System.Windows.Forms.Button btnLeftJustified;
    }
}

