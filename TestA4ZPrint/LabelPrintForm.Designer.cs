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
      this.btnGX430Standard = new System.Windows.Forms.Button();
      this.btnGX430LeftJustified = new System.Windows.Forms.Button();
      this.gbGX430t = new System.Windows.Forms.GroupBox();
      this.gbZD420t = new System.Windows.Forms.GroupBox();
      this.btnZD420Standard = new System.Windows.Forms.Button();
      this.gbGX430t.SuspendLayout();
      this.gbZD420t.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnGX430Standard
      // 
      this.btnGX430Standard.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnGX430Standard.Location = new System.Drawing.Point(135, 44);
      this.btnGX430Standard.Name = "btnGX430Standard";
      this.btnGX430Standard.Size = new System.Drawing.Size(104, 42);
      this.btnGX430Standard.TabIndex = 0;
      this.btnGX430Standard.Text = "Standard";
      this.btnGX430Standard.UseVisualStyleBackColor = true;
      this.btnGX430Standard.Click += new System.EventHandler(this.btnStandard_Click);
      // 
      // btnGX430LeftJustified
      // 
      this.btnGX430LeftJustified.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnGX430LeftJustified.Location = new System.Drawing.Point(125, 135);
      this.btnGX430LeftJustified.Name = "btnGX430LeftJustified";
      this.btnGX430LeftJustified.Size = new System.Drawing.Size(124, 40);
      this.btnGX430LeftJustified.TabIndex = 1;
      this.btnGX430LeftJustified.Text = "Left Justified";
      this.btnGX430LeftJustified.UseVisualStyleBackColor = true;
      this.btnGX430LeftJustified.Click += new System.EventHandler(this.btnLeftJustified_Click);
      // 
      // gbGX430t
      // 
      this.gbGX430t.Controls.Add(this.btnGX430LeftJustified);
      this.gbGX430t.Controls.Add(this.btnGX430Standard);
      this.gbGX430t.Location = new System.Drawing.Point(196, 34);
      this.gbGX430t.Name = "gbGX430t";
      this.gbGX430t.Size = new System.Drawing.Size(383, 211);
      this.gbGX430t.TabIndex = 2;
      this.gbGX430t.TabStop = false;
      this.gbGX430t.Text = "GX430t";
      // 
      // gbZD420t
      // 
      this.gbZD420t.Controls.Add(this.btnZD420Standard);
      this.gbZD420t.Location = new System.Drawing.Point(196, 294);
      this.gbZD420t.Name = "gbZD420t";
      this.gbZD420t.Size = new System.Drawing.Size(385, 178);
      this.gbZD420t.TabIndex = 3;
      this.gbZD420t.TabStop = false;
      this.gbZD420t.Text = "ZD420t";
      // 
      // btnZD420Standard
      // 
      this.btnZD420Standard.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnZD420Standard.Location = new System.Drawing.Point(140, 68);
      this.btnZD420Standard.Name = "btnZD420Standard";
      this.btnZD420Standard.Size = new System.Drawing.Size(104, 42);
      this.btnZD420Standard.TabIndex = 1;
      this.btnZD420Standard.Text = "Standard";
      this.btnZD420Standard.UseVisualStyleBackColor = true;
      this.btnZD420Standard.Click += new System.EventHandler(this.btnZD420Standard_Click);
      // 
      // frmPrint
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 517);
      this.Controls.Add(this.gbZD420t);
      this.Controls.Add(this.gbGX430t);
      this.Name = "frmPrint";
      this.Text = "Label Printing";
      this.gbGX430t.ResumeLayout(false);
      this.gbZD420t.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnGX430Standard;
        private System.Windows.Forms.Button btnGX430LeftJustified;
    private System.Windows.Forms.GroupBox gbGX430t;
    private System.Windows.Forms.GroupBox gbZD420t;
    private System.Windows.Forms.Button btnZD420Standard;
  }
}

