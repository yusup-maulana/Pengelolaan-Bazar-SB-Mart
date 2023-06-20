Imports System.Data.OleDb
Public Class fromlogin




    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        koneksi()
        cmd = New OleDbCommand("SELECT * From login WHERE user1 = '" & TextBox1.Text & "' and pass1 = '" & TextBox2.Text & "'", con)
        rd = cmd.ExecuteReader
        If (rd.Read()) Then
            Form1.Show()
            Me.Hide()
        Else
            MsgBox("user name dan password salah")
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        End
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox1_OnValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.OnValueChanged

    End Sub

    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1.PerformClick()
        End If
    End Sub

    Private Sub TextBox2_OnValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.OnValueChanged

    End Sub

    Private Sub fromlogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox1.Focus()
    End Sub
End Class