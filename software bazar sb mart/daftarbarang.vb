Imports System.Data.OleDb
Public Class daftarbarang
    Sub tampil()
        Dim table As DataTable
        ap = New OleDbDataAdapter("SELECT *   FROM barang order by kodebarang desc", con)
        dt = New DataSet
        dt.Clear()
        ap.Fill(dt, "barang")
        table = dt.Tables("barang")
        dgv.DataSource = table
        dgv.AutoGenerateColumns = False
    End Sub
    Sub comboPangkat()
        Try
            ComboBox1.Items.Clear()
            cmd = New OleDbCommand("select * from data_rak", con)
            rd = cmd.ExecuteReader()
            While rd.Read()
                ComboBox1.Items.Add(rd("rak"))
            End While
        Catch ex As Exception
            MessageBox.Show("Koneksi Gagal !!!, karena " & ex.Message)
        End Try
    End Sub
    Private Sub daftarbarang_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        koneksi()
        tampil()
        comboPangkat()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        Dim i As Integer = dgv.CurrentRow.Index
        Try
            txtkode.Text = dgv.Item(0, i).Value
            txtnama.Text = dgv.Item(1, i).Value
            txtharga.Text = dgv.Item(2, i).Value
        Catch ex As Exception

        End Try
    End Sub

   
    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        cmd = New OleDbCommand("delete from barang where kodebarang = '" & txtkode.Text & "'", con)
        cmd.ExecuteNonQuery()
        tampil()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        txtcari.text = ""
        txtkode.Clear()
        txtnama.Clear()
        txtharga.Clear()
        ComboBox1.Text = ""
    End Sub

    Private Sub txtcari_OnTextChange(sender As System.Object, e As System.EventArgs) Handles txtcari.OnTextChange
     
            Dim table As DataTable
            ap = New OleDbDataAdapter("select * from barang where kodebarang like '%" & txtcari.text & "%' or deskripsi like '%" & txtcari.text & "%'", con)
            dt = New DataSet
            dt.Clear()
            ap.Fill(dt, "barang")
            table = dt.Tables("barang")
            dgv.DataSource = table
            Try
            Catch ex As Exception
            End Try


    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Try
            cmd = New OleDbCommand("update barang set deskripsi='" & txtnama.Text & "', harga='" & txtharga.Text & "', exp='" & DateTimePicker1.Text & "', rak = '" & ComboBox1.Text & "' where kodebarang = '" & txtkode.Text & "'", con)
            cmd.ExecuteNonQuery()
            tampil()
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try



    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        cmd = New OleDbCommand("insert into barang (kodebarang, deskripsi, harga, exp, rak) values ('" & txtkode.Text & "','" & txtnama.Text & "','" & txtharga.Text & "','" & DateTimePicker1.Text & "','" & ComboBox1.Text & "')", con)
        cmd.ExecuteNonQuery()
        tampil()
    End Sub

    Private Sub dgv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellContentClick

    End Sub

    Private Sub dgv_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv.SelectionChanged
        Dim i As Integer = dgv.CurrentRow.Index
        Try
            txtkode.Text = dgv.Item(0, i).Value
            txtnama.Text = dgv.Item(1, i).Value
            txtharga.Text = dgv.Item(2, i).Value
        Catch ex As Exception

        End Try
    End Sub
End Class