Imports System.Data.OleDb
Public Class admin
    Sub tampil()
        Dim table As DataTable
        ap = New OleDbDataAdapter("SELECT * FROM login order by user1", con)
        dt = New DataSet
        dt.Clear()
        ap.Fill(dt, "login")
        table = dt.Tables("login")
        DataGridView1.DataSource = table
        DataGridView1.AutoGenerateColumns = False
    End Sub
    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox2.Visible = True
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        tampil()
        TextBox2.Visible = True
    End Sub

    Private Sub admin_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            tampil()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim a As String = "INSERT INTO login (user1, pass1, nama1) VALUES ('" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox1.Text & "')"
        cmd = New OleDbCommand(a, con)
        cmd.ExecuteNonQuery()
        MsgBox("Data tersimpan")
        tampil()
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        cmd = New OleDbCommand("delete * from login where user1='" & TextBox2.Text & "'", con)
        cmd.ExecuteNonQuery()
        MsgBox("Data terhapus")
        tampil()
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        TextBox2.Visible = True
        cmd = New OleDbCommand("update login set nama1='" & TextBox1.Text & "', pass1='" & TextBox3.Text & "' where user1 = '" & TextBox2.Text & "'", con)
        cmd.ExecuteNonQuery()
        MsgBox("Data terubah")
        tampil()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        TextBox2.Visible = False
        Dim i As Integer = DataGridView1.CurrentRow.Index
        Try
            TextBox2.Text = DataGridView1.Item(0, i).Value
            TextBox1.Text = DataGridView1.Item(1, i).Value
            TextBox3.Text = DataGridView1.Item(2, i).Value
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class