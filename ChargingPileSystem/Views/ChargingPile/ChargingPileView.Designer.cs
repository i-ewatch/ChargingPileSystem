
namespace ChargingPileSystem.Views.ChargingPile
{
    partial class ChargingPileView
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
            this.ChargingPilepanelControl = new DevExpress.XtraEditors.PanelControl();
            this.MasterMetenavigationFrame = new DevExpress.XtraBars.Navigation.NavigationFrame();
            ((System.ComponentModel.ISupportInitialize)(this.ChargingPilepanelControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MasterMetenavigationFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // ChargingPilepanelControl
            // 
            this.ChargingPilepanelControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ChargingPilepanelControl.Location = new System.Drawing.Point(0, 361);
            this.ChargingPilepanelControl.Name = "ChargingPilepanelControl";
            this.ChargingPilepanelControl.Size = new System.Drawing.Size(1716, 687);
            this.ChargingPilepanelControl.TabIndex = 5;
            // 
            // MasterMetenavigationFrame
            // 
            this.MasterMetenavigationFrame.Location = new System.Drawing.Point(5, 2);
            this.MasterMetenavigationFrame.Name = "MasterMetenavigationFrame";
            this.MasterMetenavigationFrame.SelectedPage = null;
            this.MasterMetenavigationFrame.Size = new System.Drawing.Size(1705, 350);
            this.MasterMetenavigationFrame.TabIndex = 4;
            this.MasterMetenavigationFrame.Text = "navigationFrame1";
            // 
            // ChargingPileView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ChargingPilepanelControl);
            this.Controls.Add(this.MasterMetenavigationFrame);
            this.Name = "ChargingPileView";
            this.Size = new System.Drawing.Size(1716, 1048);
            ((System.ComponentModel.ISupportInitialize)(this.ChargingPilepanelControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MasterMetenavigationFrame)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl ChargingPilepanelControl;
        private DevExpress.XtraBars.Navigation.NavigationFrame MasterMetenavigationFrame;
    }
}
