Imports System.Data.OleDb

Module Module1
    Public con As OleDbConnection
    Public cmd As OleDbCommand
    Public ap As OleDbDataAdapter
    Public dt As DataSet
    Public rd As OleDbDataReader
    Sub koneksi()
        con = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|db_bazar.mdb")
        con.Open()
    End Sub

    Private Function KeKata(ByVal n As Double)

        Dim t As String

        Dim Angka() As String

        Angka = Split(",Satu,Dua,Tiga,Empat,Lima,Enam,Tujuh,Delapan,Sembilan,Sepuluh,Sebelas", ",")



        If n < 12 Then

            t = " " & Angka(n)

        ElseIf n < 20 Then

            t = KeKata(n - 10) & " belas"

        ElseIf n < 100 Then

            t = KeKata(n \ 10) & " puluh" & KeKata(n Mod 10)

        ElseIf n < 200 Then

            t = " seratus" & KeKata(n - 100)

        ElseIf n < 1000 Then

            t = KeKata(n \ 100) & " ratus" & KeKata(n Mod 100)

        ElseIf n < 2000 Then

            t = " seribu" & KeKata(n - 1000)

        ElseIf n < 1000000 Then

            t = KeKata(n \ 1000) & " ribu" & KeKata(n Mod 1000)

        ElseIf n < 1000000000 Then

            t = KeKata(n \ 1000000) & " juta" & KeKata(n Mod 1000000)

        ElseIf n < 1000000000000.0# Then

            t = KeKata(Fix(n / 1000000000)) & " milyar" & KeKata(n - (Fix(n / 1000000000) * 1000000000))

        ElseIf n < 1.0E+15 Then

            t = KeKata(Fix(n / 1000000000000.0#)) & " trilyun" & KeKata(n - (Fix(n / 1000000000000.0#) * 1000000000000.0#))

        End If



        KeKata = t

    End Function



    Private Function KeKataDesimal(ByVal n As Double)

        Dim t As String, s As String, d As String, i As Integer

        Dim Angka() As String

        d = Mid(5 / 2, 2, 1)

        Angka = Split("nol,satu,dua,tiga,empat,lima,enam,tujuh,delapan,sembilan", ",")



        s = Split(n, d)(1)

        For i = 1 To Len(s)

            t = t & " " & Angka(Mid(s, i, 1))

        Next



        KeKataDesimal = t

    End Function



    Public Function Terbilang(ByVal Nilai As Double, Optional ByVal AwalKapital As Boolean = False)

        Dim s As String, n As Double, d As String

        d = Mid(5 / 2, 2, 1)

        n = System.Math.Abs(Nilai)



        If InStr(Nilai, d) Then

            s = KeKata(n) & " koma " & KeKataDesimal(n)

        Else

            s = KeKata(n)

        End If



        If Nilai < 0 Then s = "Kurang " & s



        s = Trim(Replace(s, "  ", " "))



        Terbilang = IIf(AwalKapital = True, StrConv(s, 3), s)

    End Function
End Module
