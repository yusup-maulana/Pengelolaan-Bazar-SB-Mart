Imports System.Data.OleDb
Public Class inputbarang
    Sub comboPangkat()
        Try
            ComboBox2.Items.Clear()
            cmd = New OleDbCommand("select nama_pemilik from data_rak group by nama_pemilik", con)
            rd = cmd.ExecuteReader()
            While rd.Read()
                ComboBox2.Items.Add(rd("nama_pemilik"))
            End While
        Catch ex As Exception
            MessageBox.Show("Koneksi Gagal !!!, karena " & ex.Message)
        End Try
    End Sub
    Sub comboPangkat_raknomber()
        Try
            ComboBox1.Items.Clear()
            cmd = New OleDbCommand("select rak from data_rak where nama_pemilik='" & ComboBox2.Text & "'", con)
            rd = cmd.ExecuteReader()
            While rd.Read()
                ComboBox1.Items.Add(rd("rak"))
            End While
        Catch ex As Exception
            MessageBox.Show("Koneksi Gagal !!!, karena " & ex.Message)
        End Try
    End Sub
    Sub isikode()
        Dim strtemp As String = ""
        Dim strvalue As String = ""
        Dim sql As String
        Dim cmd As OleDb.OleDbCommand
        Dim baca As OleDb.OleDbDataReader
        sql = "select * from [proses] order by [kunci] desc"
        cmd = New OleDbCommand(sql, con)
        baca = cmd.ExecuteReader
        If baca.Read Then
            strtemp = Mid(baca.Item("kunci"), 4, 4)
        Else
            TextBox2.Text = "USR0001"

            Exit Sub
        End If
        strvalue = Val(strtemp) + 1
        TextBox2.Text = "USR" & Mid("0000", 1, 4 - strvalue.Length) & strvalue

    End Sub
    Sub isikodebarang()
        Dim strtemp As String = ""
        Dim strvalue As String = ""
        Dim sql As String
        Dim cmd As OleDb.OleDbCommand
        Dim baca As OleDb.OleDbDataReader
        sql = "select * from [barang] order by [kodebarang] desc"
        cmd = New OleDbCommand(sql, con)
        baca = cmd.ExecuteReader
        If baca.Read Then
            strtemp = Mid(baca.Item("kodebarang"), 4, 4)
        Else
            txtkode.Text = "NEW0001"

            Exit Sub
        End If
        strvalue = Val(strtemp) + 1
        txtkode.Text = "NEW" & Mid("0000", 1, 4 - strvalue.Length) & strvalue

    End Sub
    Private Sub inputbarang_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        comboPangkat()
        isikodebarang()
        isikode()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        isikode()
        If txtkode.Text = "" Then
            MsgBox("Semua box harus terisi")
        ElseIf txtnama.Text = "" Then
            MsgBox("Semua box harus terisi")
        ElseIf txtharga.Text = "" Then
            MsgBox("Semua box harus terisi")
        ElseIf ComboBox1.Text = "" Then
            MsgBox("Semua box harus terisi")
        ElseIf ComboBox2.Text = "" Then
            MsgBox("Semua box harus terisi")
        ElseIf TextBox3.Text = "" Then
        Else
            Try
                cmd = New OleDbCommand("insert into barang (kodebarang, deskripsi,exp, harga, rak) values ('" & txtkode.Text & "','" & txtnama.Text & "','" & "" & "','" & txtharga.Text & "','" & ComboBox1.Text & "')", con)
                Dim datenow As Date = Today
                cmd = New OleDbCommand("insert into proses (kunci,kodebarang, namabarang, harga, rak, tgl_update, tgl_exp, masa, stok) values ('" & TextBox2.Text & "','" & txtkode.Text & "','" & txtnama.Text & "','" & txtharga.Text & "','" & ComboBox2.Text & "','" & datenow & "','" & DateTimePicker1.Value & "','" & TextBox1.Text & "','" & TextBox3.Text & "')", con)
                cmd.ExecuteNonQuery()
                Label5.ForeColor = Color.Green
                Label5.Text = "Update succes "

            Catch ex As Exception
                Label5.ForeColor = Color.Red
                Label5.Text = "Update Failed " & ex.Message
            End Try


            txtharga.Clear()
            txtkode.Clear()
            txtnama.Clear()
            isikode()
            isikodebarang()
        End If
       
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As System.Object, e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        Dim nowdate As Date = Today
        TextBox1.Text = DateDiff(DateInterval.Day, nowdate, DateTimePicker1.Value.Date)
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        comboPangkat_raknomber()
    End Sub
End Class