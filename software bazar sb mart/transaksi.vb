Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.OleDb
Public Class transaksi
    Sub total_beli()
        Try
            cmd = New OleDbCommand("SELECT sum(total) as grand  FROM transaksi_proses", con)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows Then
                txtgrand.Text = rd.Item("grand")
            End If
            txtgrand2.Text = txtgrand.Text
            If txtgrand.Text.Length <= 2 Then ' jika panjang karakter pada textbox1 <= 2

            Else ' jika tidak
                txtgrand.Text = FormatCurrency(txtgrand.Text) ' mengubah format pada textbox1 menjadi currency
                txtgrand.SelectionStart = txtgrand.Text.Length '  menetapkan titik awal dari teks yang dipilih pada textbox
            End If
            txtgrand2.Text = Replace(txtgrand2.Text, ".", "")
            txtgrand2.Text = Replace(txtgrand2.Text, "Rp", "")
        Catch ex As Exception
            MsgBox("Keranjang masih kosong", MsgBoxStyle.Exclamation)

        End Try
      
    End Sub


    Private Sub transaksi_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("id-ID")
        System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("id-ID")
        total_beli()
        txtbayar.Focus()
    End Sub

    Private Sub BunifuThinButton21_Click_1(sender As System.Object, e As System.EventArgs) Handles BunifuThinButton21.Click
        If txtbayar.Text = "" Then
            MsgBox("Pembayaran masih belum di lakukan", MsgBoxStyle.Exclamation)
        ElseIf Val(txtbayar2.Text) < Val(txtgrand2.Text) Then
            MsgBox("Pembayaran masih kurang", MsgBoxStyle.Exclamation)
        Else
            Try
                Dim str As String = "struk.rpt"
                Dim rpt As New ReportDocument()
                rpt.Load(str)
                rpt.SetParameterValue("grand", txtgrand.Text)
                rpt.SetParameterValue("bayar", txtbayar.Text)
                rpt.SetParameterValue("kembalian", txtkembalian.Text)
                struk.Refresh()
                struk.ReportSource = rpt
                struk.Show()
                TabControl1.SelectedTab = TabPage2

            Catch ex As Exception

            End Try

        End If
       
    End Sub

    Private Sub txtbayar_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtbayar.TextChanged
        txtbayar2.Text = txtbayar.Text
        If txtbayar.Text.Length <= 2 Then ' jika panjang karakter pada textbox1 <= 2

        Else ' jika tidak
            txtbayar.Text = FormatCurrency(txtbayar.Text) ' mengubah format pada textbox1 menjadi currency
            txtbayar.SelectionStart = txtbayar.Text.Length '  menetapkan titik awal dari teks yang dipilih pada textbox
        End If
        txtbayar2.Text = Replace(txtbayar2.Text, ".", "")
        txtbayar2.Text = Replace(txtbayar2.Text, "Rp", "")
        Dim bayar, tambah, total As Integer
        bayar = Val(txtbayar2.Text)
        total = Val(txtgrand2.Text)
        tambah = bayar - total
        txtkembalian.Text = tambah

        txtkembalian.Text = txtkembalian.Text
        If txtkembalian.Text.Length <= 2 Then ' jika panjang karakter pada textbox1 <= 2

        Else ' jika tidak
            txtkembalian.Text = FormatCurrency(txtkembalian.Text) ' mengubah format pada textbox1 menjadi currency
            txtkembalian.SelectionStart = txtkembalian.Text.Length '  menetapkan titik awal dari teks yang dipilih pada textbox
        End If

        lblterbilang.Text = Terbilang(txtkembalian.Text)
        Form1.txtbayar.Text = txtbayar.Text
        Form1.txtkembalian.Text = txtkembalian.Text
        Form1.lblterbilang.Text = lblterbilang.Text
    End Sub

    Private Sub txtkembalian_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtkembalian.TextChanged

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Me.Close()
        Dim nowtgl As Date = today
        Dim i As Integer = Form1.DataGridView2.RowCount
        Dim u As Integer = Form1.DataGridView2.CurrentRow.Index
        For a As Integer = 0 To i - 2
            cmd = New OleDbCommand("insert into bazar_terjual (kodebarang, namabarang, rak, tgl_update, tgl_exp,masa,harga, diskon, total, tgl_beli) values ('" & Form1.DataGridView2.Item(0, a).Value & "','" & Form1.DataGridView2.Item(1, a).Value & "','" & Form1.DataGridView2.Item(2, a).Value & "','" & Form1.DataGridView2.Item(3, a).Value & "','" & Form1.DataGridView2.Item(4, a).Value & "','" & Form1.DataGridView2.Item(5, a).Value & "','" & Form1.DataGridView2.Item(6, a).Value & "','" & Form1.DataGridView2.Item(7, a).Value & "','" & Form1.DataGridView2.Item(8, a).Value & "','" & nowtgl & "')", con)
            cmd.ExecuteNonQuery()
        Next

       

        'penghapusan  proses transaksi di tabel
        cmd = New OleDbCommand("delete * from transaksi_proses", con)
        cmd.ExecuteNonQuery()

        Dim table As DataTable
        ap = New OleDbDataAdapter("SELECT kodebarang,namabarang,rak,tgl_update,tgl_exp,masa,harga,diskon,total   FROM transaksi_proses", con)
        dt = New DataSet
        dt.Clear()
        ap.Fill(dt, "proses")
        table = dt.Tables("proses")
        Form1.DataGridView2.DataSource = table
        Form1.DataGridView2.AutoGenerateColumns = False
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        'pemngambilan stok list code
        koneksi()
        ap = New OleDbDataAdapter("select kodebarang from transaksi_proses group by kodebarang", con)
        dt = New DataSet
        dt.Clear()
        ap.Fill(dt, "transaksi_proses")
        DataGridView1.DataSource = dt.Tables("transaksi_proses")
        Dim cek As Integer = DataGridView1.RowCount
        For a As Integer = 0 To cek - 2
            Dim SqlQuery As String = " SELECT kodebarang FROM transaksi_proses where kodebarang ='" & DataGridView1.Item(0, a).Value & "'"
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
            Dim kode As Integer = 0
            For i = 0 To TABLE.Rows.Count
                kode = kode + 1
            Next
            cmd = New OleDbCommand("update proses set stok = '" & kode - 1 & "' where kodebarang = '" & DataGridView1.Item(0, a).Value & "'", con)
            cmd.ExecuteNonQuery()

        Next
       
    End Sub
End Class