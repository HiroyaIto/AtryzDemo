Imports System.IO.Ports

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboInit() 'Comboboxを初期化する
        'RxRingInit() 'RxRingの初期化を行う 
    End Sub

    'Comboboxを初期化する
    Private Sub ComboInit()
        ComPortChk() 'ComboBox1の初期化（ポートの検索）
        ComboBox2.Items.Add("9600")
        ComboBox2.Items.Add("19200")
        ComboBox2.Items.Add("115200")
        ComboBox2.Text = "115200"
    End Sub

    '存在するポートを検索する
    Private Sub ComPortChk()
        Dim comno() As String = SerialPort.GetPortNames
        Dim i As Integer

        ComboBox1.Items.Clear() 'アイテムをクリア

        If comno.Length = 0 Then
            ComboBox1.Text = "COMポートがみつかりません。"
        Else
            For i = 0 To comno.Length - 1
                ComboBox1.Items.Add(comno(i))
            Next
            ComboBox1.Text = comno(0)
        End If
    End Sub

    Private Sub SerialPort1_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        Dim sz As Integer = SerialPort1.BytesToRead
        Dim i As Integer
        Dim rxdat(sz - 1) As Byte

        SerialPort1.Read(rxdat, 0, sz)

        For i = 0 To sz - 1
            RxRingSet(rxdat(i))
        Next
    End Sub

    'RxRingのwpを更新する
    Public Sub RxRingSet(ByVal dat As Byte)
        RxRing.dat(RxRing.wp) = dat
        RxRing.wp += 1

        If RxRing.wp = RxRing.dat.Length Then
            RxRing.wp = 0
        End If
    End Sub

    Structure TYP_COMRING
        Dim rp As Integer
        Dim wp As Integer
        Dim dat() As Byte
    End Structure

    Dim RxRing As TYP_COMRING '受信したデータを管理

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ComPortChk() '存在するポートを検索する
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        PortOpen() 'ポートをオープンする
    End Sub

    'ポートをオープンする
    Private Sub PortOpen()
        Try
            With SerialPort1
                .Close() 'ポートを閉じる
                .PortName = ComboBox1.Text
                .BaudRate = ComboBox2.Text
                .DataBits = 8
                .Parity = Parity.None
                .StopBits = StopBits.One
                .Handshake = Handshake.None
                .RtsEnable = False
                .DtrEnable = False
                .Open() 'ポートをオープンする
            End With

            Timer1.Enabled = True
        Catch ex As Exception
            MsgBox(ComboBox1.Text & "をオープンできませんでした。", MsgBoxStyle.OkOnly)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If SerialPort1.IsOpen Then
            SerialPort1.Write(TextBox1.Text)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TextBox2.Clear()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim sz As Integer
        Dim i As Integer

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

End Class
