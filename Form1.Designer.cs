
namespace PortScanTool
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
            this.btnScan = new System.Windows.Forms.Button();
            this.tbIPStart = new System.Windows.Forms.TextBox();
            this.tbIPEnd = new System.Windows.Forms.TextBox();
            this.lblIPStart = new System.Windows.Forms.Label();
            this.lblIPEnd = new System.Windows.Forms.Label();
            this.lblIPRange = new System.Windows.Forms.Label();
            this.trckBarParallelTaskNumber = new System.Windows.Forms.TrackBar();
            this.lblParallelTaskNumber = new System.Windows.Forms.Label();
            this.lblParallelTaskNumberValue = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.lvResult = new System.Windows.Forms.ListView();
            this.chIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chIsOpen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblResult = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trckBarParallelTaskNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(12, 187);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(100, 23);
            this.btnScan.TabIndex = 0;
            this.btnScan.Text = "Scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // tbIPStart
            // 
            this.tbIPStart.Location = new System.Drawing.Point(12, 46);
            this.tbIPStart.Name = "tbIPStart";
            this.tbIPStart.Size = new System.Drawing.Size(100, 20);
            this.tbIPStart.TabIndex = 1;
            // 
            // tbIPEnd
            // 
            this.tbIPEnd.Location = new System.Drawing.Point(118, 46);
            this.tbIPEnd.Name = "tbIPEnd";
            this.tbIPEnd.Size = new System.Drawing.Size(100, 20);
            this.tbIPEnd.TabIndex = 2;
            // 
            // lblIPStart
            // 
            this.lblIPStart.AutoSize = true;
            this.lblIPStart.Location = new System.Drawing.Point(12, 30);
            this.lblIPStart.Name = "lblIPStart";
            this.lblIPStart.Size = new System.Drawing.Size(33, 13);
            this.lblIPStart.TabIndex = 3;
            this.lblIPStart.Text = "From:";
            // 
            // lblIPEnd
            // 
            this.lblIPEnd.AutoSize = true;
            this.lblIPEnd.Location = new System.Drawing.Point(115, 30);
            this.lblIPEnd.Name = "lblIPEnd";
            this.lblIPEnd.Size = new System.Drawing.Size(23, 13);
            this.lblIPEnd.TabIndex = 4;
            this.lblIPEnd.Text = "To:";
            // 
            // lblIPRange
            // 
            this.lblIPRange.AutoSize = true;
            this.lblIPRange.Location = new System.Drawing.Point(12, 17);
            this.lblIPRange.Name = "lblIPRange";
            this.lblIPRange.Size = new System.Drawing.Size(52, 13);
            this.lblIPRange.TabIndex = 5;
            this.lblIPRange.Text = "IP Range";
            // 
            // trckBarParallelTaskNumber
            // 
            this.trckBarParallelTaskNumber.Location = new System.Drawing.Point(15, 113);
            this.trckBarParallelTaskNumber.Name = "trckBarParallelTaskNumber";
            this.trckBarParallelTaskNumber.Size = new System.Drawing.Size(203, 45);
            this.trckBarParallelTaskNumber.TabIndex = 6;
            this.trckBarParallelTaskNumber.Scroll += new System.EventHandler(this.trckBarParallelTaskNumber_Scroll);
            // 
            // lblParallelTaskNumber
            // 
            this.lblParallelTaskNumber.AutoSize = true;
            this.lblParallelTaskNumber.Location = new System.Drawing.Point(12, 97);
            this.lblParallelTaskNumber.Name = "lblParallelTaskNumber";
            this.lblParallelTaskNumber.Size = new System.Drawing.Size(114, 13);
            this.lblParallelTaskNumber.TabIndex = 7;
            this.lblParallelTaskNumber.Text = "Parallel action number:";
            // 
            // lblParallelTaskNumberValue
            // 
            this.lblParallelTaskNumberValue.AutoSize = true;
            this.lblParallelTaskNumberValue.Location = new System.Drawing.Point(129, 97);
            this.lblParallelTaskNumberValue.Name = "lblParallelTaskNumberValue";
            this.lblParallelTaskNumberValue.Size = new System.Drawing.Size(13, 13);
            this.lblParallelTaskNumberValue.TabIndex = 8;
            this.lblParallelTaskNumberValue.Text = "0";
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(118, 187);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 23);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lvResult
            // 
            this.lvResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chIP,
            this.chPort,
            this.chIsOpen});
            this.lvResult.HideSelection = false;
            this.lvResult.Location = new System.Drawing.Point(265, 30);
            this.lvResult.Name = "lvResult";
            this.lvResult.Size = new System.Drawing.Size(288, 180);
            this.lvResult.TabIndex = 10;
            this.lvResult.UseCompatibleStateImageBehavior = false;
            // 
            // chIP
            // 
            this.chIP.Text = "IP";
            // 
            // chPort
            // 
            this.chPort.Text = "Port";
            // 
            // chIsOpen
            // 
            this.chIsOpen.Text = "IsOpen";
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(262, 14);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(40, 13);
            this.lblResult.TabIndex = 11;
            this.lblResult.Text = "Result:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 221);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.lvResult);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lblParallelTaskNumberValue);
            this.Controls.Add(this.lblParallelTaskNumber);
            this.Controls.Add(this.trckBarParallelTaskNumber);
            this.Controls.Add(this.lblIPRange);
            this.Controls.Add(this.lblIPEnd);
            this.Controls.Add(this.lblIPStart);
            this.Controls.Add(this.tbIPEnd);
            this.Controls.Add(this.tbIPStart);
            this.Controls.Add(this.btnScan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Port Scan Tool";
            ((System.ComponentModel.ISupportInitialize)(this.trckBarParallelTaskNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.TextBox tbIPStart;
        private System.Windows.Forms.TextBox tbIPEnd;
        private System.Windows.Forms.Label lblIPStart;
        private System.Windows.Forms.Label lblIPEnd;
        private System.Windows.Forms.Label lblIPRange;
        private System.Windows.Forms.TrackBar trckBarParallelTaskNumber;
        private System.Windows.Forms.Label lblParallelTaskNumber;
        private System.Windows.Forms.Label lblParallelTaskNumberValue;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ListView lvResult;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.ColumnHeader chIP;
        private System.Windows.Forms.ColumnHeader chPort;
        private System.Windows.Forms.ColumnHeader chIsOpen;
    }
}

