namespace GravitySimulator
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.frameTimer = new System.Windows.Forms.Timer(this.components);
            this.skglSurface = new SkiaSharp.Views.Desktop.SKGLControl();
            this.SuspendLayout();
            // 
            // frameTimer
            // 
            this.frameTimer.Enabled = true;
            this.frameTimer.Interval = 1;
            this.frameTimer.Tick += new System.EventHandler(this.FrameTimer_Tick);
            // 
            // skglSurface
            // 
            this.skglSurface.BackColor = System.Drawing.Color.Black;
            this.skglSurface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skglSurface.Location = new System.Drawing.Point(0, 0);
            this.skglSurface.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.skglSurface.Name = "skglSurface";
            this.skglSurface.Size = new System.Drawing.Size(982, 553);
            this.skglSurface.TabIndex = 0;
            this.skglSurface.VSync = false;
            this.skglSurface.PaintSurface += new System.EventHandler<SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs>(this.SkglSurface_PaintSurface);
            this.skglSurface.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SkglSurface_KeyDown);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 553);
            this.Controls.Add(this.skglSurface);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GravitySimulator";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer frameTimer;
        private SkiaSharp.Views.Desktop.SKGLControl skglSurface;
    }
}

