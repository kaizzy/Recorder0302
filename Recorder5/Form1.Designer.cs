namespace Recorder5
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_refresh = new System.Windows.Forms.Button();
            this.listview_sources = new System.Windows.Forms.ListView();
            this.button_Start = new System.Windows.Forms.Button();
            this.button_stop = new System.Windows.Forms.Button();
            this.btn_Upload = new System.Windows.Forms.Button();
            this.btn_toText = new System.Windows.Forms.Button();
            this.btn_translat = new System.Windows.Forms.Button();
            this.Label_Status = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_keyPhase = new System.Windows.Forms.Button();
            this.List_Language = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // button_refresh
            // 
            this.button_refresh.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_refresh.Location = new System.Drawing.Point(57, 154);
            this.button_refresh.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(295, 38);
            this.button_refresh.TabIndex = 0;
            this.button_refresh.Text = "Reflesh_Source";
            this.button_refresh.UseVisualStyleBackColor = true;
            this.button_refresh.Click += new System.EventHandler(this.button_refresh_Click);
            // 
            // listview_sources
            // 
            this.listview_sources.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listview_sources.Location = new System.Drawing.Point(57, 54);
            this.listview_sources.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.listview_sources.Name = "listview_sources";
            this.listview_sources.Size = new System.Drawing.Size(295, 103);
            this.listview_sources.TabIndex = 1;
            this.listview_sources.UseCompatibleStateImageBehavior = false;
            this.listview_sources.SelectedIndexChanged += new System.EventHandler(this.listview_sources_SelectedIndexChanged);
            // 
            // button_Start
            // 
            this.button_Start.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Start.Location = new System.Drawing.Point(57, 202);
            this.button_Start.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(123, 38);
            this.button_Start.TabIndex = 2;
            this.button_Start.Text = "Rec Start";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // button_stop
            // 
            this.button_stop.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_stop.Location = new System.Drawing.Point(216, 202);
            this.button_stop.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(136, 38);
            this.button_stop.TabIndex = 3;
            this.button_stop.Text = "Rec Stop";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // btn_Upload
            // 
            this.btn_Upload.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_Upload.Location = new System.Drawing.Point(388, 54);
            this.btn_Upload.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btn_Upload.Name = "btn_Upload";
            this.btn_Upload.Size = new System.Drawing.Size(297, 38);
            this.btn_Upload.TabIndex = 4;
            this.btn_Upload.Text = "音声をGoogle Storageへのアップロード";
            this.btn_Upload.UseVisualStyleBackColor = true;
            this.btn_Upload.Click += new System.EventHandler(this.btn_Upload_Click);
            // 
            // btn_toText
            // 
            this.btn_toText.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_toText.Location = new System.Drawing.Point(388, 109);
            this.btn_toText.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btn_toText.Name = "btn_toText";
            this.btn_toText.Size = new System.Drawing.Size(297, 38);
            this.btn_toText.TabIndex = 5;
            this.btn_toText.Text = "音声を日本語テキストに変換";
            this.btn_toText.UseVisualStyleBackColor = true;
            this.btn_toText.Click += new System.EventHandler(this.btn_toText_Click);
            // 
            // btn_translat
            // 
            this.btn_translat.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_translat.Location = new System.Drawing.Point(710, 205);
            this.btn_translat.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btn_translat.Name = "btn_translat";
            this.btn_translat.Size = new System.Drawing.Size(297, 38);
            this.btn_translat.TabIndex = 6;
            this.btn_translat.Text = "日本語テキストを英語テキストに変換";
            this.btn_translat.UseVisualStyleBackColor = true;
            this.btn_translat.Click += new System.EventHandler(this.btn_translat_Click);
            // 
            // Label_Status
            // 
            this.Label_Status.AutoSize = true;
            this.Label_Status.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label_Status.Location = new System.Drawing.Point(48, 455);
            this.Label_Status.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label_Status.Name = "Label_Status";
            this.Label_Status.Size = new System.Drawing.Size(93, 27);
            this.Label_Status.TabIndex = 7;
            this.Label_Status.Text = "待機中";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.CausesValidation = false;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(53, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 26);
            this.label1.TabIndex = 8;
            this.label1.Text = "1 録音";
            this.label1.UseCompatibleTextRendering = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.CausesValidation = false;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(388, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(243, 26);
            this.label2.TabIndex = 9;
            this.label2.Text = "2 音声 ⇒ 日本語テキスト化";
            this.label2.UseCompatibleTextRendering = true;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.CausesValidation = false;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(710, 7);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(168, 26);
            this.label3.TabIndex = 10;
            this.label3.Text = "3 翻訳・DB・Web化";
            this.label3.UseCompatibleTextRendering = true;
            // 
            // btn_keyPhase
            // 
            this.btn_keyPhase.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_keyPhase.Location = new System.Drawing.Point(710, 260);
            this.btn_keyPhase.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btn_keyPhase.Name = "btn_keyPhase";
            this.btn_keyPhase.Size = new System.Drawing.Size(297, 38);
            this.btn_keyPhase.TabIndex = 11;
            this.btn_keyPhase.Text = "キーワード抽出";
            this.btn_keyPhase.UseVisualStyleBackColor = true;
            this.btn_keyPhase.Click += new System.EventHandler(this.btn_keyPhase_Click);
            // 
            // List_Language
            // 
            this.List_Language.DisplayMember = "English";
            this.List_Language.FormattingEnabled = true;
            this.List_Language.ItemHeight = 20;
            this.List_Language.Items.AddRange(new object[] {
            "English",
            "France",
            "German",
            "China"});
            this.List_Language.Location = new System.Drawing.Point(710, 54);
            this.List_Language.Name = "List_Language";
            this.List_Language.Size = new System.Drawing.Size(307, 104);
            this.List_Language.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1467, 750);
            this.Controls.Add(this.List_Language);
            this.Controls.Add(this.btn_keyPhase);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Label_Status);
            this.Controls.Add(this.btn_translat);
            this.Controls.Add(this.btn_toText);
            this.Controls.Add(this.btn_Upload);
            this.Controls.Add(this.button_stop);
            this.Controls.Add(this.button_Start);
            this.Controls.Add(this.listview_sources);
            this.Controls.Add(this.button_refresh);
            this.Font = new System.Drawing.Font("MS UI Gothic", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_refresh;
        private System.Windows.Forms.ListView listview_sources;
        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.Button btn_Upload;
        private System.Windows.Forms.Button btn_toText;
        private System.Windows.Forms.Button btn_translat;
        private System.Windows.Forms.Label Label_Status;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_keyPhase;
        private System.Windows.Forms.ListBox List_Language;
    }
}

