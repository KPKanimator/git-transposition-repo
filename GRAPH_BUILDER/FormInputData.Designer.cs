namespace GRAPH_BUILDER
{
  partial class FormInputData
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
      this.label1 = new System.Windows.Forms.Label();
      this.tbCoordinateR0 = new System.Windows.Forms.TextBox();
      this.tbCoordinateRX = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.tbCoordinateRY = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.tbRadiuses = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.tbX = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.tbY = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnDefault = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(23, 28);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(27, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "R0=";
      // 
      // tbCoordinateR0
      // 
      this.tbCoordinateR0.Location = new System.Drawing.Point(56, 25);
      this.tbCoordinateR0.Name = "tbCoordinateR0";
      this.tbCoordinateR0.Size = new System.Drawing.Size(100, 20);
      this.tbCoordinateR0.TabIndex = 1;
      // 
      // tbCoordinateRX
      // 
      this.tbCoordinateRX.Location = new System.Drawing.Point(207, 25);
      this.tbCoordinateRX.Name = "tbCoordinateRX";
      this.tbCoordinateRX.Size = new System.Drawing.Size(100, 20);
      this.tbCoordinateRX.TabIndex = 3;
      this.tbCoordinateRX.Text = "0";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(174, 28);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(23, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "X =";
      // 
      // tbCoordinateRY
      // 
      this.tbCoordinateRY.Location = new System.Drawing.Point(356, 25);
      this.tbCoordinateRY.Name = "tbCoordinateRY";
      this.tbCoordinateRY.Size = new System.Drawing.Size(100, 20);
      this.tbCoordinateRY.TabIndex = 5;
      this.tbCoordinateRY.Text = "0";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(323, 28);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(23, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "Y =";
      // 
      // tbRadiuses
      // 
      this.tbRadiuses.Location = new System.Drawing.Point(39, 65);
      this.tbRadiuses.Multiline = true;
      this.tbRadiuses.Name = "tbRadiuses";
      this.tbRadiuses.Size = new System.Drawing.Size(129, 290);
      this.tbRadiuses.TabIndex = 7;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(12, 65);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(19, 13);
      this.label4.TabIndex = 6;
      this.label4.Text = "r =";
      // 
      // tbX
      // 
      this.tbX.Location = new System.Drawing.Point(193, 65);
      this.tbX.Multiline = true;
      this.tbX.Name = "tbX";
      this.tbX.Size = new System.Drawing.Size(126, 290);
      this.tbX.TabIndex = 9;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(174, 65);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(21, 13);
      this.label5.TabIndex = 8;
      this.label5.Text = "x =";
      // 
      // tbY
      // 
      this.tbY.Location = new System.Drawing.Point(350, 65);
      this.tbY.Multiline = true;
      this.tbY.Name = "tbY";
      this.tbY.Size = new System.Drawing.Size(132, 290);
      this.tbY.TabIndex = 11;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(323, 65);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(21, 13);
      this.label6.TabIndex = 10;
      this.label6.Text = "y =";
      // 
      // btnSave
      // 
      this.btnSave.Location = new System.Drawing.Point(356, 369);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(137, 23);
      this.btnSave.TabIndex = 12;
      this.btnSave.Text = "Сохранить";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(25, 369);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(131, 23);
      this.btnCancel.TabIndex = 13;
      this.btnCancel.Text = "Отмена";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // btnDefault
      // 
      this.btnDefault.Location = new System.Drawing.Point(193, 369);
      this.btnDefault.Name = "btnDefault";
      this.btnDefault.Size = new System.Drawing.Size(131, 23);
      this.btnDefault.TabIndex = 14;
      this.btnDefault.Text = "По умолчанию";
      this.btnDefault.UseVisualStyleBackColor = true;
      this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
      // 
      // FormInputData
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(505, 404);
      this.Controls.Add(this.btnDefault);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnSave);
      this.Controls.Add(this.tbY);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.tbX);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.tbRadiuses);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.tbCoordinateRY);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.tbCoordinateRX);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.tbCoordinateR0);
      this.Controls.Add(this.label1);
      this.Name = "FormInputData";
      this.Text = "FormInputData";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox tbCoordinateR0;
    private System.Windows.Forms.TextBox tbCoordinateRX;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox tbCoordinateRY;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox tbRadiuses;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox tbX;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox tbY;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnDefault;
  }
}