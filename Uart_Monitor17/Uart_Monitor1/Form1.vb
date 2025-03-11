Imports System.IO.Ports
Imports System.Threading.Tasks

Public Class Form1



#Const MNI_ENABLE = 0'1：WMI有 0:WMI無し 

    Const RXRGSZ As Integer = 64
    'Const RXRGSZ As Integer = 2048

    Structure TYP_COMRING
        Dim rp As Integer
        Dim wp As Integer
        Dim dat() As Byte
    End Structure

    Structure COM_PORT
        Dim dvicename As String
        Dim comno As String
    End Structure

    Dim RxRing As TYP_COMRING '受信したデータを管理
    Dim ComInfo() As COM_PORT
    Dim selectComNo As Integer


    'RxRingの初期化を行う
    Private Sub RxRingInit()
        RxRing.wp = 0
        RxRing.rp = 0
        ReDim RxRing.dat(RXRGSZ - 1)
    End Sub

    'Comboboxを初期化する
    Private Sub ComboInit()

#If MNI_ENABLE Then
        ComPortChk2() 
#Else
        ComPortChk() 'ComboBox1の初期化（ポートの検索）
#End If
        ComboBox2.Items.Add("9600")
        ComboBox2.Items.Add("19200")
        ComboBox2.Items.Add("115200")
        ComboBox2.Text = "19200"
    End Sub

    '存在するポートを検索する
    Private Sub ComPortChk()
        Dim comno() As String = SerialPort.GetPortNames
        Dim i As Integer

        ComboBox1.Items.Clear() 'アイテムをクリア

        If comno.Length = 0 Then
            ComboBox1.Text = "COM無し"
        Else
            For i = 0 To comno.Length - 1
                ComboBox1.Items.Add(comno(i))
            Next
            ComboBox1.Text = comno(0)
        End If

    End Sub

    '存在するポートを検索する
    Private Sub ComPortChk2()
        'Dim mngstr As New Management.ManagementObjectSearcher("Select * from Win32_SerialPort")
        'Dim mc As Management.ManagementObjectCollection
        'Dim serial As Management.ManagementBaseObject
        Dim comno() As String = SerialPort.GetPortNames
        Dim i, j As Integer
        Dim serialcnt As Integer
        Dim comcnt As Integer
        Dim strno As Byte
        Dim str As String
        Dim comstr As String

        'mc = mngstr.Get()
        'serialcnt = mc.Count
        comcnt = comno.Length
        ReDim ComInfo(serialcnt - 1)

        'For Each serial In mc
        'ComInfo(i).dvicename = serial("Name")

        'strno = InStr(ComInfo(i).dvicename, "(COM")
        'If strno <> 0 Then
        'strno += 1
        'comstr = ""
        'For j = 0 To 6
        'str = Mid(ComInfo(i).dvicename, strno + j, 1)
        'If str <> ")" Then
        'comstr &= str
        'Else
        'Exit For
        'End If
        'Next
        'ComInfo(i).comno = comstr
        'End If

        'i += 1
        'Next

        ComboBox1.Items.Clear() 'アイテムをクリア

        If serialcnt = 0 Then
            ComboBox1.Text = "COMポートがみつかりません。"
        Else
            For i = 0 To ComInfo.Length - 1
                ComboBox1.Items.Add(ComInfo(i).dvicename)
            Next
            ComboBox1.Text = ComInfo(0).dvicename
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

#If MNI_ENABLE Then
        ComPortChk2() '存在するポートを検索する
#Else
        ComPortChk() '存在するポートを検索する
#End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        ' RGB値を指定してボタンの背景色を設定（ここでは赤色）
        Dim red As Integer = 155
        Dim green As Integer = 230
        Dim blue As Integer = 253

        If com_flg = 0 Then
            com_flg = 1
            PortOpen() 'ポートをオープンする
        Else
            com_flg = 0 'ポートをクローズする
            PortClose()
        End If

        If com_flg = 1 Then
            Button2.Text = "COMポートオン中"
            Button2.BackColor = Color.FromArgb(red, green, blue)
        Else
            Button2.Text = "COMポートオフ中"
            Button2.BackColor = Color.White
        End If






        'PortOpen() 'ポートをオープンする
    End Sub

    'ポートをクローズする
    Private Sub PortClose()
        'Me.Invoke(dlg, New Object() {dat}) 'Form1_Shownで表示できたので不要
        'Me.Invoke(dlg2, New Object() {dat})
        Try
            With SerialPort1
                .Close() 'ポートを閉じる
                ''MsgBox(ComboBox1.Text & "をクローズできた。", MsgBoxStyle.OkOnly)
            End With

            'Timer1.Enabled = True
        Catch ex As Exception
            MsgBox(ComboBox1.Text & "をクローズできませんでした。", MsgBoxStyle.OkOnly)
        End Try
    End Sub


    'ポートをオープンする
    Private Sub PortOpen()
        'Me.Invoke(dlg, New Object() {dat}) 'Form1_Shownで表示できたので不要
        'Me.Invoke(dlg2, New Object() {dat})
        Try
            With SerialPort1
                .Close() 'ポートを閉じる

#If MNI_ENABLE Then
                .PortName = ComInfo(selectComNo).comno
#Else
                .PortName = ComboBox1.Text
#End If
                .BaudRate = ComboBox2.Text
                .DataBits = 8
                .Parity = Parity.None
                .StopBits = StopBits.One
                .Handshake = Handshake.None
                .RtsEnable = False
                .DtrEnable = False
                .Open() 'ポートをオープンする
                ''MsgBox(ComboBox1.Text & "をオープンできた。", MsgBoxStyle.OkOnly)
            End With

            'Timer1.Enabled = True
        Catch ex As Exception
            MsgBox(ComboBox1.Text & "をオープンできませんでした。", MsgBoxStyle.OkOnly)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        SerialWrite()
    End Sub

    Private Sub SerialWrite()

        If SerialPort1.IsOpen Then
            SerialPort1.Write(TextBox1.Text)
        End If

    End Sub

    Dim data_out_res As Integer = 0 '2024.10.26
    Dim buf_chk(32) As Byte  '2024.10.26
    Dim count_32 As Integer = 0 '2024.10.26

    Dim i As Integer = 0
    Dim i2 As Integer = 0
    Dim x As Single = 0 '■座標ｘ１初期値 ０
    Dim xx As Single = 0 '■座標ｙ２
    Dim x2 As Single = 0 '■座標ｘ１初期値 ０
    Dim xx2 As Single = 0 '■座標ｙ２

    Delegate Sub DisplayTextDelegate(ByVal dat As Short)
    Delegate Sub DisplayTextAllDelegate(ByRef datz() As Short, ByRef daty() As Short, ByRef datx() As Short)

    'Dim ii As Single = 0  '■繰り返し計算用
    Dim Zy As Single = 100 '■座標ｙ１初期値 １００（オフセット）
    Dim Zyy As Single '■座標ｘ２
    Dim Yy As Single = 100 '■座標ｙ１初期値 １００（オフセット）
    Dim Yyy As Single '■座標ｘ２
    Dim Xy As Single = 100 '■座標ｙ１初期値 １００（オフセット）
    Dim Xyy As Single '■座標ｘ２

    Dim Zy2 As Single = 100 '■座標ｙ１初期値 １００（オフセット）
    Dim Zyy2 As Single '■座標ｘ２
    Dim Yy2 As Single = 100 '■座標ｙ１初期値 １００（オフセット）
    Dim Yyy2 As Single '■座標ｘ２
    Dim Xy2 As Single = 100 '■座標ｙ１初期値 １００（オフセット）
    Dim Xyy2 As Single '■座標ｘ２

    Private Sub DisplayText_init(ByVal dat As Short)

        'テキストBOXに文字列を追加  
        'Me.TextBox1.Text &= dat & " "

        '描画スタート
        'Static Dim x As Single = 0 '■座標ｘ１初期値 ０
        Static Dim y As Single = 100 '■座標ｙ１初期値 １００（オフセット）
        Static Dim i As Single '■繰り返し計算用
        Static Dim yy As Single '■座標ｘ２
        Static Dim g As Graphics = PictureBox1.CreateGraphics '■PictureBox1に書く
        Dim blackPen As New Pen(Color.Black, 1)
        Dim RedPen As New Pen(Color.Red, 2)

        'blackPen.DashStyle = DashStyle.Dot
        
        ''g.DrawLine(Pens.Black, 0, 150, 400, 150)
        ''For i = 1 To 15
        ''    g.DrawLine(blackPen, 0, 150 + i * 10, 400, 150 + i * 10)
        ''Next
        ''For i = 1 To 15
        ''    g.DrawLine(blackPen, 0, 150 - i * 10, 400, 150 - i * 10)
        ''Next
        ''For i = 1 To 39
        ''For i = 0 To 39
        ''    g.DrawLine(blackPen, i * 10, 0, i * 10, 300)
        ''Next

        blackPen.Width = 1.5

        g.DrawLine(blackPen, 0, 150, 400, 150)

        'blackPen.DashStyle = Drawing2D.DashStyle.Dash
        blackPen.DashStyle = Drawing2D.DashStyle.Dot

        For i2 = 2 To 8 Step 2
            g.DrawLine(blackPen, i2 * 50, 0, i2 * 50, 400)
        Next

        For i2 = 1 To 2
            g.DrawLine(blackPen, 0, i2 * 50, 400, i2 * 50)
        Next


        'Dim num As Integer = Integer.Parse(strDisp)

        'If (i = 0) Then
        '    y = -(dat - 128) + 128 '■座標yyの計算（"-"で上下反転，*10で拡大，+100でオフセット）
        '    'g.DrawLine(Pens.Red, x, y, x, y) '■ライン描画 始点x,y ～ 終点xx,yy
        '    g.DrawLine(RedPen, x, y, x, y)
        '    i = i + 1
        'Else

        '    yy = -(dat - 128) + 128 '■座標yyの計算（"-"で上下反転，*10で拡大，+100でオフセット）
        '    'xx = xx + 10 '■座標xxの計算 （*10で拡大）
        '    xx = xx + 1 '■座標xxの計算 （*10で拡大）
        '    'g.DrawLine(Pens.Red, x, y, xx, yy) '■ライン描画 始点x,y ～ 終点xx,yy
        '    g.DrawLine(RedPen, x, y, xx, yy)
        '    x = xx '■終点xxを次の始点xとする
        '    y = yy '■終点yyを次の始点yとする
        'End If



    End Sub


    Private Sub DisplayText_init2(ByVal dat As Short)

        'テキストBOXに文字列を追加  
        'Me.TextBox1.Text &= dat & " "

        '描画スタート
        'Static Dim x As Single = 0 '■座標ｘ１初期値 ０
        Static Dim y As Single = 100 '■座標ｙ１初期値 １００（オフセット）
        Static Dim i2 As Single '■繰り返し計算用
        Static Dim yy As Single '■座標ｘ２
        'Static Dim xx As Single '■座標ｙ２
        'Static Dim xx As Single '■座標ｙ２
        Static Dim g As Graphics = PictureBox2.CreateGraphics '■PictureBox1に書く
        'Dim blackPen As New Pen(Color.Black, 1)
        'Dim blackPen As New Pen(Color.Black, 0.1)
        Dim blackPen As New Pen(Color.Black, 2)
        Dim RedPen As New Pen(Color.Red, 2)

        'blackPen.DashStyle = DashStyle.Dot

        ''blackPen.Width = 0.1
        ''g.DrawLine(Pens.Black, 0, 150, 400, 150)
        ''For i2 = 1 To 15
        ''    g.DrawLine(blackPen, 0, 150 + i2 * 10, 400, 150 + i2 * 10)
        ''Next
        ''For i2 = 1 To 15
        ''    g.DrawLine(blackPen, 0, 150 - i2 * 10, 400, 150 - i2 * 10)
        ''Next
        ''For i = 1 To 39
        ''    For i2 = 0 To 39
        ''        g.DrawLine(blackPen, i2 * 10, 0, i2 * 10, 300)
        ''    Next

        blackPen.Width = 1.5

        g.DrawLine(blackPen, 0, 150, 400, 150)

        'blackPen.DashStyle = Drawing2D.DashStyle.Dash
        blackPen.DashStyle = Drawing2D.DashStyle.Dot

        For i2 = 2 To 8 Step 2
            g.DrawLine(blackPen, i2 * 50, 0, i2 * 50, 400)
        Next

        For i2 = 1 To 2
            g.DrawLine(blackPen, 0, i2 * 50, 400, i2 * 50)
        Next



        'Dim num As Integer = Integer.Parse(strDisp)

        'If (i = 0) Then
        '    y = -(dat - 128) + 128 '■座標yyの計算（"-"で上下反転，*10で拡大，+100でオフセット）
        '    'g.DrawLine(Pens.Red, x, y, x, y) '■ライン描画 始点x,y ～ 終点xx,yy
        '    g.DrawLine(RedPen, x, y, x, y)
        '    i = i + 1
        'Else

        '    yy = -(dat - 128) + 128 '■座標yyの計算（"-"で上下反転，*10で拡大，+100でオフセット）
        '    'xx = xx + 10 '■座標xxの計算 （*10で拡大）
        '    xx = xx + 1 '■座標xxの計算 （*10で拡大）
        '    'g.DrawLine(Pens.Red, x, y, xx, yy) '■ライン描画 始点x,y ～ 終点xx,yy
        '    g.DrawLine(RedPen, x, y, xx, yy)
        '    x = xx '■終点xxを次の始点xとする
        '    y = yy '■終点yyを次の始点yとする
        'End If



    End Sub


    Dim dlg As New DisplayTextDelegate(AddressOf DisplayText_init)
    Dim dlg2 As New DisplayTextDelegate(AddressOf DisplayText_init2)

    Dim dat As Short = 20

    Dim Dispz1() As Short = {150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150}
    Dim Dispy1() As Short = {150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150}
    Dim Dispx1() As Short = {150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150}

    'Dim Dispz1() As Short = {150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150}
    'Dim Dispy1() As Short = {150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150}
    'Dim Dispx1() As Short = {150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150}
    Dim Dispi1 As Integer = 0
    Dim k1 As Integer = 0  ' local variable for statement 


    Private Sub DisplayTextAll(ByRef datz() As Short, ByRef daty() As Short, ByRef datx() As Short)

        Static Dim g As Graphics = PictureBox1.CreateGraphics '■PictureBox1に書く

        Dim h As Graphics = PictureBox1.CreateGraphics()


        Dim blackPen As New Pen(Color.Black, 1)
        Dim RedPen As New Pen(Color.Red, 2)
        Dim GreenPen As New Pen(Color.Green, 2)
        Dim BluePen As New Pen(Color.Blue, 2)

        Dim Dispz1A(40) As Short  '2024.11.19
        Dim Dispy1A(40) As Short  '2024.11.19
        Dim Dispx1A(40) As Short  '2024.11.19




        Dim i As Integer = 0  ' local variable for statement 

        g.DrawLine(Pens.Black, 0, 150, 400, 150)

        RedPen.Width = 1
        GreenPen.Width = 1
        BluePen.Width = 1

        For i = 39 To 0 Step -1
            'For i = 38 To 0 Step -1
            Dispz1(i + 1) = Dispz1(i)
            Dispy1(i + 1) = Dispy1(i)
            Dispx1(i + 1) = Dispx1(i)
        Next
        Dispz1(0) = 150 - datz(0)  '■座標yyの計算（"-"で上下反転，*10で拡大，+100でオフセット）
        Dispy1(0) = 150 - daty(0)  '■座標yyの計算（"-"で上下反転，*10で拡大，+100でオフセット）
        Dispx1(0) = 150 - datx(0)  '■座標yyの計算（"-"で上下反転，*10で拡大，+100でオフセット）

        If expand_flg = 1 Then
           For i = 40 To 0 Step -1
                'For i = 38 To 0 Step -1
                Dispz1A(i) = 150 - (150 - Dispz1(i)) * 2
                Dispy1A(i) = 150 - (150 - Dispy1(i)) * 2
                Dispx1A(i) = 150 - (150 - Dispx1(i)) * 2
            Next
        Else
            For i = 40 To 0 Step -1
                'For i = 38 To 0 Step -1
                Dispz1A(i) = Dispz1(i)
                Dispy1A(i) = Dispy1(i)
                Dispx1A(i) = Dispx1(i)
            Next
        End If

        'End If
        h.Clear(Color.White)

        Me.Invoke(dlg, New Object() {dat})
        'h.Dispose()

        'For i = 0 To 38
        For i = 0 To 39
            'g.DrawLine(BluePen, (i * 10), Dispx(i), ((i + 1) * 10), Dispx(i + 1))   ' dispay diff
            g.DrawLine(BluePen, (i * 10), Dispx1A(i), ((i + 1) * 10), Dispx1A(i + 1))   ' dispay diff

        Next



    End Sub

    'Dim Dispz() As Short = {150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150}
    'Dim Dispy() As Short = {150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150}
    'Dim Dispx() As Short = {150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150}

    Dim Dispz() As Short = {150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150}
    Dim Dispy() As Short = {150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150}
    Dim Dispx() As Short = {150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150}
    Dim Dispi As Integer = 0

    Dim k2 As Integer = 0  ' local variable for statement 
    Private Sub DisplayTextAll2(ByRef datz() As Short, ByRef daty() As Short, ByRef datx() As Short)

        Static Dim g As Graphics = PictureBox2.CreateGraphics '■PictureBox1に書く

        Dim h As Graphics = PictureBox2.CreateGraphics()


        Dim blackPen As New Pen(Color.Black, 1)
        Dim RedPen As New Pen(Color.Red, 2)
        Dim GreenPen As New Pen(Color.Green, 2)
        Dim BluePen As New Pen(Color.Blue, 2)

        Dim Dispz1A(40) As Short  '2024.11.19
        Dim Dispy1A(40) As Short  '2024.11.19
        Dim Dispx1A(40) As Short  '2024.11.19


        Dim i2 As Integer = 0  ' local variable for statement 


        g.DrawLine(Pens.Black, 0, 150, 400, 150)

        RedPen.Width = 1
        GreenPen.Width = 1
        BluePen.Width = 1




        'For i2 = 38 To 0 Step -1
        For i2 = 39 To 0 Step -1
            Dispz(i2 + 1) = Dispz(i2)
            Dispy(i2 + 1) = Dispy(i2)
            Dispx(i2 + 1) = Dispx(i2)
        Next
        Dispz(0) = 150 - datz(0)  '■座標yyの計算（"-"で上下反転，*10で拡大，+100でオフセット）
        Dispy(0) = 150 - daty(0)  '■座標yyの計算（"-"で上下反転，*10で拡大，+100でオフセット）
        Dispx(0) = 150 - datx(0)  '■座標yyの計算（"-"で上下反転，*10で拡大，+100でオフセット）


        If expand_flg = 1 Then
            For i = 40 To 0 Step -1
                'For i = 38 To 0 Step -1
                Dispz1A(i) = 150 - (150 - Dispz(i)) * 2
                Dispy1A(i) = 150 - (150 - Dispy(i)) * 2
                Dispx1A(i) = 150 - (150 - Dispx(i)) * 2
            Next
        Else
            For i = 40 To 0 Step -1
                'For i = 38 To 0 Step -1
                Dispz1A(i) = Dispz(i)
                Dispy1A(i) = Dispy(i)
                Dispx1A(i) = Dispx(i)
            Next
        End If





        'Me.Invalidate()
        'Me.Update()

        h.Clear(Color.White)

        Me.Invoke(dlg2, New Object() {dat})
        'h.Dispose()

        'For i2 = 0 To 38
        For i2 = 0 To 39
            'g.DrawLine(RedPen, (i2 * 10), Dispz(i2), ((i2 + 1) * 10), Dispz(i2 + 1))  ' display useful
            'g.DrawLine(GreenPen, (i2 * 10), Dispy(i2), ((i2 + 1) * 10), Dispy(i2 + 1)) ' display average
            g.DrawLine(RedPen, (i2 * 10), Dispz1A(i2), ((i2 + 1) * 10), Dispz1A(i2 + 1))  ' display useful
            g.DrawLine(GreenPen, (i2 * 10), Dispy1A(i2), ((i2 + 1) * 10), Dispy1A(i2 + 1)) ' display average

        Next

        'BluePen.Dispose()
        'g.Dispose()

        'Me.Invalidate()
        'Me.Invoke(dlg2, New Object() {dat})

        'End If

        'If (x2 = 400) Then
        '    PictureBox2.Refresh()
        '   x2 = 0
        '   xx2 = 0
        'Dim dlg As New DisplayTextDelegate(AddressOf DisplayText_init)

        'Dim dat As Short

        'dat = 20
        'MsgBox(dat)
        ''Invoke(New Delegate_RcvDataToTextBox(AddressOf Me.RcvDataToTextBox), args)
        '  Me.Invoke(dlg2, New Object() {dat})
        ' 'Else
        'End If

    End Sub




    Dim datz(0) As Short
    Dim daty(0) As Short
    Dim datx(0) As Short

    Dim disp_recv_str_log As String  ' 2021.2.11   
    Dim fileflg As Int16 = 0
    Dim com_flg As Int16 = 0

    Dim asciiString_useful As String
    Dim asciiString_average As String
    Dim asciiString_diff As String

    Dim file_num As Integer = 0
    Dim expand_flg As Integer = 0

    Private Sub SerialPort1_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived


        Dim dlgzyx As New DisplayTextAllDelegate(AddressOf DisplayTextAll)
        Dim dlgzyx2 As New DisplayTextAllDelegate(AddressOf DisplayTextAll2)

        Dim bmp As New Bitmap(PictureBox4.Width, PictureBox4.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)
        Dim brush As New SolidBrush(Color.Aqua)
        Dim brushB As New SolidBrush(Color.Blue)

        While SerialPort1.BytesToRead > 0
            Dim receivedByte As Byte = SerialPort1.ReadByte()
            buf_chk(31) = buf_chk(30)
            buf_chk(30) = buf_chk(29)
            buf_chk(29) = buf_chk(28)
            buf_chk(28) = buf_chk(27)
            buf_chk(27) = buf_chk(26)
            buf_chk(26) = buf_chk(25)
            buf_chk(25) = buf_chk(24)
            buf_chk(24) = buf_chk(23)
            buf_chk(23) = buf_chk(22)
            buf_chk(22) = buf_chk(21)
            buf_chk(21) = buf_chk(20)
            buf_chk(20) = buf_chk(19)
            buf_chk(19) = buf_chk(18)
            buf_chk(18) = buf_chk(17)
            buf_chk(17) = buf_chk(16)
            buf_chk(16) = buf_chk(15)
            buf_chk(15) = buf_chk(14)
            buf_chk(14) = buf_chk(13)
            buf_chk(13) = buf_chk(12)
            buf_chk(12) = buf_chk(11)
            buf_chk(11) = buf_chk(10)
            buf_chk(10) = buf_chk(9)
            buf_chk(9) = buf_chk(8)
            buf_chk(8) = buf_chk(7)
            buf_chk(7) = buf_chk(6)
            buf_chk(6) = buf_chk(5)
            buf_chk(5) = buf_chk(4)
            buf_chk(4) = buf_chk(3)
            buf_chk(3) = buf_chk(2)
            buf_chk(2) = buf_chk(1)
            buf_chk(1) = buf_chk(0)
            buf_chk(0) = receivedByte

            If (buf_chk(31) = &H3A) Then
                data_out_res = 1
            Else
                data_out_res = 0
            End If

            If (data_out_res = 1) Then
                count_32 = 0
            Else
                count_32 = count_32 + 1
            End If

            If (count_32 = 0) Then

                'TextBox2.Text = buf_chk(30).ToString() & buf_chk(29).ToString() & buf_chk(28).ToString() & buf_chk(27).ToString() &
                'buf_chk(26).ToString() & buf_chk(25).ToString() & buf_chk(24).ToString()
                ''TextBox2.Text = buf_chk(30) & buf_chk(29) & buf_chk(28) & buf_chk(27) & buf_chk(26) & buf_chk(25) & buf_chk(24)
                Dim byteArray_useful() As Byte = {buf_chk(30), buf_chk(29), buf_chk(28), buf_chk(27), buf_chk(26), buf_chk(25), buf_chk(24), buf_chk(23)}
                asciiString_useful = System.Text.Encoding.ASCII.GetString(byteArray_useful)
                TextBox3.Text = asciiString_useful 'GUのテキストボックス用
                Dim number_useful As Integer = Convert.ToInt32(asciiString_useful)
                TextBox6.Text = number_useful.ToString() 'グラフ表示の演算用変数

                Dim byteArray_average() As Byte = {buf_chk(21), buf_chk(20), buf_chk(19), buf_chk(18), buf_chk(17), buf_chk(16), buf_chk(15), buf_chk(14)}
                asciiString_average = System.Text.Encoding.ASCII.GetString(byteArray_average)
                TextBox4.Text = asciiString_average 'GUのテキストボックス用
                Dim number_average As Integer = Convert.ToInt32(asciiString_average)
                TextBox7.Text = number_average.ToString() 'グラフ表示の演算用変数

                Dim byteArray_diff() As Byte = {buf_chk(12), buf_chk(11), buf_chk(10), buf_chk(9), buf_chk(8), buf_chk(7), buf_chk(6), buf_chk(5)}
                asciiString_diff = System.Text.Encoding.ASCII.GetString(byteArray_diff)
                TextBox5.Text = asciiString_diff 'GUのテキストボックス用
                Dim number_diff As Integer = Convert.ToInt32(asciiString_diff)
                TextBox8.Text = number_diff.ToString() 'グラフ表示の演算用変数

                If buf_chk(3) = 48 Then
                    g.FillEllipse(brush, 0, 0, bmp.Width, bmp.Height)
                    PictureBox4.Image = bmp
                Else
                    g.FillEllipse(brushB, 0, 0, bmp.Width, bmp.Height)
                    PictureBox4.Image = bmp
                End If

                'TextBox2.Text &= asciiString & Environment.NewLine
                'Dim number As Integer = Convert.ToInt32(asciiString)
                'TextBox2.Text = number.ToString()
                'Console.WriteLine(number)

                'datz(0) = number_useful / 40
                'daty(0) = number_average / 40
                'datx(0) = number_diff / 40

                'If expand_flg = 0 Then  '通常時で拡大しない時
                '    datz(0) = number_useful / 10000
                '    daty(0) = number_average / 10000
                '    datx(0) = number_diff / 10000
                'Else  '2倍に拡大するとき
                '    datz(0) = number_useful / 5000
                '    daty(0) = number_average / 5000
                '    datx(0) = number_diff / 5000
                'End If
                '戻した。
                datz(0) = number_useful / 10000
                daty(0) = number_average / 10000
                datx(0) = number_diff / 10000

                If run_flg = 1 Then
                    'count_32 = 0になるのは、0.1秒づつなので、0.1秒毎に、グラフを表示する。
                    Me.Invoke(dlgzyx, New Object() {datz, daty, datx}) 'Me.Invoke(dlgzyx, New Object() {datz, daty, datt2})  ' ファイル出力しながら、画面表示するとジャギーになるので、排他制御する                          
                    Me.Invoke(dlgzyx2, New Object() {datz, daty, datx})
                End If

                'ファイル出力処理
                If (fileflg = 1) Then

                    Dim disp_recv_str_log3 As String

                    disp_recv_str_log3 = ""

                    disp_recv_str_log3 = disp_recv_str_log3 & "," & asciiString_useful & "," & asciiString_average & "," & asciiString_diff '
                    disp_recv_str_log3 = disp_recv_str_log3 & vbCrLf
                    Writer.Write(disp_recv_str_log3)

                End If


            End If



        End While
    End Sub

    'Private Sub SerialPort1_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
    'Dim sz As Integer = SerialPort1.BytesToRead
    'Dim i As Integer
    'Dim rxdat(sz - 1) As Byte
    '
    '    SerialPort1.Read(rxdat, 0, sz)
    '    Console.WriteLine("受信したバイト数: " & sz.ToString())


    'For i = 0 To sz - 1
    '        RxRingSet(rxdat(i))
    '        'RxRingSet(48)
    '        Console.WriteLine("受信したバイト: " & rxdat(i).ToString())
    'Next

    'End Sub

    'RxRingのwpを更新する
    Public Sub RxRingSet(ByVal dat)

        RxRing.dat(RxRing.wp) = dat

        RxRing.wp += 1
        If RxRing.wp = RxRing.dat.Length Then
            RxRing.wp = 0
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim sz As Integer
        Dim i As Integer

        ComPortChk() '存在するポートを検索する' COMポートリストを定期更新する

        sz = RxRing.wp - RxRing.rp

        If sz < 0 Then
            sz += RxRing.dat.Length
        End If

        If sz > 0 Then
            For i = 0 To sz - 1
                TextBox2.Text &= Chr(RxRing.dat(RxRing.rp))
                RingRpAdd()
            Next

        End If

    End Sub

    'RxRingのrpを更新する
    Private Sub RingRpAdd()

        RxRing.rp += 1
        If RxRing.rp >= RxRing.dat.Length Then
            RxRing.rp = 0
        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TextBox2.Clear()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        selectComNo = ComboBox1.SelectedIndex
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Dim FileName As String
    Dim Writer As IO.StreamWriter
    Dim Encode As System.Text.Encoding
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        ' RGB値を指定してボタンの背景色を設定（ここでは赤色）
        Dim red As Integer = 155
        Dim green As Integer = 230
        Dim blue As Integer = 253

        If fileflg = 0 Then
            fileflg = 1

            Dim dt1 As DateTime = DateTime.Now

            'disp_recv_str_log = dt1.ToString("yyyy-MM-dd HH時mm分ss秒")
            disp_recv_str_log = dt1.ToString("yyyy.MM.dd HH.mm.ss")

            Encode = System.Text.Encoding.GetEncoding("Shift-JIS")
            'FileName = "G:\NTE\Data_Logger\data\out" & file_num & ".txt"
            'FileName = "D:\NTE\Data_Logger\data\out" & file_num & ".csv"
            'FileName = "out" & file_num & ".csv"
            'FileName = "out" & disp_recv_str_log & ".csv"
            FileName = disp_recv_str_log & ".csv"
            Writer = New IO.StreamWriter(FileName, False, Encode)




            'disp_recv_str_log = dt1.ToString("yyyy/MM/dd HH:mm:ss")

            disp_recv_str_log = disp_recv_str_log & "  ログ開始" & vbCrLf
            Writer.Write(disp_recv_str_log)

            disp_recv_str_log = ""
            disp_recv_str_log = disp_recv_str_log & " " & "," & "     useful" & "," & "   average" & "," & "          diff " & vbCrLf
            Writer.Write(disp_recv_str_log)

        Else
            fileflg = 0

            Writer.Close()
        End If

        If fileflg = 1 Then
            Button5.Text = "ファイル書き込み中"
            Button5.BackColor = Color.FromArgb(red, green, blue)
        Else
            Button5.Text = "ファイルclose中"
            Button5.BackColor = Color.White
        End If

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub



    'Dim bmp As New Bitmap(PictureBox4.Width, PictureBox4.Height)
    'Dim g As Graphics = Graphics.FromImage(bmp)

    'Dim brush As New SolidBrush(Color.White)
    'Dim brushB As New SolidBrush(Color.Blue)


Public Class LEDControl
    Inherits UserControl

    Private _isOn As Boolean
    Public Property IsOn() As Boolean
        Get
            Return _isOn
        End Get
        Set(ByVal value As Boolean)
            _isOn = value
            Me.Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g As Graphics = e.Graphics
        Dim brush As Brush = If(_isOn, Brushes.Red, Brushes.Gray)
        g.FillEllipse(brush, 0, 0, Me.Width, Me.Height)
    End Sub
End Class

Public Class LEDLamp2
    Inherits UserControl

    Private _isOn As Boolean
    Public Property IsOn() As Boolean
        Get
            Return _isOn
        End Get
        Set(ByVal value As Boolean)
            _isOn = value
            Me.Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g As Graphics = e.Graphics
        Dim brush As Brush = If(_isOn, Brushes.Yellow, Brushes.Gray)
        g.FillEllipse(brush, 0, 0, Me.Width, Me.Height)
        g.DrawEllipse(Pens.Black, 0, 0, Me.Width - 1, Me.Height - 1)

        ' 光沢を表現するための白いハイライト
        Dim highlightBrush As New SolidBrush(Color.FromArgb(128, Color.White))
        Dim highlightRect As New Rectangle(0, 0, Me.Width \ 2, Me.Height \ 2)
        g.FillEllipse(highlightBrush, highlightRect)
    End Sub
End Class

    Public Class LEDLamp
        Inherits UserControl

        Private _isOn As Boolean
        Public Property IsOn() As Boolean
            Get
                Return _isOn
            End Get
            Set(ByVal value As Boolean)
                _isOn = value
                Me.Invalidate()
            End Set
        End Property

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            MyBase.OnPaint(e)
            Dim g As Graphics = e.Graphics
            Dim brush As Brush = If(_isOn, Brushes.Yellow, Brushes.Gray)
            g.FillEllipse(brush, 0, 0, Me.Width, Me.Height)
            g.DrawEllipse(Pens.Black, 0, 0, Me.Width - 1, Me.Height - 1)
        End Sub
    End Class

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboInit() 'Comboboxを初期化する
        RxRingInit() 'RxRingの初期化を行う
        com_flg = 0
        fileflg = 0
        Timer1.Enabled = True '定期的なポートチェックは、timerが必要。

        PictureBox4.Width = 50
        PictureBox4.Height = 50

        Dim bmp As New Bitmap(PictureBox4.Width, PictureBox4.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)

        Dim brush As New SolidBrush(Color.Aqua)
        Dim brushB As New SolidBrush(Color.Blue)
        g.FillEllipse(brush, 0, 0, bmp.Width, bmp.Height)
        PictureBox4.Image = bmp

        'Dim led As New LEDControl()
        'Dim led As New LEDLamp()
        Dim led As New LEDLamp2()

        led.Size = New Size(50, 50)
        led.Location = New Point(10, 10)
Me.Controls.Add(led)

' LEDを点灯させる
led.IsOn = True

    End Sub



    Public Class CustomGroupBox
        Inherits GroupBox

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)

            ' カスタム枠線の色とスタイルを指定
            Dim borderColor As Color = Color.Black
            Dim borderThickness As Integer = 1

            ' ペンを作成
            Dim pen As New Pen(borderColor, borderThickness)

            ' 枠線を描画
            e.Graphics.DrawRectangle(pen, 0, 0, Me.Width - 1, Me.Height - 1)
            'e.Graphics.DrawRectangle(pen, 700, 50, 100, 100)
        End Sub
    End Class

    'Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown
    Private Async Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown ' 非同期処理をしたら、初期画面でdlgのpictureが表示できた。
        ' フォームが表示された直後に実行される処理
        'DisplayMessage()
        Await Task.Delay(500)
        Me.Invoke(dlg, New Object() {dat})
        Me.Invoke(dlg2, New Object() {dat})
        'Me.PictureBox1.Refresh()
        'Me.PictureBox2.Refresh()



    End Sub

    Public Sub New()
        ' この呼び出しは、Windows フォーム デザイナーで必要です。 
        InitializeComponent()
        ' ダブルバッファリングを有効にする 
        Me.DoubleBuffered = True
    End Sub

    Dim run_flg As Integer = 0


    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            run_flg = 1 'Label1.Text = "チェックされています"
        Else
            run_flg = 0 'Label1.Text = "チェックされていません"
        End If
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs)

    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            expand_flg = 1 'Label1.Text = "チェックされています"
            Label6.Text = "  750000"
            Label7.Text = -250000
            Label8.Text = "  750000"
            Label9.Text = -250000
        Else
            expand_flg = 0 'Label1.Text = "チェックされていません"
            Label6.Text = 1500000
            Label7.Text = -500000
            Label8.Text = 1500000
            Label9.Text = -500000
        End If
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click

    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click

    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click

    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs)

    End Sub
End Class