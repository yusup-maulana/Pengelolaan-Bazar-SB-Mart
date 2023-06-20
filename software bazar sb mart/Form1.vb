Imports System.Data.OleDb
Public Class Form1
    Sub isikode_transaksi()
        Dim strtemp As String = ""
        Dim strvalue As String = ""
        Dim sql As String
        Dim cmd As OleDb.OleDbCommand
        Dim baca As OleDb.OleDbDataReader
        sql = "select * from [transaksi_proses] order by [id] desc"
        cmd = New OleDbCommand(sql, con)
        baca = cmd.ExecuteReader
        If baca.Read Then
            strtemp = Mid(baca.Item("id"), 4, 4)
        Else
            txtidtransaksi_sementara.Text = "TRS0001"

            Exit Sub
        End If
        strvalue = Val(strtemp) + 1
        txtidtransaksi_sementara.Text = "TRS" & Mid("0000", 1, 4 - strvalue.Length) & strvalue

    End Sub
    Sub tampiltransaksi_proses()
        Try
            'ambil stok -1
            cmd = New OleDbCommand("select stok from proses where kunci = '" & lblkunci.Text & "'", con)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows Then
                Dim i As Integer = rd.Item("stok")
                If i <= 0 Then
                    MsgBox("Stok ini sudah habis")


                Else
                    Dim kurangsatu As Integer = i - 1
                    cmd = New OleDbCommand("update proses set stok ='" & kurangsatu & "' where kunci='" & lblkunci.Text & "'", con)
                    cmd.ExecuteNonQuery()

                    Try
                        Dim l As Integer = DataGridView1.CurrentRow.Index
                        cmd = New OleDbCommand("insert into transaksi_proses (kodebarang, namabarang, harga, rak, tgl_update, tgl_exp,masa, diskon, total, kunci,id ) values ('" & btn_kodebarang.Text & "','" & txt_barang.Text & "','" & txt_harga.Text & "','" & txt_rak.Text & "','" & TextBox3.Text & "','" & txt_exp.Text & "','" & txt_selisih_hari.Text & "','" & txt_diskon_real.Text & "','" & txt_hargaexp.Text & "','" & lblkunci.Text & "','" & txtidtransaksi_sementara.Text & "')", con)
                        cmd.ExecuteNonQuery()
                        Dim table As DataTable
                        ap = New OleDbDataAdapter("SELECT kodebarang,namabarang,rak,tgl_update,tgl_exp,masa,harga,diskon,total,kunci,id   FROM transaksi_proses", con)
                        dt = New DataSet
                        dt.Clear()
                        ap.Fill(dt, "proses")
                        table = dt.Tables("proses")
                        DataGridView2.DataSource = table
                        DataGridView2.AutoGenerateColumns = False
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                    total_beli()
                End If

            End If
            tampil()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Sub total_beli()
        Try
            cmd = New OleDbCommand("SELECT sum(total) as grand  FROM transaksi_proses", con)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows Then
                txt_beli.Text = rd.Item("grand")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try
       
    End Sub
    Sub bersih()
        txt_barang.Clear()
        txt_diskon_real.Clear()
        txt_harga.Clear()
        txt_hargaexp.Clear()
        txt_rak.Clear()
        txt_selisih_hari.Clear()
        txt_exp.Clear()
        txt_harga.Clear()
        btn_kodebarang.Text = ""

    End Sub
    Sub tampil()
        Dim table As DataTable
        ap = New OleDbDataAdapter("SELECT kodebarang,namabarang,harga,rak,tgl_update,tgl_exp,masa,stok,kunci   FROM proses  where tgl_exp between Date() and DateAdd('d', 95, DATE())", con)
        dt = New DataSet
        dt.Clear()
        ap.Fill(dt, "proses")
        table = dt.Tables("proses")
        DataGridView1.DataSource = table
        DataGridView1.AutoGenerateColumns = False
        Label10.Text = DataGridView1.RowCount - 1 & " Barang expired tersedia"
    End Sub
    Sub tampil2()

        Dim SqlQuery As String = " SELECT kodebarang,namabarang,harga,rak,tgl_update,tgl_exp,masa,stok,kunci FROM proses "

        Dim SqlCommand As New OleDbCommand

        Dim sqlAdapter As New OleDbDataAdapter

        Dim TABLE As New DataTable

        With SqlCommand

            .CommandText = SqlQuery

            .Connection = con

        End With

        With sqlAdapter

            .SelectCommand = SqlCommand

            .Fill(TABLE)

        End With

        DataGridView1.Rows.Clear()

        For i = 0 To TABLE.Rows.Count - 1

            With DataGridView1

                .Rows.Add(TABLE.Rows(i)("kodebarang"), TABLE.Rows(i)("namabarang"), TABLE.Rows(i)("harga"), TABLE.Rows(i)("rak"), TABLE.Rows(i)("tgl_update"), TABLE.Rows(i)("tgl_exp"), TABLE.Rows(i)("masa"), TABLE.Rows(i)("stok"))

            End With

        Next

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
    Sub comboPangkat2()
        Try
            ComboBox5.Items.Clear()
            cmd = New OleDbCommand("select nama_pemilik from data_rak group by nama_pemilik", con)
            rd = cmd.ExecuteReader()
            While rd.Read()
                ComboBox5.Items.Add(rd("nama_pemilik"))
            End While
        Catch ex As Exception

        End Try
    End Sub
    Sub buat_kolom()
        With DataGridView1
            .Columns.Add("Diskon", "Diskon")
            .Columns.Add("Harga exp", "Harga exp")

            .Columns("Diskon").Width = 100
            .Columns("Harga exp").Width = 100
        End With


    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("id-ID")
        System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("id-ID")
        koneksi()
        
        cmd = New OleDbCommand("select kunci,kodebarang,namabarang,harga,rak,tgl_update,tgl_exp,masa,stok,kunci from proses", con)
        rd = cmd.ExecuteReader()
        Dim datenow As Date = Today
        Do While rd.Read()
            Dim masa As String = DateDiff(DateInterval.Day, datenow, rd.Item("tgl_exp").Date)
            cmd = New OleDbCommand("update proses set masa ='" & masa & "' where kunci='" & rd.Item("kunci") & "'", con)
            cmd.ExecuteNonQuery()
        Loop
        rd.Close()
        cmd = New OleDbCommand("select * from proses", con)
        rd = cmd.ExecuteReader()
        Do While rd.Read()
            Dim masa As Integer = DateDiff(DateInterval.Day, datenow, rd.Item("tgl_exp").Date)
            If masa <= 0 Then
                cmd = New OleDbCommand("insert into pemusnahan (kunci, kodebarang, namabarang, harga, rak, tgl_update, tgl_exp, stok) values ('" & rd.Item("kunci") & "','" & rd.Item("kodebarang") & "','" & rd.Item("namabarang") & "','" & rd.Item("harga") & "','" & rd.Item("rak") & "','" & rd.Item("tgl_update") & "','" & rd.Item("tgl_exp") & "','" & rd.Item("stok") & "')", con)
                cmd.ExecuteNonQuery()
            End If
        Loop
        cmd = New OleDbCommand("delete from proses where masa <= '" & "0" & "'", con)
        cmd.ExecuteNonQuery()
        tampil()
        comboPangkat()


        ' mencari stok yang sudah kosong di tabel proses dan menghapus nya
        cmd = New OleDbCommand("delete from proses where stok = '" & "0" & "'", con)
        cmd.ExecuteNonQuery()
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            tampiltransaksi_proses()
            total_beli()

            If txt_beli.Text.Length <= 2 Then ' jika panjang karakter pada textbox1 <= 2

            Else ' jika tidak
                txt_beli.Text = FormatCurrency(txt_beli.Text) ' mengubah format pada textbox1 menjadi currency
                txt_beli.SelectionStart = txt_beli.Text.Length '  menetapkan titik awal dari teks yang dipilih pada textbox
            End If
            txtbayar.Clear()
            txtkembalian.Clear()
            lblterbilang.Text = ""
            TextBox1.Clear()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If ComboBox1.Text = "KODE BARANG" Then
            Try
                cmd = New OleDbCommand("SELECT kodebarang,namabarang,harga,rak,tgl_update,tgl_exp,masa,stok,kunci  FROM proses  where kodebarang like '%" & TextBox1.Text & "%' and tgl_exp between Date() and DateAdd('d', 95, DATE())", con)
                rd = cmd.ExecuteReader
                rd.Read()
                If rd.HasRows Then
                    ap = New OleDbDataAdapter("SELECT kodebarang,namabarang,harga,rak,tgl_update,tgl_exp,masa,stok,kunci   FROM proses  where kodebarang like '%" & TextBox1.Text & "%'  and tgl_exp between Date() and DateAdd('d', 95, DATE())", con)
                    dt = New DataSet
                    dt.Clear()
                    ap.Fill(dt, "proses")
                    DataGridView1.DataSource = dt.Tables("proses")
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        ElseIf ComboBox1.Text = "DESKRIPSI" Then
            Try
                cmd = New OleDbCommand("SELECT kodebarang,namabarang,harga,rak,tgl_update,tgl_exp,masa,stok,kunci  FROM proses  where namabarang like '%" & TextBox1.Text & "%' and tgl_exp between Date() and DateAdd('d', 95, DATE())", con)
                rd = cmd.ExecuteReader
                rd.Read()
                If rd.HasRows Then
                    ap = New OleDbDataAdapter("SELECT kodebarang,namabarang,harga,rak,tgl_update,tgl_exp,masa,stok,kunci  FROM proses   where namabarang like '%" & TextBox1.Text & "%' and tgl_exp between Date() and DateAdd('d', 95, DATE())", con)
                    dt = New DataSet
                    dt.Clear()
                    ap.Fill(dt, "proses")
                    DataGridView1.DataSource = dt.Tables("proses")
                End If
            Catch ex As Exception
            End Try
        ElseIf ComboBox1.Text = "" Then
            Try
                cmd = New OleDbCommand("SELECT kodebarang,namabarang,harga,rak,tgl_update,tgl_exp,masa,stok,kunci FROM proses where  like '%" & TextBox1.Text & "%' and tgl_exp between Date() and DateAdd('d', 95, DATE())", con)
                rd = cmd.ExecuteReader
                rd.Read()
                If rd.HasRows Then
                    ap = New OleDbDataAdapter("SELECT kodebarang,namabarang,harga,rak,tgl_update,tgl_exp,masa,stok,kunci   FROM proses  where  like '%" & TextBox1.Text & "%' and tgl_exp between Date() and DateAdd('d', 95, DATE())", con)
                    dt = New DataSet
                    dt.Clear()
                    ap.Fill(dt, "proses")
                    DataGridView1.DataSource = dt.Tables("proses")
                End If
            Catch ex As Exception
            End Try
        ElseIf ComboBox1.Text = "RAK" Then
            If ComboBox5.Text = "" Then
                MsgBox("Pilih RAK terlebih dahulu", MsgBoxStyle.Information)
            Else
                Try
                    dt = New DataSet
                    dt.Clear()
                    cmd = New OleDbCommand("select * from data_rak where nama_pemilik = '" & ComboBox5.Text & "'", con)
                    rd = cmd.ExecuteReader
                    rd.Read()
                    If rd.HasRows Then

                        ap = New OleDbDataAdapter("SELECT kodebarang,namabarang,harga,rak,tgl_update,tgl_exp,masa,stok,kunci  from proses where rak = '" & ComboBox5.Text & "' and  namabarang like '%" & TextBox1.Text & "%' and tgl_exp between Date() and DateAdd('d', 95, DATE())", con)
                        ap.Fill(dt, "proses")
                        DataGridView1.DataSource = dt.Tables("proses")

                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        End If


        Label10.Text = DataGridView1.RowCount - 1 & " Barang expired tersedia"
    End Sub

    Dim harga_sudah_diskon As Integer
    Dim hari As Integer
    Dim hari_exp As Integer

    Private Sub DataGridView1_SelectionChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged

        Try

            Dim i As Integer = DataGridView1.CurrentRow.Index
            bersih()

            btn_kodebarang.Text = DataGridView1.Item(0, i).Value
            txt_barang.Text = DataGridView1.Item(1, i).Value
            txt_harga.Text = DataGridView1.Item(2, i).Value
            txt_exp.Text = DataGridView1.Item(5, i).Value
            TextBox3.Text = DataGridView1.Item(4, i).Value
            txt_rak.Text = DataGridView1.Item(3, i).Value
            TextBox2.Text = DataGridView1.Item(7, i).Value
            lblkunci.Text = DataGridView1.Item(8, i).Value
            Dim masa As Integer = DataGridView1.Item(6, i).Value


            Dim harga_sudah_diskon As Integer

            If masa <= 35 Then
                harga_sudah_diskon = Val(txt_harga.Text) * 50 / 100
                txt_diskon_real.Text = "50%"
            ElseIf masa <= 65 Then
                harga_sudah_diskon = Val(txt_harga.Text) * 30 / 100
                txt_diskon_real.Text = "30%"
            ElseIf masa <= 95 Then
                harga_sudah_diskon = Val(txt_harga.Text) * 20 / 100
                txt_diskon_real.Text = "20%"
            End If
            txt_hargaexp.Text = Val(txt_harga.Text) - Val(harga_sudah_diskon)
            txt_selisih_hari.Text = masa

        Catch ex As Exception

        End Try


    End Sub

    Private Sub PemilikRakToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        form_kelola_rak.Show()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        Try
            dt = New DataSet

            dt.Clear()

            cmd = New OleDbCommand("select nama_pemilik from data_rak where nama_pemilik = '" & ComboBox2.Text & "' ", con)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows Then

                ap = New OleDbDataAdapter("SELECT kodebarang,namabarang,harga,rak,tgl_update,tgl_exp,masa,stok,kunci from proses where rak = '" & ComboBox2.Text & "'  and tgl_exp between Date() and DateAdd('d', 95, DATE())", con)

                ap.Fill(dt, "proses")
                DataGridView1.DataSource = dt.Tables("proses")

            End If
        Catch ex As Exception

        End Try
        Label10.Text = DataGridView1.RowCount - 1 & " Barang expired tersedia"
    End Sub


    Private Sub LogoutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogoutToolStripMenuItem.Click
        Dim cek As String
        cek = MsgBox("apakah anda ingin keluar", MsgBoxStyle.YesNo)
        If cek = vbYes Then
            Me.Hide()
            fromlogin.Show()
        Else


        End If
    End Sub



    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        tampil()
    End Sub



    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.Text = "EXP Mendekati 3 bulan" Then

            Dim table As DataTable
            ap = New OleDbDataAdapter("SELECT kodebarang,namabarang,harga,rak,tgl_update,tgl_exp,masa,stok,kunci FROM proses  where tgl_exp between  Dateadd('d', 65, DATE()) and DateAdd('d',  95, DATE())", con)
            dt = New DataSet
            dt.Clear()
            ap.Fill(dt, "proses")
            table = dt.Tables("proses")
            DataGridView1.DataSource = table
        ElseIf ComboBox3.Text = "EXP Mendekati 2 bulan" Then
            Dim table As DataTable
            ap = New OleDbDataAdapter("SELECT kodebarang,namabarang,harga,rak,tgl_update,tgl_exp,masa,stok,kunci  FROM proses where tgl_exp between Dateadd('d', 35, DATE()) and DateAdd('d', 65, DATE())", con)
            dt = New DataSet
            dt.Clear()
            ap.Fill(dt, "proses")
            table = dt.Tables("proses")
            DataGridView1.DataSource = table
        ElseIf ComboBox3.Text = "EXP Mendekati 1 bulan" Then
            Dim table As DataTable
            ap = New OleDbDataAdapter("SELECT kodebarang,namabarang,harga,rak,tgl_update,tgl_exp,masa,stok,kunci   FROM proses where tgl_exp between Date() and DateAdd('d', 35, DATE())", con)
            dt = New DataSet
            dt.Clear()
            ap.Fill(dt, "proses")
            table = dt.Tables("proses")
            DataGridView1.DataSource = table
        End If
        Label10.Text = DataGridView1.RowCount - 1 & " Barang expired tersedia"
    End Sub

    Private Sub ToolStripDropDownButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub

    Private Sub SettingsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SettingsToolStripMenuItem.Click
        admin.Show()
    End Sub



    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        comboPangkat2()
        If ComboBox1.Text = "RAK" Then
            ComboBox5.Visible = True
        Else
            ComboBox5.Visible = False
        End If
    End Sub

    Private Sub HargaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HargaToolStripMenuItem.Click
        kelolabarang.Show()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        isikode_transaksi()
        tampiltransaksi_proses()


        If txt_beli.Text.Length <= 2 Then ' jika panjang karakter pada textbox1 <= 2

        Else ' jika tidak
            txt_beli.Text = FormatCurrency(txt_beli.Text) ' mengubah format pada textbox1 menjadi currency
            txt_beli.SelectionStart = txt_beli.Text.Length '  menetapkan titik awal dari teks yang dipilih pada textbox
        End If
        txtbayar.Clear()
        txtkembalian.Clear()
        lblterbilang.Text = ""

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim i As Integer = DataGridView2.RowCount
        If i <= 1 Then
            MsgBox("Keranjang masih kosong", MsgBoxStyle.Exclamation)

        Else
            transaksi.Show()
        End If

    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        cmd = New OleDbCommand("delete * from transaksi_proses", con)
        cmd.ExecuteNonQuery()

        Dim table As DataTable
        ap = New OleDbDataAdapter("SELECT kodebarang,namabarang,rak,tgl_update,tgl_exp,masa,harga,diskon,total,kunci,id   FROM transaksi_proses", con)
        dt = New DataSet
        dt.Clear()
        ap.Fill(dt, "proses")
        table = dt.Tables("proses")
        DataGridView2.DataSource = table
        DataGridView2.AutoGenerateColumns = False
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        Try
            Dim u As Integer = DataGridView2.CurrentRow.Index
            cmd = New OleDbCommand("select stok from proses where kunci = '" & DataGridView2.Item(9, u).Value & "'", con)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows Then
                Dim i As Integer = rd.Item("stok")
                Dim tambahsatu As Integer = i + 1
                cmd = New OleDbCommand("update proses set stok ='" & tambahsatu & "' where kunci='" & DataGridView2.Item(9, u).Value & "'", con)
                cmd.ExecuteNonQuery()
                tampil()
            End If

            cmd = New OleDbCommand("delete * from transaksi_proses where kunci='" & DataGridView2.Item(9, u).Value & "' and id ='" & DataGridView2.Item(10, u).Value & "'", con)
            cmd.ExecuteNonQuery()

            Dim table As DataTable
            ap = New OleDbDataAdapter("SELECT kodebarang,namabarang,rak,tgl_update,tgl_exp,masa,harga,diskon,total,kunci,id   FROM transaksi_proses", con)
            dt = New DataSet
            dt.Clear()
            ap.Fill(dt, "proses")
            table = dt.Tables("proses")
            DataGridView2.DataSource = table
            DataGridView2.AutoGenerateColumns = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub



    Private Sub DataGridView2_SelectionChanged(sender As Object, e As System.EventArgs) Handles DataGridView2.SelectionChanged
        Dim i As Integer = DataGridView2.CurrentRow.Index
        Try
            lbl_id_dgv2.Text = DataGridView2.Item(0, i).Value
        Catch ex As Exception

        End Try
    End Sub



    Private Sub DataBarangToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DataBarangToolStripMenuItem.Click
        daftarbarang.Show()
    End Sub

    Private Sub DataPemusnahanToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DataPemusnahanToolStripMenuItem.Click
        daftarpemusnahan.Show()
    End Sub

    Private Sub ExToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ExToolStripMenuItem.Click
        daftarterjualbazar.Show()
    End Sub

    Private Sub DiskonToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        diskonform.Show()
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem2.Click
        inputbarang.Show()
    End Sub

    Private Sub HasilPenjualanBazarToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles HasilPenjualanBazarToolStripMenuItem.Click
        strukform.Show()
    End Sub

    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem5.Click
        lap_pemusnahan.Show()
    End Sub

    Private Sub DataRakToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataRakToolStripMenuItem.Click
        form_kelola_rak.Show()
    End Sub
End Class
