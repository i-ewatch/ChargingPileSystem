
namespace ChargingPileSystem.Views.Setting
{
    partial class DeviceNameSettingUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.SavesimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.CancelsimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.NewDeviceNametextEdit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.OldDeviceNamelabelControl = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NewDeviceNametextEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.SavesimpleButton);
            this.panelControl1.Controls.Add(this.CancelsimpleButton);
            this.panelControl1.Controls.Add(this.NewDeviceNametextEdit);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.OldDeviceNamelabelControl);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(359, 118);
            this.panelControl1.TabIndex = 0;
            // 
            // SavesimpleButton
            // 
            this.SavesimpleButton.AllowFocus = false;
            this.SavesimpleButton.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.SavesimpleButton.Appearance.Font = new System.Drawing.Font("微軟正黑體", 16F);
            this.SavesimpleButton.Appearance.Options.UseBackColor = true;
            this.SavesimpleButton.Appearance.Options.UseFont = true;
            this.SavesimpleButton.Location = new System.Drawing.Point(180, 81);
            this.SavesimpleButton.Name = "SavesimpleButton";
            this.SavesimpleButton.Size = new System.Drawing.Size(83, 32);
            this.SavesimpleButton.TabIndex = 21;
            this.SavesimpleButton.Text = "儲存";
            this.SavesimpleButton.Click += new System.EventHandler(this.SavesimpleButton_Click);
            // 
            // CancelsimpleButton
            // 
            this.CancelsimpleButton.AllowFocus = false;
            this.CancelsimpleButton.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.CancelsimpleButton.Appearance.Font = new System.Drawing.Font("微軟正黑體", 16F);
            this.CancelsimpleButton.Appearance.Options.UseBackColor = true;
            this.CancelsimpleButton.Appearance.Options.UseFont = true;
            this.CancelsimpleButton.Location = new System.Drawing.Point(269, 80);
            this.CancelsimpleButton.Name = "CancelsimpleButton";
            this.CancelsimpleButton.Size = new System.Drawing.Size(83, 32);
            this.CancelsimpleButton.TabIndex = 20;
            this.CancelsimpleButton.Text = "取消";
            this.CancelsimpleButton.Click += new System.EventHandler(this.CancelsimpleButton_Click);
            // 
            // NewDeviceNametextEdit
            // 
            this.NewDeviceNametextEdit.Location = new System.Drawing.Point(105, 49);
            this.NewDeviceNametextEdit.Name = "NewDeviceNametextEdit";
            this.NewDeviceNametextEdit.Properties.AllowFocused = false;
            this.NewDeviceNametextEdit.Properties.Appearance.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.NewDeviceNametextEdit.Properties.Appearance.Options.UseFont = true;
            this.NewDeviceNametextEdit.Size = new System.Drawing.Size(249, 26);
            this.NewDeviceNametextEdit.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("微軟正黑體", 20F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(5, 45);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(94, 34);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "新名稱 :";
            // 
            // OldDeviceNamelabelControl
            // 
            this.OldDeviceNamelabelControl.Appearance.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.OldDeviceNamelabelControl.Appearance.Options.UseFont = true;
            this.OldDeviceNamelabelControl.Location = new System.Drawing.Point(105, 12);
            this.OldDeviceNamelabelControl.Name = "OldDeviceNamelabelControl";
            this.OldDeviceNamelabelControl.Size = new System.Drawing.Size(32, 20);
            this.OldDeviceNamelabelControl.TabIndex = 1;
            this.OldDeviceNamelabelControl.Text = "N/A";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("微軟正黑體", 20F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(5, 5);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(94, 34);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "舊名稱 :";
            // 
            // DeviceNameSettingUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "DeviceNameSettingUserControl";
            this.Size = new System.Drawing.Size(359, 118);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NewDeviceNametextEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit NewDeviceNametextEdit;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl OldDeviceNamelabelControl;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton SavesimpleButton;
        private DevExpress.XtraEditors.SimpleButton CancelsimpleButton;
    }
}
