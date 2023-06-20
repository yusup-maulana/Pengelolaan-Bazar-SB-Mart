Imports System.Data.OleDb
Public Class daftarpemusnahan
    Sub tampil()
        Dim table As DataTable
        ap = New OleDbDataAdapter("SELECT *   FROM pemusnahan order by kodebarang desc", con)
        dt = New DataSet
        dt.Clear()
        ap.Fill(dt, "barang")
        table = dt.Tables("barang")
        dgv.DataSource = table
        dgv.AutoGenerateColumns = False
    End Sub

    Private Sub daftarpemusnahan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        tampil()
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        cmd = New OleDbCommand("delete * from pemusnahan", con)
        cmd.ExecuteNonQuery()
        tampil()

    End Sub

    Private Sub dgv_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        Dim i As Integer = dgv.CurrentRow.Index
        Try
            txtkode.Text = dgv.Item(0, i).Value
            txtnama.Text = dgv.Item(1, i).Value
            txtharga.Text = dgv.Item(2, i).Value

        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgv_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellContentClick

    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        cmd = New OleDbCommand("delete from pemusnahan where kunci ='" & txtkode.Text & "'", con)
        cmd.ExecuteNonQuery()
        tampil()

    End Sub

    Private Sub txtcari_OnTextChange(sender As System.Object, e As System.EventArgs) Handles txtcari.OnTextChange
        Dim table As DataTable
        ap = New OleDbDataAdapter("select * from pemusnahan where kodebarang like '%" & txtcari.text & "%' or namabarang like '%" & txtcari.text & "%'", con)
        dt = New DataSet
        dt.Clear()
        ap.Fill(dt, "pemusnahan")
        table = dt.Tables("pemusnahan")
        dgv.DataSource = table
        Try
        Catch ex As Exception
        End Try
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