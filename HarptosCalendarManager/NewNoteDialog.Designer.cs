﻿namespace HarptosCalendarManager
{
    partial class NewNoteDialog
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
            this.components = new System.ComponentModel.Container();
            this.generalBox = new System.Windows.Forms.CheckBox();
            this.noAlert = new System.Windows.Forms.RadioButton();
            this.AlertCampaign = new System.Windows.Forms.RadioButton();
            this.alertAll = new System.Windows.Forms.RadioButton();
            this.generalTip = new System.Windows.Forms.ToolTip(this.components);
            this.globalTip = new System.Windows.Forms.ToolTip(this.components);
            this.campaignTip = new System.Windows.Forms.ToolTip(this.components);
            this.noAlertTip = new System.Windows.Forms.ToolTip(this.components);
            this.newNoteBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // generalBox
            // 
            this.generalBox.AutoSize = true;
            this.generalBox.Location = new System.Drawing.Point(201, 10);
            this.generalBox.Name = "generalBox";
            this.generalBox.Size = new System.Drawing.Size(63, 17);
            this.generalBox.TabIndex = 3;
            this.generalBox.Text = "General";
            this.generalBox.UseVisualStyleBackColor = true;
            this.generalBox.CheckedChanged += new System.EventHandler(this.generalBox_CheckedChanged);
            // 
            // noAlert
            // 
            this.noAlert.AutoSize = true;
            this.noAlert.Location = new System.Drawing.Point(201, 79);
            this.noAlert.Name = "noAlert";
            this.noAlert.Size = new System.Drawing.Size(68, 17);
            this.noAlert.TabIndex = 4;
            this.noAlert.TabStop = true;
            this.noAlert.Text = "No Alerts";
            this.noAlert.UseVisualStyleBackColor = true;
            // 
            // AlertCampaign
            // 
            this.AlertCampaign.AutoSize = true;
            this.AlertCampaign.Location = new System.Drawing.Point(201, 56);
            this.AlertCampaign.Name = "AlertCampaign";
            this.AlertCampaign.Size = new System.Drawing.Size(72, 17);
            this.AlertCampaign.TabIndex = 5;
            this.AlertCampaign.TabStop = true;
            this.AlertCampaign.Text = "Campaign";
            this.AlertCampaign.UseVisualStyleBackColor = true;
            // 
            // alertAll
            // 
            this.alertAll.AutoSize = true;
            this.alertAll.Location = new System.Drawing.Point(201, 33);
            this.alertAll.Name = "alertAll";
            this.alertAll.Size = new System.Drawing.Size(55, 17);
            this.alertAll.TabIndex = 6;
            this.alertAll.TabStop = true;
            this.alertAll.Text = "Global";
            this.alertAll.UseVisualStyleBackColor = true;
            // 
            // newNoteBox
            // 
            this.newNoteBox.Location = new System.Drawing.Point(12, 30);
            this.newNoteBox.Multiline = true;
            this.newNoteBox.Name = "newNoteBox";
            this.newNoteBox.Size = new System.Drawing.Size(183, 66);
            this.newNoteBox.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Note Contents:";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(120, 102);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 9;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(201, 102);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 10;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // NewNoteDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 131);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.newNoteBox);
            this.Controls.Add(this.alertAll);
            this.Controls.Add(this.AlertCampaign);
            this.Controls.Add(this.noAlert);
            this.Controls.Add(this.generalBox);
            this.Name = "NewNoteDialog";
            this.ShowIcon = false;
            this.Text = "New Note";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox generalBox;
        private System.Windows.Forms.RadioButton noAlert;
        private System.Windows.Forms.RadioButton AlertCampaign;
        private System.Windows.Forms.RadioButton alertAll;
        private System.Windows.Forms.ToolTip generalTip;
        private System.Windows.Forms.ToolTip globalTip;
        private System.Windows.Forms.ToolTip campaignTip;
        private System.Windows.Forms.ToolTip noAlertTip;
        private System.Windows.Forms.TextBox newNoteBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}