Imports CrystalDecisions.CrystalReports.Engine
Public Class strukform

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Try

            Dim str As String = "penjualan_bazar.rpt"
            Dim rpt As New ReportDocument()
            rpt.Load(str)
            rpt.SetParameterValue("TanggalMulai", dtppindah1.Text)
            rpt.SetParameterValue("TanggalSelesai", dtppindah2.Text)
            penjual_bazar.Refresh()
            penjual_bazar.ReportSource = rpt
            penjual_bazar.Show()



        Catch ex As Exception
        End Try
    End Sub
End Class