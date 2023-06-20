Imports System.Data.OleDb
Public Class daftarterjualbazar
    Sub tampil()
        Dim table As DataTable
        ap = New OleDbDataAdapter("SELECT *   FROM bazar_terjual order by kodebarang desc", con)
        dt = New DataSet
        dt.Clear()
        ap.Fill(dt, "bazar_terjual")
        table = dt.Tables("bazar_terjual")
        dgv.DataSource = table
        dgv.AutoGenerateColumns = False
    End Sub
    Sub comboPangkat_raknomber()
        Try
            ComboBox2.Items.Clear()
            cmd = New OleDbCommand("select rak from data_rak where nama_pemilik='" & ComboBox1.Text & "'", con)
            rd = cmd.ExecuteReader()
            While rd.Read()
                ComboBox2.Items.Add(rd("rak"))
            End While
        Catch ex As Exception
            MessageBox.Show("Koneksi Gagal !!!, karena " & ex.Message)
        End Try
    End Sub
    Sub comboPangkat()
        Try
            ComboBox1.Items.Clear()
            cmd = New OleDbCommand("select nama_pemilik from data_rak group by nama_pemilik", con)
            rd = cmd.ExecuteReader()
            While rd.Read()
                ComboBox1.Items.Add(rd("nama_pemilik"))
            End While
        Catch ex As Exception
            MessageBox.Show("Koneksi Gagal !!!, karena " & ex.Message)
        End Try
    End Sub
    Private Sub daftarterjualbazar_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        koneksi()
        tampil()
        comboPangkat()

    End Sub

    Private Sub txtcari_OnTextChange(sender As System.Object, e As System.EventArgs) Handles txtcari.OnTextChange
        Dim table As DataTable
        ap = New OleDbDataAdapter("select * from bazar_terjual where kodebarang like '%" & txtcari.text & "%' or namabarang like '%" & txtcari.text & "%'", con)
        dt = New DataSet
        dt.Clear()
        ap.Fill(dt, "bazar_terjual")
        table = dt.Tables("bazar_terjual")
        dgv.DataSource = table
        Try
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        cmd = New OleDbCommand("delete from bazar_terjual where kodebarang = '" & txtkode.Text & "'", con)
        cmd.ExecuteNonQuery()
        tampil()

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        txtharga.Clear()
        txttotal.Clear()
        ComboBox1.Text = ""
        ComboBox2.Text = ""
        txtkode.Clear()
        txtkode.Clear()
        txtnama.Clear()
        txtcari.text = ""
        txtdiskon.Clear()
        ComboBox2.Visible = False
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        If ComboBox1.Text = "" Then
            MsgBox("rak belum di tentukan", MsgBoxStyle.Exclamation)
        ElseIf ComboBox2.Text = "" Then
            MsgBox("Number rak belum di tentukan", MsgBoxStyle.Exclamation)
        Else
            Dim diskon As String = Replace(txtdiskon.Text, "%", "")
            cmd = New OleDbCommand("update bazar_terjual set  namabarang='" & txtnama.Text & "', rak = '" & ComboBox1.Text & "', harga = '" & txtharga.Text & "', tgl_update='" & dateupdate.Text & "', tgl_exp = '" & dateexpired.Text & "', masa='" & txtmasa.Text & "', diskon ='" & diskon & "%" & "', total ='" & txttotal.Text & "' where kodebarang = '" & txtkode.Text & "'", con)
            cmd.ExecuteNonQuery()
            tampil()
        End If
      
        ComboBox2.Visible = False

    End Sub

    Private Sub dgv_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        Dim i As Integer = dgv.CurrentRow.Index
        Try
            txtkode.Text = dgv.Item(0, i).Value
            txtnama.Text = dgv.Item(1, i).Value

            dateupdate.Text = dgv.Item(3, i).Value
            dateexpired.Text = dgv.Item(4, i).Value
            txtmasa.Text = dgv.Item(5, i).Value
            txtharga.Text = dgv.Item(6, i).Value
            txtdiskon.Text = dgv.Item(7, i).Value
            txttotal.Text = dgv.Item(8, i).Value
        Catch ex As Exception

        End Try
    End Sub

   
    Private Sub dateexpired_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dateexpired.ValueChanged
        Dim nowdate As Date = Today
        txtmasa.Text = DateDiff(DateInterval.Day, dateupdate.Value, dateexpired.Value.Date)
    End Sub

    Private Sub txtharga_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtharga.TextChanged
        Dim b, c As String

        Dim diskon As String = Replace(txtdiskon.Text, "%", "")
        b = "0." & diskon
        c = Val(txtharga.Text) * diskon / 100
        txttotal.Text = c

    End Sub

    Private Sub txtdiskon_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtdiskon.TextChanged
        Dim b, c As String

        Dim diskon As String = Replace(txtdiskon.Text, "%", "")
        b = "0." & diskon
        c = Val(txtharga.Text) * diskon / 100
        txttotal.Text = c
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        comboPangkat_raknomber()
        ComboBox2.Visible = True
    End Sub

    Private Sub dgv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellContentClick

    End Sub

    Private Sub dgv_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv.SelectionChanged
        Dim i As Integer = dgv.CurrentRow.Index
        Try
            txtkode.Text = dgv.Item(0, i).Value
            txtnama.Text = dgv.Item(1, i).Value

            dateupdate.Text = dgv.Item(3, i).Value
            dateexpired.Text = dgv.Item(4, i).Value
            txtmasa.Text = dgv.Item(5, i).Value
            txtharga.Text = dgv.Item(6, i).Value
            txtdiskon.Text = dgv.Item(7, i).Value
            txttotal.Text = dgv.Item(8, i).Value
        Catch ex As Exception

        End Try
    End Sub
End Class