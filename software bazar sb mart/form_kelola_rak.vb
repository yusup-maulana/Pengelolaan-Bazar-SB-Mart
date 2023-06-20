Imports System.Data.OleDb
Public Class form_kelola_rak
    Sub tampil()
        Dim table As DataTable
        ap = New OleDbDataAdapter("select rak from data_rak where biner = '" & "0" & "'", con)
        dt = New DataSet
        dt.Clear()
        ap.Fill(dt, "data_rak")
        table = dt.Tables("data_rak")
        dgv.DataSource = table

    End Sub
    Sub comboPangkat()
        Try
            cmd = New OleDbCommand("select nama_pemilik from data_rak group by nama_pemilik", con)
            rd = cmd.ExecuteReader()
            While rd.Read()
                Dim cek As Object = rd("nama_pemilik")
                If IsDBNull(cek) Then

                Else
                    If cek = "" Then
                    Else
                        ComboBox1.Items.Add(rd("nama_pemilik"))
                    End If

                End If

            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub form_kelola_rak_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        koneksi()

        comboPangkat()
        tampil()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            ListBox1.Items.Clear()
            cmd = New OleDbCommand("select * from data_rak where nama_pemilik ='" & ComboBox1.Text & "'", con)
            rd = cmd.ExecuteReader()
            While rd.Read()
                ListBox1.Items.Add(rd("rak"))
                lblid.Text = rd("idrak")
                txtubahnama.Text = rd("nama_pemilik")

            End While

            lbl_pemilik.Text = ComboBox1.SelectedItem
            Button4.Text = "<< Tambahkan Ke pemilik Rak " & txtubahnama.Text
            Button4.Visible = True
        Catch ex As Exception
            txtubahnama.Text = ""
            lbl_pemilik.Text = ""
            lblid.Text = ""
            Button4.Visible = False
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        cmd = New OleDbCommand("select idrak from data_rak group by idrak order by idrak", con)
        rd = cmd.ExecuteReader
        Dim jumlah As integer
        While rd.Read
            jumlah = jumlah + 1
        End While
        Dim i As Integer = dgv.CurrentRow.Index
        cmd = New OleDbCommand("update data_rak set idrak='" & jumlah & "', nama_pemilik='" & TextBox1.Text & "', biner='" & "1" & "' where rak='" & dgv.Item(0, i).Value & "'", con)
        cmd.ExecuteNonQuery()
        tampil()
        TextBox1.Text = ""
        ComboBox1.Items.Clear()
        comboPangkat()
        jumlah = 0
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        cmd = New OleDbCommand("select * from data_rak where idrak='" & lblid.Text & "'", con)
        rd = cmd.ExecuteReader
        While rd.Read
            cmd = New OleDbCommand("update data_rak set nama_pemilik='" & txtubahnama.Text & "' where idrak='" & lblid.Text & "'", con)
            cmd.ExecuteNonQuery()
        End While
        ComboBox1.Items.Clear()
        comboPangkat()
        MsgBox("Nama Pemilik Berhasil diubah", MsgBoxStyle.Information)
        lbl_pemilik.Text = txtubahnama.Text
        rd.Close()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Button5.Visible = True
    End Sub

   
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim i As Integer = dgv.CurrentRow.Index
        cmd = New OleDbCommand("update data_rak set idrak='" & lblid.Text & "', nama_pemilik='" & txtubahnama.Text & "', biner='" & "1" & "' where rak='" & dgv.Item(0, i).Value & "'", con)
        cmd.ExecuteNonQuery()
        tampil()
        Try
            ListBox1.Items.Clear()
            cmd = New OleDbCommand("select * from data_rak where nama_pemilik ='" & ComboBox1.Text & "'", con)
            rd = cmd.ExecuteReader()
            While rd.Read()
                ListBox1.Items.Add(rd("rak"))
                lblid.Text = rd("idrak")
                txtubahnama.Text = rd("nama_pemilik")

            End While

            lbl_pemilik.Text = ComboBox1.SelectedItem
            Button4.Text = "<< Tambahkan Ke pemilik Rak " & txtubahnama.Text
            Button4.Visible = False
        Catch ex As Exception
            txtubahnama.Text = ""
            lbl_pemilik.Text = ""
            lblid.Text = ""
            Button4.Visible = False
        End Try
    End Sub

    Private Sub dgv_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        Button4.Visible = True
    End Sub

    Private Sub dgv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellContentClick

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        cmd = New OleDbCommand("select * from data_rak where idrak='" & lblid.Text & "'", con)
        rd = cmd.ExecuteReader
        While rd.Read
            Dim a As String = rd("rak")
            cmd = New OleDbCommand("update data_rak set nama_pemilik='" & "" & "', idrak='" & "" & "', biner='" & "0" & "' where rak='" & a & "'", con)
            cmd.ExecuteNonQuery()
           
        End While
        ComboBox1.Items.Clear()
        comboPangkat()
        MsgBox("Rak Sudah Dikosongkan", MsgBoxStyle.Information)
        lbl_pemilik.Text = txtubahnama.Text
        rd.Close()
        tampil()
        ListBox1.Items.Clear()
        txtubahnama.Clear()
        lbl_pemilik.Text = ""
        lblid.Text = ""
        Button4.Visible = False
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim a As String = ListBox1.SelectedItem
        cmd = New OleDbCommand("update data_rak set nama_pemilik='" & "" & "', idrak='" & "" & "', biner='" & "0" & "' where rak='" & a & "'", con)
        cmd.ExecuteNonQuery()
        Try
            ListBox1.Items.Clear()
            cmd = New OleDbCommand("select * from data_rak where nama_pemilik ='" & ComboBox1.Text & "'", con)
            rd = cmd.ExecuteReader()
            While rd.Read()
                ListBox1.Items.Add(rd("rak"))
                lblid.Text = rd("idrak")
                txtubahnama.Text = rd("nama_pemilik")

            End While

            lbl_pemilik.Text = ComboBox1.SelectedItem
            Button4.Text = "<< Tambahkan Ke pemilik Rak " & txtubahnama.Text
            Button4.Visible = True
        Catch ex As Exception
            txtubahnama.Text = ""
            lbl_pemilik.Text = ""
            lblid.Text = ""
            Button4.Visible = False
        End Try

        tampil()

        Button5.Visible = False
    End Sub
End Class