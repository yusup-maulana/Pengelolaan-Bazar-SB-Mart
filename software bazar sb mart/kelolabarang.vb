Imports System.Data.OleDb
Public Class kelolabarang
    Sub tampil()
        Dim table As DataTable
        ap = New OleDbDataAdapter("SELECT *   FROM proses order by tgl_update desc", con)
        dt = New DataSet
        dt.Clear()
        ap.Fill(dt, "proses")
        table = dt.Tables("proses")
        dgv.DataSource = table
        dgv.AutoGenerateColumns = False
    End Sub
    Sub dgv2()
        Try

            Dim i As Integer = DataGridView1.CurrentRow.Index
            txtcari.text = DataGridView1.Item(0, i).Value

            If txtcari.text = "" Then
                MsgBox("masih kosong", MsgBoxStyle.Exclamation)
            Else

                Dim wwwwwww2 As String
                wwwwwww2 = "select * from  barang where kodebarang = '" & txtcari.text & "'"
                cmd = New OleDbCommand(wwwwwww2, con)
                rd = cmd.ExecuteReader
                rd.Read()

                If rd.HasRows Then
                    txtkode.Text = rd.Item("kodebarang")
                    txtnama.Text = rd.Item("deskripsi")
                    txtharga.Text = rd.Item("harga")
                    Button2.Visible = True
                    rd.Close()
                Else
                    MsgBox("Data tersebut Telah Dipindahkan")
                    rd.Close()
                    Button2.Visible = False
                End If


            End If
        Catch ex As Exception

        End Try
        DataGridView1.Visible = False
    End Sub
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
    Private Sub BunifuTextbox1_OnTextChange(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcari.OnTextChange
        Label5.ForeColor = Color.Black
        Label5.Text = ""
        If txtcari.text = "" Then
            DataGridView1.Visible = False

        Else
            DataGridView1.Visible = True

            Dim table As DataTable
            ap = New OleDbDataAdapter("select * from barang where kodebarang like '%" & txtcari.text & "%' or deskripsi like '%" & txtcari.text & "%'", con)
            dt = New DataSet
            dt.Clear()
            ap.Fill(dt, "barang")
            table = dt.Tables("barang")
            DataGridView1.DataSource = table
            Try
            Catch ex As Exception
            End Try
        End If

    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    End Sub

    Private Sub kelolabarang_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tampil()
        koneksi()
        comboPangkat()
    End Sub

    Private Sub DataGridView1_CellClick1(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        dgv2()

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_CellMouseClick1(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        dgv2()
    End Sub

    Private Sub DataGridView1_LostFocus1(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.LostFocus
        dgv2()
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

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        isikode()
        Try
            Dim datenow As Date = Today
            cmd = New OleDbCommand("insert into proses (kunci,kodebarang, namabarang, harga, rak, tgl_update, tgl_exp, masa, stok) values ('" & TextBox2.Text & "','" & txtkode.Text & "','" & txtnama.Text & "','" & txtharga.Text & "','" & ComboBox2.Text & "','" & datenow & "','" & DateTimePicker1.Value & "','" & TextBox1.Text & "','" & TextBox3.Text & "')", con)
            cmd.ExecuteNonQuery()
            Label5.ForeColor = Color.Green
            Label5.Text = "Update succes " & txtcari.text

        Catch ex As Exception
            Label5.ForeColor = Color.Red
            Label5.Text = "Update Failed " & txtcari.text & " " & ex.Message
        End Try


        txtharga.Clear()
        txtkode.Clear()
        txtnama.Clear()
        isikode()
        Form1.Button5.PerformClick()
        tampil()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        Dim nowdate As Date = Today
        TextBox1.Text = DateDiff(DateInterval.Day, nowdate, DateTimePicker1.Value.Date)
    End Sub


End Class