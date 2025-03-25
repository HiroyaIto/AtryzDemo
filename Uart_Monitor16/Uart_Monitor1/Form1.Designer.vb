<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.TextBox8 = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.Label19 = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SerialPort1
        '
        '
        'ComboBox2
        '
        Me.ComboBox2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(1254, 669)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(121, 32)
        Me.ComboBox2.TabIndex = 1
        Me.ComboBox2.Visible = False
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button1.Location = New System.Drawing.Point(1254, 25)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(149, 69)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "COMポート再読み込み"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(709, 708)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(108, 49)
        Me.Button3.TabIndex = 4
        Me.Button3.Text = "送信"
        Me.Button3.UseVisualStyleBackColor = True
        Me.Button3.Visible = False
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(391, 720)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(306, 25)
        Me.TextBox1.TabIndex = 5
        Me.TextBox1.Visible = False
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.White
        Me.TextBox2.Location = New System.Drawing.Point(1047, 708)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox2.Size = New System.Drawing.Size(492, 217)
        Me.TextBox2.TabIndex = 6
        Me.TextBox2.Visible = False
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Checked = True
        Me.CheckBox1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox1.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CheckBox1.Location = New System.Drawing.Point(791, 283)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(326, 32)
        Me.CheckBox1.TabIndex = 0
        Me.CheckBox1.Text = "RUN (waveの動作-停止)"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(186, 714)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(149, 23)
        Me.Button4.TabIndex = 8
        Me.Button4.Text = "テキストエリア"
        Me.Button4.UseVisualStyleBackColor = True
        Me.Button4.Visible = False
        '
        'Timer1
        '
        '
        'TextBox3
        '
        Me.TextBox3.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox3.Location = New System.Drawing.Point(791, 443)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(150, 35)
        Me.TextBox3.TabIndex = 9
        '
        'TextBox4
        '
        Me.TextBox4.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox4.Location = New System.Drawing.Point(996, 443)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(150, 35)
        Me.TextBox4.TabIndex = 10
        '
        'TextBox5
        '
        Me.TextBox5.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox5.Location = New System.Drawing.Point(1197, 443)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(150, 35)
        Me.TextBox5.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(786, 399)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 28)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "USEFUL"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(1001, 399)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(135, 28)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "AVERAGE"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(1216, 399)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 28)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "DIFF"
        '
        'TextBox6
        '
        Me.TextBox6.Location = New System.Drawing.Point(835, 675)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(100, 25)
        Me.TextBox6.TabIndex = 15
        Me.TextBox6.Visible = False
        '
        'TextBox7
        '
        Me.TextBox7.Location = New System.Drawing.Point(968, 675)
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New System.Drawing.Size(100, 25)
        Me.TextBox7.TabIndex = 16
        Me.TextBox7.Visible = False
        '
        'TextBox8
        '
        Me.TextBox8.Location = New System.Drawing.Point(1102, 675)
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.Size = New System.Drawing.Size(100, 25)
        Me.TextBox8.TabIndex = 17
        Me.TextBox8.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Location = New System.Drawing.Point(96, 52)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(668, 300)
        Me.PictureBox1.TabIndex = 18
        Me.PictureBox1.TabStop = False
        '
        'Button5
        '
        Me.Button5.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button5.Location = New System.Drawing.Point(791, 527)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(143, 78)
        Me.Button5.TabIndex = 19
        Me.Button5.Text = "ログ出力のON-OFF"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.White
        Me.PictureBox2.Location = New System.Drawing.Point(96, 400)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(5)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(668, 300)
        Me.PictureBox2.TabIndex = 20
        Me.PictureBox2.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(105, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 28)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "【DIFF】"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(105, 367)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(276, 28)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "【USEFUL/AVERAGE】"
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CheckBox2.Location = New System.Drawing.Point(791, 341)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(375, 32)
        Me.CheckBox2.TabIndex = 23
        Me.CheckBox2.Text = "MACRO (波形拡大ON-OFF)"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(14, 45)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(71, 18)
        Me.Label6.TabIndex = 24
        Me.Label6.Text = "1500000"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(14, 340)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(71, 18)
        Me.Label7.TabIndex = 25
        Me.Label7.Text = "-500000"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(14, 393)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(71, 18)
        Me.Label8.TabIndex = 26
        Me.Label8.Text = "1500000"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(14, 688)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(71, 18)
        Me.Label9.TabIndex = 27
        Me.Label9.Text = "-500000"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(64, 270)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(17, 18)
        Me.Label10.TabIndex = 28
        Me.Label10.Text = "0"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(64, 617)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(17, 18)
        Me.Label11.TabIndex = 29
        Me.Label11.Text = "0"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(951, 447)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(40, 28)
        Me.Label12.TabIndex = 30
        Me.Label12.Text = "－"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(1152, 447)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(40, 28)
        Me.Label13.TabIndex = 31
        Me.Label13.Text = "＝"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(251, 708)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(25, 18)
        Me.Label14.TabIndex = 32
        Me.Label14.Text = "1s"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(418, 708)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(25, 18)
        Me.Label15.TabIndex = 33
        Me.Label15.Text = "2s"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(575, 711)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(25, 18)
        Me.Label16.TabIndex = 34
        Me.Label16.Text = "3s"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(751, 708)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(25, 18)
        Me.Label17.TabIndex = 35
        Me.Label17.Text = "4s"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.ComboBox1)
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Location = New System.Drawing.Point(791, 52)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(411, 190)
        Me.Panel1.TabIndex = 36
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label18.Location = New System.Drawing.Point(13, 18)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(178, 24)
        Me.Label18.TabIndex = 37
        Me.Label18.Text = "シリアル通信接続"
        '
        'ComboBox1
        '
        Me.ComboBox1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(27, 74)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(143, 35)
        Me.ComboBox1.TabIndex = 0
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button2.Location = New System.Drawing.Point(228, 48)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(150, 82)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "COMポートのON-OFF"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'PictureBox4
        '
        Me.PictureBox4.Location = New System.Drawing.Point(1081, 527)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(50, 50)
        Me.PictureBox4.TabIndex = 37
        Me.PictureBox4.TabStop = False
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label19.Location = New System.Drawing.Point(1181, 546)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(154, 24)
        Me.Label19.TabIndex = 38
        Me.Label19.Text = "タッチ ON-OFF"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.ClientSize = New System.Drawing.Size(1414, 744)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.CheckBox2)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.TextBox8)
        Me.Controls.Add(Me.TextBox7)
        Me.Controls.Add(Me.TextBox6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox5)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ComboBox2)
        Me.Name = "Form1"
        Me.Text = "Uart_Monitor"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents SerialPort1 As IO.Ports.SerialPort
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Button4 As Button
    Friend WithEvents Timer1 As Timer
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents TextBox7 As TextBox
    Friend WithEvents TextBox8 As TextBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Button5 As Button
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Label18 As Label
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents Label19 As Label
End Class
