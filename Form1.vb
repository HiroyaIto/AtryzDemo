'****************************************
'         SockT0001 テスト用画面
'            2017-7-23 v3.00
'****************************************

Imports System.Reflection

Public Class Form1
    Dim SendRepeatFlag As Boolean
    Dim SendDataCount As Integer
    Dim SendDataSize As Integer
    Dim RecvRepeatFlag As Boolean
    Dim RecvDataCount As Integer
    Dim RecvDataSize As Long

    '***** 文字列を数値に変換(エラー時は0を返す) *****
    Private Function CIntErrZero(ByVal data_str As String) As Integer
        Try
            CIntErrZero = Integer.Parse(data_str)
        Catch ex As Exception
            CIntErrZero = 0
        End Try
    End Function

    Dim i As Integer = 0
    Dim x As Single = 0 '■座標ｘ１初期値 ０
    Dim xx As Single = 0 '■座標ｙ２

    '***** フォームの生成 *****
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TextBox1001.MaxLength = 1000000
        'TextBox1001.MaxLength = 10000
        TextBox1001.MaxLength = 100000
        'k = 0
    End Sub

    '***** ログ表示 *****
    Private Sub AddLog(ByVal log_str As String)
        If (TextBox1001.TextLength >= TextBox1001.MaxLength) Then
            'Dim log_full_str As String = "ログが一杯です。" & Environment.NewLine
            'If (TextBox1001.Text.Substring(TextBox1001.TextLength - log_full_str.Length) <> log_full_str) Then
            '    TextBox1001.AppendText(log_full_str)
            'End If
            'Exit Sub
            TextBox1001.Text = ""
        End If
        'TextBox1001.AppendText("[" & String.Format("{0:yyyy/MM/dd hh:mm:ss:ffff}", DateTime.Now) & "]" & log_str & Environment.NewLine)
        TextBox1001.AppendText(log_str & Environment.NewLine)
        TextBox1001.SelectionStart = TextBox1001.TextLength
        TextBox1001.ScrollToCaret()
    End Sub
    Private Sub AddLogWithSender(ByVal sender As Object, ByVal log_str As String)
        AddLog(log_str)
        'AddLog(DirectCast(sender, Control).Name & ":" & log_str)
    End Sub

    '***** ログクリアボタン *****
    Private Sub Button1001_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1001.Click
        TextBox1001.Text = ""
    End Sub

    '***** Listenボタン *****
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        SockT0001Ctrl2.BindIP = TextBox3.Text
        SockT0001Ctrl2.LocalPort = CIntErrZero(TextBox4.Text)
        SockT0001Ctrl2.Listen()
    End Sub

    '***** Connectボタン *****
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        SockT0001Ctrl1.RemoteIP = TextBox1.Text
        SockT0001Ctrl1.RemotePort = CIntErrZero(TextBox2.Text)
        SockT0001Ctrl1.BindIP = TextBox3.Text
        SockT0001Ctrl1.Connect()
    End Sub

    '***** Close(Listen)ボタン *****
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        SockT0001Ctrl2.Close()
    End Sub

    '***** Closeボタン *****
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        SockT0001Ctrl1.Close()
    End Sub

    '***** Sendボタン *****
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim send_data() As Byte
        Dim send_str As String

        send_str = TextBox5.Text
        send_data = System.Text.Encoding.GetEncoding("Shift_JIS").GetBytes(send_str)
        SockT0001Ctrl1.SendData(send_data)
    End Sub

    '***** Open(UDP)ボタン *****
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        SockT0001Ctrl1.RemoteIP = TextBox6.Text
        SockT0001Ctrl1.RemotePort = CIntErrZero(TextBox7.Text)
        SockT0001Ctrl1.BindIP = TextBox8.Text
        SockT0001Ctrl1.Open()
    End Sub

    '***** Bind(UDP)ボタン *****
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        SockT0001Ctrl1.RemoteIP = TextBox6.Text
        SockT0001Ctrl1.BindIP = TextBox8.Text
        SockT0001Ctrl1.LocalPort = CIntErrZero(TextBox9.Text)
        SockT0001Ctrl1.Bind()
    End Sub

    '***** Close(UDP)ボタン *****
    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        SockT0001Ctrl1.Close()
    End Sub

    '***** SendTo(UDP)ボタン *****
    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Dim send_data() As Byte
        Dim send_str As String

        send_str = TextBox10.Text
        send_data = System.Text.Encoding.GetEncoding("Shift_JIS").GetBytes(send_str)
        SockT0001Ctrl1.SendDataTo(send_data)
    End Sub

    '***** 連続送信ボタン *****
    Private Sub Button10_Click(sender As System.Object, e As System.EventArgs) Handles Button10.Click
        Dim tmp_int As Integer

        If (SendRepeatFlag = False) Then
            SendRepeatFlag = True
            Button10.BackColor = Color.Yellow
            SendDataCount = &H30
            SendDataSize = CIntErrZero(TextBox11.Text)
            If (SendDataSize < 0) Then SendDataSize = 0
            AutoSendData()
            tmp_int = CIntErrZero(TextBox12.Text)
            If (tmp_int < 0) Then tmp_int = 0
            'Timer1.Interval = tmp_int
            'Timer1.Enabled = True
        Else
            SendRepeatFlag = False
            Button10.BackColor = SystemColors.Control
            'Timer1.Enabled = False
        End If
    End Sub

    Private Sub AutoSendData()
        Dim i As Integer
        Dim send_data() As Byte

        ReDim send_data(SendDataSize - 1)
        For i = 0 To SendDataSize - 1
            send_data(i) = SendDataCount
            SendDataCount = SendDataCount + 1
            If (SendDataCount > 255) Then SendDataCount = 0
        Next
        If (RadioButton1.Checked = True) Then
            SockT0001Ctrl1.SendData(send_data)
        Else
            SockT0001Ctrl1.SendDataTo(send_data)
        End If
    End Sub

    '***** 連続受信ボタン *****
    Private Sub Button11_Click(sender As System.Object, e As System.EventArgs) Handles Button11.Click
        If (RecvRepeatFlag = False) Then
            RecvRepeatFlag = True
            RecvDataCount = &H30
            RecvDataSize = 0
            Button11.BackColor = Color.Yellow
        Else
            RecvRepeatFlag = False
            Button11.BackColor = SystemColors.Control
        End If
    End Sub

    '***** 送受信バッファ設定ボタン *****
    Private Sub Button12_Click(sender As System.Object, e As System.EventArgs) Handles Button12.Click
        SockT0001Ctrl1.SendBuffSize = CIntErrZero(TextBox13.Text)
    End Sub
    Private Sub Button13_Click(sender As System.Object, e As System.EventArgs) Handles Button13.Click
        SockT0001Ctrl1.RecvBuffSize = CIntErrZero(TextBox14.Text)
    End Sub

    '***** IPv6モード設定ボタン *****
    Private Sub Button14_Click(sender As System.Object, e As System.EventArgs) Handles Button14.Click
        SockT0001Ctrl1.IPv6Mode = CIntErrZero(TextBox15.Text)
        SockT0001Ctrl2.IPv6Mode = CIntErrZero(TextBox15.Text)
    End Sub

    '***** ソケット通信コントロールのイベント処理 ここから *****

    Private Sub SockT0001Ctrl1_Error(ByVal sender As Object, ByVal err_msg As String) Handles SockT0001Ctrl1.Error
        AddLogWithSender(sender, err_msg)
    End Sub

    Private Sub SockT0001Ctrl2_Error(ByVal sender As Object, ByVal err_msg As String) Handles SockT0001Ctrl2.Error
        AddLogWithSender(sender, err_msg)
    End Sub

    Private Sub SockT0001Ctrl1_StateChanged(ByVal sender As Object) Handles SockT0001Ctrl1.StateChanged
        Select Case SockT0001Ctrl1.State
            Case 0
                Label1.Text = "切断"
                Label1.BackColor = SystemColors.Control
            Case 1
                Label1.Text = "接続待ち"
                Label1.BackColor = Color.Yellow
            Case 2
                Label1.Text = "通信中"
                Label1.BackColor = Color.Blue
        End Select
    End Sub

    Private Sub SockT0001Ctrl2_StateChanged(ByVal sender As Object) Handles SockT0001Ctrl2.StateChanged
        Select Case SockT0001Ctrl2.State
            Case 0
                Label2.Text = "切断"
                Label2.BackColor = SystemColors.Control
            Case 1
                Label2.Text = "接続待ち"
                Label2.BackColor = Color.Yellow
            Case 2
                Label2.Text = "通信中"
                Label2.BackColor = Color.Blue
        End Select
    End Sub

    Private Sub SockT0001Ctrl1_Opened(ByVal sender As Object) Handles SockT0001Ctrl1.Opened
        AddLogWithSender(sender, ":通信開始(UDP)。")
    End Sub

    Private Sub SockT0001Ctrl1_Bound(ByVal sender As Object) Handles SockT0001Ctrl1.Bound
        AddLogWithSender(sender, ":受信開始(UDP)。")
    End Sub

    Private Sub SockT0001Ctrl1_Connecting(ByVal sender As Object) Handles SockT0001Ctrl1.Connecting
        AddLogWithSender(sender, ":接続開始。")
    End Sub

    Private Sub SockT0001Ctrl1_Connected(ByVal sender As Object) Handles SockT0001Ctrl1.Connected
        AddLogWithSender(sender, ":接続しました。" & SockT0001Ctrl1.State)
    End Sub

    Private Sub SockT0001Ctrl1_Closed(ByVal sender As Object, ByVal disc_state As Integer) Handles SockT0001Ctrl1.Closed
        If (disc_state = 0) Then
            AddLogWithSender(sender, ":切断しました。" & SockT0001Ctrl1.State)
        Else
            AddLogWithSender(sender, ":切断されました。" & SockT0001Ctrl1.State)
        End If
    End Sub




    Delegate Sub DisplayTextDelegate(ByVal dat As Short)
    'Delegate Sub DisplayTextAllDelegate(ByVal datz As Short, ByVal daty As Short, ByVal datx As Short)
    Delegate Sub DisplayTextAllDelegate(ByRef datz() As Short, ByRef daty() As Short, ByRef datx() As Short)

    'Dim ii As Single = 0  '■繰り返し計算用
    Dim Zy As Single = 100 '■座標ｙ１初期値 １００（オフセット）
    Dim Zyy As Single '■座標ｘ２
    Dim Yy As Single = 100 '■座標ｙ１初期値 １００（オフセット）
    Dim Yyy As Single '■座標ｘ２
    Dim Xy As Single = 100 '■座標ｙ１初期値 １００（オフセット）
    Dim Xyy As Single '■座標ｘ２

    'Private Sub DisplayTextAll(ByVal datz As Short, ByVal daty As Short, ByVal datx As Short)
    Private Sub DisplayTextAll(ByRef datz() As Short, ByRef daty() As Short, ByRef datx() As Short)

        Static Dim g As Graphics = PictureBox1.CreateGraphics '■PictureBox1に書く
        Dim blackPen As New Pen(Color.Black, 1)
        Dim RedPen As New Pen(Color.Red, 2)
        Dim GreenPen As New Pen(Color.Green, 2)
        Dim BluePen As New Pen(Color.Blue, 2)

        Dim i As Integer = 0  ' local variable for statement 

        'blackPen.DashStyle = DashStyle.Dot

        g.DrawLine(Pens.Black, 0, 150, 400, 150)


        For i = 0 To 9
            'For i = 0 To 49
            'For i = 0 To 19
            Zyy = 150 - datz(i)  '■座標yyの計算（"-"で上下反転，*10で拡大，+100でオフセット）
            Yyy = 150 - daty(i)  '■座標yyの計算（"-"で上下反転，*10で拡大，+100でオフセット）
            Xyy = 150 - datx(i)  '■座標yyの計算（"-"で上下反転，*10で拡大，+100でオフセット）
            xx = xx + 1 '■座標xxの計算 （*10で拡大）
            g.DrawLine(RedPen, x, Zy, xx, Zyy)
            g.DrawLine(GreenPen, x, Yy, xx, Yyy)
            g.DrawLine(BluePen, x, Xy, xx, Xyy)
            x = xx '■終点xxを次の始点xとする
            Zy = Zyy '■終点yyを次の始点yとする
            Yy = Yyy '■終点yyを次の始点yとする
            Xy = Xyy '■終点yyを次の始点yとする
        Next

        'End If

        If (x = 400) Then
            Refresh()
            x = 0
            xx = 0
            Dim dlg As New DisplayTextDelegate(AddressOf DisplayText_init)

            Dim dat As Short

            dat = 20
            'MsgBox(dat)
            ''Invoke(New Delegate_RcvDataToTextBox(AddressOf Me.RcvDataToTextBox), args)
            Me.Invoke(dlg, New Object() {dat})
            'Else
        End If

    End Sub

    Private Sub DisplayTextRed(ByVal dat As Short)

        'テキストBOXに文字列を追加  
        'Me.TextBox1.Text &= dat & " "

        '描画スタート
        'Static Dim x As Single = 0 '■座標ｘ１初期値 ０
        Static Dim y As Single = 100 '■座標ｙ１初期値 １００（オフセット）
        Static Dim i As Single '■繰り返し計算用
        Static Dim yy As Single '■座標ｘ２
        'Static Dim xx As Single '■座標ｙ２
        'Static Dim xx As Single '■座標ｙ２
        Static Dim g As Graphics = PictureBox1.CreateGraphics '■PictureBox1に書く
        Dim blackPen As New Pen(Color.Black, 1)
        Dim RedPen As New Pen(Color.Red, 2)

        'blackPen.DashStyle = DashStyle.Dot

        g.DrawLine(Pens.Black, 0, 128, 400, 128)
        'For i = 1 To 12
        '    g.DrawLine(blackPen, 0, 128 + i * 10, 400, 128 + i * 10)
        'Next
        'For i = 1 To 12
        '    g.DrawLine(blackPen, 0, 128 - i * 10, 400, 128 - i * 10)
        'Next
        'For i = 1 To 39
        '    g.DrawLine(blackPen, i * 10, 0, i * 10, 256)
        'Next

        'blackPen.Width = 2
        'For i = 1 To 2
        '    g.DrawLine(blackPen, 0, 128 - i * 50, 400, 128 - i * 50)
        '    g.DrawLine(blackPen, 0, 128 + i * 50, 400, 128 + i * 50)
        'Next
        'g.DrawLine(blackPen, 0, 128, 400, 128)
        'For i = 1 To 8
        '    g.DrawLine(blackPen, i * 50, 0, i * 50, 256)
        'Next


        'Dim num As Integer = Integer.Parse(strDisp)

        If (i = 0) Then
            y = -(dat - 128) + 128 '■座標yyの計算（"-"で上下反転，*10で拡大，+100でオフセット）
            'g.DrawLine(Pens.Red, x, y, x, y) '■ライン描画 始点x,y ～ 終点xx,yy
            g.DrawLine(RedPen, x, y, x, y)
            i = i + 1
        Else

            yy = -(dat - 128) + 128 '■座標yyの計算（"-"で上下反転，*10で拡大，+100でオフセット）
            'xx = xx + 10 '■座標xxの計算 （*10で拡大）
            xx = xx + 1 '■座標xxの計算 （*10で拡大）
            'g.DrawLine(Pens.Red, x, y, xx, yy) '■ライン描画 始点x,y ～ 終点xx,yy
            g.DrawLine(RedPen, x, y, xx, yy)
            x = xx '■終点xxを次の始点xとする
            y = yy '■終点yyを次の始点yとする
        End If



    End Sub

    Private Sub DisplayTextGreen(ByVal dat As Short)

        'テキストBOXに文字列を追加  
        'Me.TextBox1.Text &= dat & " "

        '描画スタート
        'Static Dim x As Single = 0 '■座標ｘ１初期値 ０
        Static Dim y As Single = 100 '■座標ｙ１初期値 １００（オフセット）
        Static Dim i As Single '■繰り返し計算用
        Static Dim yy As Single '■座標ｘ２
        'Static Dim xx As Single '■座標ｙ２
        'Static Dim xx As Single '■座標ｙ２
        Static Dim g As Graphics = PictureBox1.CreateGraphics '■PictureBox1に書く
        Dim blackPen As New Pen(Color.Black, 1)
        Dim GreenPen As New Pen(Color.Green, 2)

        'blackPen.DashStyle = DashStyle.Dot

        g.DrawLine(Pens.Black, 0, 128, 400, 128)
        'For i = 1 To 12
        '    g.DrawLine(blackPen, 0, 128 + i * 10, 400, 128 + i * 10)
        'Next
        'For i = 1 To 12
        '    g.DrawLine(blackPen, 0, 128 - i * 10, 400, 128 - i * 10)
        'Next
        'For i = 1 To 39a
        '    g.DrawLine(blackPen, i * 10, 0, i * 10, 256)
        'Next

        'blackPen.Width = 2
        'For i = 1 To 2
        '    g.DrawLine(blackPen, 0, 128 - i * 50, 400, 128 - i * 50)
        '    g.DrawLine(blackPen, 0, 128 + i * 50, 400, 128 + i * 50)
        'Next
        'g.DrawLine(blackPen, 0, 128, 400, 128)
        'For i = 1 To 8
        '    g.DrawLine(blackPen, i * 50, 0, i * 50, 256)
        'Next


        'Dim num As Integer = Integer.Parse(strDisp)

        If (i = 0) Then
            y = -(dat - 128) + 128 '■座標yyの計算（"-"で上下反転，*10で拡大，+100でオフセット）
            'g.DrawLine(Pens.Red, x, y, x, y) '■ライン描画 始点x,y ～ 終点xx,yy
            g.DrawLine(GreenPen, x, y, x, y)
            i = i + 1
        Else

            yy = -(dat - 128) + 128 '■座標yyの計算（"-"で上下反転，*10で拡大，+100でオフセット）
            'xx = xx + 10 '■座標xxの計算 （*10で拡大）
            xx = xx + 1 '■座標xxの計算 （*10で拡大）
            'g.DrawLine(Pens.Red, x, y, xx, yy) '■ライン描画 始点x,y ～ 終点xx,yy
            g.DrawLine(GreenPen, x, y, xx, yy)
            x = xx '■終点xxを次の始点xとする
            y = yy '■終点yyを次の始点yとする
        End If



    End Sub

    Private Sub DisplayTextBlue(ByVal dat As Short)

        'テキストBOXに文字列を追加  
        'Me.TextBox1.Text &= dat & " "

        '描画スタート
        'Static Dim x As Single = 0 '■座標ｘ１初期値 ０
        Static Dim y As Single = 100 '■座標ｙ１初期値 １００（オフセット）
        Static Dim i As Single '■繰り返し計算用
        Static Dim yy As Single '■座標ｘ２
        'Static Dim xx As Single '■座標ｙ２
        'Static Dim xx As Single '■座標ｙ２
        Static Dim g As Graphics = PictureBox1.CreateGraphics '■PictureBox1に書く
        Dim blackPen As New Pen(Color.Black, 1)
        Dim BluePen As New Pen(Color.Blue, 2)

        'blackPen.DashStyle = DashStyle.Dot

        g.DrawLine(Pens.Black, 0, 128, 400, 128)
        'For i = 1 To 12
        '    g.DrawLine(blackPen, 0, 128 + i * 10, 400, 128 + i * 10)
        'Next
        'For i = 1 To 12
        '    g.DrawLine(blackPen, 0, 128 - i * 10, 400, 128 - i * 10)
        'Next
        'For i = 1 To 39
        '    g.DrawLine(blackPen, i * 10, 0, i * 10, 256)
        'Next

        'blackPen.Width = 2
        'For i = 1 To 2
        '    g.DrawLine(blackPen, 0, 128 - i * 50, 400, 128 - i * 50)
        '    g.DrawLine(blackPen, 0, 128 + i * 50, 400, 128 + i * 50)
        'Next
        'g.DrawLine(blackPen, 0, 128, 400, 128)
        'For i = 1 To 8
        '    g.DrawLine(blackPen, i * 50, 0, i * 50, 256)
        'Next


        'Dim num As Integer = Integer.Parse(strDisp)

        If (i = 0) Then
            y = -(dat - 128) + 128 '■座標yyの計算（"-"で上下反転，*10で拡大，+100でオフセット）
            'g.DrawLine(Pens.Red, x, y, x, y) '■ライン描画 始点x,y ～ 終点xx,yy
            g.DrawLine(BluePen, x, y, x, y)
            i = i + 1
        Else

            yy = -(dat - 128) + 128 '■座標yyの計算（"-"で上下反転，*10で拡大，+100でオフセット）
            'xx = xx + 10 '■座標xxの計算 （*10で拡大）
            xx = xx + 1 '■座標xxの計算 （*10で拡大）
            'g.DrawLine(Pens.Red, x, y, xx, yy) '■ライン描画 始点x,y ～ 終点xx,yy
            g.DrawLine(BluePen, x, y, xx, yy)
            x = xx '■終点xxを次の始点xとする
            y = yy '■終点yyを次の始点yとする
        End If



    End Sub

    Private Sub DisplayText_init(ByVal dat As Short)

        'テキストBOXに文字列を追加  
        'Me.TextBox1.Text &= dat & " "

        '描画スタート
        'Static Dim x As Single = 0 '■座標ｘ１初期値 ０
        Static Dim y As Single = 100 '■座標ｙ１初期値 １００（オフセット）
        Static Dim i As Single '■繰り返し計算用
        Static Dim yy As Single '■座標ｘ２
        'Static Dim xx As Single '■座標ｙ２
        'Static Dim xx As Single '■座標ｙ２
        Static Dim g As Graphics = PictureBox1.CreateGraphics '■PictureBox1に書く
        Dim blackPen As New Pen(Color.Black, 1)
        Dim RedPen As New Pen(Color.Red, 2)

        'blackPen.DashStyle = DashStyle.Dot

        g.DrawLine(Pens.Black, 0, 150, 400, 150)
        For i = 1 To 15
            g.DrawLine(blackPen, 0, 150 + i * 10, 400, 150 + i * 10)
        Next
        For i = 1 To 15
            g.DrawLine(blackPen, 0, 150 - i * 10, 400, 150 - i * 10)
        Next
        For i = 1 To 39
            g.DrawLine(blackPen, i * 10, 0, i * 10, 300)
        Next

        blackPen.Width = 2
        For i = 1 To 2
            g.DrawLine(blackPen, 0, 150 - i * 50, 400, 150 - i * 50)
            g.DrawLine(blackPen, 0, 150 + i * 50, 400, 150 + i * 50)
        Next
        g.DrawLine(blackPen, 0, 150, 400, 150)
        For i = 1 To 8
            g.DrawLine(blackPen, i * 50, 0, i * 50, 400)
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

    Dim remain As Integer = 0
    Dim remainBuffer(6) As Byte 'remain buffer 2020.05.05
    Dim fout_count As Integer
    'Dim newBuffer(160024) As Byte   '2020.05.05
    'Dim data100ms(160000) As Byte
    Dim newBuffer(32016) As Byte   '2020.05.05
    Dim data100ms(16000) As Byte
    Dim data100ms_a() As Byte
    Dim recv_Pointer As Integer = 0   '2020.06.28
    Dim buf_chk(7) As Byte  '2020.06.28

    Dim header_det As Integer = 0 '2020.06.28
    Dim data_out_res As Integer = 0 '2020.06.28
    Dim count_8 As Integer = 0 '2020.06.28
    Dim count_2000 As Integer = 0 '2020.06.28

    'Dim k As Integer = 0
    'Dim data100ms_a() As Byte
    Dim k As Integer = 0
    Dim end_flg As Integer = 0
    Dim file_num As Integer = 0
    Dim recv_str As String
    Dim disp_recv_str As String
    Dim disp_recv_str_log As String

    Dim disp_recv_str_log2 As String  ' 2021.2.11

    Dim fileflg As Int16 = 0

    Dim datz(49) As Short
    Dim daty(49) As Short
    Dim datx(49) As Short
    Dim datt(49) As UShort
    Dim datt2(49) As UShort

    Dim datz2(9, 7) As Short ' for printout

    Dim datz3(1999, 7) As Short ' for full file out data 2021.2.11
    'Dim datz4(1999, 3) As Integer ' for full file out data 2021.2.14 サンプルNO、x加速度、y加速度、z加速度

    Dim datz4(2000, 3) As Integer ' for full file out data 2021.2.14 サンプルNO、x加速度、y加速度、z加速度

    Dim therm As Single  '2020.3.6
    Dim vol As Single

    Dim volin As Single  '2023.6.10
    Dim volbuf(20) As Single

    Dim thermTemp As Single
    Dim softAP As Single

    Private Sub SockT0001Ctrl1_DataArrival(ByVal sender As Object, ByVal recv_data() As Byte, ByVal recv_bytes As Integer) Handles SockT0001Ctrl1.DataArrival
        Dim i As Integer
        'Dim recv_str As String
        'Dim disp_recv_str As String
        'Dim disp_recv_str_log As String
        Dim k As Integer = 0
        Dim j As Integer = 0

        Dim dlgzyx As New DisplayTextAllDelegate(AddressOf DisplayTextAll)
        'Dim dlgz As New DisplayTextRedDelegate(AddressOf DisplayTextRed)

        ' comment out 2021.02.08
        'Dim datz(49) As Short
        'Dim daty(49) As Short
        'Dim datx(49) As Short
        'Dim datz2(9, 7) As Short ' for printout


        '***** 連続受信ONのとき *****
        If (RecvRepeatFlag = True) Then
            For i = 0 To recv_bytes - 1
                If (recv_data(i) = RecvDataCount) Then
                    RecvDataCount = RecvDataCount + 1
                    If (RecvDataCount > 255) Then RecvDataCount = 0
                    If (RecvDataSize < Long.MaxValue) Then
                        RecvDataSize = RecvDataSize + 1
                    End If
                Else
                    AddLogWithSender(sender, ":受信予定データ=" & RecvDataCount & ":受信データ=" & recv_data(i) & ":ここまでの正常受信バイト数=" & RecvDataSize)
                    RecvDataCount = recv_data(i) + 1
                    If (RecvDataCount > 255) Then RecvDataCount = 0
                    RecvDataSize = 0
                End If
            Next
            Exit Sub
        End If

        '***** バイナリ受信表示のチェック *****
        Dim ii As Integer
        For ii = 0 To 16000
            data100ms(ii) = 0
        Next

        'Dim iii As Integer
        'For iii = 0 To 9
        'volbuf(iii) = 0
        'Next

        'k = 0
        If (CheckBox1.Checked = True) Then
            If (recv_bytes > 0) Then

                'ファイル出力する場合
                If (fileflg = 1) Then
                    AddLogWithSender(sender, "wifiデータ受信" & recv_bytes)  '2021.2.13
                End If

                For fout_count = 0 To recv_bytes - 1
                    newBuffer(fout_count) = recv_data(fout_count)
                Next

                For fout_count = 0 To recv_bytes - 1
                    buf_chk(7) = buf_chk(6)
                    buf_chk(6) = buf_chk(5)
                    buf_chk(5) = buf_chk(4)
                    buf_chk(4) = buf_chk(3)
                    buf_chk(3) = buf_chk(2)
                    buf_chk(2) = buf_chk(1)
                    buf_chk(1) = buf_chk(0)
                    buf_chk(0) = recv_data(fout_count)


                    If (buf_chk(7) = &HFF) And (buf_chk(6) = &HFF) And (buf_chk(5) = &HFF) And (buf_chk(4) = &HFF) And (buf_chk(3) = &HFF) And (buf_chk(2) = &HFF) And (buf_chk(1) = &HFF) And (buf_chk(0) = &HFF) Then
                        header_det = 1
                        data_out_res = 1
                    ElseIf (buf_chk(7) = &HFE) And (buf_chk(6) = &HFE) And (buf_chk(5) = &HFE) And (buf_chk(4) = &HFE) And (buf_chk(3) = &HFE) And (buf_chk(2) = &HFE) And (buf_chk(1) = &HFE) And (buf_chk(0) = &HFE) Then
                        'ElseIf (buf_chk(7) = &H30) And (buf_chk(6) = &H30) And (buf_chk(5) = &H30) And (buf_chk(4) = &H30) And (buf_chk(3) = &H30) And (buf_chk(2) = &H30) And (buf_chk(1) = &H30) And (buf_chk(0) = &H30) Then
                        MsgBox("All zero is detected")
                    Else
                        data_out_res = 0
                    End If

                    If (data_out_res = 1) Then
                        count_8 = 0
                        count_2000 = 0
                    Else
                        count_8 = count_8 + 1
                    End If

                    If (count_8 = 8) Then


                        ' plot out
                        'For j = 0 To 49
                        For j = 0 To 9
                            'For j = 0 To 1
                            'If (count_2000 = (40 * j)) Then
                            If (count_2000 = (100 * j)) Then

                                'If (count_2000 = 2) Then

                                'z axis'                    'z axis
                                If (buf_chk(0) < 128) Then
                                    'datz(j) = ((buf_chk(1) + 256 * buf_chk(0)) * 0.00096) * 50
                                    datz(j) = ((buf_chk(1) + 256 * buf_chk(0)) * 0.001) * 50
                                Else
                                    'datz(j) = ((-1) * (65536 - (buf_chk(1) + 256 * buf_chk(0))) * 0.00096) * 50
                                    datz(j) = ((-1) * (65536 - (buf_chk(1) + 256 * buf_chk(0))) * 0.001) * 50
                                End If

                                datz2(j, 0) = buf_chk(0)
                                datz2(j, 1) = buf_chk(1)
                                datz2(j, 2) = buf_chk(2)
                                datz2(j, 3) = buf_chk(3)
                                datz2(j, 4) = buf_chk(4)
                                datz2(j, 5) = buf_chk(5)
                                datz2(j, 6) = buf_chk(6)
                                datz2(j, 7) = buf_chk(7)


                                'y axis'                                                                          'y axis
                                If (buf_chk(2) < 128) Then                                                        'If (buf_chk(2) < 128) Then
                                    daty(j) = ((buf_chk(3) + 256 * buf_chk(2)) / 1000) * 150                  '    daty(j) = ((buf_chk(3) + 256 * buf_chk(2)) * 0.00096) * 50
                                    'daty(j) = 0'((buf_chk(3) + 256 * buf_chk(2)) / 32768) * 150                  '    daty(j) = ((buf_chk(3) + 256 * buf_chk(2)) * 0.00096) * 50
                                Else                                                                              'Else
                                    daty(j) = (((-1) * (65536 - (buf_chk(3) + 256 * buf_chk(2)))) / 1000) * 150 '    daty(j) = ((-1) * (65536 - (buf_chk(3) + 256 * buf_chk(2))) * 0.00096) * 50
                                    'daty(j) = 0'(((-1) * (65536 - (buf_chk(3) + 256 * buf_chk(2)))) / 32768) * 150 '    daty(j) = ((-1) * (65536 - (buf_chk(3) + 256 * buf_chk(2))) * 0.00096) * 50

                                End If                                                                            'End If

                                'x axis                                                                           'x axis
                                If (buf_chk(4) < 128) Then                                                        'If (buf_chk(4) < 128) Then
                                    datx(j) = ((buf_chk(5) + 256 * buf_chk(4)) / 1000) * 150                 '    datx(j) = ((buf_chk(5) + 256 * buf_chk(4)) * 0.00096) * 50
                                Else                                                                              'Else
                                    datx(j) = (((-1) * (65536 - (buf_chk(5) + 256 * buf_chk(4)))) / 1000) * 150 '    datx(j) = ((-1) * (65536 - (buf_chk(5) + 256 * buf_chk(4))) * 0.00096) * 50
                                End If                                                                            'End If

                                'time data 
                                datt2(j) = ((buf_chk(7) + 256 * buf_chk(6)) * 0.00005) * 150
                                'datt(j) = buf_chk(7) + 256 * buf_chk(6)


                            End If

                        Next


                        ' prepare output file data 2021.2.11
                        ' start
                        'For mm = 0 To 7
                        'datz3(count_2000, mm) = buf_chk(mm)
                        'Next
                        ' end
                        ' prepare output file data 2021.2.14
                        'z axis
                        'If (buf_chk(0) < 128) Then
                        'datz4(count_2000, 3) = ((buf_chk(1) + 256 * buf_chk(0)) * 0.00096) * 50
                        'Else
                        'datz4(count_2000, 3) = CInt(((-1) * (65536 - (buf_chk(1) + 256 * buf_chk(0))) * 0.00096) * 50)
                        'End If

                        'y axis
                        'If (buf_chk(2) < 128) Then
                        'datz4(count_2000, 2) = ((buf_chk(3) + 256 * buf_chk(2)) * 0.00096) * 50
                        'Else
                        'datz4(count_2000, 2) = CInt(((-1) * (65536 - (buf_chk(3) + 256 * buf_chk(2))) * 0.00096) * 50)
                        'End If

                        'x axis
                        'If (buf_chk(4) < 128) Then
                        'datz4(count_2000, 1) = ((buf_chk(5) + 256 * buf_chk(4)) * 0.00096) * 50
                        'Else
                        'datz4(count_2000, 1) = CInt(((-1) * (65536 - (buf_chk(5) + 256 * buf_chk(4))) * 0.00096) * 50)
                        'End If

                        'datz4(count_2000, 0) = buf_chk(7) + 256 * buf_chk(6)




                        If (count_2000 = 2000) Then
                            'If (count_2000 = 900) Then
                            'Me.Invoke(dlgzyx, New Object() {datz, daty, datt})
                            'Me.Invoke(dlgzyx, New Object() {datz, daty, datt2})  ' stop displaying to escape jaggy.

                            datz4(0, 3) = buf_chk(1) + 256 * buf_chk(0)
                            datz4(0, 2) = buf_chk(3) + 256 * buf_chk(2)  'thermistor
                            datz4(0, 1) = buf_chk(5) + 256 * buf_chk(4)  'battery
                            datz4(0, 0) = buf_chk(7) + 256 * buf_chk(6)

                            therm = datz4(0, 2) * 100 / (4096 - datz4(0, 2))
                            'vol = datz4(0, 1) * 2.5 / 4096 * (200 + 180) / 200
                            '''vol = datz4(0, 1) * 2.5 / 4096 * (200 + 220) / 200   '抵抗を180kΩ⇒220kΩに変更の為
                            volin = datz4(0, 1) * 2.5 / 4096 * (200 + 220) / 200   '抵抗を180kΩ⇒220kΩに変更の為

                            'volの１秒平均
                            volbuf(19) = volbuf(18)
                            volbuf(18) = volbuf(17)
                            volbuf(17) = volbuf(16)
                            volbuf(16) = volbuf(15)
                            volbuf(15) = volbuf(14)
                            volbuf(14) = volbuf(13)
                            volbuf(13) = volbuf(12)
                            volbuf(12) = volbuf(11)
                            volbuf(11) = volbuf(10)
                            volbuf(10) = volbuf(9)
                            volbuf(9) = volbuf(8)
                            volbuf(8) = volbuf(7)
                            volbuf(7) = volbuf(6)
                            volbuf(6) = volbuf(5)
                            volbuf(5) = volbuf(4)
                            volbuf(4) = volbuf(3)
                            volbuf(3) = volbuf(2)
                            volbuf(2) = volbuf(1)
                            volbuf(1) = volbuf(0)
                            volbuf(0) = volin
                            vol = (volbuf(19) + volbuf(18) + volbuf(17) + volbuf(16) + volbuf(15) + volbuf(14) + volbuf(13) + volbuf(12) + volbuf(11) + volbuf(10) + volbuf(9) + volbuf(8) + volbuf(7) + volbuf(6) + volbuf(5) + volbuf(4) + volbuf(3) + volbuf(2) + volbuf(1) + volbuf(0)) / 20

                            softAP = datz4(0, 3)

                            If (therm > 326.6) Then
                                thermTemp = -1
                            ElseIf (therm > 310.4) Then
                                thermTemp = 0
                            ElseIf (therm > 295.1) Then
                                thermTemp = 1
                            ElseIf (therm > 280.6) Then
                                thermTemp = 2
                            ElseIf (therm > 266.9) Then
                                thermTemp = 3
                            ElseIf (therm > 254.0) Then
                                thermTemp = 4
                            ElseIf (therm > 241.8) Then
                                thermTemp = 5
                            ElseIf (therm > 230.2) Then
                                thermTemp = 6
                            ElseIf (therm > 219.2) Then
                                thermTemp = 7
                            ElseIf (therm > 208.9) Then
                                thermTemp = 8
                            ElseIf (therm > 199.0) Then
                                thermTemp = 9
                            ElseIf (therm > 189.7) Then
                                thermTemp = 10
                            ElseIf (therm > 180.9) Then
                                thermTemp = 11
                            ElseIf (therm > 172.6) Then
                                thermTemp = 12
                            ElseIf (therm > 164.7) Then
                                thermTemp = 13
                            ElseIf (therm > 157.1) Then
                                thermTemp = 14
                            ElseIf (therm > 150.0) Then
                                thermTemp = 15
                            ElseIf (therm > 143.2) Then
                                thermTemp = 16
                            ElseIf (therm > 136.8) Then
                                thermTemp = 17
                            ElseIf (therm > 130.7) Then
                                thermTemp = 18
                            ElseIf (therm > 124.9) Then
                                thermTemp = 19
                            ElseIf (therm > 119.4) Then
                                thermTemp = 20
                            ElseIf (therm > 114.2) Then
                                thermTemp = 21
                            ElseIf (therm > 109.2) Then
                                thermTemp = 22
                            ElseIf (therm > 104.5) Then
                                thermTemp = 23
                            ElseIf (therm > 100.0) Then
                                thermTemp = 24
                            ElseIf (therm > 95.72) Then
                                thermTemp = 25
                            ElseIf (therm > 91.65) Then
                                thermTemp = 26
                            ElseIf (therm > 87.77) Then
                                thermTemp = 27
                            ElseIf (therm > 84.08) Then
                                thermTemp = 28
                            ElseIf (therm > 80.55) Then
                                thermTemp = 29
                            ElseIf (therm > 77.21) Then
                                thermTemp = 30
                            ElseIf (therm > 74.02) Then
                                thermTemp = 31
                            ElseIf (therm > 70.98) Then
                                thermTemp = 32
                            ElseIf (therm > 68.08) Then
                                thermTemp = 33
                            ElseIf (therm > 65.31) Then
                                thermTemp = 34
                            ElseIf (therm > 62.67) Then
                                thermTemp = 35
                            ElseIf (therm > 60.15) Then
                                thermTemp = 36
                            ElseIf (therm > 57.74) Then
                                thermTemp = 37
                            ElseIf (therm > 55.45) Then
                                thermTemp = 38
                            ElseIf (therm > 53.25) Then
                                thermTemp = 39
                            ElseIf (therm > 51.16) Then
                                thermTemp = 40
                            ElseIf (therm > 49.16) Then
                                thermTemp = 41
                            ElseIf (therm > 47.25) Then
                                thermTemp = 42
                            ElseIf (therm > 45.43) Then
                                thermTemp = 43
                            ElseIf (therm > 43.68) Then
                                thermTemp = 44
                            ElseIf (therm > 42.01) Then
                                thermTemp = 45
                            ElseIf (therm > 40.41) Then
                                thermTemp = 46
                            ElseIf (therm > 38.88) Then
                                thermTemp = 47
                            ElseIf (therm > 37.42) Then
                                thermTemp = 48
                            ElseIf (therm > 36.02) Then
                                thermTemp = 49
                            ElseIf (therm > 34.68) Then
                                thermTemp = 50
                            ElseIf (therm > 33.4) Then
                                thermTemp = 51
                            ElseIf (therm > 32.17) Then
                                thermTemp = 52
                            ElseIf (therm > 30.99) Then
                                thermTemp = 53
                            ElseIf (therm > 29.87) Then
                                thermTemp = 54
                            ElseIf (therm > 28.78) Then
                                thermTemp = 55
                            ElseIf (therm > 27.75) Then
                                thermTemp = 56
                            ElseIf (therm > 26.75) Then
                                thermTemp = 57
                            ElseIf (therm > 25.8) Then
                                thermTemp = 58
                            ElseIf (therm > 24.88) Then
                                thermTemp = 59
                            ElseIf (therm > 24.01) Then
                                thermTemp = 60
                            ElseIf (therm > 23.17) Then
                                thermTemp = 61
                            ElseIf (therm > 22.36) Then
                                thermTemp = 62
                            ElseIf (therm > 21.58) Then
                                thermTemp = 63
                            ElseIf (therm > 20.84) Then
                                thermTemp = 64
                            ElseIf (therm > 20.12) Then
                                thermTemp = 65
                            ElseIf (therm > 19.44) Then
                                thermTemp = 66
                            ElseIf (therm > 18.78) Then
                                thermTemp = 67
                            ElseIf (therm > 18.14) Then
                                thermTemp = 68
                            ElseIf (therm > 17.53) Then
                                thermTemp = 69
                            ElseIf (therm > 16.95) Then
                                thermTemp = 70
                            ElseIf (therm > 16.38) Then
                                thermTemp = 71
                            ElseIf (therm > 15.84) Then
                                thermTemp = 72
                            ElseIf (therm > 15.32) Then
                                thermTemp = 73
                            ElseIf (therm > 14.82) Then
                                thermTemp = 74
                            ElseIf (therm > 14.33) Then
                                thermTemp = 75
                            ElseIf (therm > 13.87) Then
                                thermTemp = 76
                            ElseIf (therm > 13.42) Then
                                thermTemp = 77
                            ElseIf (therm > 12.99) Then
                                thermTemp = 78
                            ElseIf (therm > 12.57) Then
                                thermTemp = 79
                            ElseIf (therm > 12.18) Then
                                thermTemp = 80
                            ElseIf (therm > 11.79) Then
                                thermTemp = 81
                            ElseIf (therm > 11.42) Then
                                thermTemp = 82
                            ElseIf (therm > 11.06) Then
                                thermTemp = 83
                            ElseIf (therm > 10.72) Then
                                thermTemp = 84
                            ElseIf (therm > 10.39) Then
                                thermTemp = 85
                            End If
                            'TextBox16.Text = vol.ToString("#0.000")   'TextBox16.Text = vol.ToString("#0.000") '20230503 復活
                            'TextBox17.Text = therm.ToString("#0.000") 'TextBox17.Text = therm.ToString("#0.000")
                        End If
                        If (count_2000 = 900) Then
                            TextBox16.Text = vol.ToString
                            TextBox17.Text = therm.ToString
                            TextBox18.Text = thermTemp.ToString
                            TextBox19.Text = softAP.ToString
                            'ファイル出力する場合
                            If (fileflg = 1) Then

                                Dim abcd As String
                                abcd = vol.ToString
                                TextBox16.Text = abcd
                                'TextBox16.Text = vol.ToString
                                TextBox17.Text = therm.ToString
                                TextBox18.Text = thermTemp.ToString
                                TextBox19.Text = softAP.ToString
                                'For j = 0 To 49
                                Dim kkk As Integer
                                Dim disp_recv_str_log3 As String
                                'For j = 0 To 2000

                                disp_recv_str_log3 = ""
                                'For kkk = 7 To 0 Step -1
                                For kkk = 0 To 3
                                    'disp_recv_str_log3 = disp_recv_str_log3 & datz4(j, kkk).ToString & ","
                                    disp_recv_str_log3 = disp_recv_str_log3 & datz4(0, kkk).ToString & ","
                                Next

                                disp_recv_str_log3 = disp_recv_str_log3 & "," & vol.ToString & "," & therm.ToString '2023.5.3 thermo vol チェックの為
                                disp_recv_str_log3 = disp_recv_str_log3 & vbCrLf
                                Writer.Write(disp_recv_str_log3)

                                'Next
                                'kkk = kkk + 1  ' for debug
                                '画面出力する場合
                            Else
                                Dim abc As String
                                abc = vol.ToString
                                TextBox16.Text = abc
                                'TextBox16.Text = "abc" 'vol.ToString
                                Me.Invoke(dlgzyx, New Object() {datz, daty, datx}) 'Me.Invoke(dlgzyx, New Object() {datz, daty, datt2})  ' ファイル出力しながら、画面表示するとジャギーになるので、排他制御する                          


                            End If


                        End If


                        If count_2000 = 2000 Then
                            count_2000 = 0
                        Else
                            count_2000 = count_2000 + 1
                        End If

                        count_8 = 0

                    End If

                Next


            End If

        End If

    End Sub


    Private Sub cal_disp(ByVal p2 As Integer)
        'disp_recv_str = BitConverter.ToString(data100ms)
        'disp_recv_str_log = disp_recv_str.Substring(p1, p1 + 23)
        'AddLogWithSender(sender, ":受信完了:" & recv_bytes & ":" & disp_recv_str_log)  '先頭(0～5)を2回分表示。

        Dim dlgzyx As New DisplayTextAllDelegate(AddressOf DisplayTextAll)
        Dim datz As Short
        Dim daty As Short
        Dim datx As Short

        'z axis
        If (data100ms(p2 + 7) < 128) Then
            datz = ((data100ms(p2 + 6) + 256 * data100ms(p2 + 7)) * 0.00096) * 50
        Else
            datz = ((-1) * (65536 - (data100ms(p2 + 6) + 256 * data100ms(p2 + 7))) * 0.00096) * 50
        End If
        'datz = 128 + datz

        'y axis
        If (data100ms(p2 + 5) < 128) Then
            daty = ((data100ms(p2 + 4) + 256 * data100ms(p2 + 5)) * 0.00096) * 50
        Else
            daty = ((-1) * (65536 - (data100ms(p2 + 4) + 256 * data100ms(p2 + 5))) * 0.00096) * 50
        End If
        'daty = 128 + daty

        'x axis
        If (data100ms(p2 + 3) < 128) Then
            datx = ((data100ms(p2 + 2) + 256 * data100ms(p2 + 3)) * 0.00096) * 50
        Else
            datx = ((-1) * (65536 - (data100ms(p2 + 2) + 256 * data100ms(p2 + 3))) * 0.00096) * 50
        End If
        'datx = 128 + datx

        'Me.Invoke(dlgzyx, New Object() {datz}, New Object() {daty}, New Object() {datx})
        Me.Invoke(dlgzyx, New Object() {datz, daty, datx})




    End Sub


    Private Sub SockT0001Ctrl1_SendProgress(ByVal sender As Object, ByVal send_bytes As Integer) Handles SockT0001Ctrl1.SendProgress
        '***** 連続送信ONのとき *****
        If (SendRepeatFlag = True) Then
            Exit Sub
        End If

        AddLogWithSender(sender, ":送信中:" & send_bytes)
    End Sub

    Private Sub SockT0001Ctrl1_SendComplete(ByVal sender As Object, ByVal send_bytes As Integer) Handles SockT0001Ctrl1.SendComplete
        '***** 連続送信ONのとき *****
        If (SendRepeatFlag = True) Then
            Exit Sub
        End If

        AddLogWithSender(sender, ":送信完了:" & send_bytes)
    End Sub

    Private Sub SockT0001Ctrl2_Listening(ByVal sender As Object) Handles SockT0001Ctrl2.Listening
        AddLogWithSender(sender, ":待ち受け開始。")
    End Sub

    Private Sub SockT0001Ctrl2_ConnectionRequest(ByVal sender As Object, ByVal comm_socket As System.Net.Sockets.Socket) Handles SockT0001Ctrl2.ConnectionRequest
        AddLogWithSender(sender, ":接続要求受付。")
        AddLog("RemotIP=" & SockT0001Ctrl2.RemoteIP)
        AddLog("RemotPort=" & SockT0001Ctrl2.RemotePort)
        SockT0001Ctrl1.Accept(comm_socket)
    End Sub

    Private Sub SockT0001Ctrl2_Closed(ByVal sender As Object, ByVal disc_state As Integer) Handles SockT0001Ctrl2.Closed
        If (disc_state = 0) Then
            AddLogWithSender(sender, ":切断しました。")
        Else
            AddLogWithSender(sender, ":切断されました。")
        End If
    End Sub

    Private Sub SockT0001Ctrl1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SockT0001Ctrl1.Load

    End Sub

    Private Sub SockT0001Ctrl2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SockT0001Ctrl2.Load

    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Refresh()
        x = 0 : xx = 0
        i = 0

        Dim dlg As New DisplayTextDelegate(AddressOf DisplayText_init)

        Dim dat As Short

        dat = 20
        'MsgBox(dat)
        ''Invoke(New Delegate_RcvDataToTextBox(AddressOf Me.RcvDataToTextBox), args)
        Me.Invoke(dlg, New Object() {dat})
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label28_Click(sender As Object, e As EventArgs) Handles Label28.Click

    End Sub

    Private Sub TextBox1001_TextChanged(sender As Object, e As EventArgs) Handles TextBox1001.TextChanged

    End Sub

    Private Sub TextBox14_TextChanged(sender As Object, e As EventArgs) Handles TextBox14.TextChanged

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Dim FileName As String
    Dim Writer As IO.StreamWriter
    Dim Encode As System.Text.Encoding
    'Dim file_num As Integer = 0

    Private Sub Button16_Click_1(sender As Object, e As EventArgs) Handles Button16.Click
        If fileflg = 0 Then
            fileflg = 1

            Encode = System.Text.Encoding.GetEncoding("Shift-JIS")
            'FileName = "G:\NTE\Data_Logger\data\out" & file_num & ".txt"
            'FileName = "D:\NTE\Data_Logger\data\out" & file_num & ".csv"
             FileName = "out" & file_num & ".csv"
            Writer = New IO.StreamWriter(FileName, False, Encode)

            Dim dt1 As DateTime = DateTime.Now
            'dt1.ToString("yyyy/MM/dd HH:mm:ss")) 

            'disp_recv_str_log = dat(j).ToString
            disp_recv_str_log = dt1.ToString("yyyy/MM/dd HH:mm:ss")
            disp_recv_str_log = disp_recv_str_log & vbCrLf
            Writer.Write(disp_recv_str_log)

        Else
            fileflg = 0

            Writer.Close()
        End If

        If fileflg = 1 Then
            Button16.Text = "ファイル書き込み中"
        Else
            Button16.Text = "ファイルclose中"
        End If
    End Sub

    Private Sub Label31_Click(sender As Object, e As EventArgs) Handles Label31.Click

    End Sub
End Class
