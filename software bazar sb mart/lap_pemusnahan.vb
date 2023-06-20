Imports CrystalDecisions.CrystalReports.Engine
Public Class lap_pemusnahan

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try

            Dim str As String = "pemusnahan.rpt"
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